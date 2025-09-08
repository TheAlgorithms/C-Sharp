namespace DataStructures.LinkedList.CircularLinkedList
{
    /// <summary>
    /// CircularLinkedList.
    /// @author Mohit Singh. <a href="https://github.com/mohit-gogitter">mohit-gogitter</a>
    /// </summary>
    /// <typeparam name="T">The generic type parameter.</typeparam>
    public class CircularLinkedList<T>
    {
        /// <summary>
        /// Points to the last node in the Circular Linked List.
        /// </summary>
        private CircularLinkedListNode<T>? tail;

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularLinkedList{T}"/> class.
        /// </summary>
        public CircularLinkedList()
        {
            tail = null;
        }

        /// <summary>
        /// Gets the head node (tail.Next) of the Circular Linked List.
        /// </summary>
        public CircularLinkedListNode<T>? GetHead()
        {
            return tail?.Next;
        }

        /// <summary>
        /// Determines whether the Circular Linked List is empty.
        /// </summary>
        /// <returns>True if the list is empty; otherwise, false.</returns>
        public bool IsEmpty()
        {
            return tail == null;
        }

        /// <summary>
        /// Inserts a new node at the beginning of the Circular Linked List.
        /// </summary>
        /// <param name="data">The data to insert into the new node.</param>
        public void InsertAtBeginning(T data)
        {
            var newNode = new CircularLinkedListNode<T>(data);
            if (IsEmpty())
            {
                tail = newNode;
                tail.Next = tail;
            }
            else
            {
                newNode.Next = tail!.Next;
                tail.Next = newNode;
            }
        }

        /// <summary>
        /// Inserts a new node at the end of the Circular Linked List.
        /// </summary>
        /// <param name="data">The data to insert into the new node.</param>
        public void InsertAtEnd(T data)
        {
            var newNode = new CircularLinkedListNode<T>(data);
            if (IsEmpty())
            {
                tail = newNode;
                tail.Next = tail;
            }
            else
            {
                newNode.Next = tail!.Next;
                tail.Next = newNode;
                tail = newNode;
            }
        }

        /// <summary>
        /// Inserts a new node after a specific value in the list.
        /// </summary>
        /// <param name="value">The value to insert the node after.</param>
        /// <param name="data">The data to insert into the new node.</param>
        public void InsertAfter(T value, T data)
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("List is empty.");
            }

            var current = tail!.Next;
            do
            {
                if (current!.Data!.Equals(value))
                {
                    var newNode = new CircularLinkedListNode<T>(data);
                    newNode.Next = current.Next;
                    current.Next = newNode;

                    return;
                }

                current = current.Next;
            }
            while (current != tail.Next);
        }

        /// <summary>
        /// Deletes a node with a specific value from the list.
        /// </summary>
        /// <param name="value">The value of the node to delete.</param>
        public void DeleteNode(T value)
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("List is empty.");
            }

            var current = tail!.Next;
            var previous = tail;

            do
            {
                if (current!.Data!.Equals(value))
                {
                    if (current == tail && current.Next == tail)
                    {
                        tail = null;
                    }
                    else if (current == tail)
                    {
                        previous!.Next = tail.Next;
                        tail = previous;
                    }
                    else if (current == tail.Next)
                    {
                        tail.Next = current.Next;
                    }
                    else
                    {
                        previous!.Next = current.Next;
                    }

                    return;
                }

                previous = current;
                current = current.Next;
            }
            while (current != tail!.Next);
        }
    }
}
