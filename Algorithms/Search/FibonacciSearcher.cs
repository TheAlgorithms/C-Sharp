using System;

namespace Algorithms.Search;

/// <summary>
///     Class that implements Fibonacci search algorithm.
/// </summary>
/// <typeparam name="T">Type of array element.</typeparam>
public class FibonacciSearcher<T> where T : IComparable<T>
{
    /// <summary>
    ///     Finds the index of the item searched for in the array.
    ///     Time complexity:
    ///     worst-case: O(log n),
    ///     average-case: O(log n),
    ///     best-case: O(1).
    /// </summary>
    /// <param name="array">Sorted array to be searched in. Cannot be null.</param>
    /// <param name="item">Item to be searched for. Cannot be null.</param>
    /// <returns>If an item is found, return index. If the array is empty or an item is not found, return -1.</returns>
    /// <exception cref="ArgumentNullException">Gets thrown when the given array or item is null.</exception>
    public int FindIndex(T[] array, T item)
    {
        if (array is null)
        {
            throw new ArgumentNullException("array");
        }

        if (item is null)
        {
            throw new ArgumentNullException("item");
        }

        var arrayLength = array.Length;

        if (arrayLength > 0)
        {
            // find the smallest Fibonacci number that equals or is greater than the array length
            var fibonacciNumberBeyondPrevious = 0;
            var fibonacciNumPrevious = 1;
            var fibonacciNum = fibonacciNumPrevious;

            while (fibonacciNum <= arrayLength)
            {
                fibonacciNumberBeyondPrevious = fibonacciNumPrevious;
                fibonacciNumPrevious = fibonacciNum;
                fibonacciNum = fibonacciNumberBeyondPrevious + fibonacciNumPrevious;
            }

            // offset to drop the left part of the array
            var offset = -1;

            while (fibonacciNum > 1)
            {
                var index = Math.Min(offset + fibonacciNumberBeyondPrevious, arrayLength - 1);

                switch (item.CompareTo(array[index]))
                {
                    // reject approximately 1/3 of the existing array in front
                    // by moving Fibonacci numbers
                    case > 0:
                        fibonacciNum = fibonacciNumPrevious;
                        fibonacciNumPrevious = fibonacciNumberBeyondPrevious;
                        fibonacciNumberBeyondPrevious = fibonacciNum - fibonacciNumPrevious;
                        offset = index;
                        break;

                    // reject approximately 2/3 of the existing array behind
                    // by moving Fibonacci numbers
                    case < 0:
                        fibonacciNum = fibonacciNumberBeyondPrevious;
                        fibonacciNumPrevious = fibonacciNumPrevious - fibonacciNumberBeyondPrevious;
                        fibonacciNumberBeyondPrevious = fibonacciNum - fibonacciNumPrevious;
                        break;
                    default:
                        return index;
                }
            }

            // check the last element
            if (fibonacciNumPrevious == 1 && item.Equals(array[^1]))
            {
                return arrayLength - 1;
            }
        }

        return -1;
    }
}
