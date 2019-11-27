using System;

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
        private int[] array;

        /// <summary>
        /// Finds the index of the given item in the array.
        /// </summary>
        /// <param name="array">Defines the span of numbers.</param>
        /// <param name="item">The number to be found.</param>
        /// <returns>Returns the index of the item in the array.</returns>
        public int FindIndex(int[] array, int item)
        {
            this.array = array;
            int from = 0, to = array.Length - 1;
            return FindIndex(from, to, item);
        }

        /// <summary>
        /// Finds index of first item in array that satisfies specified term
        /// throws ItemNotFoundException if the item couldn't be found.
        /// </summary>
        /// <param name="sectionStartIndex">Defines the lower boundary of the section.</param>
        /// <param name="sectionEndIndex">Defines the upper boundary of the section.</param>
        /// <param name="item">Term to check against.</param>
        /// <returns>Index of first item that satisfies term.</returns>
        private int FindIndex(int sectionStartIndex, int sectionEndIndex, int item)
        {
            if (sectionStartIndex < sectionEndIndex)
            {
                var indexBinary = (sectionStartIndex + sectionEndIndex) / 2;

                int[] section =
                {
                 sectionEndIndex - sectionStartIndex,
                 item - array[sectionStartIndex],
                 array[sectionEndIndex] - array[sectionStartIndex],
                };
                if (section[2] == 0)
                {
                    section[2]++;
                }

                var indexInterpolation = sectionStartIndex + (section[0] * section[1] / section[2]);

                indexInterpolation = Math.Abs(indexInterpolation);
                indexBinary = Math.Abs(indexBinary);

                if (indexInterpolation > array[sectionEndIndex])
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

                if (item == array[indexBinary])
                {
                    return indexBinary;
                }
                else if (item == array[indexInterpolation])
                {
                    return indexInterpolation;
                }
                else if (item < array[indexBinary])
                {
                    return FindIndex(sectionStartIndex, indexBinary - 1, item);
                }
                else if (item < array[indexInterpolation])
                {
                    return FindIndex(indexBinary + 1, indexInterpolation - 1, item);
                }
                else
                {
                    return FindIndex(indexInterpolation + 1, sectionEndIndex, item);
                }
            }
            else if (sectionStartIndex == sectionEndIndex)
            {
                if (array[sectionStartIndex] == item)
                {
                    return sectionStartIndex;
                }
            }

            throw new ItemNotFoundException();
        }
    }
}
