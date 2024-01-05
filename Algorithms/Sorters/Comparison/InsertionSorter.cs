using System.Collections.Generic;

namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Class that implements insertion sort algorithm.
/// </summary>
/// <typeparam name="T">Type of array element.</typeparam>
public class InsertionSorter<T> : IComparisonSorter<T>
{
    /// <summary>
    ///     Sorts array using specified comparer,
    ///     internal, in-place, stable,
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
            for (var j = i; j > 0 && comparer.Compare(array[j], array[j - 1]) < 0; j--)
            {
                var temp = array[j - 1];
                array[j - 1] = array[j];
                array[j] = temp;
            }
        }
    }
}
