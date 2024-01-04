using System;
using System.Linq;

namespace Algorithms.Sorters.Integer;

/// <summary>
///     Counting sort is an algorithm for sorting a collection of objects according to keys that are small integers; that
///     is, it is an integer sorting algorithm. It operates by counting the number of objects that have each distinct key
///     value, and using arithmetic on those counts to determine the positions of each key value in the output sequence.
///     Its running time is linear in the number of items and the difference between the maximum and minimum key values, so
///     it is only suitable for direct use in situations where the variation in keys is not significantly greater than the
///     number of items. However, it is often used as a subroutine in another sorting algorithm, radix sort, that can
///     handle larger keys more efficiently.
/// </summary>
public class CountingSorter : IIntegerSorter
{
    /// <summary>
    ///     <para>
    ///         Sorts input array using counting sort algorithm.
    ///     </para>
    ///     <para>
    ///         Time complexity: O(n+k), where k is the range of the non-negative key values.
    ///     </para>
    ///     <para>
    ///         Space complexity: O(n+k), where k is the range of the non-negative key values.
    ///     </para>
    /// </summary>
    /// <param name="array">Input array.</param>
    public void Sort(int[] array)
    {
        if (array.Length == 0)
        {
            return;
        }

        var max = array.Max();
        var min = array.Min();
        var count = new int[max - min + 1];
        var output = new int[array.Length];
        for (var i = 0; i < array.Length; i++)
        {
            count[array[i] - min]++;
        }

        for (var i = 1; i < count.Length; i++)
        {
            count[i] += count[i - 1];
        }

        for (var i = array.Length - 1; i >= 0; i--)
        {
            output[count[array[i] - min] - 1] = array[i];
            count[array[i] - min]--;
        }

        Array.Copy(output, array, array.Length);
    }
}
