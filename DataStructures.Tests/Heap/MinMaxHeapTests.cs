using DataStructures.Heap;

namespace DataStructures.Tests.Heap;

[TestFixture]
public static class MinMaxHeapTests
{
    private static readonly object[] CollectionsSource =
    [
        new[] { 5, 10, -2, 0, 3, 13, 5, -8, 41, -5, -7, -60, -12 },
        new[] { 'e', '4', 'x', 'D', '!', '$', '-', '_', '2', ')', 'Z', 'q' },
        new[] { "abc", "abc", "xyz", "bcd", "klm", "opq", "ijk" },
    ];

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

    [TestCaseSource(nameof(CollectionsSource))]
    public static void AddTest<T>(IEnumerable<T> collection)
    {
        var mmh = new MinMaxHeap<T>();
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

    [TestCaseSource(nameof(CollectionsSource))]
    public static void ExtractMaxTest<T>(IEnumerable<T> collection)
    {
        var ordered = collection.OrderByDescending(x => x);
        var mmh = new MinMaxHeap<T>(collection);
        var emptyHeap = new MinMaxHeap<int>();

        var first = mmh.ExtractMax();
        var second = mmh.GetMax();

        Assert.Throws<InvalidOperationException>(() => emptyHeap.ExtractMax());
        Assert.That(ordered.ElementAt(0), Is.EqualTo(first));
        Assert.That(ordered.ElementAt(1), Is.EqualTo(second));
        Assert.That(collection.Count() - 1, Is.EqualTo(mmh.Count));
    }

    [TestCaseSource(nameof(CollectionsSource))]
    public static void ExtractMinTest<T>(IEnumerable<T> collection)
    {
        var ordered = collection.OrderBy(x => x);
        var mmh = new MinMaxHeap<T>(collection);
        var emptyHeap = new MinMaxHeap<int>();

        var first = mmh.ExtractMin();
        var second = mmh.GetMin();

        Assert.Throws<InvalidOperationException>(() => emptyHeap.ExtractMin());
        Assert.That(ordered.ElementAt(0), Is.EqualTo(first));
        Assert.That(ordered.ElementAt(1), Is.EqualTo(second));
        Assert.That(collection.Count() - 1, Is.EqualTo(mmh.Count));
    }

    [TestCaseSource(nameof(CollectionsSource))]
    public static void GetMaxTest<T>(IEnumerable<T> collection)
    {
        var emptyHeap = new MinMaxHeap<T>();
        var mmh = new MinMaxHeap<T>(collection);

        var maxValue = mmh.GetMax();

        Assert.Throws<InvalidOperationException>(() => emptyHeap.GetMax());
        Assert.That(collection.Max(), Is.EqualTo(maxValue));
    }

    [TestCaseSource(nameof(CollectionsSource))]
    public static void GetMinTest<T>(IEnumerable<T> collection)
    {
        var emptyHeap = new MinMaxHeap<T>();
        var mmh = new MinMaxHeap<T>(collection);

        var minValue = mmh.GetMin();

        Assert.Throws<InvalidOperationException>(() => emptyHeap.GetMin());
        Assert.That(collection.Min(), Is.EqualTo(minValue));
    }

    [Test]
    public static void HeapSortUsingGet<T>(
        [ValueSource(nameof(CollectionsSource))] IEnumerable<T> collection,
        [Values] bool ascending)
    {
        var ordered = ascending ? collection.OrderBy(x => x) : collection.OrderByDescending(x => x);
        var mmh = new MinMaxHeap<T>(collection);
        var extracted = new List<T>();

        while (mmh.Count > 0)
        {
            T value;
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

    [Test]
    public static void HeapSortUsingExtract<T>(
        [ValueSource(nameof(CollectionsSource))] IEnumerable<T> collection,
        [Values] bool ascending)
    {
        var ordered = ascending ? collection.OrderBy(x => x) : collection.OrderByDescending(x => x);
        var mmh = new MinMaxHeap<T>(collection);
        var extracted = new List<T>();

        while (mmh.Count > 0)
        {
            var value = ascending ? mmh.ExtractMin() : mmh.ExtractMax();
            extracted.Add(value);
        }

        Assert.That(ordered.SequenceEqual(extracted), Is.True);
    }
}
