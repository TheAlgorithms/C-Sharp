using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataStructures.ArrayBasedCircularQueue.Abstractions;
using DataStructures.ArrayBasedCircularQueue.Enumerators;

namespace DataStructures.ArrayBasedCircularQueue
{
    /// <summary>
    /// Circular Queue based on array implementation. FIFO style. It is also called ‘Ring Buffer’.
    /// </summary>
    /// <remarks>
    /// <para>Usages:</para>
    /// <para>Memory Management.</para>
    /// <para>Traffic system.</para>
    /// <para>CPU Scheduling.</para>
    /// </remarks>
    /// <typeparam name="T">Generic Type.</typeparam>
    public class CircularQueue<T> : IQueueOperations<T>
    {
        private readonly int size = 10;
        private int front = -1;
        private int rear = -1;
        private T[] queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularQueue{T}"/> class.
        /// </summary>
        public CircularQueue()
        {
            this.front = this.rear = -1;
            this.queue = new T[this.size];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularQueue{T}"/> class.
        /// </summary>
        public CircularQueue(int size)
        {
            this.front = this.rear = -1;
            this.size = size;
            this.queue = new T[this.size];
        }

        /// <summary>
        /// Clears the queue.
        /// </summary>
        public void Clear()
        {
            this.front = this.rear = -1;
            this.queue = new T[this.size];
        }

        /// <summary>
        /// Returns the first item in the queue and removes it from the queue.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
        public T Dequeue()
        {
            T item;
            if (this.IsEmpty())
            {
                throw new InvalidOperationException("Queue is Empty");
            }
            else if (this.front == this.rear)
            {
                item = this.queue[this.front];
                this.front = this.rear = -1;
            }
            else
            {
                item = this.queue[this.front];
                this.front = (this.front + 1) % this.size;
            }

            return item;
        }

        /// <summary>
        /// Adds an item at the last position in the queue.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the queue is full.</exception>
        public void Enqueue(T item)
        {
            if (this.IsFull())
            {
                throw new InvalidOperationException("Queue is Full");
            }
            else if (this.IsEmpty())
            {
                this.front = this.rear = 0;
                this.queue[this.rear] = item;
            }
            else
            {
                this.rear = (this.rear + 1) % this.size;
                this.queue[this.rear] = item;
            }
        }

        /// <summary>
        /// Returns Enumerator so that circular queue can be enumerable.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return new QueueEnumerator<T>(this.queue, this.front, this.rear, p => (p + 1) % this.size, (p, r) => p != r);
        }

        /// <summary>
        /// Returns a boolean indicating whether the queue is empty.
        /// </summary>
        public bool IsEmpty()
        {
            return this.front == -1 && this.rear == -1;
        }

        /// <summary>
        /// Returns a boolean indicating whether the queue is full.
        /// </summary>
        public bool IsFull()
        {
            return this.front == ((this.rear + 1) % this.size);
        }

        /// <summary>
        /// Returns the first item in the queue and keeps it in the queue.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
        public T Peek()
        {
            T item;
            if (this.IsEmpty())
            {
                throw new InvalidOperationException("Queue is Empty");
            }
            else
            {
                item = this.queue[this.front];
            }

            return item;
        }

        /// <summary>
        /// Returns Enumerator so that circular queue can be enumerable.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
