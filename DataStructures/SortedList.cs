using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    ///     Implementation of SortedList using binary search.
    /// </summary>
    /// <typeparam name="T">Generic Type.</typeparam>
    public class SortedList<T> : ICollection<T>, IReadOnlyCollection<T>
    {
        private readonly IComparer<T> comparer;
        private readonly List<T> memory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SortedList{T}" /> class. Uses a Comparer.Default for type T.
        /// </summary>
        public SortedList()
            : this(Comparer<T>.Default)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SortedList{T}" /> class.
        /// </summary>
        /// <param name="comparer">Comparer user for binary search.</param>
        public SortedList(IComparer<T> comparer)
        {
            memory = new List<T>();
            this.comparer = comparer;
        }

        /// <summary>
        ///     Adds new item to <see cref="SortedList{T}" /> instance, maintaining the order.
        /// </summary>
        /// <param name="item">An element to insert.</param>
        /// <exception cref="ArgumentException">Being thrown is <see cref="SortedList{T}" /> already contains an equal element.</exception>
        public void Add(T item)
        {
            var index = IndexFor(item, out var found);

            if (found)
            {
                throw new ArgumentException($"List already contains an item: {item}", nameof(item));
            }

            memory.Insert(index, item);
        }

        /// <summary>
        /// Removes all elements from <see cref="SortedList{T}" />.
        /// </summary>
        public void Clear()
            => memory.Clear();

        /// <summary>
        /// Indicates whether a <see cref="SortedList{T}" /> contains a certain element.
        /// </summary>
        /// <param name="item">An element to search.</param>
        /// <returns>true - <see cref="SortedList{T}" /> contains an element, otherwise - false.</returns>
        public bool Contains(T item)
        {
            _ = IndexFor(item, out var found);
            return found;
        }

        /// <summary>
        /// Removes a certain element from <see cref="SortedList{T}" />.
        /// </summary>
        /// <param name="item">An element to remove.</param>
        /// <returns>true - element is found and removed, otherwise false.</returns>
        public bool Remove(T item)
        {
            var index = IndexFor(item, out var found);

            if (found)
            {
                memory.RemoveAt(index);
            }

            return found;
        }

        public void CopyTo(T[] array, int arrayIndex)
            => memory.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator()
            => memory.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        int ICollection<T>.Count => memory.Count;

        public bool IsReadOnly => false;

        int IReadOnlyCollection<T>.Count => memory.Count;

        private int IndexFor(T item, out bool found)
        {
            var left = 0;
            var right = memory.Count;

            while (right - left > 0)
            {
                var mid = (left + right) / 2;

                switch (comparer.Compare(item, memory[mid]))
                {
                    case 0:
                        found = true;
                        return mid;
                    case > 0:
                        left = mid + 1;
                        break;
                    case < 0:
                        right = mid;
                        break;
                }
            }

            found = false;
            return left;
        }
    }
}
