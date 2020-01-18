using System;
using System.Collections.Generic;
using Utilities.Exceptions;

namespace DataStructures.DoublyLinkedList
{
    /// <summary>
    /// Similar to a Singly Linked List but each node contains a refenrence to the previous node in the list.
    /// <see cref="System.Collections.Generic.LinkedList{T}"/> is a doubly linked list.
    ///
    /// Compared to singly linked lists it can be traversed forwards and backwards.
    /// Adding a node to a doubly linked list is simpler because ever node contains a reference to the previous node.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    public class DoublyLinkedList<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoublyLinkedList{T}"/> class.
        /// </summary>
        /// <param name="data"> Data of the original head of the list.</param>
        public DoublyLinkedList(T data)
        {
            Head = new DoublyLinkedListNode<T>(data);
            Tail = Head;
            Count = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoublyLinkedList{T}"/> class from an enumerable.
        /// </summary>
        /// <param name="data"> Enumerable of data to be stored in the list.</param>
        public DoublyLinkedList(IEnumerable<T> data)
        {
            foreach (var d in data)
            {
                Add(d);
            }
        }

        /// <summary>
        /// Gets the amount of nodes in the list.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets or sets the first node of the list.
        /// </summary>
        private DoublyLinkedListNode<T>? Head { get; set; }

        /// <summary>
        /// Gets or sets the last node of the list.
        /// </summary>
        private DoublyLinkedListNode<T>? Tail { get; set; }

        /// <summary>
        /// Replaces the Head of the list with the new value.
        /// </summary>
        /// <param name="data"> Value for the new Head of the list.</param>
        /// <returns>The new Head node.</returns>
        public DoublyLinkedListNode<T> AddHead(T data)
        {
            var node = new DoublyLinkedListNode<T>(data);

            if (Head is null)
            {
                Head = node;
                Tail = node;
                Count = 1;
                return node;
            }

            Head.Previous = node;
            node.Next = Head;
            Head = node;
            Count++;
            return node;
        }

        /// <summary>
        /// Adds a new value at the end of the list.
        /// </summary>
        /// <param name="data"> New value to be added to the list.</param>
        /// <returns>The new node created based on the new value.</returns>
        public DoublyLinkedListNode<T> Add(T data)
        {
            if (Head is null)
            {
                return AddHead(data);
            }

            var node = new DoublyLinkedListNode<T>(data);
            Tail!.Next = node;
            node.Previous = Tail;
            Tail = node;
            Count++;
            return node;
        }

        /// <summary>
        /// Adds a new value after an existing node.
        /// </summary>
        /// <param name="data"> New value to be added to the list.</param>
        /// <param name="existingNode"> An existing node in the list.</param>
        /// <returns>The new node created based on the new value.</returns>
        public DoublyLinkedListNode<T> AddAfter(T data, DoublyLinkedListNode<T> existingNode)
        {
            if (existingNode == Tail)
            {
                return Add(data);
            }

            var node = new DoublyLinkedListNode<T>(data);
            node.Next = existingNode.Next;
            node.Previous = existingNode;
            existingNode.Next = node;

            if (existingNode.Next != null)
            {
                existingNode.Next.Previous = node;
            }

            Count++;
            return node;
        }

        /// <summary>
        /// Gets an enumerable based on the data in the list.
        /// </summary>
        /// <returns>The data in the list in an IEnumerable. It can used to create a list or an array with LINQ.</returns>
        public IEnumerable<T> GetData()
        {
            var current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        /// <summary>
        /// Gets an enumerable based on the data in the list reversed.
        /// </summary>
        /// <returns>The data in the list in an IEnumerable. It can used to create a list or an array with LINQ.</returns>
        public IEnumerable<T> GetDataReversed()
        {
            var current = Tail;
            while (current != null)
            {
                yield return current.Data;
                current = current.Previous;
            }
        }

        /// <summary>
        /// Reverses the list. Because of how doubly linked list are structured this is not a complex action.
        /// </summary>
        public void Reverse()
        {
            DoublyLinkedListNode<T>? current = Head;
            DoublyLinkedListNode<T>? temp = null;

            while (current != null)
            {
                temp = current.Previous;
                current.Previous = current.Next;
                current.Next = temp;
                current = current.Previous;
            }

            Tail = Head;

            // temp can be null on empty list
            if (temp != null)
            {
                Head = temp.Previous;
            }
        }

        /// <summary>
        /// Looks for a node in the list that contains the value of the parameter.
        /// </summary>
        /// <param name="data"> Value to be looked for in a node.</param>
        /// <returns>The node in the list the has the paramater as a value or null if not found.</returns>
        public DoublyLinkedListNode<T> Find(T data)
        {
            var current = Head;
            while (current != null)
            {
                if ((current.Data is null && data is null) || (current.Data != null && current.Data.Equals(data)))
                {
                    return current;
                }

                current = current.Next;
            }

            throw new ItemNotFoundException();
        }

        /// <summary>
        /// Looks for a node in the list that contains the value of the parameter.
        /// </summary>
        /// <param name="position"> Position in the list.</param>
        /// <returns>The node in the list the has the paramater as a value or null if not found.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when position is negative or out range of the list.</exception>
        public DoublyLinkedListNode<T> GetAt(int position)
        {
            if (position < 0 || position >= Count)
            {
                throw new ArgumentOutOfRangeException($"Max count is {Count}");
            }

            var current = Head;
            for (var i = 0; i < position; i++)
            {
                current = current!.Next;
            }

            return current ?? throw new ArgumentOutOfRangeException($"{nameof(position)} must be an index in the list");
        }

        /// <summary>
        /// Removes the Head and replaces it with the second node in the list.
        /// </summary>
        public void RemoveHead()
        {
            if (Head is null)
            {
                throw new InvalidOperationException();
            }

            Head = Head.Next;
            if (Head is null)
            {
                Tail = null;
                Count = 0;
                return;
            }

            Head.Previous = null;
            Count--;
        }

        /// <summary>
        /// Removes the last node in the list.
        /// </summary>
        public void Remove()
        {
            if (Tail is null)
            {
                throw new InvalidOperationException("Cannot prune empty list");
            }

            Tail = Tail.Previous;
            if (Tail is null)
            {
                Head = null;
                Count = 0;
                return;
            }

            Tail.Next = null;
            Count--;
        }

        /// <summary>
        /// Removes specific node.
        /// </summary>
        /// <param name="node"> Node to be removed.</param>
        public void RemoveNode(DoublyLinkedListNode<T> node)
        {
            if (node == Head)
            {
                RemoveHead();
                return;
            }

            if (node == Tail)
            {
                Remove();
                return;
            }

            if (node.Previous is null || node.Next is null)
            {
                throw new ArgumentException($"{nameof(node)} cannot have Previous or Next null if it's an internal node");
            }

            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
            Count--;
        }

        /// <summary>
        /// Removes a node that contains the data from the parameter.
        /// </summary>
        /// <param name="data"> Data to be removed form the list.</param>
        public void Remove(T data)
        {
            var node = Find(data);
            RemoveNode(node);
        }

        /// <summary>
        /// Looks for the index of the node with the parameter as data.
        /// </summary>
        /// <param name="data"> Data to look for.</param>
        /// <returns>Returns the index of the node if it is found or -1 if the node is not found.</returns>
        public int IndexOf(T data)
        {
            var current = Head;
            var index = 0;
            while (current != null)
            {
                if ((current.Data is null && data is null) || (current.Data != null && current.Data.Equals(data)))
                {
                    return index;
                }

                index++;
                current = current.Next;
            }

            return -1;
        }

        /// <summary>
        /// List contains a node that has the parameter as data.
        /// </summary>
        /// <param name="data"> Node to be removed.</param>
        /// <returns>True if the node is found. False if it isn't.</returns>
        public bool Contains(T data)
        {
            return IndexOf(data) != -1;
        }
    }
}
