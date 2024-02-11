using System;
using System.Collections.Generic;
using DataStructures.Heap;
using NUnit.Framework;

namespace DataStructures.Tests.Heap;

internal static class BinaryHeapTests
{
    private static BinaryHeap<int> BuildTestHeap()
    {
        var heap = new BinaryHeap<int>();
        var elems = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        foreach (var i in elems)
        {
            heap.Push(i);
        }

        return heap;
    }

    [Test]
    public static void Constructor_UseCustomComparer_BuildCorrectHeap()
    {
        var revHeap = new BinaryHeap<int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
        foreach (var i in new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })
        {
            revHeap.Push(i);
        }

        Assert.That(revHeap.Count, Is.EqualTo(10));
        Assert.That(revHeap.Peek(), Is.EqualTo(1));
        Assert.That(revHeap.Pop(), Is.EqualTo(1));
        Assert.That(revHeap.Peek(), Is.EqualTo(2));
    }

    [Test]
    public static void Push_AddElements_BuildCorrectHeap()
    {
        var heap = BuildTestHeap();

        Assert.That(heap.Peek(), Is.EqualTo(10));
        Assert.That(heap.Count, Is.EqualTo(10));
    }

    public static void Pop_RemoveElements_HeapStillValid()
    {
        var heap = BuildTestHeap();

        Assert.That(heap.Peek(), Is.EqualTo(10));
        Assert.That(heap.Count, Is.EqualTo(10));

        Assert.That(heap.Pop(), Is.EqualTo(10));
        Assert.That(heap.Count, Is.EqualTo(9));
        Assert.That(heap.Contains(10),Is.False);

        Assert.That(heap.Pop(), Is.EqualTo(9));
        Assert.That(heap.Count, Is.EqualTo(8));
        Assert.That(heap.Contains(9), Is.False);
    }

    [Test]
    public static void Pop_EmptyHeap_ThrowsCorrectException()
    {
        var heap = new BinaryHeap<int>();

        Assert.Throws<InvalidOperationException>(() => heap.Pop());
    }

    [Test]
    public static void Peek_NonEmptyHeap_ReturnsCorrectAnswer()
    {
        var heap = BuildTestHeap();

        Assert.That(heap.Peek(), Is.EqualTo(10));
    }

    [Test]
    public static void Peek_EmptyHeap_ThrowsCorrectException()
    {
        var heap = new BinaryHeap<int>();

        Assert.Throws<InvalidOperationException>(() => heap.Peek());
    }

    [Test]
    public static void PushPop_EmptyHeap_ReturnsCorrectAnswer()
    {
        var heap = new BinaryHeap<int>();

        Assert.That(heap.PushPop(10), Is.EqualTo(10));
    }

    [Test]
    public static void PushPop_NonEmptyHeap_ReturnsCorrectAnswer()
    {
        var heap = BuildTestHeap();

        Assert.That(heap.PushPop(20), Is.EqualTo(20));
        Assert.That(heap.PushPop(-10), Is.EqualTo(10));
    }

    [Test]
    public static void Contains_NonEmptyHeap_ReturnsCorrectAnswer()
    {
        var heap = BuildTestHeap();

        Assert.That(heap.Contains(1), Is.True);
        Assert.That(heap.Contains(5), Is.True);
        Assert.That(heap.Contains(10), Is.True);
        Assert.That(heap.Contains(11), Is.False);
    }

    [Test]
    public static void Contains_EmptyHeap_ReturnsCorrectAnswer()
    {
        var heap = new BinaryHeap<int>();

        Assert.That(heap.Contains(1), Is.False);
        Assert.That(heap.Contains(5), Is.False);
        Assert.That(heap.Contains(10), Is.False);
        Assert.That(heap.Contains(11), Is.False);
    }

    [Test]
    public static void Remove_NonEmptyHeap_HeapStillValid()
    {
        var heap = BuildTestHeap();

        heap.Remove(2);
        Assert.That(heap.Contains(2), Is.False);
        Assert.That(heap.Peek(), Is.EqualTo(10));
        Assert.That(heap.Count, Is.EqualTo(9));

        heap.Remove(8);
        Assert.That(heap.Contains(8), Is.False);
        Assert.That(heap.Peek(), Is.EqualTo(10));
        Assert.That(heap.Count, Is.EqualTo(8));

        heap.Remove(5);
        Assert.That(heap.Contains(5), Is.False);
        Assert.That(heap.Peek(), Is.EqualTo(10));
        Assert.That(heap.Count, Is.EqualTo(7));

        Assert.Throws<ArgumentException>(() => heap.Remove(11));
    }

    [Test]
    public static void Remove_EmptyHeap_ThrowsCorrectException()
    {
        var heap = new BinaryHeap<int>();

        Assert.Throws<ArgumentException>(() => heap.Remove(1));
    }
}
