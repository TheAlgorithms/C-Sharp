namespace DataStructures.SinglyLinkedList
{
    /// <summary>
    /// TODO.
    /// </summary>
    /// <typeparam name="T">TODO. 2.</typeparam>
    public class SinglyLinkedListNode<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SinglyLinkedListNode{T}"/> class.
        /// TODO.
        /// </summary>
        /// <param name="data">TODO. 2.</param>
        public SinglyLinkedListNode(T data)
        {
            Data = data;
            Next = null;
        }

        /// <summary>
        /// Gets elements from current LinkedList.
        /// </summary>
        public T Data { get; }

        /// <summary>
        /// Gets or sets tODO. TODO.
        /// </summary>
        public SinglyLinkedListNode<T>? Next { get; set; }
    }
}
