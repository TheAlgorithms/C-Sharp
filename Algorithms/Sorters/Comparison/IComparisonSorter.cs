using System.Collections.Generic;

namespace Algorithms.Sorters.Comparison
{
    /// <summary>
    /// Sorts array in ascending order using comparison sort.
    /// </summary>
    /// <typeparam name="T">Type of array item.</typeparam>
    public interface IComparisonSorter<T>
    {
        /// <summary>
        /// Sorts array in ascending order.
        /// </summary>
        /// <param name="array">Array to sort.</param>
        /// <param name="comparer">Comparer to compare items of <paramref name="array"/>.</param>
        void Sort(T[] array, IComparer<T> comparer);
    }
}
