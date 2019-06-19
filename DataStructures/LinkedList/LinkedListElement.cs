using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.LinkedList
{
    class LinkedListElement<T>
    {
        private T Content;
        private LinkedListElement<T> PointerToNext;

        public LinkedListElement(T data)
        {
            this.Data = data;
            this.Next = null;
        }

        public T Data
        {
            get
            {
                return Content;
            }

            set
            {
                Content = value;
            }
        }

        public LinkedListElement<T> Next
        {
            get
            {
                return PointerToNext;
            }

            set
            {
                PointerToNext = value;
            }
        }

        public override string ToString()
        {
            return ("Data: " + Data);
        }
    }
}


