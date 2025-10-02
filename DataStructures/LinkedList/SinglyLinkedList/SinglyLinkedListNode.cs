namespace DataStructures.LinkedList.SinglyLinkedList;

public class SinglyLinkedListNode<T>(T data)
{
    public T Data { get; } = data;

    public SinglyLinkedListNode<T>? Next { get; set; }
}
