using System;
using System.Collections.Generic;

namespace DataStructures.ArrayBasedStack
{
    /// <summary>
    /// Implementation of an array based stack. FIFO style.
    /// </summary>
    /// <typeparam name="T">Generic Type.</typeparam>
    public class ArrayBasedStack<T>
    {
        /// <summary>
        /// <see cref="Array"/> based stack.
        /// </summary>
        private T[] stack;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayBasedStack{T}"/> class.
        /// </summary>
        public ArrayBasedStack() => stack = Array.Empty<T>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayBasedStack{T}"/> class.
        /// </summary>
        /// <param name="item">Item to push onto the <see cref="ArrayBasedStack{T}"/>.</param>
        public ArrayBasedStack(T item)
        : this() => Push(item);

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayBasedStack{T}"/> class.
        /// </summary>
        /// <param name="items">Items to push onto the <see cref="ArrayBasedStack{T}"/>.</param>
        public ArrayBasedStack(IEnumerable<T> items)
        : this()
        {
            foreach (var item in items)
            {
                Push(item);
            }
        }

        /// <summary>
        /// Gets the number of elements on the <see cref="ArrayBasedStack{T}"/>.
        /// </summary>
        public int Count => stack.Length;

        /// <summary>
        /// Removes all items from the <see cref="ArrayBasedStack{T}"/>.
        /// </summary>
        public void Clear() => Array.Resize(ref stack, 0);

        /// <summary>
        /// Determines whether an element is in the <see cref="ArrayBasedStack{T}"/>.
        /// </summary>
        /// <param name="item">The item to locate in the <see cref="ArrayBasedStack{T}"/>.</param>
        /// <returns>True, if the item is in the stack.</returns>
        public bool Contains(T item) => Array.IndexOf(stack, item) > -1;

        /// <summary>
        /// Returns the item at the top of the <see cref="ArrayBasedStack{T}"/> without removing it.
        /// </summary>
        /// <returns>The item at the top of the <see cref="ArrayBasedStack{T}"/>.</returns>
        public T Peek() => stack[^1];

        /// <summary>
        /// Removes and returns the item at the top of the <see cref="ArrayBasedStack{T}"/>.
        /// </summary>
        /// <returns>The item removed from the top of the <see cref="ArrayBasedStack{T}"/>.</returns>
        public T Pop()
        {
            var item = stack[^1];
            Array.Resize(ref stack, stack.Length - 1);
            return item;
        }

        /// <summary>
        /// Inserts an item at the top of the <see cref="ArrayBasedStack{T}"/>.
        /// </summary>
        /// <param name="item">The item to push onto the <see cref="ArrayBasedStack{T}"/>.</param>
        public void Push(T item)
        {
            Array.Resize(ref stack, stack.Length + 1);
            stack[^1] = item;
        }
    }
}
