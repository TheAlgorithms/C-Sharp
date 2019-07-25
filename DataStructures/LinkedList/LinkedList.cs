using System.Collections.Generic;

namespace DataStructures.LinkedList
{
    /// <summary>
    /// TODO.
    /// </summary>
    /// <typeparam name="T">TODO. 2.</typeparam>
    public class LinkedList<T>
    {
        // points to the start of the list
        private LinkedListElementNode<T> Head { get; set; }

        /// <summary>
        /// TODO. Add new element to the list.
        /// </summary>
        /// <param name="data">TODO. 2.</param>
        public void AddListElement(T data)
        {
            var newListElement = new LinkedListElementNode<T>(data);

            // if head is null, the added element is the first, hence it is the head
            if (Head == null)
            {
                Head = newListElement;
            }
            else
            {
                // temp ListElement to avoid overwriting the original
                var tempElement = Head;

                // iterates through all elements
                while (tempElement.Next != null)
                {
                    tempElement = tempElement.Next;
                }

                // adds the new element to the last one
                tempElement.Next = newListElement;
            }
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="pos">TODO. 2.</param>
        /// <returns>TODO. 3.</returns>
        public T GetElementByIndex(int pos)
        {
            var tempElement = Head;

            // iterates through all elements until pos is reached
            for (var i = 0; i < pos; i++)
            {
                // iterate throuh list elements
                if (tempElement.Next != null)
                {
                    tempElement = tempElement.Next;
                }
            }

            return tempElement.Data;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <returns>TODO. 2.</returns>
        public int Length()
        {
            var length = 0;

            // checks if there is a head
            if (Head == null)
            {
                return length;
            }

            var tempElement = Head;

            while (tempElement != null)
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
            var tempElement = Head;

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
            LinkedListElementNode<T> previousElement = null;

            // iterates through all elements
            while (currentElement != null)
            {
                // checks if the element, which should get deleted is in this list element
                if (currentElement.Data.Equals(element))
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
