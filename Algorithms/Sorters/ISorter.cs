using System.Collections.Generic;

namespace Algorithms.Sorters
{
    /// <summary>
    /// Sorts array in ascending order.
    /// </summary>
    /// <typeparam name="T">Type of array item.</typeparam>
    public interface ISorter<T>
    {
        /// <summary>
        /// Sorts array in ascending order.
        /// </summary>
        /// <param name="array">Array to sort.</param>
        /// <param name="comparer">Comparer to compare items of <paramref name="array"/>.</param>
        void Sort(T[] array, IComparer<T> comparer);
    }
}
