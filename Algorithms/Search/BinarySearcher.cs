using System;

namespace Algorithms.Search
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
    }
}
