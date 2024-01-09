using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.AVLTree;
using FluentAssertions;
using NUnit.Framework;
using static FluentAssertions.FluentActions;

namespace DataStructures.Tests;

internal class AvlTreeTests
{
    private static readonly int[] Data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    private static readonly int[] PreOrder = { 4, 2, 1, 3, 8, 6, 5, 7, 9, 10 };
    private static readonly int[] PostOrder = { 1, 3, 2, 5, 7, 6, 10, 9, 8, 4 };

    [Test]
    public void Constructor_UseCustomComparer_FormsCorrectTree()
    {
        var tree = new AvlTree<int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));

        tree.AddRange(Data);

        tree.GetMin().Should().Be(10);

        tree.GetMax().Should().Be(1);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                Data.Reverse(),
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Add_MultipleKeys_FormsCorrectTree()
    {
        var tree = new AvlTree<int>();

        for (var i = 0; i < Data.Length; ++i)
        {
            tree.Add(Data[i]);
            tree.Count.Should().Be(i + 1);
        }

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                Data,
                config => config.WithStrictOrdering());

        tree.GetKeysPreOrder()
            .Should()
            .BeEquivalentTo(
                PreOrder,
                config => config.WithStrictOrdering());

        tree.GetKeysPostOrder()
            .Should()
            .BeEquivalentTo(
                PostOrder,
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Add_KeyAlreadyInTree_ThrowsException()
    {
        var tree = new AvlTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5 });

        Invoking(() => tree.Add(1)).Should().ThrowExactly<ArgumentException>();
    }

    [Test]
    public void AddRange_MultipleKeys_FormsCorrectTree()
    {
        var tree = new AvlTree<char>();
        tree.AddRange(new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });

        tree.Count.Should().Be(7);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' },
                config => config.WithStrictOrdering());

        tree.GetKeysPreOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 'd', 'b', 'a', 'c', 'f', 'e', 'g' },
                config => config.WithStrictOrdering());

        tree.GetKeysPostOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 'a', 'c', 'b', 'e', 'g', 'f', 'd' },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Remove_MultipleKeys_TreeStillValid()
    {
        var tree = new AvlTree<int>();
        tree.AddRange(Data);

        tree.Remove(7);

        tree.Count.Should().Be(9);
        tree.Contains(7).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 4, 5, 6, 8, 9, 10 },
                config => config.WithStrictOrdering());

        tree.GetKeysPreOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 4, 2, 1, 3, 8, 6, 5, 9, 10 },
                config => config.WithStrictOrdering());

        tree.GetKeysPostOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 3, 2, 5, 6, 10, 9, 8, 4 },
                config => config.WithStrictOrdering());

        tree.Remove(2);

        tree.Count.Should().Be(8);
        tree.Contains(2).Should().BeFalse();

        tree.Remove(1);

        tree.Count.Should().Be(7);
        tree.Contains(1).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 3, 4, 5, 6, 8, 9, 10 },
                config => config.WithStrictOrdering());

        tree.GetKeysPreOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 8, 4, 3, 6, 5, 9, 10 },
                config => config.WithStrictOrdering());

        tree.GetKeysPostOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 3, 5, 6, 4, 10, 9, 8 },
                config => config.WithStrictOrdering());

        tree.Remove(9);

        tree.Count.Should().Be(6);
        tree.Contains(9).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 3, 4, 5, 6, 8, 10 },
                config => config.WithStrictOrdering());

        tree.GetKeysPreOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 6, 4, 3, 5, 8, 10 },
                config => config.WithStrictOrdering());

        tree.GetKeysPostOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 3, 5, 4, 10, 8, 6 },
                config => config.WithStrictOrdering());

        tree.Remove(3);
        tree.Remove(4);
        tree.Remove(5);
        tree.Remove(6);
        tree.Remove(8);
        tree.Remove(10);

        tree.Count.Should().Be(0);
        tree.GetKeysInOrder().Should().BeEmpty();
    }

    [Test]
    public void Remove_MultipleKeys_TreeStillValid_Variant2()
    {
        var tree = new AvlTree<int>();
        tree.AddRange(Data);

        tree.Remove(10);

        tree.Count.Should().Be(9);
        tree.Contains(10).Should().BeFalse();

        tree.Remove(5);

        tree.Count.Should().Be(8);
        tree.Contains(5).Should().BeFalse();

        tree.Remove(7);

        tree.Count.Should().Be(7);
        tree.Contains(7).Should().BeFalse();

        tree.Remove(9);

        tree.Count.Should().Be(6);
        tree.Contains(9).Should().BeFalse();

        tree.Remove(1);

        tree.Count.Should().Be(5);
        tree.Contains(1).Should().BeFalse();

        tree.Remove(3);

        tree.Count.Should().Be(4);
        tree.Contains(3).Should().BeFalse();

        tree.Remove(2);

        tree.Count.Should().Be(3);
        tree.Contains(2).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 4,6,8 },
                config => config.WithStrictOrdering());

        tree.GetKeysPreOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 6,4,8 },
                config => config.WithStrictOrdering());

        tree.GetKeysPostOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 4,8,6 },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Remove_EmptyTree_ThrowsException()
    {
        var tree = new AvlTree<int>();

        Invoking(() => tree.Remove(1)).Should().ThrowExactly<KeyNotFoundException>();
    }

    [Test]
    public void Remove_KeyNotInTree_ThrowsException()
    {
        var tree = new AvlTree<int>();
        tree.AddRange(Data);

        Invoking(() => tree.Remove(24)).Should().ThrowExactly<KeyNotFoundException>();
    }

    [Test]
    public void Contains_CorrectReturn()
    {
        var tree = new AvlTree<int>();

        tree.AddRange(Data);

        tree.Contains(3).Should().BeTrue();
        tree.Contains(7).Should().BeTrue();
        tree.Contains(24).Should().BeFalse();
        tree.Contains(-1).Should().BeFalse();
    }

    [Test]
    public void Contains_EmptyTree_ReturnsFalse()
    {
        var tree = new AvlTree<int>();

        tree.Contains(5).Should().BeFalse();
        tree.Contains(-12).Should().BeFalse();
    }

    [Test]
    public void GetMin_CorrectReturn()
    {
        var tree = new AvlTree<int>();
        tree.AddRange(Data);

        tree.GetMin().Should().Be(1);
    }

    [Test]
    public void GetMin_EmptyTree_ThrowsException()
    {
        var tree = new AvlTree<int>();

        Invoking(() => tree.GetMin()).Should().ThrowExactly<InvalidOperationException>();
    }

    [Test]
    public void GetMax_CorrectReturn()
    {
        var tree = new AvlTree<int>();
        tree.AddRange(Data);

        tree.GetMax().Should().Be(10);
    }

    [Test]
    public void GetMax_EmptyTree_ThrowsException()
    {
        var tree = new AvlTree<int>();

        Invoking(() => tree.GetMax()).Should().ThrowExactly<InvalidOperationException>();
    }

    [Test]
    public void GetKeysInOrder_CorrectReturn()
    {
        var tree = new AvlTree<int>();
        tree.AddRange(Data);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                Data,
                config => config.WithStrictOrdering());
    }

    [Test]
    public void GetKeysInOrder_EmptyTree_CorrectReturn()
    {
        var tree = new AvlTree<int>();

        tree.GetKeysInOrder().Should().BeEmpty();
    }

    [Test]
    public void GetKeysPreOrder_CorrectReturn()
    {
        var tree = new AvlTree<int>();

        tree.AddRange(Data);

        tree.GetKeysPreOrder()
            .Should()
            .BeEquivalentTo(
                PreOrder,
                config => config.WithStrictOrdering());
    }

    [Test]
    public void GetKeysPreOrder_EmptyTree_CorrectReturn()
    {
        var tree = new AvlTree<int>();

        tree.GetKeysPreOrder().Should().BeEmpty();
    }

    [Test]
    public void GetKeysPostOrder_CorrectReturn()
    {
        var tree = new AvlTree<int>();
        tree.AddRange(Data);

        tree.GetKeysPostOrder()
            .Should()
            .BeEquivalentTo(
                PostOrder,
                config => config.WithStrictOrdering());
    }

    [Test]
    public void GetKeysPostOrder_EmptyTree_CorrectReturn()
    {
        var tree = new AvlTree<int>();

        tree.GetKeysPostOrder().Should().BeEmpty();
    }
}
