namespace DataStructures.BTree;

/// <summary>
///     Generic class to represent nodes in a <see cref="BTree{TKey}"/>
///     instance.
/// </summary>
/// <typeparam name="TKey">The type of key for the node.</typeparam>
/// <remarks>
///     Initializes a new instance of the
///     <see cref="BTreeNode{TKey}"/> class.
/// </remarks>
/// <param name="minDegree">Minimum degree of the B-Tree.</param>
/// <param name="isLeaf">Whether this node is a leaf node.</param>
internal class BTreeNode<TKey>(int minDegree, bool isLeaf)
{
    /// <summary>
    ///     Gets the minimum degree (t) of the B-Tree.
    ///     A node can contain at most 2t-1 keys and at least t-1 keys (except root).
    /// </summary>
    public int MinDegree { get; } = minDegree;

    /// <summary>
    ///     Gets or sets a value indicating whether this node is a leaf node.
    /// </summary>
    public bool IsLeaf { get; set; } = isLeaf;

    /// <summary>
    ///     Gets or sets the current number of keys stored in this node.
    /// </summary>
    public int KeyCount { get; internal set; }

    /// <summary>
    ///     Gets the array of keys stored in this node.
    ///     Maximum size is 2*MinDegree - 1.
    /// </summary>
    public TKey[] Keys { get; } = new TKey[2 * minDegree - 1];

    /// <summary>
    ///     Gets the array of child pointers.
    ///     Maximum size is 2*MinDegree.
    /// </summary>
    public BTreeNode<TKey>?[] Children { get; } = new BTreeNode<TKey>[2 * minDegree];

    /// <summary>
    ///     Inserts a key at the specified position in the keys array.
    /// </summary>
    /// <param name="index">Position to insert the key.</param>
    /// <param name="key">Key to insert.</param>
    public void InsertKey(int index, TKey key)
    {
        Keys[index] = key;
        KeyCount++;
    }

    /// <summary>
    ///     Removes the key at the specified position in the keys array.
    /// </summary>
    /// <param name="index">Position of the key to remove.</param>
    public void RemoveKey(int index)
    {
        for (var i = index; i < KeyCount - 1; i++)
        {
            Keys[i] = Keys[i + 1];
        }

        Keys[KeyCount - 1] = default!;
        KeyCount--;
    }

    /// <summary>
    ///     Inserts a child pointer at the specified position.
    /// </summary>
    /// <param name="index">Position to insert the child.</param>
    /// <param name="child">Child node to insert.</param>
    public void InsertChild(int index, BTreeNode<TKey> child)
    {
        Children[index] = child;
    }

    /// <summary>
    ///     Checks if the node is full (contains maximum number of keys).
    /// </summary>
    /// <returns>True if the node is full, false otherwise.</returns>
    public bool IsFull() => KeyCount == 2 * MinDegree - 1;
}
