using System;

namespace Algorithms.Search;

/// <summary>
///     Jump Search checks fewer elements by jumping ahead by fixed steps.
///     The optimal steps to jump is √n, where n refers to the number of elements in the array.
///     Time Complexity: O(√n)
///     Note: The array has to be sorted beforehand.
/// </summary>
/// <typeparam name="T">Type of the array element.</typeparam>
public class JumpSearcher<T> where T : IComparable<T>
{
    /// <summary>
    ///     Find the index of the item searched for in the array.
    /// </summary>
    /// <param name="sortedArray">Sorted array to be search in. Cannot be null.</param>
    /// <param name="searchItem">Item to be search for. Cannot be null.</param>
    /// <returns>If item is found, return index. If array is empty or item not found, return -1.</returns>
    public int FindIndex(T[] sortedArray, T searchItem)
    {
        if (sortedArray is null)
        {
            throw new ArgumentNullException("sortedArray");
        }

        if (searchItem is null)
        {
            throw new ArgumentNullException("searchItem");
        }

        int jumpStep = (int)Math.Floor(Math.Sqrt(sortedArray.Length));
        int currentIndex = 0;
        int nextIndex = jumpStep;

        if (sortedArray.Length != 0)
        {
            while (sortedArray[nextIndex - 1].CompareTo(searchItem) < 0)
            {
                currentIndex = nextIndex;
                nextIndex += jumpStep;

                if (nextIndex >= sortedArray.Length)
                {
                    nextIndex = sortedArray.Length - 1;
                    break;
                }
            }

            for (int i = currentIndex; i <= nextIndex; i++)
            {
                if (sortedArray[i].CompareTo(searchItem) == 0)
                {
                    return i;
                }
            }
        }

        return -1;
    }
}
