using System;

namespace Algorithms.Search;

/// <summary>
///     Binary Searcher checks an array for element specified by checking
///     if element is greater or less than the half being checked.
///     time complexity: O(log(n)),
///     space complexity: O(1).
///     Note: Array must be sorted beforehand.
/// </summary>
/// <typeparam name="T">Type of element stored inside array. 2.</typeparam>
public class BinarySearcher<T> where T : IComparable<T>
{
    /// <summary>
    ///     Finds index of an array by using binary search.
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
