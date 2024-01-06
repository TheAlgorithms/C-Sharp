namespace DataStructures.Heap.PairingHeap;

/// <summary>
/// Node represented the value and connections.
/// </summary>
/// <typeparam name="T">Type, supported comparing.</typeparam>
public class PairingHeapNode<T>
{
    public PairingHeapNode(T value)
    {
        Value = value;
    }

    public T Value { get; set; }

    public PairingHeapNode<T> ChildrenHead { get; set; } = null!;

    public bool IsHeadChild => Previous != null && Previous.ChildrenHead == this;

    public PairingHeapNode<T> Previous { get; set; } = null!;

    public PairingHeapNode<T> Next { get; set; } = null!;
}
