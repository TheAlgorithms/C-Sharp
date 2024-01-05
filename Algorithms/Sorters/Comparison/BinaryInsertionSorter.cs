using System;
using System.Collections.Generic;

namespace Algorithms.Sorters.Comparison;

/// <summary>
///     TODO.
/// </summary>
/// <typeparam name="T">TODO. 2.</typeparam>
public class BinaryInsertionSorter<T> : IComparisonSorter<T>
{
    /// <summary>
    ///     Sorts array using specified comparer,
    ///     variant of insertion sort where binary search is used to find place for next element
    ///     internal, in-place, unstable,
    ///     time complexity: O(n^2),
    ///     space complexity: O(1),
    ///     where n - array length.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    /// <param name="comparer">Compares elements.</param>
    public void Sort(T[] array, IComparer<T> comparer)
    {
        for (var i = 1; i < array.Length; i++)
        {
            var target = array[i];
            var moveIndex = i - 1;
            var targetInsertLocation = BinarySearch(array, 0, moveIndex, target, comparer);
            Array.Copy(array, targetInsertLocation, array, targetInsertLocation + 1, i - targetInsertLocation);

            array[targetInsertLocation] = target;
        }
    }

    /// <summary>Implementation of Binary Search using an iterative approach.</summary>
    /// <param name="array">
    ///     An array of values sorted in ascending order between the index values left and right to search
    ///     through.
    /// </param>
    /// <param name="from">Left index to search from (inclusive).</param>
    /// <param name="to">Right index to search to (inclusive).</param>
    /// <param name="target">The value to find placefor in the provided array.</param>
    /// <param name="comparer">TODO.</param>
    /// <returns>The index where to insert target value.</returns>
    private static int BinarySearch(T[] array, int from, int to, T target, IComparer<T> comparer)
    {
        var left = from;
        var right = to;
        while (right > left)
        {
            var middle = (left + right) / 2;
            var comparisonResult = comparer.Compare(target, array[middle]);

            if (comparisonResult == 0)
            {
                return middle + 1;
            }

            if (comparisonResult > 0)
            {
                left = middle + 1;
            }
            else
            {
                right = middle - 1;
            }
        }

        return comparer.Compare(target, array[left]) < 0 ? left : left + 1;
    }
}
