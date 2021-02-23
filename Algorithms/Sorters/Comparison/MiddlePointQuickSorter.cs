using System.Collections.Generic;

namespace Algorithms.Sorters.Comparison
{
    /// <summary>
    /// Sorts arrays using quicksort (selecting middle point as a pivot).
    /// </summary>
    /// <typeparam name="T">Type of array element.</typeparam>
    public sealed class MiddlePointQuickSorter<T> : QuickSorter<T>
    {
        protected override T SelectPivot(T[] array, IComparer<T> comparer, int left, int right) => array[left + (right - left) / 2];
    }
}
