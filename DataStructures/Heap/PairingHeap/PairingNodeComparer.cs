namespace DataStructures.Heap.PairingHeap;

/// <summary>
/// Node comparer.
/// </summary>
/// <typeparam name="T">Node type.</typeparam>
public class PairingNodeComparer<T>(Sorting sortDirection, IComparer<T> comparer) : IComparer<T> where T : IComparable
{
    private readonly bool isMax = sortDirection == Sorting.Descending;
    private readonly IComparer<T> nodeComparer = comparer;

    public int Compare(T? x, T? y)
    {
        return !isMax
            ? CompareNodes(x, y)
            : CompareNodes(y, x);
    }

    private int CompareNodes(T? one, T? second)
    {
        return nodeComparer.Compare(one, second);
    }
}
