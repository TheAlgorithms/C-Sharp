using System.Collections.Generic;

namespace Algorithms.Sorters
{
    internal interface ISorter<T>
    {
        void Sort(T[] array, IComparer<T> comparer);
    }
}
