using System;
using System.Collections;
using System.Collections.Generic;
using DataStructures.ArrayBasedQueue.Abstractions;
using DataStructures.ArrayBasedQueue.Enumerators;

namespace DataStructures.ArrayBasedQueue
{
    /// <summary>
    /// Implementation of an array based queue. FIFO style.
    /// </summary>
    /// <typeparam name="T">Generic Type.</typeparam>
    public class ArrayBasedQueue<T> : IQueueOperations<T>
    {
        private readonly T[] queue;
        private readonly int capacity;
        private int front;
        private int rear;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayBasedQueue{T}"/> class.
        /// </summary>
        public ArrayBasedQueue(int capacity)
        {
            this.capacity = capacity;
            queue = new T[capacity];
            Clear();
        }

        /// <summary>
        /// Clears the queue.
        /// </summary>
        public void Clear() => front = rear = 0;

        /// <summary>
        /// Returns the first item in the queue and removes it from the queue.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
        public T Dequeue()
        {
            T item;
            if (IsEmpty())
            {
                throw new InvalidOperationException("There are no items in the queue.");
            }
            else if (rear - 1 == front)
            {
                item = this.queue[front];
                rear = front = 0;
            }
            else
            {
                item = this.queue[front++];
            }

            return item;
        }

        /// <summary>
        /// Returns a boolean indicating whether the queue is empty.
        /// </summary>
        public bool IsEmpty() => rear == 0 && front == 0;

        /// <summary>
        /// Returns a boolean indicating whether the queue is full.
        /// </summary>
        public bool IsFull() => rear >= this.queue.Length;

        /// <summary>
        /// Returns the first item in the queue and keeps it in the queue.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
        public T Peek()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("There are no items in the queue.");
            }

            return queue[front];
        }

        /// <summary>
        /// Adds an item at the last position in the queue.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the queue is full.</exception>
        public void Enqueue(T item)
        {
            if (IsFull())
            {
                throw new InvalidOperationException("The queue has reached its capacity.");
            }
            else
            {
                this.queue[rear++] = item;
            }
        }

        /// <summary>
        /// Returns Enumerator so that circular queue can be enumerable.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return new QueueEnumerator<T>(queue, front, rear, p => ++p, (p, r) => p < r);
        }

        /// <summary>
        /// Returns Enumerator so that circular queue can be enumerable.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
