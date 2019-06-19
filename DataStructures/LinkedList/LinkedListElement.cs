using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.LinkedList
{
    class LinkedListElement<T>
    {
        private T data { get; set; }
        private LinkedListElement<T> next { get; set; }

        public LinkedListElement(T data)
        {
            this.data = data;
            this.next = null;
        }

        

        public override string ToString()
        {
            return ("Data: " + data);
        }
    }
}


