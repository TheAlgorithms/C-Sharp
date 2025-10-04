namespace DataStructures.BinarySearchTree;

/// <summary>
///     Generic node class for BinarySearchTree.
///     Keys for each node are immutable.
/// </summary>
/// <typeparam name="TKey">Type of key for the node. Keys must implement IComparable.</typeparam>
/// <remarks>
///     Initializes a new instance of the <see cref="BinarySearchTreeNode{TKey}" /> class.
/// </remarks>
/// <param name="key">The key of this node.</param>
public class BinarySearchTreeNode<TKey>(TKey key)
{
    /// <summary>
    ///     Gets the key for this node.
    /// </summary>
    public TKey Key { get; } = key;

    /// <summary>
    ///     Gets or sets the reference to a child node that precedes/comes before this node.
    /// </summary>
    public BinarySearchTreeNode<TKey>? Left { get; set; }

    /// <summary>
    ///     Gets or sets the reference to a child node that follows/comes after this node.
    /// </summary>
    public BinarySearchTreeNode<TKey>? Right { get; set; }
}
