using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.ArrayBasedQueue.Abstractions
{
    public interface IQueueOperations<T> : IEnumerable<T>
    {
        void Clear();

        bool IsEmpty();

        void Enqueue(T item);

        T Dequeue();

        bool IsFull();

        T Peek();
    }
}
