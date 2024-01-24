using System;
using System.Collections.Generic;
using DataStructures.ScapegoatTree;
using NUnit.Framework;

namespace DataStructures.Tests.ScapegoatTree;

public class ScapegoatTreeTests
{
    [Test]
    public void Constructor_NoParameters_InstanceIsValid()
    {
        var tree = new ScapegoatTree<int>();

        Assert.That(tree.Root, Is.Null);
        Assert.That(tree.Size == 0, Is.True);
        Assert.That(tree.MaxSize == 0, Is.True);
        Assert.That(tree.Alpha, Is.EqualTo(0.5));
    }

    [Test]
    public void Constructor_AlphaParameter_InstanceIsValid()
    {
        var expected = 0.6;

        var tree = new ScapegoatTree<int>(expected);

        Assert.That(tree.Root, Is.Null);
        Assert.That(tree.Size == 0, Is.True);
        Assert.That(tree.MaxSize == 0, Is.True);
        Assert.That(tree.Alpha, Is.EqualTo(expected));
    }

    [TestCase(1.1)]
    [TestCase(0.4)]
    public void Constructor_AlphaParameterIsInvalid_ThrowsException(double alpha)
    {
        Assert.Throws<ArgumentException>(() => new ScapegoatTree<int>(alpha));
        Assert.Throws<ArgumentException>(() => new ScapegoatTree<int>(1, alpha));
    }

    [Test]
    public void Constructor_KeyParameter_InstanceIsValid()
    {
        var expected = 10;

        var tree = new ScapegoatTree<int>(expected);

        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Root!.Key == expected, Is.True);
        Assert.That(tree.Size == 1, Is.True);
        Assert.That(tree.MaxSize == 1, Is.True);
        Assert.That(tree.Alpha, Is.EqualTo(0.5));
    }

    [Test]
    public void Constructor_KeyAndAlphaParameters_InstanceIsValid()
    {
        var key = 10;
        var alpha = 0.8;

        var tree = new ScapegoatTree<int>(key, alpha);

        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Size == 1, Is.True);
        Assert.That(tree.MaxSize == 1, Is.True);
        Assert.That(tree.Alpha, Is.EqualTo(alpha));
    }

    [Test]
    public void Constructor_NodeAndAlphaParameters_InstanceIsValid()
    {
        var node = new Node<int>(10, new Node<int>(11), new Node<int>(1));
        var alpha = 0.8;

        var tree = new ScapegoatTree<int>(node, alpha);

        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Size == 3, Is.True);
        Assert.That(tree.MaxSize == 3, Is.True);
        Assert.That(tree.Alpha, Is.EqualTo(alpha));
    }

    [Test]
    public void IsAlphaWeightBalanced_RootIsNull_ReturnsTrue()
    {
        var tree = new ScapegoatTree<int>();

        var result = tree.IsAlphaWeightBalanced();

        Assert.That(result, Is.True);
    }

    [Test]
    public void Search_RootIsNull_ReturnsNull()
    {
        var tree = new ScapegoatTree<int>();

        var result = tree.Search(1);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Search_KeyIsPresent_ReturnsKey()
    {
        var tree = new ScapegoatTree<int>(key: 1);

        var result = tree.Search(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Key, Is.EqualTo(1));
    }

    [TestCase(-2)]
    [TestCase(3)]
    public void Search_KeyIsNotPresent_ReturnsNull(int key)
    {
        var root = new Node<int>(1, new Node<int>(2), new Node<int>(-1));

        var tree = new ScapegoatTree<int>(root, 0.5);

        var result = tree.Search(key);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Insert_RootIsNull_InsertsRoot()
    {
        var tree = new ScapegoatTree<int>();

        var inserted = tree.Insert(1);

        Assert.That(inserted, Is.True);
        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Root!.Key, Is.EqualTo(1));
        Assert.That(tree.Size, Is.EqualTo(1));
        Assert.That(tree.MaxSize, Is.EqualTo(1));
    }

    [Test]
    public void Delete_RootIsNull_ReturnsFalse()
    {
        var tree = new ScapegoatTree<int>();

        var deleted = tree.Delete(1);

        Assert.That(deleted, Is.False);
    }

    [Test]
    public void Delete_KeyIsNotPresent_ReturnsFalse()
    {
        var tree = new ScapegoatTree<int>(1);

        var deleted = tree.Delete(2);

        Assert.That(deleted, Is.False);
        Assert.That(tree.Size, Is.EqualTo(1));
    }

    [Test]
    public void Insert_KeyIsPresent_ReturnsFalse()
    {
        var tree = new ScapegoatTree<int>(1);

        var inserted = tree.Insert(1);

        Assert.That(inserted, Is.False);
        Assert.That(tree.Size, Is.EqualTo(1));
        Assert.That(tree.MaxSize, Is.EqualTo(1));
    }

    [Test]
    public void Remove_KeyIsPresent_RemovesKey()
    {
        var tree = new ScapegoatTree<int>(1);

        var inserted = tree.Insert(2);

        Assert.That(inserted, Is.True);

        var deleted = tree.Delete(2);

        Assert.That(deleted, Is.True);
        Assert.That(tree.Size, Is.EqualTo(1));
    }

    [Test]
    public void Remove_KeyIsRootWithNoChildren_RemovesKey()
    {
        var tree = new ScapegoatTree<int>(1);

        var deleted = tree.Delete(1);

        Assert.That(deleted, Is.True);
        Assert.That(tree.Root, Is.Null);
        Assert.That(tree.Size, Is.EqualTo(0));
    }

    [Test]
    public void Remove_KeyIsRootWithOneLeftChild_RemovesKey()
    {
        var tree = new ScapegoatTree<int>(1);

        var inserted = tree.Insert(-1);

        Assert.That(inserted, Is.True);

        var deleted = tree.Delete(1);

        Assert.That(deleted, Is.True);
        Assert.That(tree.Size, Is.EqualTo(1));
    }

    [Test]
    public void Remove_KeyIsRootWithOneRightChild_RemovesKey()
    {
        var tree = new ScapegoatTree<int>(1);

        var inserted = tree.Insert(2);

        Assert.That(inserted, Is.True);

        var deleted = tree.Delete(1);

        Assert.That(deleted, Is.True);
        Assert.That(tree.Size, Is.EqualTo(1));
    }

    [Test]
    public void Remove_KeyIsRootWithTwoChildren_RemovesKey()
    {
        var tree = new ScapegoatTree<int>(1);

        var inserted = tree.Insert(-1);

        Assert.That(inserted, Is.True);

        inserted = tree.Insert(2);

        Assert.That(inserted, Is.True);

        var deleted = tree.Delete(1);

        Assert.That(deleted, Is.True);
        Assert.That(tree.Size, Is.EqualTo(2));
    }

    [Test]
    public void Insert_KeyIsNotPresent_KeyIsInserted()
    {
        var tree = new ScapegoatTree<int>(1);

        var inserted = tree.Insert(2);

        Assert.That(inserted, Is.True);
        Assert.That(tree.Size, Is.EqualTo(2));
        Assert.That(tree.MaxSize, Is.EqualTo(2));
    }

    [TestCase(3, new[]{2,5,1,6}, -1, 0.5)]
    public void Insert_TreeIsUnbalanced_RebuildsTree(int root, int[] keys, int candidate, double alpha)
    {
        var tree = new ScapegoatTree<int>(root, alpha);

        tree.TreeIsUnbalanced += FailTreeIsUnbalanced;

        foreach (var item in keys)
        {
            Assert.DoesNotThrow(() => tree.Insert(item));
        }

        tree.TreeIsUnbalanced -= FailTreeIsUnbalanced;
        tree.TreeIsUnbalanced += PassTreeIsUnbalanced;

        Assert.Throws<SuccessException>(() => tree.Insert(candidate));
    }

    [TestCase(20, new[]{10,30,5,11,29,40,50, 1, 12}, new[]{50,40,30,29}, 0.7)]
    public void Delete_TreeIsUnbalanced_BalancesTree(int root, int[] keys, int[] candidates, double alpha)
    {
        var tree = new ScapegoatTree<int>(root, alpha);

        tree.TreeIsUnbalanced += FailTreeIsUnbalanced;

        foreach (var item in keys)
        {
            Assert.DoesNotThrow(() => tree.Insert(item));
        }

        tree.TreeIsUnbalanced -= FailTreeIsUnbalanced;
        tree.TreeIsUnbalanced += PassTreeIsUnbalanced;

        Assert.Throws<SuccessException>(() =>
        {
            foreach (var item in candidates)
            {
                tree.Delete(item);
            }
        });
    }

    [TestCase(20, new[]{10,30,5,11,29,40,50}, 10, 1)]
    public void Delete_TreeIsUnbalanced_MaxSizeEqualsSize(int root, int[] keys, int candidate, double alpha)
    {
        var tree = new ScapegoatTree<int>(root, alpha);

        tree.TreeIsUnbalanced += FailTreeIsUnbalanced;

        foreach (var item in keys)
        {
            Assert.DoesNotThrow(() => tree.Insert(item));
        }

        tree.TreeIsUnbalanced -= FailTreeIsUnbalanced;

        tree.Delete(candidate);

        Assert.That(tree.MaxSize, Is.EqualTo(tree.Size));
    }

    [TestCase(3, new[]{2,5,1,6}, -1, 0.5)]
    [TestCase(3, new[]{2,5,1,6}, 7, 0.5)]
    public void Insert_TreeIsUnbalanced_BalancesTree(int root, int[] keys, int candidate, double alpha)
    {
        var tree = new ScapegoatTree<int>(root, alpha);

        tree.TreeIsUnbalanced += FailTreeIsUnbalanced;

        foreach (var item in keys)
        {
            Assert.DoesNotThrow(() => tree.Insert(item));
        }

        tree.TreeIsUnbalanced -= FailTreeIsUnbalanced;

        var inserted = tree.Insert(candidate);

        Assert.That(inserted, Is.True);
        Assert.That(tree.Size == 6, Is.True);
        Assert.That(tree.IsAlphaWeightBalanced(), Is.True);
    }

    [TestCase(3, 5, 0.5)]
    public void Insert_TreeIsUnbalanced_BalancesTree2(int root, int candidate, double alpha)
    {
        var tree = new ScapegoatTree<int>(root, alpha);

        var inserted = tree.Insert(candidate);

        Assert.That(inserted, Is.True);
        Assert.That(tree.Size == 2, Is.True);
        Assert.That(tree.IsAlphaWeightBalanced(), Is.True);
    }

    [Test]
    public void Contains_RootIsNull_ReturnsFalse()
    {
        var tree = new ScapegoatTree<int>();

        Assert.That(tree.Contains(1), Is.False);
    }

    [Test]
    public void Contains_RootHasKey_ReturnsTrue()
    {
        var tree = new ScapegoatTree<int>(1);

        Assert.That(tree.Contains(1), Is.True);
    }

    [Test]
    public void Contains_TreeHasKey_ReturnsTrue()
    {
        var tree = new ScapegoatTree<int>(1);

        tree.Insert(2);

        Assert.That(tree.Contains(2), Is.True);
    }

    [Test]
    public void Contains_TreeDoesNotContainKey_ReturnsFalse()
    {
        var tree = new ScapegoatTree<int>(1);

        tree.Insert(2);

        Assert.That(tree.Contains(-1), Is.False);
    }

    [Test]
    public void Clear_TreeHasKeys_ClearsTree()
    {
        var tree = new ScapegoatTree<int>(1);

        tree.Clear();

        Assert.That(tree.Size == 0, Is.True);
        Assert.That(tree.MaxSize == 0, Is.True);
        Assert.That(tree.Root, Is.Null);
    }

    [Test]
    public void Tune_AlphaIsValid_ChangesAlpha()
    {
        var expected = 0.7;

        var tree = new ScapegoatTree<int>();

        tree.Tune(expected);

        Assert.That(tree.Alpha, Is.EqualTo(expected));
    }

    [Test]
    public void Tune_AlphaIsNotValid_ThrowsException()
    {
        var expected = 9.9;

        var tree = new ScapegoatTree<int>();

        Assert.Throws<ArgumentException>(() => tree.Tune(expected));
    }

    [Test]
    public void FindScapegoatInPath_PathIsEmpty_ThrowsAnException()
    {
        var tree = new ScapegoatTree<int>();
        Assert.Throws<ArgumentException>(() => tree.FindScapegoatInPath(new Stack<Node<int>>()));
    }

    [Test]
    public void FindScapegoatInPath_ScapegoatIsNotPresent_ThrowsAnException()
    {
        var tree = new ScapegoatTree<int>(1, 1);
        var path = new Stack<Node<int>>();
        path.Push(tree.Root!);
        Assert.Throws<InvalidOperationException>(() => tree.FindScapegoatInPath(path));
    }

    private static void FailTreeIsUnbalanced(object? sender, EventArgs? e)
    {
        Assert.Fail();
    }

    private static void PassTreeIsUnbalanced(object? sender, EventArgs? e)
    {
        Assert.Pass();
    }
}
