using System;
using System.Collections.Generic;
using System.Linq;

using DataStructures.BinarySearchTree;

using NUnit.Framework;

namespace DataStructures.Tests
{
    public static class BinarySearchTreeTests
    {
        [Test]
        public static void Constructor_UseCustomComparer_FormsCorrectTree()
        {
            var cmpFunc = Comparer<string>.Create((x, y) => x.Length - y.Length);
            var tree = new BinarySearchTree<string>(cmpFunc);
            var elems = new[] { "z", "yy", "vvv", "bbbb", "fffff", "pppppp" };
            tree.AddRange(elems);

            Assert.IsNotNull(tree.Search("vvv"));
            Assert.AreEqual("bbbb", tree.Search("vvv")!.Right!.Key);
        }

        [Test]
        public static void Add_MultipleKeys_FormsCorrectBST()
        {
            var tree = new BinarySearchTree<int>();

            tree.Add(5);
            Assert.AreEqual(1, tree.Count);

            tree.Add(3);
            Assert.AreEqual(2, tree.Count);

            tree.Add(4);
            Assert.AreEqual(3, tree.Count);

            tree.Add(2);
            Assert.AreEqual(4, tree.Count);

            var rootNode = tree.Search(5);
            Assert.AreEqual(5, rootNode!.Key);
            Assert.AreEqual(3, rootNode!.Left!.Key);
            Assert.IsNull(rootNode!.Right);

            var threeNode = tree.Search(3);
            Assert.AreEqual(3, threeNode!.Key);
            Assert.AreEqual(2, threeNode!.Left!.Key);
            Assert.AreEqual(4, threeNode!.Right!.Key);

            var twoNode = tree.Search(2);
            Assert.IsNull(twoNode!.Left);
            Assert.IsNull(twoNode!.Right);

            var fourNode = tree.Search(4);
            Assert.IsNull(fourNode!.Left);
            Assert.IsNull(fourNode!.Right);
        }

        [Test]
        public static void Add_KeyAlreadyInTree_ThrowsCorrectException()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2 });

            _ = Assert.Throws<ArgumentException>(() => tree.Add(5));
        }

        [Test]
        public static void AddRange_MultipleKeys_FormsCorrectBST()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2 });

            var rootNode = tree.Search(5);
            Assert.AreEqual(5, rootNode!.Key);
            Assert.AreEqual(3, rootNode!.Left!.Key);
            Assert.IsNull(rootNode!.Right);

            var threeNode = tree.Search(3);
            Assert.AreEqual(3, threeNode!.Key);
            Assert.AreEqual(2, threeNode!.Left!.Key);
            Assert.AreEqual(4, threeNode!.Right!.Key);

            var twoNode = tree.Search(2);
            Assert.IsNull(twoNode!.Left);
            Assert.IsNull(twoNode!.Right);

            var fourNode = tree.Search(4);
            Assert.IsNull(fourNode!.Left);
            Assert.IsNull(fourNode!.Right);
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

        [Test]
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
        public static void Remove_LeafNodes_CorrectlyRemovesNodes()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

            var twoRemoveResult = tree.Remove(2);
            Assert.IsTrue(twoRemoveResult);
            Assert.IsNull(tree.Search(2));
            Assert.IsNull(tree.Search(3)!.Left);
            Assert.IsNotNull(tree.Search(3)!.Right);
            Assert.AreEqual(6, tree.Count);

            var fourRemoveResult = tree.Remove(4);
            Assert.IsTrue(fourRemoveResult);
            Assert.IsNull(tree.Search(4));
            Assert.IsNull(tree.Search(3)!.Left);
            Assert.IsNull(tree.Search(3)!.Right);
            Assert.AreEqual(5, tree.Count);
        }

        [Test]
        public static void Remove_NodesWithOneChild_CorrectlyRemovesNodes()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

            tree.Remove(4);
            var threeRemoveResult = tree.Remove(3);
            Assert.IsTrue(threeRemoveResult);
            Assert.IsNull(tree.Search(3));
            Assert.IsNull(tree.Search(2)!.Left);
            Assert.IsNull(tree.Search(2)!.Right);
            Assert.AreEqual(5, tree.Count);

            tree.Remove(6);
            var sevenRemoveResult = tree.Remove(7);
            Assert.IsTrue(sevenRemoveResult);
            Assert.IsNull(tree.Search(7));
            Assert.IsNull(tree.Search(8)!.Left);
            Assert.IsNull(tree.Search(8)!.Right);
            Assert.AreEqual(3, tree.Count);
        }

        [Test]
        public static void Remove_NodesWithTwoChildren_CorrectlyRemovesNodes()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

            var sevenRemoveResult = tree.Remove(7);
            Assert.IsTrue(sevenRemoveResult);
            Assert.IsNull(tree.Search(7));
            Assert.IsNull(tree.Search(6)!.Left);
            Assert.IsNotNull(tree.Search(6)!.Right);
            Assert.AreEqual(6, tree.Count);
        }

        [Test]
        public static void Remove_NonExistentElement_ReturnsFalse()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

            Assert.IsFalse(tree.Remove(999));
            Assert.AreEqual(7, tree.Count);
        }

        [Test]
        public static void Remove_EmptyTree_ReturnsFalse()
        {
            var tree = new BinarySearchTree<int>();
            Assert.IsFalse(tree.Remove(8));
            Assert.AreEqual(0, tree.Count);
        }

        [Test]
        public static void Remove_RemoveRoot_CorrectlyRemovesRoot()
        {
            var tree = new BinarySearchTree<int>();
            tree.Add(5);
            tree.Remove(5);

            Assert.AreEqual(0, tree.Count);
            Assert.IsNull(tree.Search(5));

            tree.AddRange(new List<int> { 5, 4, 6 });
            tree.Remove(5);

            Assert.AreEqual(2, tree.Count);
            Assert.IsNull(tree.Search(5));
            Assert.IsNotNull(tree.Search(4));
            Assert.IsNotNull(tree.Search(6));
            Assert.AreEqual(6, tree.Search(4)!.Right!.Key);
        }

        [Test]
        public static void GetMax_NonEmptyTree_ReturnsCorrectValue()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

            Assert.AreEqual(8, tree.GetMax()!.Key);
        }

        [Test]
        public static void GetMax_EmptyTree_ReturnsDefaultValue()
        {
            var tree = new BinarySearchTree<int>();
            Assert.IsNull(tree.GetMax());
        }

        [Test]
        public static void GetMin_NonEmptyTree_ReturnsCorrectValue()
        {
            var tree = new BinarySearchTree<int>();
            tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

            Assert.AreEqual(2, tree.GetMin()!.Key);
        }

        [Test]
        public static void GetMin_EmptyTree_ReturnsDefaultValue()
        {
            var tree = new BinarySearchTree<int>();
            Assert.IsNull(tree.GetMin());
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
