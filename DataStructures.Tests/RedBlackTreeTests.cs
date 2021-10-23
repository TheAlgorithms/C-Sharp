using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.RedBlackTree;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests
{
    internal class RedBlackTreeTests
    {
        [Test]
        public void Constructor_UseCustomComparer_FormsCorrect_Tree()
        {
            var tree = new RedBlackTree<int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
            tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            tree.GetMin().Should().Be(10);
            tree.GetMax().Should().Be(1);
            tree.GetKeysInOrder().SequenceEqual(new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }).Should().BeTrue();
        }

        [Test]
        public void Add_Case3_FormsCorrectTree()
        {
            var tree = new RedBlackTree<int>();
            tree.Add(5);
            tree.Count.Should().Be(1);
        }

        [Test]
        public void Add_Case24_FormsCorrectTree()
        {
            var tree = new RedBlackTree<int>();
            tree.AddRange(new[] { 5, 4, 6, 3 });
            tree.GetKeysPreOrder().SequenceEqual(new[] { 5, 4, 3, 6 }).Should().BeTrue();
        }

        [Test]
        public void Add_Case1_FormsCorrectTree()
        {
            var tree = new RedBlackTree<int>();
            tree.AddRange(new[] { 5, 4, 6, 3, 7 });
            tree.GetKeysPreOrder().SequenceEqual(new[] { 5, 4, 3, 6, 7 }).Should().BeTrue();
        }

        [Test]
        public void Add_Case6_FormsCorrectTree()
        {
            // Right rotation
            var tree = new RedBlackTree<int>();
            tree.AddRange(new[] { 5, 4, 6, 3, 2 });
            tree.GetKeysPreOrder().SequenceEqual(new[] { 5, 3, 2, 4, 6 }).Should().BeTrue();

            // Left rotation
            tree = new RedBlackTree<int>();
            tree.AddRange(new[] { 5, 4, 6, 7, 8 });
            tree.GetKeysPreOrder().SequenceEqual(new[] { 5, 4, 7, 6, 8 }).Should().BeTrue();
        }

        [Test]
        public void Add_Case5_FormsCorrectTree()
        {
            // Left-right rotation
            var tree = new RedBlackTree<int>();
            tree.AddRange(new[] { 5, 4, 6, 2, 3 });
            tree.GetKeysPreOrder().SequenceEqual(new[] { 5, 3, 2, 4, 6 }).Should().BeTrue();

            // Right-left rotation
            tree = new RedBlackTree<int>();
            tree.AddRange(new[] { 5, 4, 6, 8, 7 });
            tree.GetKeysPreOrder().SequenceEqual(new[] { 5, 4, 7, 6, 8 }).Should().BeTrue();
        }

        [Test]
        public void Add_MultipleKeys_FormsCorrectTree()
        {
            var tree = new RedBlackTree<int>();

            foreach (var value in new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })
            {
                tree.Add(value);
                tree.Count.Should().Be(value);
            }

            tree.GetKeysInOrder().SequenceEqual(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }).Should().BeTrue();
            tree.GetKeysPreOrder().SequenceEqual(new[] { 4, 2, 1, 3, 6, 5, 8, 7, 9, 10 }).Should().BeTrue();
        }

        [Test]
        public void Add_KeyAlreadyInTree_ThrowsException()
        {
            var tree = new RedBlackTree<int>();
            tree.AddRange(new[] { 1, 2, 3, 4, 5 });
            Assert.Throws<ArgumentException>(() => tree.Add(1));
        }

        [Test]
        public void AddRange_MultipleKeys_FormsCorrectTree()
        {
            var tree = new RedBlackTree<char>();
            tree.AddRange(new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
            tree.Count.Should().Be(7);
            tree.GetKeysInOrder().SequenceEqual(new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' }).Should().BeTrue();
            tree.GetKeysPreOrder().SequenceEqual(new[] { 'b', 'a', 'd', 'c', 'f', 'e', 'g' }).Should().BeTrue();
        }

        [Test]
        public void Remove_MultipleKeys_TreeStillValid()
        {
            var tree = new RedBlackTree<int>();
            tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            tree.Remove(7);
            tree.Count.Should().Be(9);
            tree.Contains(7).Should().BeFalse();
            tree.GetKeysInOrder().SequenceEqual(new[] { 1, 2, 3, 4, 5, 6, 8, 9, 10 }).Should().BeTrue();
            //tree.GetKeysPreOrder().SequenceEqual(new[] { 4, 2, 1, 3, 8, 6, 5, 9, 10 }).Should().BeTrue();

            tree.Remove(2);
            tree.Count.Should().Be(8);
            tree.Contains(2).Should().BeFalse();

            tree.Remove(1);
            tree.Count.Should().Be(7);
            tree.Contains(1).Should().BeFalse();
            tree.GetKeysInOrder().SequenceEqual(new[] { 3, 4, 5, 6, 8, 9, 10 }).Should().BeTrue();
            //tree.GetKeysPreOrder().SequenceEqual(new[] { 8, 4, 3, 6, 5, 9, 10 }).Should().BeTrue();

            tree.Remove(9);
            tree.Count.Should().Be(6);
            tree.Contains(9).Should().BeFalse();
            tree.GetKeysInOrder().SequenceEqual(new[] { 3, 4, 5, 6, 8, 10 }).Should().BeTrue();
            //tree.GetKeysPreOrder().SequenceEqual(new[] { 6, 4, 3, 5, 8, 10 }).Should().BeTrue();

            tree.Remove(3);
            tree.Remove(4);
            tree.Remove(5);
            tree.Remove(6);
            tree.Remove(8);
            tree.Remove(10);

            tree.Count.Should().Be(0);
            tree.GetKeysInOrder().SequenceEqual(Array.Empty<int>()).Should().BeTrue();
        }
    }
}
