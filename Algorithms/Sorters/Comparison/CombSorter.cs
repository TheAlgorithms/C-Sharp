using System;
using System.Collections.Generic;

namespace Algorithms.Sorters.Comparison
{
    /// <summary>
    /// Comb sort is a relatively simple sorting algorithm that improves on bubble sort.
    /// </summary>
    /// <typeparam name="T">Type of array element.</typeparam>
    public class CombSorter<T> : IComparisonSorter<T>
    {
        /// <summary>
        /// Sorts array using specified comparer,
        /// internal, in-place, unstable,
        /// time complexity: O(n^2),
        /// space complexity: O(1),
        /// where n - array length.
        /// </summary>
        /// <param name="array">Array to sort.</param>
        /// <param name="comparer">Compares elements.</param>
        public void Sort(T[] array, IComparer<T> comparer)
        {
            var gap = array.Length;
            const double shrinkFactor = 1.3; // Suggested as an ideal shrink factor by the authors
            var sorted = false;
            while (!sorted)
            {
                gap = (int)Math.Floor(gap / shrinkFactor);
                if (gap <= 1)
                {
                    gap = 1;
                    sorted = true;
                }

                for (var i = 0; i < array.Length - gap; i++)
                {
                    if (comparer.Compare(array[i], array[i + gap]) > 0)
                    {
                        (array[i], array[i + gap]) = (array[i + gap], array[i]);
                        sorted = false;
                    }
                }
            }
        }
    }
}
