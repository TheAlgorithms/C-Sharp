using System.Collections.Generic;

namespace Algorithms.Sorters.Comparison
{
    /// <summary>
    /// TODO.
    /// </summary>
    /// <typeparam name="T">TODO. 2.</typeparam>
    public class ShellSorter<T> : IComparisonSorter<T>
    {
        /// <summary>
        /// Sorts array using specified comparer,
        /// based on bubble sort,
        /// internal, in-place, unstable,
        /// worst-case time complexity: O(n^2),
        /// space complexity: O(1),
        /// where n - array length.
        /// </summary>
        /// <param name="array">Array to sort.</param>
        /// <param name="comparer">Compares elements.</param>
        public void Sort(T[] array, IComparer<T> comparer)
        {
            for (var step = array.Length / 2; step > 0; step /= 2)
            {
                for (var i = 0; i < step; i++)
                {
                    GappedBubbleSort(array, comparer, i, step);
                }
            }
        }

        private static void GappedBubbleSort(T[] array, IComparer<T> comparer, int start, int step)
        {
            for (var j = start; j < array.Length - step; j += step)
            {
                var wasChanged = false;
                for (var k = start; k < array.Length - j - step; k += step)
                {
                    if (comparer.Compare(array[k], array[k + step]) > 0)
                    {
                        var temp = array[k];
                        array[k] = array[k + step];
                        array[k + step] = temp;
                        wasChanged = true;
                    }
                }

                if (!wasChanged)
                {
                    break;
                }
            }
        }
    }
}
