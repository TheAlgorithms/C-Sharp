using System;
using DataStructures.Heap.FibonacciHeap;
using NUnit.Framework;

namespace DataStructures.Tests.Heap.FibonacciHeaps;

internal class TestFHeap : FibonacciHeap<int>
{
    public void RawCut(FHeapNode<int> x, FHeapNode<int> y)
    {
        Cut(x, y);
    }

    public void RawCascadingCut(FHeapNode<int> y)
    {
        CascadingCut(y);
    }

    public void RawConsolidate()
    {
        Consolidate();
    }
}

internal static class FibonacciHeapTests
{
    private static FibonacciHeap<int> BuildTestHeap()
    {
        var heap = new FibonacciHeap<int>();
        var elems = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        foreach (var i in elems)
        {
            heap.Push(i);
        }

        return heap;
    }

    [Test]
    public static void Push_AddElements_BuildCorrectHeap()
    {
        var heap = BuildTestHeap();

        Assert.That(heap.Peek(),Is.EqualTo(1));
        Assert.That(heap.Count, Is.EqualTo(10));
    }

    public static void Pop_RemoveElements_HeapStillValid()
    {
        var heap = BuildTestHeap();

        Assert.That(heap.Peek(), Is.EqualTo(1));
        Assert.That(heap.Count, Is.EqualTo(10));

        Assert.That(heap.Pop(), Is.EqualTo(1));
        Assert.That(heap.Count, Is.EqualTo(9));

        Assert.That(heap.Pop(), Is.EqualTo(2));
        Assert.That(heap.Count, Is.EqualTo(8));
    }

    [Test]
    public static void Pop_EmptyHeap_ThrowsCorrectException()
    {
        var heap = new FibonacciHeap<int>();

        Assert.Throws<InvalidOperationException>(() => heap.Pop());
    }

    [Test]
    public static void Pop_NonEmptyHeap_ReturnsInSortedOrder()
    {
        var heap = new FibonacciHeap<int>();

        var rand = new Random();
        var heapSize = 100;

        for (var i = 0; i < heapSize; i++)
        {
            heap.Push(rand.Next(1000));
        }

        var element = heap.Pop();

        for (var i = 0; i < heapSize - 1; i++)
        {
            var newElement = heap.Pop();
            Assert.That(element, Is.LessThanOrEqualTo(newElement));
            element = newElement;
        }

        Assert.That(heap.Count, Is.Zero);
    }

    [Test]
    public static void Peek_EmptyHeap_ThrowsCorrectException()
    {
        var heap = new FibonacciHeap<int>();

        Assert.Throws<InvalidOperationException>(() => heap.Peek());
    }

    [Test]
    public static void DecreaseKey_NonEmptyHeap_ReturnsCorrectAnswer()
    {
        var heap = BuildTestHeap();

        var node = heap.Push(11);
        heap.DecreaseKey(node, -1);

        Assert.That(heap.Pop(), Is.EqualTo(-1));
        Assert.That(heap.Pop(), Is.EqualTo(1));

        node = heap.Push(5);
        heap.DecreaseKey(node, 1);
        Assert.That(heap.Pop(), Is.EqualTo(1));

        Assert.That(heap.Pop(), Is.EqualTo(2));
        Assert.That(heap.Pop(), Is.EqualTo(3));
    }

    [Test]
    public static void Union_NonEmptyHeap_ReturnsSortedOrder()
    {
        var oddHeap = new FibonacciHeap<int>();

        for (var i = 1; i < 10; i += 2)
        {
            oddHeap.Push(i);
        }

        var evenHeap = new FibonacciHeap<int>();

        for (var i = 0; i < 10; i += 2)
        {
            evenHeap.Push(i);
        }

        oddHeap.Union(evenHeap);

        for (var i = 0; i < 10; i++)
        {
            Assert.That(oddHeap.Pop(), Is.EqualTo(i));
        }

        Assert.That(oddHeap.Count, Is.Zero);
        Assert.That(evenHeap.Count, Is.Zero);
    }

    [Test]
    public static void Union_EmptyHeap_BecomesOtherHeap()
    {
        var thisHeap = new FibonacciHeap<int>();
        var otherHeap = BuildTestHeap();

        var minNode = otherHeap.Peek();
        var otherCount = otherHeap.Count;

        Assert.That(thisHeap.Count, Is.Zero);

        thisHeap.Union(otherHeap);

        Assert.That(otherHeap.Count, Is.Zero);
        Assert.That(minNode, Is.EqualTo(thisHeap.Peek()));
        Assert.Throws<InvalidOperationException>(() => otherHeap.Peek());

        Assert.That(thisHeap.Count, Is.EqualTo(otherCount));
    }

    [Test]
    public static void Union_FullHeapWithEmptyHeap_Unchanged()
    {
        var thisHeap = BuildTestHeap();
        var otherHeap = new FibonacciHeap<int>();

        var previousCount = thisHeap.Count;
        var previousMin = thisHeap.Peek();

        thisHeap.Union(otherHeap);

        Assert.That(previousCount, Is.EqualTo(thisHeap.Count));
        Assert.That(previousMin, Is.EqualTo(thisHeap.Peek()));
    }

    [Test]
    public static void DecreaseKey_EmptyHeap_ThrowsCorrectException()
    {
        var heap = new FibonacciHeap<int>();
        var item = new FHeapNode<int>(1);

        Assert.Throws<ArgumentException>(() => heap.DecreaseKey(item, 0));
    }

    [Test]
    public static void DecreaseKey_TryIncreaseKey_ThrowsCorrectException()
    {
        var heap = new FibonacciHeap<int>();
        var item = heap.Push(1);

        Assert.Throws<InvalidOperationException>(() => heap.DecreaseKey(item, 2));
    }

    [Test]
    public static void DecreaseKey_NonEmptyHeap_PreservesHeapStructure()
    {
        var heap = new FibonacciHeap<int>();

        for (var i = 11; i < 20; i++)
        {
            heap.Push(i);
        }

        var item = heap.Push(10);

        for (var i = 0; i < 10; i++)
        {
            heap.Push(i);
        }

        var bigItem = heap.Push(20);

        heap.DecreaseKey(item, -1);
        Assert.That(-1, Is.EqualTo(heap.Pop()));

        var currentVal = -1;
        for (var i = 0; i < 10; i++)
        {
            var newVal = heap.Pop();
            Assert.That(currentVal < newVal, Is.True);

            currentVal = newVal;
        }

        heap.DecreaseKey(bigItem, -1);
        Assert.That(-1, Is.EqualTo(heap.Pop()));

        currentVal = -1;
        for (var i = 0; i < 9; i++)
        {
            var newVal = heap.Pop();
            Assert.That(currentVal < newVal, Is.True);

            currentVal = newVal;
        }
    }

    [Test]
    public static void Cut_EmptyHeap_ThrowsCorrectExcpetion()
    {
        var heap = new TestFHeap();
        var item1 = new FHeapNode<int>(1);
        var item2 = new FHeapNode<int>(2);

        Assert.Throws<InvalidOperationException>(() => heap.RawCut(item1, item2));
    }

    [Test]
    public static void Cut_FilledHeap_AlteredItem()
    {
        var heap = new TestFHeap();
        var item1 = heap.Push(1);
        var item2 = heap.Push(2);

        item2.Degree = -1;

        Assert.Throws<InvalidOperationException>(() => heap.RawCut(item1, item2));
    }

    [Test]
    public static void Consolidate_EmptyHeap_DoesNothing()
    {
        var heap = new TestFHeap();
        heap.RawConsolidate();

        Assert.Throws<InvalidOperationException>(() => heap.Peek());
    }
}
