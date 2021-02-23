using System;
using System.Collections.Generic;

namespace DataStructures.Queue
{
    /// <summary>
    /// Implementation of a stack based queue. FIFO style.
    /// </summary>
    /// <remarks>
    /// Enqueue is O(1) and Dequeue is amortized O(1).
    /// </remarks>
    /// <typeparam name="T">Generic Type.</typeparam>
    public class StackBasedQueue<T>
    {
        private readonly Stack<T> input;
        private readonly Stack<T> output;

        /// <summary>
        /// Initializes a new instance of the <see cref="StackBasedQueue{T}"/> class.
        /// </summary>
        public StackBasedQueue()
        {
            input = new Stack<T>();
            output = new Stack<T>();
        }

        /// <summary>
        /// Clears the queue.
        /// </summary>
        public void Clear()
        {
            input.Clear();
            output.Clear();
        }

        /// <summary>
        /// Returns the first item in the queue and removes it from the queue.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
        public T Dequeue()
        {
            if (input.Count == 0 && output.Count == 0)
            {
                throw new InvalidOperationException("The queue contains no items.");
            }

            if (output.Count == 0)
            {
                while (input.Count > 0)
                {
                    var item = input.Pop();
                    output.Push(item);
                }
            }

            return output.Pop();
        }

        /// <summary>
        /// Returns a boolean indicating whether the queue is empty.
        /// </summary>
        public bool IsEmpty() => input.Count == 0 && output.Count == 0;

        /// <summary>
        /// Returns a boolean indicating whether the queue is full.
        /// </summary>
        public bool IsFull() => false;

        /// <summary>
        /// Returns the first item in the queue and keeps it in the queue.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
        public T Peek()
        {
            if (input.Count == 0 && output.Count == 0)
            {
                throw new InvalidOperationException("The queue contains no items.");
            }

            if (output.Count == 0)
            {
                while (input.Count > 0)
                {
                    var item = input.Pop();
                    output.Push(item);
                }
            }

            return output.Peek();
        }

        /// <summary>
        /// Adds an item at the last position in the queue.
        /// </summary>
        public void Enqueue(T item) => input.Push(item);
    }
}
