namespace DataStructures.LinkedList
{
    public class LinkedListElementNode<T>
    {
        public T Data { get; set; }
        public LinkedListElementNode<T> Next { get; set; }

        public LinkedListElementNode(T data)
        {
            Data = data;
            Next = null;
        }
    }
}


