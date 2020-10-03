using System.Collections.Generic;
using System.Linq;

using DataStructures.BinarySearchTree;

using NUnit.Framework;

namespace DataStructures.Tests
{
    public static class BinarySearchTreeTests
    {
        [Test]
        public static void Add_MultipleKeys_FormsCorrectBST()
        {
            var tree = new BinarySearchTree<int>();

            tree.Add(5);
            Assert.AreEqual(1, tree.Count);
            Assert.IsNull(tree.Search(5)!.Parent);

            tree.Add(3);
            Assert.AreEqual(2, tree.Count);
            Assert.IsNotNull(tree.Search(3)!.Parent);
            Assert.AreEqual(5, tree.Search(3)!.Parent!.Key);

            tree.Add(4);
            Assert.AreEqual(3, tree.Count);
            Assert.IsNotNull(tree.Search(4)!.Parent);
            Assert.AreEqual(3, tree.Search(4)!.Parent!.Key);

            tree.Add(2);
            Assert.AreEqual(4, tree.Count);
            Assert.IsNotNull(tree.Search(2)!.Parent);
            Assert.AreEqual(3, tree.Search(2)!.Parent!.Key);
        }

        [Test]
        public static void AddRange_MultipleKeys_FormsCorrectBST()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2 });

            Assert.AreEqual(4, tree.Count);
            Assert.IsNull(tree.Search(5)!.Parent);

            Assert.IsNotNull(tree.Search(3)!.Parent);
            Assert.AreEqual(5, tree.Search(3)!.Parent!.Key);

            Assert.IsNotNull(tree.Search(4)!.Parent);
            Assert.AreEqual(3, tree.Search(4)!.Parent!.Key);

            Assert.IsNotNull(tree.Search(2)!.Parent);
            Assert.AreEqual(3, tree.Search(2)!.Parent!.Key);
        }

        [Test]
        public static void Search_MultipleKeys_FindsAllKeys()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

            Assert.AreEqual(2, tree.Search(2)!.Key);
            Assert.AreEqual(3, tree.Search(3)!.Key);
            Assert.AreEqual(4, tree.Search(4)!.Key);
            Assert.AreEqual(5, tree.Search(5)!.Key);
            Assert.AreEqual(6, tree.Search(6)!.Key);
            Assert.AreEqual(7, tree.Search(7)!.Key);
            Assert.AreEqual(8, tree.Search(8)!.Key);
        }

        public static void Contains_MultipleKeys_FindsAllKeys()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

            Assert.IsTrue(tree.Contains(2));
            Assert.IsTrue(tree.Contains(3));
            Assert.IsTrue(tree.Contains(4));
            Assert.IsTrue(tree.Contains(5));
            Assert.IsTrue(tree.Contains(6));
            Assert.IsTrue(tree.Contains(7));
            Assert.IsTrue(tree.Contains(8));
        }

        [Test]
        public static void Remove_MultipleKeys_CorrectlyRemovesNodesInAllCases()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

            var twoRemoveResult = tree.Remove(2);
            Assert.IsTrue(twoRemoveResult);
            Assert.IsNull(tree.Search(2));
            Assert.IsNull(tree.Search(3)!.Left);
            Assert.IsNotNull(tree.Search(3)!.Right);
            Assert.AreEqual(6, tree.Count);

            var threeRemoveResult = tree.Remove(3);
            Assert.IsTrue(threeRemoveResult);
            Assert.IsNull(tree.Search(3));
            Assert.IsNull(tree.Search(4)!.Left);
            Assert.IsNull(tree.Search(4)!.Right);
            Assert.AreEqual(5, tree.Count);

            var sevenRemoveResult = tree.Remove(7);
            Assert.IsTrue(sevenRemoveResult);
            Assert.IsNull(tree.Search(7));
            Assert.IsNull(tree.Search(6)!.Left);
            Assert.IsNotNull(tree.Search(6)!.Right);
            Assert.AreEqual(4, tree.Count);
        }

        [Test]
        public static void GetMax_NonEmptyTree_ReturnsCorrectValue()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

            Assert.AreEqual(8, tree.GetMax()!.Key);
        }

        [Test]
        public static void GetMin_NonEmptyTree_ReturnsCorrectValue()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

            Assert.AreEqual(2, tree.GetMin()!.Key);
        }

        [Test]
        public static void GetKeysInOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

            var keys = tree.GetKeysInOrder();
            var expected = new List<int> { 2, 3, 4, 5, 6, 7, 8 };
            Assert.IsTrue(Enumerable.SequenceEqual<int>(keys, expected));
        }

        [Test]
        public static void GetKeysPreOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

            var keys = tree.GetKeysPreOrder();
            var expected = new List<int> { 5, 3, 2, 4, 7, 6, 8 };
            Assert.IsTrue(Enumerable.SequenceEqual<int>(keys, expected));
        }

        [Test]
        public static void GetKeysPostOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

            var keys = tree.GetKeysPostOrder();
            var expected = new List<int> { 2, 4, 3, 6, 8, 7, 5 };
            Assert.IsTrue(Enumerable.SequenceEqual<int>(keys, expected));
        }
    }
}
