using System;
using System.Collections.Generic;

namespace DataStructures.Heap
{
    /// <summary>
    /// A generic implementation of a binary heap.
    /// </summary>
    /// <remarks>
    /// A binary heap is a complete binary tree that satisfies the heap property;
    /// that is every node in the tree compares greater/less than or equal to its left and right
    /// child nodes. Note that this is different from a binary search tree, where every node
    /// must be the largest/smallest node of all of its children.
    /// Although binary heaps are not very efficient, they are (probably) the simpliest heaps
    /// to understand and implement.
    /// More information: https://en.wikipedia.org/wiki/Binary_heap .
    /// </remarks>
    /// <typeparam name="T">Type of elements in binary heap.</typeparam>
    public class BinaryHeap<T>
    {
        /// <summary>
        /// Comparer to use when comparing elements.
        /// </summary>
        private readonly Comparer<T> comparer;

        /// <summary>
        /// List to hold the elements of the heap.
        /// </summary>
        private readonly List<T> data;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryHeap{T}"/> class.
        /// </summary>
        public BinaryHeap()
        {
            data = new List<T>();
            comparer = Comparer<T>.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryHeap{T}"/> class with a custom comparision function.
        /// </summary>
        /// <param name="customComparer">The custom comparing function to use to compare elements.</param>
        public BinaryHeap(Comparer<T> customComparer)
        {
            data = new List<T>();
            comparer = customComparer;
        }

        /// <summary>
        /// Gets the number of elements in the heap.
        /// </summary>
        public int Count => data.Count;

        /// <summary>
        /// Add an element to the binary heap.
        /// </summary>
        /// <remarks>
        /// Adding to the heap is done by append the element to the end of the backing list,
        /// and pushing the added element up until the heap property is restored.
        /// </remarks>
        /// <param name="element">The element to add to the heap.</param>
        /// <exception cref="ArgumentException">Thrown if element is already in heap.</exception>
        public void Push(T element)
        {
            data.Add(element);
            HeapifyUp(data.Count - 1);
        }

        /// <summary>
        /// Remove the top/root of the binary heap (ie: the largest/smallest element).
        /// </summary>
        /// <remarks>
        /// Removing from the heap is done by swapping the top/root with the last element in
        /// the backing list, removing the last element, and pushing the new root down
        /// until the heap property is restored.
        /// </remarks>
        /// <returns>The top/root of the heap.</returns>
        /// <exception cref="InvalidOperationException">Thrown if heap is empty.</exception>
        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Heap is empty!");
            }

            var elem = data[0];
            data[0] = data[data.Count - 1];
            data.RemoveAt(data.Count - 1);
            HeapifyDown(0);

            return elem;
        }

        /// <summary>
        /// Return the top/root of the heap without removing it.
        /// </summary>
        /// <returns>The top/root of the heap.</returns>
        /// <exception cref="InvalidOperationException">Thrown if heap is empty.</exception>
        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Heap is empty!");
            }

            return data[0];
        }

        /// <summary>
        /// Returns element if it compares larger to the top/root of the heap, else
        /// inserts element into the heap and returns the top/root of the heap.
        /// </summary>
        /// <param name="element">The element to check/insert.</param>
        /// <returns>element if element compares larger than top/root of heap, top/root of heap otherwise.</returns>
        public T PushPop(T element)
        {
            if (Count == 0)
            {
                return element;
            }

            if (comparer.Compare(element, data[0]) < 0)
            {
                var tmp = data[0];
                data[0] = element;
                HeapifyDown(0);
                return tmp;
            }
            else
            {
                return element;
            }
        }

        /// <summary>
        /// Check if element is in the heap.
        /// </summary>
        /// <param name="element">The element to check for.</param>
        /// <returns>true if element is in the heap, false otherwise.</returns>
        public bool Contains(T element) => data.Contains(element);

        /// <summary>
        /// Remove an element from the heap.
        /// </summary>
        /// <remarks>
        /// In removing an element from anywhere in the heap, we only need to push down or up
        /// the replacement value depending on how the removed value compares to its
        /// replacement value.
        /// </remarks>
        /// <param name="element">The element to remove from the heap.</param>
        /// <exception cref="ArgumentException">Thrown if element is not in heap.</exception>
        public void Remove(T element)
        {
            var idx = data.IndexOf(element);

            if (idx == -1)
            {
                throw new ArgumentException($"{element} not in heap!");
            }

            Swap(idx, data.Count - 1);
            var tmp = data[data.Count - 1];
            data.RemoveAt(data.Count - 1);

            if (idx < data.Count)
            {
                if (comparer.Compare(tmp, data[idx]) > 0)
                {
                    HeapifyDown(idx);
                }
                else
                {
                    HeapifyUp(idx);
                }
            }
        }

        /// <summary>
        /// Swap the elements in the heap array at the given indices.
        /// </summary>
        /// <param name="idx1">First index.</param>
        /// <param name="idx2">Second index.</param>
        private void Swap(int idx1, int idx2)
        {
            var tmp = data[idx1];
            data[idx1] = data[idx2];
            data[idx2] = tmp;
        }

        /// <summary>
        /// Recursive function to restore heap properties.
        /// </summary>
        /// <remarks>
        /// Restores heap property by swapping the element at <paramref name="elemIdx"/>
        /// with its parent if the element compares greater than its parent.
        /// </remarks>
        /// <param name="elemIdx">The element to check with its parent.</param>
        private void HeapifyUp(int elemIdx)
        {
            var parent = (elemIdx - 1) / 2;

            if (parent >= 0 && comparer.Compare(data[elemIdx], data[parent]) > 0)
            {
                Swap(elemIdx, parent);
                HeapifyUp(parent);
            }
        }

        /// <summary>
        /// Recursive function to restore heap properties.
        /// </summary>
        /// <remarks>
        /// Restores heap property by swapping the element at <paramref name="elemIdx"/>
        /// with the larger of its children.
        /// </remarks>
        /// <param name="elemIdx">The element to check with its children.</param>
        private void HeapifyDown(int elemIdx)
        {
            var left = (2 * elemIdx) + 1;
            var right = (2 * elemIdx) + 2;

            var leftLargerThanElem = left < Count && comparer.Compare(data[elemIdx], data[left]) < 0;
            var rightLargerThanElem = right < Count && comparer.Compare(data[elemIdx], data[right]) < 0;
            var leftLargerThanRight = left < Count && right < Count && comparer.Compare(data[left], data[right]) > 0;

            if (leftLargerThanElem && leftLargerThanRight)
            {
                Swap(elemIdx, left);
                HeapifyDown(left);
            }
            else if (rightLargerThanElem && !leftLargerThanRight)
            {
                Swap(elemIdx, right);
                HeapifyDown(right);
            }
            else
            {
                // if left and right child values compare smaller than the current value, the heap property has been restored.
                // no need to do anything else.
            }
        }
    }
}
