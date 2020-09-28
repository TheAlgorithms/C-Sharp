using System;

namespace DataStructures.BinarySearchTree
{
    /// <summary>
    /// Utility class to save a node.
    /// </summary>
     /// <typeparam name="T">Generic type taht have to be comparable.</typeparam>
    public class Node<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Node{T}"/> class.
        /// </summary>
        /// <param name="value">Key value of type <c>T</c>.</param>
        public Node(T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the left child of this node.
        /// </summary>
        public Node<T>? Left { get; set; }

        /// <summary>
        /// Gets or sets the right child of this node.
        /// </summary>
        public Node<T>? Right { get; set; }

        /// <summary>
        /// Gets or sets the value of this node.
        /// </summary>
        public T Value { get; set; }
    }
}
