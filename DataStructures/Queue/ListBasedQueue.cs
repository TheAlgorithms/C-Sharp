using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Queue
{
    /// <summary>
    /// Implementation of a list based queue. FIFO style.
    /// </summary>
    /// <typeparam name="T">Generic Type.</typeparam>
    public class ListBasedQueue<T>
    {
        private readonly LinkedList<T> queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListBasedQueue{T}"/> class.
        /// </summary>
        public ListBasedQueue() => queue = new LinkedList<T>();

        /// <summary>
        /// Clears the queue.
        /// </summary>
        public void Clear()
        {
            queue.Clear();
        }

        /// <summary>
        /// Returns the first item in the queue and removes it from the queue.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
        public T Dequeue()
        {
            if (queue.First is null)
            {
                throw new InvalidOperationException("There are no items in the queue.");
            }

            var item = queue.First;
            queue.RemoveFirst();
            return item.Value;
        }

        /// <summary>
        /// Returns a boolean indicating whether the queue is empty.
        /// </summary>
        public bool IsEmpty()
        {
            return !queue.Any();
        }

        /// <summary>
        /// Returns a boolean indicating whether the queue is full.
        /// </summary>
        public bool IsFull()
        {
            return false;
        }

        /// <summary>
        /// Returns the first item in the queue and keeps it in the queue.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
        public T Peek()
        {
            if (queue.First is null)
            {
                throw new InvalidOperationException("There are no items in the queue.");
            }

            return queue.First.Value;
        }

        /// <summary>
        /// Adds an item at the last position in the queue.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the queue is full.</exception>
        public void Enqueue(T item)
        {
            queue.AddLast(item);
        }
    }
}
