using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.LinkedList
{
    class LinkedListElement<T>
    {
        private T data;
        private LinkedListElement<T> next;

        public LinkedListElement(T data)
        {
            this.data = data;
            this.next = null;
        }

        public T Data { get => data; set => data = value; }
        public LinkedListElement<T> Next { get => next; set => next = value; }

        public override string ToString()
        {
            return ("Data: " + data);
        }
    }
}


