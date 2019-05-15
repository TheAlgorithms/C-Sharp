using System.Collections.Generic;

namespace Algorithms.Sorters
{
    interface ISorter<T>
    {
        void Sort(T[] array, IComparer<T> comparer);
    }
}
