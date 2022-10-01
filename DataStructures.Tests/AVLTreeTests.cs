using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.AVLTree;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests
{
    internal class AvlTreeTests
    {
        [Test]
        public void Constructor_UseCustomComparer_FormsCorrectTree()
        {
            var tree = new AvlTree<int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
            tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            tree.GetMin().Should().Be(10);
            tree.GetMax().Should().Be(1);
            tree.GetKeysInOrder().SequenceEqual(new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }).Should().BeTrue();
        }

        [Test]
        public void Add_MultipleKeys_FormsCorrectTree()
        {
            var tree = new AvlTree<int>();

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
            var tree = new AvlTree<int>();
            tree.AddRange(new[] { 1, 2, 3, 4, 5 });
            Assert.Throws<ArgumentException>(() => tree.Add(1));
        }

        [Test]
        public void AddRange_MultipleKeys_FormsCorrectTree()
        {
            var tree = new AvlTree<char>();
            tree.AddRange(new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
            tree.Count.Should().Be(7);
            tree.GetKeysInOrder().SequenceEqual(new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' }).Should().BeTrue();
            tree.GetKeysPreOrder().SequenceEqual(new[] { 'd', 'b', 'a', 'c', 'f', 'e', 'g' }).Should().BeTrue();
        }

        [Test]
        public void Remove_MultipleKeys_TreeStillValid()
        {
            var tree = new AvlTree<int>();
            tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            tree.Remove(7);
            tree.Count.Should().Be(9);
            tree.Contains(7).Should().BeFalse();
            tree.GetKeysInOrder().SequenceEqual(new[] { 1, 2, 3, 4, 5, 6, 8, 9, 10 }).Should().BeTrue();
            tree.GetKeysPreOrder().SequenceEqual(new[] { 4, 2, 1, 3, 8, 6, 5, 9, 10 }).Should().BeTrue();

            tree.Remove(2);
            tree.Count.Should().Be(8);
            tree.Contains(2).Should().BeFalse();

            tree.Remove(1);
            tree.Count.Should().Be(7);
            tree.Contains(1).Should().BeFalse();
            tree.GetKeysInOrder().SequenceEqual(new[] { 3, 4, 5, 6, 8, 9, 10 }).Should().BeTrue();
            tree.GetKeysPreOrder().SequenceEqual(new[] { 8, 4, 3, 6, 5, 9, 10 }).Should().BeTrue();

            tree.Remove(9);
            tree.Count.Should().Be(6);
            tree.Contains(9).Should().BeFalse();
            tree.GetKeysInOrder().SequenceEqual(new[] { 3, 4, 5, 6, 8, 10 }).Should().BeTrue();
            tree.GetKeysPreOrder().SequenceEqual(new[] { 6, 4, 3, 5, 8, 10 }).Should().BeTrue();

            tree.Remove(3);
            tree.Remove(4);
            tree.Remove(5);
            tree.Remove(6);
            tree.Remove(8);
            tree.Remove(10);

            tree.Count.Should().Be(0);
            tree.GetKeysInOrder().SequenceEqual(Array.Empty<int>()).Should().BeTrue();
        }

        [Test]
        public void Remove_EmptyTree_ThrowsException()
        {
            var tree = new AvlTree<int>();
            Assert.Throws<InvalidOperationException>(() => tree.Remove(1));
        }

        [Test]
        public void Remove_KeyNotInTree_ThrowsException()
        {
            var tree = new AvlTree<int>();
            tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            Assert.Throws<KeyNotFoundException>(() => tree.Remove(24));
        }

        [Test]
        public void Contains_CorrectReturn()
        {
            var tree = new AvlTree<int>();
            tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
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
            tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            tree.GetMin().Should().Be(1);
        }

        [Test]
        public void GetMin_EmptyTree_ThrowsException()
        {
            var tree = new AvlTree<int>();
            Assert.Throws<InvalidOperationException>(() => tree.GetMin());
        }

        [Test]
        public void GetMax_CorrectReturn()
        {
            var tree = new AvlTree<int>();
            tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            tree.GetMax().Should().Be(10);
        }

        [Test]
        public void GetMax_EmptyTree_ThrowsException()
        {
            var tree = new AvlTree<int>();
            Assert.Throws<InvalidOperationException>(() => tree.GetMax());
        }

        [Test]
        public void GetKeysInOrder_CorrectReturn()
        {
            var tree = new AvlTree<int>();
            tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            tree.GetKeysInOrder().SequenceEqual(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }).Should().BeTrue();
        }

        [Test]
        public void GetKeysInOrder_EmptyTree_CorrectReturn()
        {
            var tree = new AvlTree<int>();
            tree.GetKeysInOrder().SequenceEqual(Array.Empty<int>()).Should().BeTrue();
        }

        [Test]
        public void GetKeysPreOrder_CorrectReturn()
        {
            var tree = new AvlTree<int>();
            tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            tree.GetKeysPreOrder().SequenceEqual(new[] { 4, 2, 1, 3, 8, 6, 5, 7, 9, 10 }).Should().BeTrue();
        }

        [Test]
        public void GetKeysPreOrder_EmptyTree_CorrectReturn()
        {
            var tree = new AvlTree<int>();
            tree.GetKeysPreOrder().SequenceEqual(Array.Empty<int>()).Should().BeTrue();
        }

        [Test]
        public void GetKeysPostOrder_CorrectReturn()
        {
            var tree = new AvlTree<int>();
            tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            tree.GetKeysPostOrder().SequenceEqual(new[] { 1, 3, 2, 5, 7, 6, 10, 9, 8, 4 }).Should().BeTrue();
        }

        [Test]
        public void GetKeysPostOrder_EmptyTree_CorrectReturn()
        {
            var tree = new AvlTree<int>();
            tree.GetKeysPostOrder().SequenceEqual(Array.Empty<int>()).Should().BeTrue();
        }
    }
}
