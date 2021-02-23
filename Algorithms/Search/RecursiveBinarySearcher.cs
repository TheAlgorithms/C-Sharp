using System;
using System.Collections.Generic;

namespace Algorithms.Search
{
    /// <summary>
    /// RecursiveBinarySearcher.
    /// </summary>
    /// <typeparam name="T">Type of searcher target.</typeparam>
    public class RecursiveBinarySearcher<T> where T : IComparable<T>
    {
        /// <summary>
        /// Finds index of item in collection that equals to item searched for,
        /// time complexity: O(log(n)),
        /// space complexity: O(1),
        /// where n - collection size.
        /// </summary>
        /// <param name="collection">Sorted collection to search in.</param>
        /// <param name="item">Item to search for.</param>
        /// <exception cref="ArgumentNullException">Thrown if input collection is null.</exception>
        /// <returns>Index of item that equals to item searched for or -1 if none found.</returns>
        public int FindIndex(IList<T>? collection, T item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            var leftIndex = 0;
            var rightIndex = collection.Count - 1;

            return FindIndex(collection, item, leftIndex, rightIndex);
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
        private int FindIndex(IList<T> collection, T item, int leftIndex, int rightIndex)
        {
            if (leftIndex > rightIndex)
            {
                return -1;
            }

            var middleIndex = leftIndex + (rightIndex - leftIndex) / 2;
            var result = item.CompareTo(collection[middleIndex]);

            return result switch
            {
                var r when r == 0 => middleIndex,
                var r when r > 0 => FindIndex(collection, item, middleIndex + 1, rightIndex),
                var r when r < 0 => FindIndex(collection, item, leftIndex, middleIndex - 1),
                _ => -1
            };
        }
    }
}
