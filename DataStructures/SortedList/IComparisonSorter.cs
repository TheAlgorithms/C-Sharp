using System.Collections.Generic;

namespace DataStructures.SortedList
{
    public interface IComparisonSorter<T>
    {
        void Sort(List<T> list, IComparer<T> comparer);
    }
}
