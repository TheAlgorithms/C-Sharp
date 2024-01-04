using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Divide and Conquer algorithm, which splits
///     array in two halves, calls itself for the two
///     halves and then merges the two sorted halves.
/// </summary>
/// <typeparam name="T">Type of array elements.</typeparam>
public class MergeSorter<T> : IComparisonSorter<T>
{
    /// <summary>
    ///     Sorts array using merge sort algorithm,
    ///     originally designed as external sorting algorithm,
    ///     internal, stable,
    ///     time complexity: O(n log(n)),
    ///     space complexity: O(n),
    ///     where n - array length.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    /// <param name="comparer">Comparer to compare elements of <paramref name="array" />.</param>
    public void Sort(T[] array, IComparer<T> comparer)
    {
        if (array.Length <= 1)
        {
            return;
        }

        var (left, right) = Split(array);
        Sort(left, comparer);
        Sort(right, comparer);
        Merge(array, left, right, comparer);
    }

    private static void Merge(T[] array, T[] left, T[] right, IComparer<T> comparer)
    {
        var mainIndex = 0;
        var leftIndex = 0;
        var rightIndex = 0;

        while (leftIndex < left.Length && rightIndex < right.Length)
        {
            var compResult = comparer.Compare(left[leftIndex], right[rightIndex]);
            array[mainIndex++] = compResult <= 0 ? left[leftIndex++] : right[rightIndex++];
        }

        while (leftIndex < left.Length)
        {
            array[mainIndex++] = left[leftIndex++];
        }

        while (rightIndex < right.Length)
        {
            array[mainIndex++] = right[rightIndex++];
        }
    }

    private static (T[] left, T[] right) Split(T[] array)
    {
        var mid = array.Length / 2;
        return (array.Take(mid).ToArray(), array.Skip(mid).ToArray());
    }
}
