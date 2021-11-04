using System;
using System.Collections.Generic;
using DataStructures.ScapegoatTree;
using NUnit.Framework;

namespace DataStructures.Tests.ScapegoatTree
{
    public class ExtensionsTests
    {
        [Test]
        public void RebuildFlatTree_ValidFlatTree_RebuildsTree()
        {
            var expected = new Node<int>(3)
            {
                Left = new Node<int>(2)
                {
                    Left = new Node<int>(1)
                    {
                        Left = new Node<int>(-1),
                    },
                },
                Right = new Node<int>(5)
                {
                    Right = new Node<int>(6),
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

            Assert.AreEqual(list.Count, tree.GetSize());
            Assert.AreEqual(expected, tree);

        }

        [Test]
        public void RebuildFromList_RangeIsInvalid_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Extensions.RebuildFromList(new List<Node<int>>(), 1, 0));
        }
    }
}
