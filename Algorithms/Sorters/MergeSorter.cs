using System.Collections.Generic;

namespace Algorithms.Sorters
{

    /// <summary>
    /// Divide and Conquer algorithm, which splits#
    /// input array in two halves, calls itself for the two halves and
    /// then merges the two sorted halves.
    /// </summary>
    /// <typeparam name="T">type of array</typeparam>
    public class MergeSorter<T> : ISorter<T>
    {
        /// <summary>
        /// Sorts array using merge algorithm.
        /// </summary>
        /// <param name="array">Input integer array.</param>
        /// <param name="comparer">Comparer.</param>
        public void Sort(T[] array, IComparer<T> comparer) => SortMerge(array, 0, array.Length - 1, comparer);

        private static void MainMerge(IList<T> numbers, int left, int mid, int right, IComparer<T> comparer)
        {
            var temp = new T[numbers.Count];

            int i;

            var eol = mid - 1;
            var pos = left;
            var num = right - left + 1;

            while (left <= eol && mid <= right)
            {
                var compResult = comparer.Compare(numbers[left], numbers[mid]);
                temp[pos++] = compResult <= 0 ? numbers[left++] : numbers[mid++];
            }

            while (left <= eol)
            {
                temp[pos++] = numbers[left++];
            }

            while (mid <= right)
            {
                temp[pos++] = numbers[mid++];
            }

            for (i = 0; i < num; i++)
            {
                numbers[right] = temp[right];
                right--;
            }
        }

        private static void SortMerge(IList<T> numbers, int left, int right, IComparer<T> comparer)
        {
            if (right <= left)
            {
                return;
            }

            var mid = left + (right - left) / 2;
            SortMerge(numbers, left, mid, comparer);
            SortMerge(numbers, mid + 1, right, comparer);
            MainMerge(numbers, left, mid + 1, right, comparer);
        }
    }
}
