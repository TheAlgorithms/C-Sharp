using System;
using System.Collections.Generic;
using System.Linq;

using DataStructures.AATree;

using NUnit.Framework;

namespace DataStructures.Tests
{
    class AATreeTests
    {
        [Test]
        public static void Constructor_UseCustomComparer_FormsCorrectTree()
        {
            var tree = new AATree<int>(Comparer<int>.Create((x, y) => x.CompareTo(y) * -1));
            tree.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            Assert.AreEqual(1, tree.GetMax());
            Assert.AreEqual(10, tree.GetMin());

            var expected = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            var actual = tree.GetKeysInOrder();
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));

            Assert.DoesNotThrow(() => tree.Validate());
        }

        [Test]
        public static void Add_MultipleKeys_FormsCorrectTree()
        {
            var tree = new AATree<int>();

            foreach(int elem in new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 ,10 })
            {
                tree.Add(elem);
                Assert.AreEqual(elem, tree.Count);
                Assert.IsTrue(tree.Contains(elem));
            }

            var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var actual = tree.GetKeysInOrder();
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));

            expected = new int[] { 1, 3, 2, 5, 7, 10, 9, 8, 6, 4 };
            actual = tree.GetKeysPostOrder();
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));

            Assert.DoesNotThrow(() => tree.Validate());
        }

        [Test]
        public static void Add_KeyAlreadyInTree_ThrowsException()
        {
            var tree = new AATree<int>();
            tree.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            Assert.Throws<ArgumentException>(() => tree.Add(1));
        }

        [Test]
        public static void AddRange_MultipleKeys_FormsCorrectTree()
        {
            var tree = new AATree<int>();
            tree.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            Assert.AreEqual(10, tree.Count);

            var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var actual = tree.GetKeysInOrder();
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));

            expected = new int[] { 1, 3, 2, 5, 7, 10, 9, 8, 6, 4 };
            actual = tree.GetKeysPostOrder();
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));

            Assert.DoesNotThrow(() => tree.Validate());
        }

        [Test]
        public static void Remove_MultipleKeys_TreeStillValid()
        {
            var tree = new AATree<int>();
            tree.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            Assert.IsTrue(tree.Remove(8));
            Assert.IsFalse(tree.Contains(8));
            Assert.AreEqual(9, tree.Count);

            Assert.IsTrue(tree.Remove(4));
            Assert.IsFalse(tree.Contains(4));
            Assert.AreEqual(8, tree.Count);

            Assert.IsTrue(tree.Remove(1));
            Assert.IsFalse(tree.Contains(1));
            Assert.AreEqual(7, tree.Count);

            Assert.DoesNotThrow(() => tree.Validate());
        }

        [Test]
        public static void Remove_KeyNotInTree_ReturnsFalse()
        {
            var tree = new AATree<int>();
            tree.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            Assert.IsFalse(tree.Remove(999));
        }

        [Test]
        public static void Remove_EmptyTree_ReturnsFalse()
        {
            var tree = new AATree<int>();

            Assert.IsFalse(tree.Remove(999));
        }

        [Test]
        public static void Contains_NonEmptyTree_ReturnsCorrectAnswer()
        {
            var tree = new AATree<int>();
            tree.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            Assert.IsTrue(tree.Contains(6));
            Assert.IsFalse(tree.Contains(999));
        }

        [Test]
        public static void Contains_EmptyTree_ReturnsFalse()
        {
            var tree = new AATree<int>();

            Assert.IsFalse(tree.Contains(999));
        }

        [Test]
        public static void GetMax_NonEmptyTree_ReturnsCorrectAnswer()
        {
            var tree = new AATree<int>();
            tree.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            Assert.AreEqual(10, tree.GetMax());
        }

        [Test]
        public static void GetMax_EmptyTree_ThrowsCorrectException()
        {
            var tree = new AATree<int>();
            Assert.Throws<InvalidOperationException>(() => tree.GetMax());
        }

        [Test]
        public static void GetMin_NonEmptyTree_ReturnsCorrectAnswer()
        {
            var tree = new AATree<int>();
            tree.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            Assert.AreEqual(1, tree.GetMin());
        }

        [Test]
        public static void GetMin_EmptyTree_ThrowsCorrectException()
        {
            var tree = new AATree<int>();
            Assert.Throws<InvalidOperationException>(() => tree.GetMin());
        }

        [Test]
        public static void GetKeysInOrder_NonEmptyTree_ReturnsCorrectAnswer()
        {
            var tree = new AATree<int>();
            tree.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var actual = tree.GetKeysInOrder();
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [Test]
        public static void GetKeysInOrder_EmptyTree_ReturnsCorrectAnswer()
        {
            var tree = new AATree<int>();
            Assert.IsTrue(tree.GetKeysInOrder().ToList().Count == 0);
        }

        [Test]
        public static void GetKeysPreOrder_NonEmptyTree_ReturnsCorrectAnswer()
        {
            var tree = new AATree<int>();
            tree.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            var expected = new int[] { 4, 2, 1, 3, 6, 5, 8, 7, 9, 10 };
            var actual = tree.GetKeysPreOrder();
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [Test]
        public static void GetKeysPreOrder_EmptyTree_ReturnsCorrectAnswer()
        {
            var tree = new AATree<int>();
            Assert.IsTrue(tree.GetKeysPreOrder().ToList().Count == 0);
        }

        [Test]
        public static void GetKeysPostOrder_NonEmptyTree_ReturnsCorrectAnswer()
        {
            var tree = new AATree<int>();
            tree.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            var expected = new int[] { 1, 3, 2, 5, 7, 10, 9, 8, 6, 4 };
            var actual = tree.GetKeysPostOrder();
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [Test]
        public static void GetKeysPostOrder_EmptyTree_ReturnsCorrectAnswer()
        {
            var tree = new AATree<int>();
            Assert.IsTrue(tree.GetKeysPostOrder().ToList().Count == 0);
        }

        [Test]
        public static void PerformanceTest()
        {
            var rng = new Random();
            var numElements = 100000;
            var elemList = Enumerable.Range(0, numElements).ToList();

            for(int i = 0; i < numElements; i++)
            {
                var j = rng.Next(i);
                var tmp = elemList[i];
                elemList[i] = elemList[j];
                elemList[j] = tmp;
            }

            var tree = new AATree<int>();
            tree.AddRange(elemList);

            var numElemsToRemove = rng.Next(numElements / 4, numElements / 2);
            for(int i = 0; i < numElemsToRemove; i++)
            {
                var idx = rng.Next(numElements);

                while(elemList[idx] == -1)
                {
                    idx = rng.Next(numElements);
                }

                tree.Remove(elemList[idx]);
                elemList[idx] = -1;
            }

            for(int i = 0; i < numElements; i++)
            {
                tree.Contains(i);
            }
        }
    }
}
