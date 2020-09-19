using System;
using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    /// Implementation of a list based queue. LIFO style.
    /// </summary>
    /// <typeparam name="T">Generic Type.</typeparam>
    public class ArrayBasedQueue<T>
    {
        /// <summary>
        /// <see cref="List{T}"/> based queue.
        /// </summary>
        private readonly T[] queue;
        private int currentIndex;
        private int capacity;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayBasedQueue{T}"/> class.
        /// </summary>
        public ArrayBasedQueue(int capacity)
        {
            this.capacity = capacity;
            queue = new T[capacity];
        }

        /// <summary>
        /// Clears the queue.
        /// </summary>
        public void Clear()
        {
            currentIndex = 0;
        }

        /// <summary>
        /// Returns the first item in the queue and removes it from the queue.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
        public T Dequeue()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("There are no items in the queue.");
            }

            var item = queue[0];

            for (int i = 0; i < currentIndex - 1; i++)
            {
                queue[i] = queue[i + 1];
            }

            currentIndex--;

            return item;
        }

        /// <summary>
        /// Returns a boolean indicating whether the queue is empty.
        /// </summary>
        public bool IsEmpty()
        {
            return currentIndex == 0;
        }

        /// <summary>
        /// Returns a boolean indicating whether the queue is full.
        /// </summary>
        public bool IsFull()
        {
            return currentIndex >= capacity;
        }

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

            return queue[0];
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

            queue[currentIndex] = item;
            currentIndex++;
        }
    }
}
