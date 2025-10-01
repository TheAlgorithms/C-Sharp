namespace DataStructures.Bag;

/// <summary>
/// Generic node class for Bag.
/// </summary>
/// <typeparam name="T">A type for node.</typeparam>
public class BagNode<T>(T item)
{
    public T Item { get; } = item;

    public int Multiplicity { get; set; } = 1;

    public BagNode<T>? Next { get; set; }
}
