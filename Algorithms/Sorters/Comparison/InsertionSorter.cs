using System.Collections.Generic;

namespace Algorithms.Sorters.Comparison
{
    /// <summary>
    /// Class that implements insertion sort algorithm.
    /// </summary>
    /// <typeparam name="T">Type of array element.</typeparam>
    public class InsertionSorter<T> : IComparisonSorter<T>
    {
        /// <summary>
        /// Sorts array using specified comparer,
        /// internal, in-place, stable,
        /// time complexity: O(n^2),
        /// space complexity: O(1),
        /// where n - array length.
        /// </summary>
        /// <param name="array">Array to sort.</param>
        /// <param name="comparer">Compares elements.</param>
        public void Sort(T[] array, IComparer<T> comparer)
        {
            for (var i = 0; i < array.Length - 1; i++)
            {
                var imin = i;
                for (var j = i + 1; j < array.Length; j++)
                {
                    if (comparer.Compare(array[j], array[imin]) < 0)
                    {
                        imin = j;
                    }
                }

                var t = array[imin];
                array[imin] = array[i];
                array[i] = t;
            }
        }
    }
}
