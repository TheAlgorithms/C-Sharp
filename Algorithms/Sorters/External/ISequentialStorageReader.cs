using System;

namespace Algorithms.Sorters.External;

public interface ISequentialStorageReader<out T> : IDisposable
{
    T Read();
}
