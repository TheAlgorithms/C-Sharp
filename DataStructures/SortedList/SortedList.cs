using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.SortedList
{
    /// <summary>
    /// Implementation of a Sorted List. It uses Quick Sort Algorithm.
    /// </summary>
    /// <typeparam name="T">Generic Type.</typeparam>
    public class SortedList<T> : IComparisonSorter<T>, IEnumerable<T>
    {
        private readonly List<T> list;

        public SortedList() => list = new List<T>();

        public SortedList(List<T> myList)
        {
            list = new List<T>(myList.Count);
            list = myList;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)list).GetEnumerator();
        }

        /// <summary>
        /// Insert item in a sorted list.
        /// </summary>
        /// <param name="item">Item of Type T to be inserted in the list.</param>
        /// <returns>true or false.Item was inserted successfully or not.</returns>
        public bool Insert(T item)
        {
            // Use BinarySearch() method to determine if the item  exists in the list
            int binarysearchIndex = list.BinarySearch(item);

            if (binarysearchIndex < 0)
            {
                // Bitwise complement on the returned negative integer, gives positive integer.
                list.Insert(~binarysearchIndex, item);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Remove item from a sorted list.
        /// </summary>
        /// <param name="item">Item of Type T to be removed from the list.</param>
        /// <returns>true or false.Item was inserted successfully or not.</returns>
        public bool Remove(T item)
        {
            int binarysearchIndex = list.BinarySearch(item);

            if (binarysearchIndex > 0)
            {
                list.RemoveAt(binarysearchIndex);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Find if item exists in a list or not.
        /// </summary>
        /// <param name="item">Item of Type T to be searched in the list.</param>
        /// <returns>true or false.</returns>
        public bool Search(T item)
        {
            int binarysearchIndex = list.BinarySearch(item);

            if (binarysearchIndex > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sorts the list in ascending order using QuickSort algorithm.
        /// </summary>
        /// <param name="list">List to be sorted.</param>
        /// <param name="comparer">Comparer for comparing items in the list.</param>
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
