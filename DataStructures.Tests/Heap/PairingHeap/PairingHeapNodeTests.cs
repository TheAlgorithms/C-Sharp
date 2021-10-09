using System.Collections.Generic;
using DataStructures.Heap.PairingHeap;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests.Heap.PairingHeap
{
    internal class PairingHeapNodeTests
    {
        [Test]
        public void CompareTo_CheckAscending_ReturnNegative()
        {
            var node = new PairingHeapNode<int>(0);
            var node2 = new PairingHeapNode<int>(1);

            var value = node.CompareTo(node2);

            value.Should().Be(-1);
        }

        [Test]
        public void CompareTo_CheckAscending_ReturnPositive()
        {
            var node = new PairingHeapNode<int>(1);
            var node2 = new PairingHeapNode<int>(0);

            var value = node.CompareTo(node2);

            value.Should().Be(1);
        }
    }
}
