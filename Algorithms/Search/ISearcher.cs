using System;

namespace Algorithms.Search
{
    public interface ISearcher<T>
    {
        T Find(T[] data, Func<T, bool> term);
        int FindIndex(T[] data, Func<T, bool> term);
    }
}
