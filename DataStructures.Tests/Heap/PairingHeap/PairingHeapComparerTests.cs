using System.Collections.Generic;
using DataStructures.Heap.PairingHeap;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests.Heap.PairingHeap;

internal class PairingHeapComparerTests
{
    [Test]
    public void Compare_CheckAscending_ReturnNegative()
    {
        var minHeap = new PairingNodeComparer<int>(Sorting.Ascending, Comparer<int>.Default);
        var node1 = new PairingHeapNode<int>(10);
        var node2 = new PairingHeapNode<int>(20);

        var items = minHeap.Compare(node1.Value, node2.Value);

        items.Should().Be(-1);
    }

    [Test]
    public void Compare_CheckAscending_ReturnPositive()
    {
        var minHeap = new PairingNodeComparer<int>(Sorting.Descending, Comparer<int>.Default);
        var node1 = new PairingHeapNode<int>(10);
        var node2 = new PairingHeapNode<int>(20);

        var items = minHeap.Compare(node1.Value, node2.Value);

        items.Should().Be(1);
    }
}
