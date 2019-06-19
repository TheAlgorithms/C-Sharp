using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.LinkedList
{
    class LinkedList<T>
    {
        //points to the start of the list
        private LinkedListElement<T> Head { get; set; }

        //Add new element to the list
        public void AddListElement(T data)
        {
            LinkedListElement<T> newListElement = new LinkedListElement<T>(data);
            //if head is null, the added element is the first, hence it is the head
            if (Head == null)
            {
                Head = newListElement;
            }

            else
            {
                //temp ListElement to avoid overwriting the original 
                LinkedListElement<T> tempElement = Head;

                //iterates through all elements
                while (tempElement.Next != null)
                {
                    tempElement = tempElement.Next;
                }
                //adds the new element to the last one 
                tempElement.Next = newListElement;
            }

        }

        public T GetElementByIndex(int pos)
        {
            LinkedListElement<T> tempElement = Head;
            //iterates through all elements until pos is reached
            for (int i = 0; i < pos; i++)
            {
                //iterate throuh list elements
                if (tempElement.Next != null)
                {
                    tempElement = tempElement.Next;
                }
            }
            return tempElement.Data;
        }

        public int Length()
        {
            int length = 0;

            //checks if there is a head
            if (Head == null)
            {
                return length;
            }
                

            LinkedListElement<T> tempElement = Head;

            while (tempElement != null)
            {
                tempElement = tempElement.Next;
                length++;
            }

            return length;
        }

        //get the whole list
        public IEnumerable<T> GetListData()
        {
            //temp ListElement to avoid overwriting the original 
            LinkedListElement<T> tempElement = Head;

            //all elements where a next attribute exists 
            while (tempElement != null)
            {
                yield return tempElement.Data;
                tempElement = tempElement.Next;
            }
        }

        //delete a element
        public bool DeleteElement(T element)
        {
            LinkedListElement<T> currentElement = Head;
            LinkedListElement<T> previousElement = null;

            //iterates through all elements
            while (currentElement != null)
            {
                //checks if the element, which should get deleted is in this list element
                if (currentElement.Data.Equals(element))
                {
                    //if element is head just take the next one as head
                    if (currentElement.Equals(Head))
                    {
                        Head = Head.Next;
                        return true;
                    }
                    //else take the prev one and overwrite the next with the one behind the deleted
                    if(previousElement != null)
                    {
                        previousElement.Next = currentElement.Next;
                        return true;
                    }
                }

                //iterating
                previousElement = currentElement;
                currentElement = currentElement.Next;

            }

            return false;
        }
    }
}





