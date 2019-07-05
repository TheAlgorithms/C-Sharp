using System;

namespace Algorithms.Searches
{
    public class BinarySearcher<T> where T : IComparable<T>
    {
        /// <summary>
        /// Finds index of first item in array that equals to item to search
        /// Time complexity: O(logN)
        /// Space complexity: O(1)
        /// </summary>
        /// <param name="sortedData">Sorted array to search in</param>
        /// <param name="item">Item to search</param>
        /// <returns>Index of first item that equals to item to search or -1 if none found</returns>
        public int FindIndex(T[] sortedData, T item) 
        {
            var startIndex = 0;
            var finishIndex = sortedData.Length - 1;

            do
            {
                var middleIndex = startIndex + (finishIndex - startIndex) / 2;

                if (item.CompareTo(sortedData[middleIndex]) == 1)
                {
                    startIndex = middleIndex + 1;
                }
                if (item.CompareTo(sortedData[middleIndex]) == -1)
                {
                    finishIndex = middleIndex - 1;
                }
                if (item.CompareTo(sortedData[middleIndex]) == 0)
                {
                    return middleIndex;
                }
            } while (startIndex <= finishIndex);

            return -1;
        }
    }
}
