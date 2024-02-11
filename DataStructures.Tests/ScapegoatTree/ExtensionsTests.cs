using System;
using System.Collections.Generic;
using DataStructures.ScapegoatTree;
using NUnit.Framework;

namespace DataStructures.Tests.ScapegoatTree;

public class ExtensionsTests
{
    [Test]
    public void RebuildFlatTree_ValidFlatTree_RebuildsTree()
    {
        var expected = new Node<int>(3)
        {
            Left = new Node<int>(1)
            {
                Left = new Node<int>(-1),
                Right = new Node<int>(2),
            },
            Right = new Node<int>(6)
            {
                Left = new Node<int>(5),
            },
        };


        var list = new List<Node<int>>
        {
            new(-1),
            new(1),
            new(2),
            new(3),
            new(5),
            new(6),
        };

        var tree = Extensions.RebuildFromList(list, 0, list.Count - 1);

        Assert.That(list.Count, Is.EqualTo(tree.GetSize()));
        Assert.That(expected.Key, Is.EqualTo(tree.Key));
        Assert.That(tree.Left, Is.Not.Null);
        Assert.That(tree.Right, Is.Not.Null);
        Assert.That(expected.Left.Key, Is.EqualTo(tree.Left!.Key));
        Assert.That(expected.Right.Key, Is.EqualTo(tree.Right!.Key));
        Assert.That(tree.Left.Left, Is.Not.Null);
        Assert.That(tree.Left.Right, Is.Not.Null);
        Assert.That(expected.Left.Left.Key, Is.EqualTo(tree.Left!.Left!.Key));
        Assert.That(expected.Left.Right.Key, Is.EqualTo(tree.Left!.Right!.Key));
        Assert.That(tree.Right.Left, Is.Not.Null);
        Assert.That(expected.Right.Left.Key, Is.EqualTo(tree.Right!.Left!.Key));
    }

    [Test]
    public void RebuildFromList_RangeIsInvalid_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => Extensions.RebuildFromList(new List<Node<int>>(), 1, 0));
    }
}
