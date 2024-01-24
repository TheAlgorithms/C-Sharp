using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Heap;
using NUnit.Framework;

namespace DataStructures.Tests.Heap;

[TestFixture]
public static class MinMaxHeapTests
{
    private static readonly object[] CollectionsSourceInt =
    {
        new[] { 5, 10, -2, 0, 3, 13, 5, -8, 41, -5, -7, -60, -12 }
    };

    private static readonly object[] CollectionsSourceString =
    {
        new[] { "abc", "abc", "xyz", "bcd", "klm", "opq", "ijk" }
    };

    private static readonly object[] CollectionsSourceChar =
    {
        new[] { 'e', '4', 'x', 'D', '!', '$', '-', '_', '2', ')', 'Z', 'q' }
    };

    [Test]
    public static void CustomComparerTest()
    {
        var arr = new[] { "aaaa", "c", "dd", "bbb" };
        var comparer = Comparer<string>.Create((a, b) => Comparer<int>.Default.Compare(a.Length, b.Length));

        var mmh = new MinMaxHeap<string>(comparer: comparer);
        foreach (var s in arr)
        {
            mmh.Add(s);
        }

        Assert.That(comparer, Is.EqualTo(mmh.Comparer));
        Assert.That("c", Is.EqualTo(mmh.GetMin()));
        Assert.That("aaaa", Is.EqualTo(mmh.GetMax()));
    }

    #region AddTest
    [Test, TestCaseSource(nameof(CollectionsSourceInt))]
    public static void AddTestInt(IEnumerable<int> collection)
    {
        var mmh = new MinMaxHeap<int>();
        foreach (var item in collection)
        {
            mmh.Add(item);
        }

        var minValue = mmh.GetMin();
        var maxValue = mmh.GetMax();

        Assert.That(collection.Min(), Is.EqualTo(minValue));
        Assert.That(collection.Max(), Is.EqualTo(maxValue));
        Assert.That(collection.Count(), Is.EqualTo(mmh.Count));
    }

    [Test, TestCaseSource(nameof(CollectionsSourceString))]
    public static void AddTestString(IEnumerable<string> collection)
    {
        var mmh = new MinMaxHeap<string>();
        foreach (var item in collection)
        {
            mmh.Add(item);
        }

        var minValue = mmh.GetMin();
        var maxValue = mmh.GetMax();

        Assert.That(collection.Min(), Is.EqualTo(minValue));
        Assert.That(collection.Max(), Is.EqualTo(maxValue));
        Assert.That(collection.Count(), Is.EqualTo(mmh.Count));
    }

    [Test, TestCaseSource(nameof(CollectionsSourceChar))]
    public static void AddTestChar(IEnumerable<char> collection)
    {
        var mmh = new MinMaxHeap<char>();
        foreach (var item in collection)
        {
            mmh.Add(item);
        }

        var minValue = mmh.GetMin();
        var maxValue = mmh.GetMax();

        Assert.That(collection.Min(), Is.EqualTo(minValue));
        Assert.That(collection.Max(), Is.EqualTo(maxValue));
        Assert.That(collection.Count(), Is.EqualTo(mmh.Count));
    }
    #endregion

    #region ExtractMaxTest
    [Test, TestCaseSource(nameof(CollectionsSourceInt))]
    public static void ExtractMaxTestInt(IEnumerable<int> collection)
    {
        var ordered = collection.OrderByDescending(x => x);
        var mmh = new MinMaxHeap<int>(collection);
        var emptyHeap = new MinMaxHeap<int>();

        var first = mmh.ExtractMax();
        var second = mmh.GetMax();

        Assert.Throws<InvalidOperationException>(() => emptyHeap.ExtractMax());
        Assert.That(ordered.ElementAt(0), Is.EqualTo(first));
        Assert.That(ordered.ElementAt(1), Is.EqualTo(second));
        Assert.That(collection.Count() - 1, Is.EqualTo(mmh.Count));
    }

    [Test, TestCaseSource(nameof(CollectionsSourceString))]
    public static void ExtractMaxTestString(IEnumerable<string> collection)
    {
        var ordered = collection.OrderByDescending(x => x);
        var mmh = new MinMaxHeap<string>(collection);
        var emptyHeap = new MinMaxHeap<string>();

        var first = mmh.ExtractMax();
        var second = mmh.GetMax();

        Assert.Throws<InvalidOperationException>(() => emptyHeap.ExtractMax());
        Assert.That(ordered.ElementAt(0), Is.EqualTo(first));
        Assert.That(ordered.ElementAt(1), Is.EqualTo(second));
        Assert.That(collection.Count() - 1, Is.EqualTo(mmh.Count));
    }

    [Test, TestCaseSource(nameof(CollectionsSourceChar))]
    public static void ExtractMaxTestChar(IEnumerable<char> collection)
    {
        var ordered = collection.OrderByDescending(x => x);
        var mmh = new MinMaxHeap<char>(collection);
        var emptyHeap = new MinMaxHeap<char>();

        var first = mmh.ExtractMax();
        var second = mmh.GetMax();

        Assert.Throws<InvalidOperationException>(() => emptyHeap.ExtractMax());
        Assert.That(ordered.ElementAt(0), Is.EqualTo(first));
        Assert.That(ordered.ElementAt(1), Is.EqualTo(second));
        Assert.That(collection.Count() - 1, Is.EqualTo(mmh.Count));
    }
    #endregion

    #region ExtractMinTest
    [Test, TestCaseSource(nameof(CollectionsSourceInt))]
    public static void ExtractMinTestInt(IEnumerable<int> collection)
    {
        var ordered = collection.OrderBy(x => x);
        var mmh = new MinMaxHeap<int>(collection);
        var emptyHeap = new MinMaxHeap<int>();

        var first = mmh.ExtractMin();
        var second = mmh.GetMin();

        Assert.Throws<InvalidOperationException>(() => emptyHeap.ExtractMin());
        Assert.That(ordered.ElementAt(0), Is.EqualTo(first));
        Assert.That(ordered.ElementAt(1), Is.EqualTo(second));
        Assert.That(collection.Count() - 1, Is.EqualTo(mmh.Count));
    }

    [Test, TestCaseSource(nameof(CollectionsSourceString))]
    public static void ExtractMinTestString(IEnumerable<string> collection)
    {
        var ordered = collection.OrderBy(x => x);
        var mmh = new MinMaxHeap<string>(collection);
        var emptyHeap = new MinMaxHeap<string>();

        var first = mmh.ExtractMin();
        var second = mmh.GetMin();

        Assert.Throws<InvalidOperationException>(() => emptyHeap.ExtractMin());
        Assert.That(ordered.ElementAt(0), Is.EqualTo(first));
        Assert.That(ordered.ElementAt(1), Is.EqualTo(second));
        Assert.That(collection.Count() - 1, Is.EqualTo(mmh.Count));
    }

    [Test, TestCaseSource(nameof(CollectionsSourceChar))]
    public static void ExtractMinTestChar(IEnumerable<char> collection)
    {
        var ordered = collection.OrderBy(x => x);
        var mmh = new MinMaxHeap<char>(collection);
        var emptyHeap = new MinMaxHeap<char>();

        var first = mmh.ExtractMin();
        var second = mmh.GetMin();

        Assert.Throws<InvalidOperationException>(() => emptyHeap.ExtractMin());
        Assert.That(ordered.ElementAt(0), Is.EqualTo(first));
        Assert.That(ordered.ElementAt(1), Is.EqualTo(second));
        Assert.That(collection.Count() - 1, Is.EqualTo(mmh.Count));
    }
    #endregion

    #region GetMaxTest
    [Test, TestCaseSource(nameof(CollectionsSourceInt))]
    public static void GetMaxTestInt(IEnumerable<int> collection)
    {
        var emptyHeap = new MinMaxHeap<int>();
        var mmh = new MinMaxHeap<int>(collection);

        var maxValue = mmh.GetMax();

        Assert.Throws<InvalidOperationException>(() => emptyHeap.GetMax());
        Assert.That(collection.Max(), Is.EqualTo(maxValue));
    }

    [Test, TestCaseSource(nameof(CollectionsSourceString))]
    public static void GetMaxTestString(IEnumerable<string> collection)
    {
        var emptyHeap = new MinMaxHeap<string>();
        var mmh = new MinMaxHeap<string>(collection);

        var maxValue = mmh.GetMax();

        Assert.Throws<InvalidOperationException>(() => emptyHeap.GetMax());
        Assert.That(collection.Max(), Is.EqualTo(maxValue));
    }

    [Test, TestCaseSource(nameof(CollectionsSourceChar))]
    public static void GetMaxTestChar(IEnumerable<char> collection)
    {
        var emptyHeap = new MinMaxHeap<char>();
        var mmh = new MinMaxHeap<char>(collection);

        var maxValue = mmh.GetMax();

        Assert.Throws<InvalidOperationException>(() => emptyHeap.GetMax());
        Assert.That(collection.Max(), Is.EqualTo(maxValue));
    }
    #endregion

    #region GetMinTest
    [Test, TestCaseSource(nameof(CollectionsSourceInt))]
    public static void GetMinTestInt(IEnumerable<int> collection)
    {
        var emptyHeap = new MinMaxHeap<int>();
        var mmh = new MinMaxHeap<int>(collection);

        var minValue = mmh.GetMin();

        Assert.Throws<InvalidOperationException>(() => emptyHeap.GetMin());
        Assert.That(collection.Min(), Is.EqualTo(minValue));
    }

    [Test, TestCaseSource(nameof(CollectionsSourceString))]
    public static void GetMinTestString(IEnumerable<string> collection)
    {
        var emptyHeap = new MinMaxHeap<string>();
        var mmh = new MinMaxHeap<string>(collection);

        var minValue = mmh.GetMin();

        Assert.Throws<InvalidOperationException>(() => emptyHeap.GetMin());
        Assert.That(collection.Min(), Is.EqualTo(minValue));
    }

    [Test, TestCaseSource(nameof(CollectionsSourceChar))]
    public static void GetMinTestChar(IEnumerable<char> collection)
    {
        var emptyHeap = new MinMaxHeap<char>();
        var mmh = new MinMaxHeap<char>(collection);

        var minValue = mmh.GetMin();

        Assert.Throws<InvalidOperationException>(() => emptyHeap.GetMin());
        Assert.That(collection.Min(), Is.EqualTo(minValue));
    }
    #endregion

    #region HeapSortUsingGet
    [Test]
    public static void HeapSortUsingGet(
        [ValueSource(nameof(CollectionsSourceInt))] IEnumerable<int> collection,
        [Values] bool ascending)
    {
        var ordered = ascending ? collection.OrderBy(x => x) : collection.OrderByDescending(x => x);
        var mmh = new MinMaxHeap<int>(collection);
        var extracted = new List<int>();

        while (mmh.Count > 0)
        {
            int value;
            if (ascending)
            {
                value = mmh.GetMin();
                _ = mmh.ExtractMin();
            }
            else
            {
                value = mmh.GetMax();
                _ = mmh.ExtractMax();
            }

            extracted.Add(value);
        }

        Assert.That(ordered.SequenceEqual(extracted), Is.True);
    }
    #endregion

    #region HeapSortUsingExtract
    [Test]
    public static void HeapSortUsingExtract(
        [ValueSource(nameof(CollectionsSourceInt))] IEnumerable<int> collection,
        [Values] bool ascending)
    {
        var ordered = ascending ? collection.OrderBy(x => x) : collection.OrderByDescending(x => x);
        var mmh = new MinMaxHeap<int>(collection);
        var extracted = new List<int>();

        while (mmh.Count > 0)
        {
            var value = ascending ? mmh.ExtractMin() : mmh.ExtractMax();
            extracted.Add(value);
        }

        Assert.That(ordered.SequenceEqual(extracted), Is.True);
    }
    #endregion
}
