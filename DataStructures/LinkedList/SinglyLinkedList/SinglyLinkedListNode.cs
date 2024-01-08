namespace DataStructures.LinkedList.SinglyLinkedList;

public class SinglyLinkedListNode<T>
{
    public SinglyLinkedListNode(T data)
    {
        Data = data;
        Next = null;
    }

    public T Data { get; }

    public SinglyLinkedListNode<T>? Next { get; set; }
}
