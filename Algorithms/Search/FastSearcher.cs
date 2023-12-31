using System;
using Utilities.Exceptions;

namespace Algorithms.Search;

/// <summary>
///     The idea: you could combine the advantages from both binary-search and interpolation search algorithm.
///     Time complexity:
///     worst case: Item couldn't be found: O(log n),
///     average case: O(log log n),
///     best case: O(1).
///     Note: This algorithm is recursive and the array has to be sorted beforehand.
/// </summary>
public class FastSearcher
{
    /// <summary>
    ///     Finds index of first item in array that satisfies specified term
    ///     throws ItemNotFoundException if the item couldn't be found.
    /// </summary>
    /// <param name="array">Span of sorted numbers which will be used to find the item.</param>
    /// <param name="item">Term to check against.</param>
    /// <returns>Index of first item that satisfies term.</returns>
    /// <exception cref="ItemNotFoundException"> Gets thrown when the given item couldn't be found in the array.</exception>
    public int FindIndex(Span<int> array, int item)
    {
        if (array.Length == 0)
        {
            throw new ItemNotFoundException();
        }

        if (item < array[0] || item > array[^1])
        {
            throw new ItemNotFoundException();
        }

        if (array[0] == array[^1])
        {
            return item == array[0] ? 0 : throw new ItemNotFoundException();
        }

        var (left, right) = ComputeIndices(array, item);
        var (from, to) = SelectSegment(array, left, right, item);

        return from + FindIndex(array.Slice(from, to - from + 1), item);
    }

    private (int left, int right) ComputeIndices(Span<int> array, int item)
    {
        var indexBinary = array.Length / 2;

        int[] section =
        {
            array.Length - 1,
            item - array[0],
            array[^1] - array[0],
        };
        var indexInterpolation = section[0] * section[1] / section[2];

        // Left is min and right is max of the indices
        return indexInterpolation > indexBinary
            ? (indexBinary, indexInterpolation)
            : (indexInterpolation, indexBinary);
    }

    private (int from, int to) SelectSegment(Span<int> array, int left, int right, int item)
    {
        if (item < array[left])
        {
            return (0, left - 1);
        }

        if (item < array[right])
        {
            return (left, right - 1);
        }

        return (right, array.Length - 1);
    }
}
