using System;
using System.Collections.Generic;

namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Sorts arrays using quicksort (selecting random point as a pivot).
/// </summary>
/// <typeparam name="T">Type of array element.</typeparam>
public sealed class RandomPivotQuickSorter<T> : QuickSorter<T>
{
    private readonly Random random = new();

    protected override T SelectPivot(T[] array, IComparer<T> comparer, int left, int right) =>
        array[random.Next(left, right + 1)];
}
