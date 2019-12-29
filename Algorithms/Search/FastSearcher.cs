using System;
using Utilities.Exceptions;

namespace Algorithms.Search
{
    /// <summary>
    /// The idea: you could combine the advantages from both binary-search and interpolation search algorithm.
    /// Time complexity:
    ///     worst case: Item couldn't be found: O(log n),
    ///     average case: O(log log n),
    ///     best case: O(1).
    /// Note: This algorithm is recursive and the array has to be sorted beforehand.
    /// </summary>
    public class FastSearcher
    {
        /// <summary>
        /// Finds index of first item in array that satisfies specified term
        /// throws ItemNotFoundException if the item couldn't be found.
        /// </summary>
        /// <param name="array">Span of sorted numbers which will be used to find the item.</param>
        /// <param name="item">Term to check against.</param>
        /// <returns>Index of first item that satisfies term.</returns>
        /// <exception cref="ItemNotFoundException"> Gets thrown when the given item couldn't be found in the array.</exception>
        public int FindIndex(int[] array, int item)
        {
            var indexBinary = array.Length / 2;

            int[] section =
            {
                 array.Length - 1,
                 item - array[0],
                 array[array.Length - 1] - array[0],
            };

            // prevents division by zero
            if (section[2] == 0)
            {
                section[2]++;
            }

            var indexInterpolation = section[0] * section[1] / section[2];

            indexInterpolation = Math.Abs(indexInterpolation);
            indexBinary = Math.Abs(indexBinary);

            if (indexInterpolation > array[array.Length - 1])
            {
                throw new ItemNotFoundException();
            }

            if (indexBinary > indexInterpolation)
            {
                // Swap
                var temp = indexBinary;
                indexBinary = indexInterpolation;
                indexInterpolation = temp;
            }

            int from, to;
            if (item == array[indexBinary])
            {
                return indexBinary;
            }

            if (item == array[indexInterpolation])
            {
                return indexInterpolation;
            }

            if (item < array[indexBinary])
            {
                @from = 0;
                to = indexBinary - 1;
            }
            else if (item < array[indexInterpolation])
            {
                @from = indexBinary + 1;
                to = indexInterpolation - 1;
            }
            else
            {
                @from = indexInterpolation + 1;
                to = array.Length - 1;
            }

            if (from >= to)
            {
                throw new ItemNotFoundException();
            }

            var segment = new ArraySegment<int>(array, from, to - 1).Array;
            return FindIndex(segment, item);
        }
    }
}
