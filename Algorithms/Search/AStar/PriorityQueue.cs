using System;
using System.Collections.Generic;

namespace AStar
{
    /// <summary>
    /// Generic Priority Queue.
    /// List based.
    /// </summary>
    /// <typeparam name="T">The type that will be stored.
    /// Has to be IComparable of T.</typeparam>
    public class PriorityQueue<T>
        where T : IComparable<T>
    {
        // The underlying structure.
        private readonly List<T> list;

        private readonly bool isDescending;

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class.
        /// </summary>
        public PriorityQueue() => list = new List<T>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class.
        /// </summary>
        /// <param name="isDescending">Should Reverse Sort order? Default: false.</param>
        public PriorityQueue(bool isDescending)
            : this() => this.isDescending = isDescending;

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class.
        /// </summary>
        /// <param name="capacity">Initial Capacity.</param>
        public PriorityQueue(int capacity)
            : this(capacity, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class.
        /// </summary>
        /// <param name="collection">Internal Data.</param>
        public PriorityQueue(IEnumerable<T> collection)
            : this(collection, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class.
        /// </summary>
        /// <param name="capacity">Initial Capacity.</param>
        /// <param name="isDescending">Should Reverse Sort order? Default: false.</param>
        public PriorityQueue(int capacity, bool isDescending)
        {
            list = new List<T>(capacity);
            this.isDescending = isDescending;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class.
        /// </summary>
        /// <param name="collection">Internal Data.</param>
        /// <param name="isDescending">Should Reverse Sort order? Default: false.</param>
        public PriorityQueue(IEnumerable<T> collection, bool isDescending)
            : this()
        {
            this.isDescending = isDescending;
            foreach (var item in collection)
            {
                Enqueue(item);
            }
        }

        /// <summary>
        /// Gets Number of enqueued items.
        /// </summary>
        public int Count => list.Count;

        /// <summary>
        /// Enqueues an item into the Queue.
        /// </summary>
        /// <param name="x">The item to Enqueue.</param>
        public void Enqueue(T x)
        {
            list.Add(x);
            var i = Count - 1; // Position of x

            while (i > 0)
            {
                var p = (i - 1) / 2; // Start at half of i
                if ((isDescending ? -1 : 1) * list[p].CompareTo(x) <= 0)
                {
                    break;
                }

                list[i] = list[p]; // Put P to position of i
                i = p; // I = (I-1)/2
            }

            if (Count > 0)
            {
                list[i] = x; // If while loop way executed at least once(X got replaced by some p), add it to the list
            }
        }

        /// <summary>
        /// Dequeues the item at the end of the queue.
        /// </summary>
        /// <returns>The dequeued item.</returns>
        public T Dequeue()
        {
            var target = Peek(); // Get first in list
            var root = list[Count - 1]; // Hold last of the list
            list.RemoveAt(Count - 1); // But remove it from the list

            var i = 0;
            while (i * 2 + 1 < Count)
            {
                var a = i * 2 + 1; // Every second entry starting by 1
                var b = i * 2 + 2; // Every second entries neighbour
                var c = b < Count && (isDescending ? -1 : 1) * list[b].CompareTo(list[a]) < 0 ? b : a; // Wether B(B is in range && B is smaller than A) or A

                if ((isDescending ? -1 : 1) * list[c].CompareTo(root) >= 0)
                {
                    break;
                }

                list[i] = list[c];
                i = c;
            }

            if (Count > 0)
            {
                list[i] = root;
            }

            return target;
        }

        /// <summary>
        /// Returns the next element in the queue without dequeueing.
        /// </summary>
        /// <returns>The next element of the queue.</returns>
        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            return list[0];
        }

        /// <summary>
        /// Clears the Queue.
        /// </summary>
        public void Clear() => list.Clear();

        /// <summary>
        /// Returns the Internal Data.
        /// </summary>
        /// <returns>The internal data structure.</returns>
        public List<T> GetData() => list;
    }
}
