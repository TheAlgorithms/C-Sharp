using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.SortedList
{
    /// <summary>
    /// Implementation of a Sorted List. It uses Quick Sort Algorithm.
    /// </summary>
    /// <typeparam name="T">Generic Type.</typeparam>
    public class SortedList<T> : IComparisonSorter<T>
    {
        public void Sort(List<T> list, IComparer<T> comparer) => Sort(list, comparer, 0, list.Count - 1);

        private void Sort(List<T> list, IComparer<T> comparer, int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            var p = Partition(list, comparer, left, right);
            Sort(list, comparer, left, p);
            Sort(list, comparer, p + 1, right);
        }

        private int Partition(List<T> list, IComparer<T> comparer, int left, int right)
        {
            var pivot = list[left + (right - left) / 2];
            var nleft = left;
            var nright = right;
            while (true)
            {
                while (comparer.Compare(list[nleft], pivot) < 0)
                {
                    nleft++;
                }

                while (comparer.Compare(list[nright], pivot) > 0)
                {
                    nright--;
                }

                if (nleft >= nright)
                {
                    return nright;
                }

                var t = list[nleft];
                list[nleft] = list[nright];
                list[nright] = t;

                nleft++;
                nright--;
            }
        }
    }
}
