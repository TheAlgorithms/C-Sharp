using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    //defines single elements of the list
    //gernerics make every type of data possible
    public class ListElement<T>
    {
        private T data;
        private ListElement<T> next;

        public ListElement(T data)
        {
            this.data = data;
            this.next = null;
        }

        public T Data { get => data; set => data = value; }
        public ListElement<T> Next { get => next; set => next = value; }

        public override string ToString()
        {
            return ("Data: " + data );
        }
    }

    public class List<T>
    {
        //points to the start of the list
        private ListElement<T> head { get; set; }

        public List()
        {

        }

        //Add new element to the list
        public void AddListElement(T data)
        {
            ListElement<T> newListElement = new ListElement<T>(data);
            //if head is null, the added element is the first, hence it is the head
            if (head == null)
            {
                head = newListElement;
            }

            else
            {
                //temp ListElement to avoid overwriting the original 
                ListElement<T> tempElement = head;

                //iterates through all elements
                while(tempElement.Next != null)
                {
                    tempElement = tempElement.Next;
                }
                //adds the new element to the last one 
                tempElement.Next = newListElement;
            }
            
        }

        public T getelementByIndex(int pos)
        {
            ListElement<T> tempElement = head;
            //iterates through all elements until pos is reached
            for(int i = 0; i < pos; i++)
            {
                //iterate throuh list elements
                if (tempElement.Next != null)
                    tempElement = tempElement.Next;
                //if Next os null the index pos is out of range
                else
                    throw new IndexOutOfRangeException();
            }
            return tempElement.Data;
        }

        public int Length()
        {
            int length = 0;

            //checks if there is a head
            if (head == null)
                return length;
            
            ListElement<T> tempElement = head;

            while (tempElement != null)
            {
                tempElement = tempElement.Next;
                length++;
            }

            return length;
        }

        //get the whole list
        public IEnumerable<T> getListData()
        {
            //temp ListElement to avoid overwriting the original 
            ListElement<T> tempElement = head;

            //all elements where a next attribute exists 
            while (tempElement != null)
            {
                yield return tempElement.Data;
                tempElement = tempElement.Next;
            }
        }

        //delete a element
        public bool deleteElement(T element)
        {
            ListElement<T> currentElement = head;
            ListElement<T> previousElement = null;

            //iterates through all elements
            while (currentElement != null)
            {
                //checks if the element, which should get deleted is in this list element
                if (currentElement.Data.Equals(element))
                {
                    //if element is head just take the next one as head
                    if (currentElement.Equals(head))
                    {
                        head = head.Next;
                        return true;
                    }
                    //else take the prev one and overwrite the next with the one behind the deleted
                    else
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
    
    class Program
    {
        static void Main(string[] args)
        {

            
            //Testing 
            //feel free to play around

            List<String> list = new List<String>();

            list.AddListElement("test1");
            list.AddListElement("test2");
            list.AddListElement("test3");
            list.AddListElement("test4");



            Console.WriteLine(list.getelementByIndex(14));

          /*  foreach (var x in list.getListData())
            {
                Console.WriteLine(x);
            }*/
            Console.ReadKey();
        }
    }
}
