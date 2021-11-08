using System;
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
        public void RightSetter_OtherKeyPrecedesRightKey_ThrowsException<TKey>(TKey a, TKey b)
            where TKey : IComparable
        {
            var instance = new Node<TKey>(a);
            var other = new Node<TKey>(b);

            Assert.Throws<ArgumentException>(() => instance.Right = other);
        }

        [Test]
        [TestCase(1,2)]
        [TestCase("A","B")]
        public void RightSetter_OtherKeyFollowsRightKey_AddsChild<TKey>(TKey a, TKey b)
            where TKey : IComparable
        {
            var instance = new Node<TKey>(a);
            var other = new Node<TKey>(b);

            Assert.DoesNotThrow(() => instance.Right = other);
        }

        [Test]
        [TestCase(1,2)]
        [TestCase("A","B")]
        public void LeftSetter_OtherKeyFollowsLeftKey_ThrowsException<TKey>(TKey a, TKey b)
            where TKey : IComparable
        {
            var instance = new Node<TKey>(a);
            var other = new Node<TKey>(b);

            Assert.Throws<ArgumentException>(() => instance.Left = other);
        }

        [Test]
        [TestCase(2,1)]
        [TestCase("B", "A")]
        public void LeftSetter_OtherKeyPrecedesLeftKey_AddsChild<TKey>(TKey a, TKey b)
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
            var smallest = new Node<int>(-1);
            node.Left = smaller;
            smaller.Left = smallest;

            Assert.AreEqual(node.GetSmallestKeyNode(), smallest);
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
            var larger = new Node<int>(2);
            var largest = new Node<int>(3);
            node.Right = larger;
            larger.Right = largest;

            Assert.AreEqual(node.GetLargestKeyNode(), largest);
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
    }
}
