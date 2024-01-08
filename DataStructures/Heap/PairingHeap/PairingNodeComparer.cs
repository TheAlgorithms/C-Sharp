using System;
using System.Collections.Generic;

namespace DataStructures.Heap.PairingHeap;

/// <summary>
/// Node comparer.
/// </summary>
/// <typeparam name="T">Node type.</typeparam>
public class PairingNodeComparer<T> : IComparer<T> where T : IComparable
{
    private readonly bool isMax;
    private readonly IComparer<T> nodeComparer;

    public PairingNodeComparer(Sorting sortDirection, IComparer<T> comparer)
    {
        isMax = sortDirection == Sorting.Descending;
        nodeComparer = comparer;
    }

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
