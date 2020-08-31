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
        private const int DefaultCapacity = 10;

        /// <summary>
        /// <see cref="Array"/> based stack.
        /// </summary>
        private T[] stack;

        /// <summary>
        /// How many items are in the stack right now.
        /// </summary>
        private int count;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayBasedStack{T}"/> class.
        /// </summary>
        public ArrayBasedStack() => stack = new T[DefaultCapacity];

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
        public int Count => count;

        /// <summary>
        /// Gets or sets the Capacity of the <see cref="ArrayBasedStack{T}"/>.
        /// </summary>
        public int Capacity
        {
            get
            {
                return stack.Length;
            }

            set
            {
                Array.Resize(ref stack, value);
            }
        }

        /// <summary>
        /// Removes all items from the <see cref="ArrayBasedStack{T}"/>.
        /// </summary>
        public void Clear() => count = 0;

        /// <summary>
        /// Determines whether an element is in the <see cref="ArrayBasedStack{T}"/>.
        /// </summary>
        /// <param name="item">The item to locate in the <see cref="ArrayBasedStack{T}"/>.</param>
        /// <returns>True, if the item is in the stack.</returns>
        public bool Contains(T item) => Array.IndexOf(stack, item, 0, count) > -1;

        /// <summary>
        /// Returns the item at the top of the <see cref="ArrayBasedStack{T}"/> without removing it.
        /// </summary>
        /// <returns>The item at the top of the <see cref="ArrayBasedStack{T}"/>.</returns>
        public T Peek() => stack[count - 1];

        /// <summary>
        /// Removes and returns the item at the top of the <see cref="ArrayBasedStack{T}"/>.
        /// </summary>
        /// <returns>The item removed from the top of the <see cref="ArrayBasedStack{T}"/>.</returns>
        public T Pop()
        {
            var item = stack[count - 1];
            count = count - 1;
            return item;
        }

        /// <summary>
        /// Inserts an item at the top of the <see cref="ArrayBasedStack{T}"/>.
        /// </summary>
        /// <param name="item">The item to push onto the <see cref="ArrayBasedStack{T}"/>.</param>
        public void Push(T item)
        {
            if (count == Capacity)
            {
                Capacity = Capacity * 2;
            }

            stack[count] = item;
            count = count + 1;
        }
    }
}
