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
        public static void Key_Set()
        {

        }

    }
}