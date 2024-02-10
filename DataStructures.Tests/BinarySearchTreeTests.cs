using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.BinarySearchTree;
using NUnit.Framework;

namespace DataStructures.Tests;

public static class BinarySearchTreeTests
{
    [Test]
    public static void Constructor_UseCustomComparer_FormsCorrectTree()
    {
        var cmpFunc = Comparer<string>.Create((x, y) => x.Length - y.Length);
        var tree = new BinarySearchTree<string>(cmpFunc);
        var elems = new[] { "z", "yy", "vvv", "bbbb", "fffff", "pppppp" };
        tree.AddRange(elems);

        Assert.That(tree.Search("vvv"), Is.Not.Null);
        Assert.That(tree.Search("vvv")!.Right, Is.Not.Null);
        Assert.That(tree.Search("vvv")!.Right!.Key, Is.EqualTo("bbbb"));
    }

    [Test]
    public static void Add_MultipleKeys_FormsCorrectBST()
    {
        var tree = new BinarySearchTree<int>();

        tree.Add(5);
        Assert.That(tree.Count, Is.EqualTo(1));

        tree.Add(3);
        Assert.That(tree.Count, Is.EqualTo(2));

        tree.Add(4);
        Assert.That(tree.Count, Is.EqualTo(3));

        tree.Add(2);
        Assert.That(tree.Count, Is.EqualTo(4));

        var rootNode = tree.Search(5);
        Assert.That(rootNode!.Key, Is.EqualTo(5));
        Assert.That(rootNode!.Left!.Key, Is.EqualTo(3));
        Assert.That(rootNode!.Right, Is.Null);

        var threeNode = tree.Search(3);
        Assert.That(threeNode!.Key, Is.EqualTo(3));
        Assert.That(threeNode!.Left!.Key, Is.EqualTo(2));
        Assert.That(threeNode!.Right!.Key, Is.EqualTo(4));

        var twoNode = tree.Search(2);
        Assert.That(twoNode!.Left, Is.Null);
        Assert.That(twoNode!.Right, Is.Null);

        var fourNode = tree.Search(4);
        Assert.That(fourNode!.Left, Is.Null);
        Assert.That(fourNode!.Right, Is.Null);
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
        Assert.That(rootNode!.Key, Is.EqualTo(5));
        Assert.That(rootNode!.Left!.Key, Is.EqualTo(3));
        Assert.That(rootNode!.Right, Is.Null);

        var threeNode = tree.Search(3);
        Assert.That(threeNode!.Key, Is.EqualTo(3));
        Assert.That(threeNode!.Left!.Key, Is.EqualTo(2));
        Assert.That(threeNode!.Right!.Key, Is.EqualTo(4));

        var twoNode = tree.Search(2);
        Assert.That(twoNode!.Left, Is.Null);
        Assert.That(twoNode!.Right, Is.Null);

        var fourNode = tree.Search(4);
        Assert.That(fourNode!.Left, Is.Null);
        Assert.That(fourNode!.Right, Is.Null);
    }

    [Test]
    public static void Search_MultipleKeys_FindsAllKeys()
    {
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

        Assert.That(tree.Search(2)!.Key, Is.EqualTo(2));
        Assert.That(tree.Search(3)!.Key, Is.EqualTo(3));
        Assert.That(tree.Search(4)!.Key, Is.EqualTo(4));
        Assert.That(tree.Search(5)!.Key, Is.EqualTo(5));
        Assert.That(tree.Search(6)!.Key, Is.EqualTo(6));
        Assert.That(tree.Search(7)!.Key, Is.EqualTo(7));
        Assert.That(tree.Search(8)!.Key, Is.EqualTo(8));
    }

    [Test]
    public static void Contains_MultipleKeys_FindsAllKeys()
    {
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

        Assert.That(tree.Contains(2), Is.True);
        Assert.That(tree.Contains(3), Is.True);
        Assert.That(tree.Contains(4), Is.True);
        Assert.That(tree.Contains(5), Is.True);
        Assert.That(tree.Contains(6), Is.True);
        Assert.That(tree.Contains(7), Is.True);
        Assert.That(tree.Contains(8), Is.True);
    }

    [Test]
    public static void Remove_LeafNodes_CorrectlyRemovesNodes()
    {
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

        var twoRemoveResult = tree.Remove(2);
        Assert.That(twoRemoveResult, Is.True);
        Assert.That(tree.Search(2), Is.Null);
        Assert.That(tree.Search(3)!.Left, Is.Null);
        Assert.That(tree.Search(3)!.Right, Is.Not.Null);
        Assert.That(tree.Count, Is.EqualTo(6));

        var fourRemoveResult = tree.Remove(4);
        Assert.That(fourRemoveResult, Is.True);
        Assert.That(tree.Search(4), Is.Null);
        Assert.That(tree.Search(3)!.Left, Is.Null);
        Assert.That(tree.Search(3)!.Right, Is.Null);
        Assert.That(tree.Count, Is.EqualTo(5));
    }

    [Test]
    public static void Remove_NodesWithOneChild_CorrectlyRemovesNodes()
    {
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

        tree.Remove(4);
        var threeRemoveResult = tree.Remove(3);
        Assert.That(threeRemoveResult, Is.True);
        Assert.That(tree.Search(3), Is.Null);
        Assert.That(tree.Search(2)!.Left, Is.Null);
        Assert.That(tree.Search(2)!.Right, Is.Null);
        Assert.That(tree.Count, Is.EqualTo(5));

        tree.Remove(6);
        var sevenRemoveResult = tree.Remove(7);
        Assert.That(sevenRemoveResult, Is.True);
        Assert.That(tree.Search(7), Is.Null);
        Assert.That(tree.Search(8)!.Left, Is.Null);
        Assert.That(tree.Search(8)!.Right, Is.Null);
        Assert.That(tree.Count, Is.EqualTo(3));
    }

    [Test]
    public static void Remove_NodesWithTwoChildren_CorrectlyRemovesNodes()
    {
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

        var sevenRemoveResult = tree.Remove(7);
        Assert.That(sevenRemoveResult, Is.True);
        Assert.That(tree.Search(7), Is.Null);
        Assert.That(tree.Search(6)!.Left, Is.Null);
        Assert.That(tree.Search(6)!.Right, Is.Not.Null);
        Assert.That(tree.Count, Is.EqualTo(6));
    }

    [Test]
    public static void Remove_NonExistentElement_ReturnsFalse()
    {
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

        Assert.That(tree.Remove(999), Is.False);
        Assert.That(tree.Count, Is.EqualTo(7));
    }

    [Test]
    public static void Remove_EmptyTree_ReturnsFalse()
    {
        var tree = new BinarySearchTree<int>();
        Assert.That(tree.Remove(8), Is.False);
        Assert.That(tree.Count, Is.EqualTo(0));
    }

    [Test]
    public static void Remove_RemoveRoot_CorrectlyRemovesRoot()
    {
        var tree = new BinarySearchTree<int>();
        tree.Add(5);
        tree.Remove(5);

        Assert.That(tree.Count, Is.EqualTo(0));
        Assert.That(tree.Search(5), Is.Null);

        tree.AddRange(new List<int> { 5, 4, 6 });
        tree.Remove(5);

        Assert.That(tree.Count, Is.EqualTo(2));
        Assert.That(tree.Search(5), Is.Null);
        Assert.That(tree.Search(4), Is.Not.Null);
        Assert.That(tree.Search(6), Is.Not.Null);
        Assert.That(tree.Search(4)!.Right!.Key, Is.EqualTo(6));
    }

    [Test]
    public static void GetMax_NonEmptyTree_ReturnsCorrectValue()
    {
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

        Assert.That(tree.GetMax()!.Key, Is.EqualTo(8));
    }

    [Test]
    public static void GetMax_EmptyTree_ReturnsDefaultValue()
    {
        var tree = new BinarySearchTree<int>();
        Assert.That(tree.GetMax(), Is.Null);
    }

    [Test]
    public static void GetMin_NonEmptyTree_ReturnsCorrectValue()
    {
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

        Assert.That(tree.GetMin()!.Key, Is.EqualTo(2));
    }

    [Test]
    public static void GetMin_EmptyTree_ReturnsDefaultValue()
    {
        var tree = new BinarySearchTree<int>();
        Assert.That(tree.GetMin(), Is.Null);
    }

    [Test]
    public static void GetKeysInOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder()
    {
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

        var keys = tree.GetKeysInOrder();
        var expected = new List<int> { 2, 3, 4, 5, 6, 7, 8 };
        Assert.That(keys.SequenceEqual(expected), Is.True);
    }

    [Test]
    public static void GetKeysPreOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder()
    {
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

        var keys = tree.GetKeysPreOrder();
        var expected = new List<int> { 5, 3, 2, 4, 7, 6, 8 };
        Assert.That(keys.SequenceEqual(expected), Is.True);
    }

    [Test]
    public static void GetKeysPostOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder()
    {
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });

        var keys = tree.GetKeysPostOrder();
        var expected = new List<int> { 2, 4, 3, 6, 8, 7, 5 };
        Assert.That(keys.SequenceEqual(expected), Is.True);
    }
}
