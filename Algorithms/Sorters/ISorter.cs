using System.Collections.Generic;

namespace Algorithms.Sorts
{
    interface ISorter<T>
    {
        void Sort(T[] array, IComparer<T> comparer);
    }
}
