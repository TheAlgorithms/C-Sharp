using System.Collections.Generic;

namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Heap sort is a comparison based sorting technique
///     based on Binary Heap data structure.
/// </summary>
/// <typeparam name="T">Input array type.</typeparam>
public class HeapSorter<T> : IComparisonSorter<T>
{
    /// <summary>
    ///     Sorts input array using heap sort algorithm.
    /// </summary>
    /// <param name="array">Input array.</param>
    /// <param name="comparer">Comparer type for elements.</param>
    public void Sort(T[] array, IComparer<T> comparer) => HeapSort(array, comparer);

    private static void HeapSort(IList<T> data, IComparer<T> comparer)
    {
        var heapSize = data.Count;
        for (var p = (heapSize - 1) / 2; p >= 0; p--)
        {
            MakeHeap(data, heapSize, p, comparer);
        }

        for (var i = data.Count - 1; i > 0; i--)
        {
            var temp = data[i];
            data[i] = data[0];
            data[0] = temp;

            heapSize--;
            MakeHeap(data, heapSize, 0, comparer);
        }
    }

    private static void MakeHeap(IList<T> input, int heapSize, int index, IComparer<T> comparer)
    {
        var rIndex = index;

        while (true)
        {
            var left = (rIndex + 1) * 2 - 1;
            var right = (rIndex + 1) * 2;
            var largest = left < heapSize && comparer.Compare(input[left], input[rIndex]) == 1 ? left : rIndex;

            // finds the index of the largest
            if (right < heapSize && comparer.Compare(input[right], input[largest]) == 1)
            {
                largest = right;
            }

            if (largest == rIndex)
            {
                return;
            }

            // process of reheaping / swapping
            var temp = input[rIndex];
            input[rIndex] = input[largest];
            input[largest] = temp;

            rIndex = largest;
        }
    }
}
