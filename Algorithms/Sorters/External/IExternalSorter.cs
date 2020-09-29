using System;
using System.Collections.Generic;

namespace Algorithms.Sorters.External
{
    public interface IExternalSorter<T>
    {
        /// <summary>
        /// Sorts elements in sequential storage in ascending order.
        /// </summary>
        /// <param name="mainMemory">Memory that contains array to sort and will contain the result.</param>
        /// <param name="temporaryMemory">Temporary memory for working purposes.</param>
        void Sort(ISequentialStorage<T> mainMemory, ISequentialStorage<T> temporaryMemory, IComparer<T> comparer);
    }

    public interface ISequentialStorage<T>
    {
        public int Length { get; }

        ISequentialStorageReader<T> GetReader();

        ISequentialStorageWriter<T> GetWriter();
    }

    public interface ISequentialStorageWriter<in T> : IDisposable
    {
        void Write(T value);
    }

    public interface ISequentialStorageReader<out T> : IDisposable
    {
        T Read();
    }
}
