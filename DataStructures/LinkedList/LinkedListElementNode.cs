namespace DataStructures.LinkedList
{
    /// <summary>
    /// TODO.
    /// </summary>
    /// <typeparam name="T">TODO. 2.</typeparam>
    public class LinkedListElementNode<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedListElementNode{T}"/> class.
        /// TODO.
        /// </summary>
        /// <param name="data">TODO. 2.</param>
        public LinkedListElementNode(T data)
        {
            Data = data;
            Next = null;
        }

        /// <summary>
        /// Gets or sets tODO. TODO.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets tODO. TODO.
        /// </summary>
        public LinkedListElementNode<T> Next { get; set; }
    }
}
