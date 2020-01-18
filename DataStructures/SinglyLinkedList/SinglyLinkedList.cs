using System;
using System.Collections.Generic;

namespace DataStructures.SinglyLinkedList
{
    /// <summary>
    /// TODO.
    /// </summary>
    /// <typeparam name="T">TODO. 2.</typeparam>
    public class SinglyLinkedList<T>
    {
        // points to the start of the list
        private SinglyLinkedListNode<T>? Head { get; set; }

        /// <summary>
        /// Adds new node to the start of the list,
        /// time complexity: O(1),
        /// space complexity: O(1).
        /// </summary>
        /// <param name="data">Contents of newly added node.</param>
        /// <returns>Added list node.</returns>
        public SinglyLinkedListNode<T> AddFirst(T data)
        {
            var newListElement = new SinglyLinkedListNode<T>(data)
            {
                Next = Head,
            };

            Head = newListElement;
            return newListElement;
        }

        /// <summary>
        /// Adds new node to the end of the list,
        /// time complexity: O(n),
        /// space complexity: O(1),
        /// where n - number of nodes in the list.
        /// </summary>
        /// <param name="data">Contents of newly added node.</param>
        /// <returns>Added list node.</returns>
        public SinglyLinkedListNode<T> AddLast(T data)
        {
            var newListElement = new SinglyLinkedListNode<T>(data);

            // if head is null, the added element is the first, hence it is the head
            if (Head is null)
            {
                Head = newListElement;
                return newListElement;
            }

            // temp ListElement to avoid overwriting the original
            var tempElement = Head;

            // iterates through all elements
            while (tempElement.Next != null)
            {
                tempElement = tempElement.Next;
            }

            // adds the new element to the last one
            tempElement.Next = newListElement;
            return newListElement;
        }

        /// <summary>
        /// Returns element at index <paramref name="index"/> in the list.
        /// </summary>
        /// <param name="index">Index of an element to be returned.</param>
        /// <returns>Element at index <paramref name="index"/>.</returns>
        public T GetElementByIndex(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var tempElement = Head;

            for (var i = 0; tempElement != null && i < index; i++)
            {
                tempElement = tempElement.Next;
            }

            if (tempElement is null)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return tempElement.Data;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <returns>TODO. 2.</returns>
        public int Length()
        {
            // checks if there is a head
            if (Head is null)
            {
                return 0;
            }

            var tempElement = Head;
            var length = 1;

            while (tempElement.Next != null)
            {
                tempElement = tempElement.Next;
                length++;
            }

            return length;
        }

        /// <summary>
        /// TODO. get the whole list.
        /// </summary>
        /// <returns>TODO.</returns>
        public IEnumerable<T> GetListData()
        {
            // temp ListElement to avoid overwriting the original
            SinglyLinkedListNode<T>? tempElement = Head;

            // all elements where a next attribute exists
            while (tempElement != null)
            {
                yield return tempElement.Data;
                tempElement = tempElement.Next;
            }
        }

        /// <summary>
        /// TODO. delete a element.
        /// </summary>
        /// <param name="element">TODO. 2.</param>
        /// <returns>TODO. 3.</returns>
        public bool DeleteElement(T element)
        {
            var currentElement = Head;
            SinglyLinkedListNode<T>? previousElement = null;

            // iterates through all elements
            while (currentElement != null)
            {
                // checks if the element, which should get deleted is in this list element
                if ((currentElement.Data is null && element is null) || (currentElement.Data != null && currentElement.Data.Equals(element)))
                {
                    // if element is head just take the next one as head
                    if (currentElement.Equals(Head))
                    {
                        Head = Head.Next;
                        return true;
                    }

                    // else take the prev one and overwrite the next with the one behind the deleted
                    if (previousElement != null)
                    {
                        previousElement.Next = currentElement.Next;
                        return true;
                    }
                }

                // iterating
                previousElement = currentElement;
                currentElement = currentElement.Next;
            }

            return false;
        }
    }
}
