namespace DataStructures.Heap.PairingHeap;

/// <summary>
/// Node represented the value and connections.
/// </summary>
/// <typeparam name="T">Type, supported comparing.</typeparam>
public class PairingHeapNode<T>(T value)
{
    public T Value { get; set; } = value;

    public PairingHeapNode<T> ChildrenHead { get; set; } = null!;

    public bool IsHeadChild => Previous != null && Previous.ChildrenHead == this;

    public PairingHeapNode<T> Previous { get; set; } = null!;

    public PairingHeapNode<T> Next { get; set; } = null!;
}
