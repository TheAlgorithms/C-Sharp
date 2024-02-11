using System;
using System.Collections;
using System.Linq;
using DataStructures.Heap.PairingHeap;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests.Heap.PairingHeap;

internal class PairingHeapTests
{
    [Test]
    public void BuildMinHeap_CheckEnumerator_NotThrowOnEnumerate()
    {
        var minHeap = new PairingHeap<int>();
        minHeap.Insert(1);

        var items = minHeap.ToList();

        items.Should().HaveCount(1);
    }

    [Test]
    public void BuildMinHeap_CheckEnumerable_NotThrowOnEnumerate()
    {
        var minHeap = new PairingHeap<int>();
        minHeap.Insert(1);

        foreach (var node in (IEnumerable)minHeap)
        {
            node.Should().NotBe(null);
        }
    }

    [Test]
    public void BuildMinHeap_UpdateNonExistingNode_ThrowException()
    {
        var minHeap = new PairingHeap<int>();
        minHeap.Insert(1);
        minHeap.Extract();

        Action act = () => minHeap.UpdateKey(1, 10);

        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void BuildMinHeap_UpdateBadNode_ThrowException()
    {
        var minHeap = new PairingHeap<int>();
        minHeap.Insert(10);

        Action act = () => minHeap.UpdateKey(10, 11);

        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void BuildMinHeap_CreateHeap_HeapIsCheked()
    {
        var nodeCount = 1000 * 10;
        var minHeap = new PairingHeap<int>();
        for (var i = 0; i <= nodeCount; i++)
        {
            minHeap.Insert(i);
        }

        for (var i = 0; i <= nodeCount; i++)
        {
            minHeap.UpdateKey(i, i - 1);
        }

        var min = 0;
        for (var i = 0; i <= nodeCount; i++)
        {
            min = minHeap.Extract();
            Assert.That(min, Is.EqualTo(i - 1));
        }

        Assert.That(minHeap.Count, Is.EqualTo(minHeap.Count));

        var rnd = new Random();
        var testSeries = Enumerable.Range(0, nodeCount - 1).OrderBy(_ => rnd.Next()).ToList();

        foreach (var item in testSeries)
        {
            minHeap.Insert(item);
        }

        for (var i = 0; i < testSeries.Count; i++)
        {
            var decremented = testSeries[i] - rnd.Next(0, 1000);
            minHeap.UpdateKey(testSeries[i], decremented);
            testSeries[i] = decremented;
        }

        testSeries.Sort();

        for (var i = 0; i < nodeCount - 2; i++)
        {
            min = minHeap.Extract();
            Assert.That(testSeries[i], Is.EqualTo(min));
        }

        Assert.That(minHeap.Count, Is.EqualTo(minHeap.Count));
    }

    [Test]
    public void BuildMaxHeap_CreateHeap_HeapIsCheked()
    {
        var nodeCount = 1000 * 10;
        var maxHeap = new PairingHeap<int>(Sorting.Descending);
        for (var i = 0; i <= nodeCount; i++)
        {
            maxHeap.Insert(i);
        }

        for (var i = 0; i <= nodeCount; i++)
        {
            maxHeap.UpdateKey(i, i + 1);
        }

        Assert.That(maxHeap.Count, Is.EqualTo(maxHeap.Count));

        var max = 0;
        for (var i = nodeCount; i >= 0; i--)
        {
            max = maxHeap.Extract();
            Assert.That(max, Is.EqualTo(i + 1));
        }

        var rnd = new Random();
        var testSeries = Enumerable.Range(0, nodeCount - 1).OrderBy(_ => rnd.Next()).ToList();

        foreach (var item in testSeries)
        {
            maxHeap.Insert(item);
        }

        for (var i = 0; i < testSeries.Count; i++)
        {
            var incremented = testSeries[i] + rnd.Next(0, 1000);
            maxHeap.UpdateKey(testSeries[i], incremented);
            testSeries[i] = incremented;
        }

        testSeries = testSeries.OrderByDescending(x => x).ToList();
        for (var i = 0; i < nodeCount - 2; i++)
        {
            max = maxHeap.Extract();
            Assert.That(testSeries[i], Is.EqualTo(max));
        }

        Assert.That(maxHeap.Count, Is.EqualTo(maxHeap.Count));
    }
}
