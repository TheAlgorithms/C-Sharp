using System;
using System.Collections.Generic;


namespace Algorithms.Search
{
    /// <summary>
    /// Implements an exponential search algorithm
    /// </summary>
    /// <typeparam name="T">Data type of Sorted Array and the target item. 2.</typeparam>
    public class ExponentialSearcher<T> where T : IComparable<T>
    {
        /// <summary>
        ///     Finds index of item in array that equals to item searched for,
        ///     average/worse time complexity: O(log(i)), where i is the index of the target item
        ///     space complexity: O(1),
        /// </summary>
        /// <param name="sortedData">Sorted array to search in.</param>
        /// <param name="item">Item to search for.</param>
        /// <returns>Index of item that equals to item searched for or -1 if none found.</returns>
        public int FindIndex(T[] sortedData, T item)
        {
            RecursiveBinarySearcher searcher = new RecursiveBinarySearcher();
            var startLocation = 0;
            var endLocation = sortedData.Length - 1;

            if ((endLocation - startLocation) <= 0)
            {
                return -1;
            }

            int bound = 1;

            while (bound < (endLocation - startLocation))
            {
                if (sortedData[bound] <  item)
                {
                    bound *= 2;
                }
                else
                {
                    break;
                }
            }

            return searcher.FindIndex(sortedData, item, (bound / 2), min(bound + 1, sortedData.Length));
        }
        }
    }
}
