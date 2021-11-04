using System;
using System.Linq;
using DataStructures.ScapegoatTree;
using NUnit.Framework;

namespace DataStructures.Tests.ScapegoatTree
{
    [TestFixture]
    public class ScapegoatTreeNodeTests
    {

        [Test]
        [TestCase(2,1)]
        [TestCase("B", "A")]
        public void RightSet_OtherKeyPrecedesRightKey_ThrowsException<TKey>(TKey a, TKey b)
            where TKey : IComparable
        {
            var instance = new Node<TKey>(a);
            var other = new Node<TKey>(b);

            Assert.Throws<ArgumentException>(() => instance.Right = other);
        }

        [Test]
        [TestCase(1,2)]
        [TestCase("A","B")]
        public void RightSet_OtherKeyFollowsRightKey_AddsChild<TKey>(TKey a, TKey b)
            where TKey : IComparable
        {
            var instance = new Node<TKey>(a);
            var other = new Node<TKey>(b);

            Assert.DoesNotThrow(() => instance.Right = other);
        }

        [Test]
        [TestCase(1,2)]
        [TestCase("A","B")]
        public void LeftSet_OtherKeyFollowsLeftKey_ThrowsException<TKey>(TKey a, TKey b)
            where TKey : IComparable
        {
            var instance = new Node<TKey>(a);
            var other = new Node<TKey>(b);

            Assert.Throws<ArgumentException>(() => instance.Left = other);
        }

        [Test]
        [TestCase(2,1)]
        [TestCase("B", "A")]
        public void LeftSet_OtherKeyPrecedesLeftKey_AddsChild<TKey>(TKey a, TKey b)
            where TKey : IComparable
        {
            var instance = new Node<TKey>(a);
            var other = new Node<TKey>(b);

            Assert.DoesNotThrow(() => instance.Left = other);
        }


        [Test]
        [TestCase(1,2)]
        [TestCase("A","B")]
        public void CompareTo_InstanceKeyPrecedesOtherKey_ReturnsMinusOne<TKey>(TKey a, TKey b)
            where TKey : IComparable
        {
            var instance = new Node<TKey>(a);
            var other = new Node<TKey>(b);

            var result = instance.Key.CompareTo(other.Key);

            Assert.AreEqual(result, -1);
        }

        [Test]
        [TestCase(2, 1)]
        [TestCase("B","A")]
        public void CompareTo_InstanceKeyFollowsOtherKey_ReturnsOne<TKey>(TKey a, TKey b)
            where TKey : IComparable
        {
            var instance = new Node<TKey>(a);
            var other = new Node<TKey>(b);

            var result = instance.Key.CompareTo(other.Key);

            Assert.AreEqual(result, 1);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase("A","A")]
        public void CompareTo_InstanceKeyEqualsOtherKey_ReturnsZero<TKey>(TKey a, TKey b)
            where TKey : IComparable
        {
            var instance = new Node<TKey>(a);
            var other = new Node<TKey>(b);

            var result = instance.Key.CompareTo(other.Key);

            Assert.AreEqual(result, 0);
        }

        [Test]
        public void GetSize_NodeHasNoChildren_ReturnsOne()
        {
            var node = new Node<int>(1);

            Assert.AreEqual(node.GetSize(), 1);
        }

        [Test]
        public void GetSize_NodeHasChildren_ReturnsCorrectSize()
        {
            var node = new Node<int>(1, new Node<int>(2), new Node<int>(0));

            Assert.AreEqual(node.GetSize(), 3);
        }

        [Test]
        public void GetSmallestKeyNode_NodeHasNoLeftChildren_ReturnsNode()
        {
            var node = new Node<int>(1);

            Assert.AreEqual(node.GetSmallestKeyNode(), node);
        }

        [Test]
        public void GetSmallestKeyNode_NodeHasSmallestChild_ReturnsChild()
        {
            var node = new Node<int>(1);
            var smaller = new Node<int>(0);
            var theSmallest = new Node<int>(-1);
            node.Left = smaller;
            smaller.Left = theSmallest;

            Assert.AreEqual(node.GetSmallestKeyNode(), theSmallest);
        }

        [Test]
        public void GetLargestKeyNode_NodeHasNoRightChildren_ReturnsNode()
        {
            var node = new Node<int>(1);

            Assert.AreEqual(node.GetLargestKeyNode(), node);
        }

        [Test]
        public void GetLargestKeyNode_NodeHasLargestChild_ReturnsChild()
        {
            var node = new Node<int>(1);
            var bigger = new Node<int>(2);
            var theBiggest = new Node<int>(3);
            node.Right = bigger;
            bigger.Right = theBiggest;

            Assert.AreEqual(node.GetLargestKeyNode(), theBiggest);
        }

        [Test]
        public void IsAlphaWeightBalanced_TreeIsUnbalanced_ReturnsFalse()
        {
            var root = new Node<int>(0);
            var a = new Node<int>(-1);
            var b = new Node<int>(-2);
            var c = new Node<int>(-3);
            var d = new Node<int>(1);

            root.Left = a;
            a.Left = b;
            b.Left = c;
            root.Right = d;

            Assert.IsFalse(root.IsAlphaWeightBalanced(0.5));
        }

        [Test]
        public void IsAlphaWeightBalanced_TreeIsBalanced_ReturnsTrue()
        {
            var root = new Node<int>(0);
            var a = new Node<int>(-1);
            var b = new Node<int>(-2);
            var d = new Node<int>(1);

            root.Left = a;
            a.Left = b;
            root.Right = d;

            Assert.IsTrue(root.IsAlphaWeightBalanced(0.5));
        }

        [Test]
        public void GetEnumerator_TreeHasChildren_ReturnsInOrder()
        {
            var root = new Node<int>(0);
            var a = new Node<int>(-1);
            var b = new Node<int>(-2);
            var c = new Node<int>(-3);
            var d = new Node<int>(1);

            root.Left = a;
            a.Left = b;
            b.Left = c;
            root.Right = d;

            var result = root.AsEnumerable().Aggregate(String.Empty, (s, i) => s + i.ToString());

            Assert.AreEqual("-3-2-101", result);
        }
    }
}
