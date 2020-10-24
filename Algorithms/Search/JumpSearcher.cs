using System;
using System.Collections.Generic;

namespace Algorithms.Searches
{
    /// <summary>
    /// Class that implements jump search algorithm.
    /// </summary>
    /// <typeparam name="T">Type of array element.</typeparam>
    public class JumpSearcher<T> where T : IComparable<T>
    {
        /// <summary>
        /// Finds index of item in array that equals to item searched for,
        /// time complexity: O(sqrt(n)),
        /// space complexity: O(1),
        /// where n - array size.
        /// </summary>
        /// <param name="sortedData">Sorted array to search in.</param>
        /// <param name="item">Item to search for.</param>
        /// <returns>Index of item that equals to item searched for or -1 if none found.</returns>
        public int FindIndex(T[] sortedData, T item)
        {
            int n = sortedData.Length;
            int step = Math.Sqrt(n);
            int prev = 0;

            while (item.CompareTo(sortedData[Math.Min(step, n)-1]) > 0) 
            { 
                prev = step; 
                step += Math.Sqrt(n); 
                if (prev >= n) 
                    return -1; 
            } 

            while (item.CompareTo(sortedData[prev]) > 0) 
            { 
                prev++; 
                if (prev == Math.Min(step, n)) 
                    return -1; 
            } 

            if (item.CompareTo(sortedData[prev]) == 0) 
                return prev; 
            
            return -1;
        }
    }
}
