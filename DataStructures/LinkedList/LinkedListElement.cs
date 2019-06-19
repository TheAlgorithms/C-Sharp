using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.LinkedList
{
    class LinkedListElement<T>
    {
        public T Data { get; set; }
        public LinkedListElement<T> Next { get; set; }

        public LinkedListElement(T data)
        {
            this.Data = data;
            this.Next = null;
        }

        public override string ToString()
        {
            return ("Data: " + Data);
        }
    }
}


