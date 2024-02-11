using System;
using DataStructures.ScapegoatTree;
using NUnit.Framework;

namespace DataStructures.Tests.ScapegoatTree;

[TestFixture]
public class ScapegoatTreeNodeTests
{
    [TestCase(2,1)]
    [TestCase("B", "A")]
    public void RightSetter_OtherKeyPrecedesRightKey_ThrowsException<TKey>(TKey a, TKey b)
        where TKey : IComparable
    {
        var instance = new Node<TKey>(a);
        var other = new Node<TKey>(b);

        Assert.Throws<ArgumentException>(() => instance.Right = other);
    }

    [TestCase(1,2)]
    [TestCase("A","B")]
    public void RightSetter_OtherKeyFollowsRightKey_AddsChild<TKey>(TKey a, TKey b)
        where TKey : IComparable
    {
        var instance = new Node<TKey>(a);
        var other = new Node<TKey>(b);

        Assert.DoesNotThrow(() => instance.Right = other);
    }

    [TestCase(1,2)]
    [TestCase("A","B")]
    public void LeftSetter_OtherKeyFollowsLeftKey_ThrowsException<TKey>(TKey a, TKey b)
        where TKey : IComparable
    {
        var instance = new Node<TKey>(a);
        var other = new Node<TKey>(b);

        Assert.Throws<ArgumentException>(() => instance.Left = other);
    }

    [TestCase(2,1)]
    [TestCase("B", "A")]
    public void LeftSetter_OtherKeyPrecedesLeftKey_AddsChild<TKey>(TKey a, TKey b)
        where TKey : IComparable
    {
        var instance = new Node<TKey>(a);
        var other = new Node<TKey>(b);

        Assert.DoesNotThrow(() => instance.Left = other);
    }

    [TestCase(1,2)]
    [TestCase("A","B")]
    public void CompareTo_InstanceKeyPrecedesOtherKey_ReturnsMinusOne<TKey>(TKey a, TKey b)
        where TKey : IComparable
    {
        var instance = new Node<TKey>(a);
        var other = new Node<TKey>(b);

        var result = instance.Key.CompareTo(other.Key);

        Assert.That(result, Is.EqualTo(-1));
    }

    [TestCase(2, 1)]
    [TestCase("B","A")]
    public void CompareTo_InstanceKeyFollowsOtherKey_ReturnsOne<TKey>(TKey a, TKey b)
        where TKey : IComparable
    {
        var instance = new Node<TKey>(a);
        var other = new Node<TKey>(b);

        var result = instance.Key.CompareTo(other.Key);

        Assert.That(1, Is.EqualTo(result));
    }

    [TestCase(1, 1)]
    [TestCase("A","A")]
    public void CompareTo_InstanceKeyEqualsOtherKey_ReturnsZero<TKey>(TKey a, TKey b)
        where TKey : IComparable
    {
        var instance = new Node<TKey>(a);
        var other = new Node<TKey>(b);

        var result = instance.Key.CompareTo(other.Key);

        Assert.That(0, Is.EqualTo(result));
    }

    [Test]
    public void GetSize_NodeHasNoChildren_ReturnsOne()
    {
        var node = new Node<int>(1);

        Assert.That(1, Is.EqualTo(node.GetSize()));
    }

    [Test]
    public void GetSize_NodeHasChildren_ReturnsCorrectSize()
    {
        var node = new Node<int>(1, new Node<int>(2), new Node<int>(0));

        Assert.That(3, Is.EqualTo(node.GetSize()));
    }

    [Test]
    public void GetSmallestKeyNode_NodeHasNoLeftChildren_ReturnsNode()
    {
        var node = new Node<int>(1);

        Assert.That(node, Is.EqualTo(node.GetSmallestKeyNode()));
    }

    [Test]
    public void GetSmallestKeyNode_NodeHasSmallestChild_ReturnsChild()
    {
        var node = new Node<int>(1);
        var smaller = new Node<int>(0);
        var smallest = new Node<int>(-1);
        node.Left = smaller;
        smaller.Left = smallest;

        Assert.That(smallest, Is.EqualTo(node.GetSmallestKeyNode()));
    }

    [Test]
    public void GetLargestKeyNode_NodeHasNoRightChildren_ReturnsNode()
    {
        var node = new Node<int>(1);

        Assert.That(node, Is.EqualTo(node.GetLargestKeyNode()));
    }

    [Test]
    public void GetLargestKeyNode_NodeHasLargestChild_ReturnsChild()
    {
        var node = new Node<int>(1);
        var larger = new Node<int>(2);
        var largest = new Node<int>(3);
        node.Right = larger;
        larger.Right = largest;

        Assert.That(largest, Is.EqualTo(node.GetLargestKeyNode()));
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

        Assert.That(root.IsAlphaWeightBalanced(0.5), Is.False);
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

        Assert.That(root.IsAlphaWeightBalanced(0.5), Is.True);
    }
}
