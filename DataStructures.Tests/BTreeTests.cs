using System.Collections.Generic;
using System.Linq;
using DataStructures.BTree;
using NUnit.Framework;

namespace DataStructures.Tests
{
    internal class BTreeTests
    {
        private const int Degree = 2;

        private readonly int[] testKeyData = { 10, 20, 30, 50};

        [Test]
        public void Constructor_CreateTree_FormsCorrectBTree()
        {
            var btree = new BTree<int>(Degree);

            var root = btree.Root;
            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Entries);
            Assert.IsNotNull(root.Children);
            Assert.AreEqual(0, root.Entries.Count);
            Assert.AreEqual(0, root.Children.Count);
        }


        [Test]
        public void Insert_AddOneNode_FormsCorrectBTree()
        {
            var btree = new BTree<int>(Degree);
            InsertTestDataAndValidateTree(btree, 0);
            Assert.AreEqual(1, btree.Height);
        }

        [Test]
        public void Insert_MultipleNodesToSplit_FormsCorrectBTree()
        {
            var btree = new BTree<int>(Degree);

            for (int i = 0; i < testKeyData.Length; i++)
            {
                InsertTestDataAndValidateTree(btree, i);
            }

            Assert.AreEqual(2, btree.Height);
        }

        [Test]
        public void Search_EmptyTree_ReturnZero()
        {
            var btree = new BTree<int>(Degree);

            var nonExisting = btree.Search(9999);

            Assert.IsFalse(nonExisting);
        }

        [Test]
        public void Search_NonExistingNode_ReturnZero()
        {
            var btree = new BTree<int>(Degree);

            for (int i = 0; i < testKeyData.Length; i++)
            {
                InsertTestData(btree, i);
            }

            var nonExisting = btree.Search(9999);

            Assert.IsFalse(nonExisting);
        }

        [Test]
        public void Search_ExistingNodes_CorrectReturn()
        {
            var btree = new BTree<int>(Degree);

            for (int i = 0; i < testKeyData.Length; i++)
            {
                InsertTestData(btree, i);
                SearchTestData(btree, i);
            }
        }

        [Test]
        public void Delete_AllNodes_CorrectReturn()
        {
            var btree = new BTree<int>(Degree);

            for (int i = 0; i < testKeyData.Length; i++)
            {
                InsertTestData(btree, i);
            }

            for (int i = 0; i < testKeyData.Length; i++)
            {
                btree.Delete(testKeyData[i]);
                ValidateTree(btree.Root, Degree, testKeyData.Skip(i + 1).ToArray());
            }

            Assert.AreEqual(1, btree.Height);
        }

        [Test]
        public void Delete_NonExistingNode_CorrectReturn()
        {
            var btree = new BTree<int>(Degree);

            for (int i = 0; i < testKeyData.Length; i++)
            {
                InsertTestData(btree, i);
            }

            btree.Delete(99999);
            ValidateTree(btree.Root, Degree, testKeyData.ToArray());
        }

        #region Private methods
        private void InsertTestData(BTree<int> btree, int testDataIndex)
        {
            btree.Insert(testKeyData[testDataIndex]);
        }

        private void InsertTestDataAndValidateTree(BTree<int> btree, int testDataIndex)
        {
            btree.Insert(testKeyData[testDataIndex]);
            ValidateTree(btree.Root, Degree, testKeyData.Take(testDataIndex + 1).ToArray());
        }

        private void SearchTestData(BTree<int> btree, int testKeyDataIndex)
        {
            for (int i = 0; i <= testKeyDataIndex; i++)
            {
                var entry = btree.Search(testKeyData[i]);
                Assert.True(entry);
            }
        }

        private void ValidateTree(BTreeNode<int> tree, int degree, params int[] expectedKeys)
        {
            var foundKeys = new Dictionary<int, List<int>>();
            ValidateSubtree(tree, tree, degree, int.MinValue, int.MaxValue, foundKeys);

            Assert.AreEqual(0, expectedKeys.Except(foundKeys.Keys).Count());
            foreach (var keyValuePair in foundKeys)
            {
                Assert.AreEqual(1, keyValuePair.Value.Count);
            }
        }

        private void ValidateSubtree(BTreeNode<int> root, BTreeNode<int> node, int degree, int nodeMin, int nodeMax, Dictionary<int, List<int>> foundKeys)
        {
            if (root != node)
            {
                Assert.IsTrue(node.Entries.Count >= degree - 1);
                Assert.IsTrue(node.Entries.Count <= 2 * degree - 1);
            }

            for (int i = 0; i <= node.Entries.Count; i++)
            {
                var subtreeMin = nodeMin;
                var subtreeMax = nodeMax;

                if (i < node.Entries.Count)
                {
                    var entry = node.Entries[i];
                    UpdateFoundKeys(foundKeys, entry);
                    Assert.IsTrue(entry >= nodeMin && entry <= nodeMax);

                    subtreeMax = entry;
                }

                if (i > 0)
                {
                    subtreeMin = node.Entries[i - 1];
                }

                if (!node.IsLeaf)
                {
                    Assert.IsTrue(node.Children.Count >= degree);
                    ValidateSubtree(root, node.Children[i], degree, subtreeMin, subtreeMax, foundKeys);
                }
            }
        }

        private void UpdateFoundKeys(Dictionary<int, List<int>> foundKeys, int entry)
        {
            if (!foundKeys.TryGetValue(entry, out var foundEntries))
            {
                foundEntries = new List<int>();
                foundKeys.Add(entry, foundEntries);
            }

            foundEntries.Add(entry);
        }

        #endregion
    }
}
