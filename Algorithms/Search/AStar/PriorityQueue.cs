using System;
using System.Collections.Generic;

namespace AStar
{
    /// <summary>
    /// Generic Priority Queue.
    /// List based.
    /// </summary>
    /// <typeparam name="T">The type that will be stored.
    /// Has to be IComparable of T.</T></typeparam>
    public class PriorityQueue<T>
        where T : IComparable<T>
    {
        // The underlying structure.
        private readonly List<T> _list;

        private readonly bool _isDescending;

        /// <summary>
        /// Creates a new instance of PriorityQueue.
        /// </summary>
        public PriorityQueue() => _list = new List<T>();

        /// <summary>
        /// Creates a new instance of PriorityQueue.
        /// </summary>
        /// <param name="isDescending">Should Reverse Sort order? Default: false.</param>
        public PriorityQueue(bool isDescending)
            : this() => _isDescending = isDescending;

        /// <summary>
        /// Creates a new instance of PriorityQueue.
        /// </summary>
        /// <param name="capacity">Initial Capacity.</param>
        public PriorityQueue(int capacity)
            : this(capacity, false)
        {
        }

        /// <summary>
        /// Creates a new instance of PriorityQueue.
        /// </summary>
        /// <param name="collection">Internal Data.</param>
        public PriorityQueue(IEnumerable<T> collection)
            : this(collection, false)
        {
        }

        /// <summary>
        /// Creates a new instance of PriorityQueue.
        /// </summary>
        /// <param name="capacity">Initial Capacity.</param>
        /// <param name="isDescending">Should Reverse Sort order? Default: false.</param>
        public PriorityQueue(int capacity, bool isDescending)
        {
            _list = new List<T>(capacity);
            _isDescending = isDescending;
        }

        /// <summary>
        /// Creates a new instance of PriorityQueue.
        /// </summary>
        /// <param name="collection">Internal Data.</param>
        /// <param name="isDescending">Should Reverse Sort order? Default: false.</param>
        public PriorityQueue(IEnumerable<T> collection, bool isDescending)
            : this()
        {
            _isDescending = isDescending;
            foreach (var item in collection)
            {
                Enqueue(item);
            }
        }

        /// <summary>
        /// Gets Number of enqueued items.
        /// </summary>
        public int Count => _list.Count;

        /// <summary>
        /// Enqueues an item into the Queue.
        /// </summary>
        /// <param name="x">The item to Enqueue.</param>
        public void Enqueue(T x)
        {
            _list.Add(x);
            int i = Count - 1; // Position of x

            while (i > 0)
            {
                int p = (i - 1) / 2; // Start at half of i
                if ((_isDescending ? -1 : 1) * _list[p].CompareTo(x) <= 0)
                {
                    break;
                }

                _list[i] = _list[p]; // Put P to position of i
                i = p; // I = (I-1)/2
            }

            if (Count > 0)
            {
                _list[i] = x; // If while loop way executed at least once(X got replaced by some p), add it to the list
            }
        }

        /// <summary>
        /// Dequeues the item at the end of the queue.
        /// </summary>
        /// <returns>The dequeued item.</returns>
        public T Dequeue()
        {
            T target = Peek(); // Get first in list
            T root = _list[Count - 1]; // Hold last of the list
            _list.RemoveAt(Count - 1); // But remove it from the list

            int i = 0;
            while (i * 2 + 1 < Count)
            {
                int a = i * 2 + 1; // Every second entry starting by 1
                int b = i * 2 + 2; // Every second entries neighbour
                int c = b < Count && (_isDescending ? -1 : 1) * _list[b].CompareTo(_list[a]) < 0 ? b : a; // Wether B(B is in range && B is smaller than A) or A

                if ((_isDescending ? -1 : 1) * _list[c].CompareTo(root) >= 0)
                {
                    break;
                }

                _list[i] = _list[c];
                i = c;
            }

            if (Count > 0)
            {
                _list[i] = root;
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

            return _list[0];
        }

        /// <summary>
        /// Clears the Queue.
        /// </summary>
        public void Clear() => _list.Clear();

        /// <summary>
        /// Returns the Internal Data.
        /// </summary>
        /// <returns>The internal data structure.</returns>
        public List<T> GetData() => _list;
    }
}
