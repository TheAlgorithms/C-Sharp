using System.Collections.Generic;

namespace Algorithms.Sorters
{
    /// <summary>
    /// Divide and Conquer algorithm, which splits#
    /// input array in two halves, calls itself for the two halves and
    /// then merges the two sorted halves.
    /// </summary>
    public class MergeSorter : ISorter<int>
    {
        private static int Len { get; set; }

        /// <summary>
        /// Sorts array using merge algorithm.
        /// </summary>
        /// <param name="array">Input integer array.</param>
        /// <param name="comparer">Comparer.</param>
        public void Sort(int[] array, IComparer<int> comparer) => SortMerge(array, 0, array.Length - 1);

        private static void MainMerge(IList<int> numbers, int left, int mid, int right)
        {
            var temp = new int[Len];

            int i;

            var eol = mid - 1;
            var pos = left;
            var num = right - left + 1;

            while (left <= eol && mid <= right)
            {
                temp[pos++] = numbers[left] <= numbers[mid] ? numbers[left++] : numbers[mid++];
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

        private static void SortMerge(IList<int> numbers, int left, int right)
        {
            Len = numbers.Count;

            if (right <= left)
            {
                return;
            }

            var mid = (right + left) / 2;
            SortMerge(numbers, left, mid);
            SortMerge(numbers, mid + 1, right);
            MainMerge(numbers, left, mid + 1, right);
        }
    }
}
