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
        public int FindIndex(int[] array, int item) => FindIndex(array, item, 0);

        private int FindIndex(int[] array, int item, int offset)
        {
            if (item < array[0] || item > array[array.Length - 1])
            {
                throw new ItemNotFoundException();
            }

            if (array[0] == array[array.Length - 1])
            {
                return item == array[0] ? offset : throw new ItemNotFoundException();
            }

            var indexBinary = array.Length / 2;

            int[] section =
            {
                array.Length - 1,
                item - array[0],
                array[array.Length - 1] - array[0],
            };

            var indexInterpolation = section[0] * section[1] / section[2];

            var (i1, i2) = indexBinary > indexInterpolation
                ? (indexInterpolation, indexBinary)
                : (indexBinary, indexInterpolation);

            int from, to;
            if (item == array[i1])
            {
                return offset + i1;
            }

            if (item == array[i2])
            {
                return offset + i2;
            }

            if (item < array[i1])
            {
                @from = 0;
                to = i1 - 1;
            }
            else if (item < array[i2])
            {
                @from = i1 + 1;
                to = i2 - 1;
            }
            else
            {
                @from = i2 + 1;
                to = array.Length - 1;
            }

            if (from >= to)
            {
                throw new ItemNotFoundException();
            }

            var segment = new int[to - from + 1];
            Array.Copy(array, from, segment, 0, segment.Length);
            return FindIndex(segment, item, offset + from);
        }
    }
}
