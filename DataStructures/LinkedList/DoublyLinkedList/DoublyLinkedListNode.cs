namespace DataStructures.LinkedList.DoublyLinkedList;

/// <summary>
///     Generic node class for Doubly Linked List.
/// </summary>
/// <typeparam name="T">Generic type.</typeparam>
/// <remarks>
///     Initializes a new instance of the <see cref="DoublyLinkedListNode{T}" /> class.
/// </remarks>
/// <param name="data">Data to be stored in this node.</param>
public class DoublyLinkedListNode<T>(T data)
{
    /// <summary>
    ///     Gets the data stored on this node.
    /// </summary>
    public T Data { get; } = data;

    /// <summary>
    ///     Gets or sets the reference to the next node in the Doubly Linked List.
    /// </summary>
    public DoublyLinkedListNode<T>? Next { get; set; }

    /// <summary>
    ///     Gets or sets the reference to the previous node in the Doubly Linked List.
    /// </summary>
    public DoublyLinkedListNode<T>? Previous { get; set; }
}
