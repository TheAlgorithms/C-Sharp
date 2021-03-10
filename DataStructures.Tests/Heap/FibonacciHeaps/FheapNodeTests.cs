using System;
using System.Collections.Generic;

using NUnit.Framework;

using DataStructures.Heap;

namespace DataStructures.Tests.Heap
{
    static class FHeapNodeTests
    {
        [Test]
        public static void Constructor_CreatesCyclicList()
        {
            var node = new FHeapNode<int>(1);

            Assert.True(node == node.Left && node == node.Right);
        }

        [Test]
        public static void SetSiblings_SingleNode()
        {
            var node_1 = new FHeapNode<int>(1);
            var node_2 = new FHeapNode<int>(2);
            var node_3 = new FHeapNode<int>(3);

            node_1.SetSiblings(node_3, node_2);
            
        }
    }
}