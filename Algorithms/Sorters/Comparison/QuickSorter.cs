using System.Collections.Generic;

namespace Algorithms.Sorters.Comparison
{
    /// <summary>
    /// Sorts arrays using quicksort.
    /// </summary>
    /// <typeparam name="T">Type of array element.</typeparam>
    public class QuickSorter<T> : IComparisonSorter<T>
    {
        /// <summary>
        /// Sorts array using Hoare partition scheme,
        /// internal, in-place,
        /// time complexity average: O(n log(n)),
        /// time complexity worst: O(n^2),
        /// space complexity: O(log(n)),
        /// where n - array length.
        /// </summary>
        /// <param name="array">Array to sort.</param>
        /// <param name="comparer">Compares elements.</param>
        public void Sort(T[] array, IComparer<T> comparer) => Sort(array, comparer, 0, array.Length - 1);

        private void Sort(T[] array, IComparer<T> comparer, int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            var p = Partition(array, comparer, left, right);
            Sort(array, comparer, left, p);
            Sort(array, comparer, p + 1, right);
        }

        private int Partition(T[] array, IComparer<T> comparer, int left, int right)
        {
            var pivot = array[left + (right - left) / 2];
            var nleft = left;
            var nright = right;
            while (true)
            {
                while (comparer.Compare(array[nleft], pivot) < 0)
                {
                    nleft++;
                }

                while (comparer.Compare(array[nright], pivot) > 0)
                {
                    nright--;
                }

                if (nleft >= nright)
                {
                    return nright;
                }

                var t = array[nleft];
                array[nleft] = array[nright];
                array[nright] = t;

                nleft++;
                nright--;
            }
        }
    }
}
