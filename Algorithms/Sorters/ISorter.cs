using System.Collections.Generic;

namespace Algorithms.Sorters
{
    public interface ISorter<T>
    {
        void Sort(T[] array, IComparer<T> comparer);
    }
}
