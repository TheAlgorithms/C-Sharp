using System;
using System.Collections.Generic;

namespace Algorithms.Searches
{
    /// <summary>
    /// TODO.
    /// </summary>
    /// <typeparam name="T">TODO. 2.</typeparam>
    public class BinarySearcher<T> where T : IComparable<T>
    {
        /// <summary>
        /// Finds index of item in array that equals to item searched for,
        /// time complexity: O(log(n)),
        /// space complexity: O(1),
        /// where n - array size.
        /// </summary>
        /// <param name="sortedData">Sorted array to search in.</param>
        /// <param name="item">Item to search for.</param>
        /// <returns>Index of item that equals to item searched for or -1 if none found.</returns>
        public int FindIndex(T[] sortedData, T item)
        {
            var leftIndex = 0;
            var rightIndex = sortedData.Length - 1;

            while (leftIndex <= rightIndex)
            {
                var middleIndex = leftIndex + (rightIndex - leftIndex) / 2;

                if (item.CompareTo(sortedData[middleIndex]) > 0)
                {
                    leftIndex = middleIndex + 1;
                    continue;
                }

                if (item.CompareTo(sortedData[middleIndex]) < 0)
                {
                    rightIndex = middleIndex - 1;
                    continue;
                }

                return middleIndex;
            }

            return -1;
        }

        /// <summary>
        /// Finds index of item in collection that equals to item searched for,
        /// time complexity: O(log(n)),
        /// space complexity: O(1),
        /// where n - collection size.
        /// </summary>
        /// <param name="collection">Sorted collection to search in.</param>
        /// <param name="item">Item to search for.</param>
        /// <returns>Index of item that equals to item searched for or -1 if none found.</returns>
        public int FindIndexRecursive(IList<T>? collection, T item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            var leftIndex = 0;
            var rightIndex = collection.Count - 1;

            return FindIndexRecursive(collection, item, leftIndex, rightIndex);
        }

        /// <summary>
        /// Finds index of item in array that equals to item searched for,
        /// time complexity: O(log(n)),
        /// space complexity: O(1),
        /// where n - array size.
        /// </summary>
        /// <param name="collection">Sorted array to search in.</param>
        /// <param name="item">Item to search for.</param>
        /// <param name="leftIndex">Minimum search range.</param>
        /// <param name="rightIndex">Maximum search range.</param>
        /// <returns>Index of item that equals to item searched for or -1 if none found.</returns>
        private int FindIndexRecursive(IList<T> collection, T item, int leftIndex, int rightIndex)
        {
            if (leftIndex > rightIndex)
            {
                return -1;
            }

            var middleIndex = leftIndex + (rightIndex - leftIndex) / 2;
            var result = item.CompareTo(collection[middleIndex]);

            return result switch
            {
                var c when c == 0 => middleIndex,
                var c when c > 0 => FindIndexRecursive(collection, item, middleIndex + 1, rightIndex),
                _ => FindIndexRecursive(collection, item, leftIndex, middleIndex - 1),
            };
        }
    }
}
