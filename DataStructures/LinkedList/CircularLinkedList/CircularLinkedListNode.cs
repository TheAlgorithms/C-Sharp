namespace DataStructures.LinkedList.CircularLinkedList
{
    /// <summary>
    /// Represents a node in the Circular Linked List.
    /// Each node contains generic data and a reference to the next node.
    /// </summary>
    /// <typeparam name="T">The type of the data stored in the node.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CircularLinkedListNode{T}"/> class.
    /// </remarks>
    /// <param name="data">The data to be stored in the node.</param>
    public class CircularLinkedListNode<T>(T data)
    {
        /// <summary>
        /// Gets or sets the data for the node.
        /// </summary>
        public T Data { get; set; } = data;

        /// <summary>
        /// Gets or sets the reference to the next node in the list.
        /// </summary>
        public CircularLinkedListNode<T>? Next { get; set; }
    }
}
