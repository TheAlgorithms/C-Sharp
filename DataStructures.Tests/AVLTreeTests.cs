using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.AVLTree;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests
{
    internal class AVLTreeTests
    {
        [Test]
        public void Constructor_UseCustomComparer_FormsCorrectTree()
        {
            var tree = new AVLTree<int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
            tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            tree.GetMin().Should().Be(10);
            tree.GetMax().Should().Be(1);
            tree.GetKeysInOrder().SequenceEqual(new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }).Should().BeTrue();
        }

        [Test]
        public void Add_MultipleKeys_FormsCorrectTree()
        {
            var tree = new AVLTree<int>();

            foreach(var value in new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10})
            {
                tree.Add(value);
                tree.Count.Should().Be(value);
            }

            tree.GetKeysInOrder().SequenceEqual(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }).Should().BeTrue();
            tree.GetKeysPreOrder().SequenceEqual(new[] { 4, 2, 1, 3, 8, 6, 5, 7, 9, 10 }).Should().BeTrue();
        }

        [Test]
        public void Add_KeyAlreadyInTree_ThrowsException()
        {
            var tree = new AVLTree<int>();
            tree.AddRange(new[] { 1, 2, 3, 4, 5 });
            Assert.Throws<ArgumentException>(() => tree.Add(1));
        }

        [Test]
        public void AddRange_MultipleKeys_FormsCorrectTree()
        {
            var tree = new AVLTree<char>();
            tree.AddRange(new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
            tree.Count.Should().Be(7);
            tree.GetKeysInOrder().SequenceEqual(new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
            tree.GetKeysPreOrder().SequenceEqual(new[] { 'd', 'b', 'a', 'c', 'f', 'e', 'g' });
        }
    }
}
