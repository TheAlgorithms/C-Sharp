namespace DataStructures.RedBlackTree;

/// <summary>
///     Enum to represent node colors.
/// </summary>
public enum NodeColor : byte
{
    /// <summary>
    ///     Represents red node
    /// </summary>
    Red,

    /// <summary>
    ///     Represents black node
    /// </summary>
    Black,
}

/// <summary>
///     Generic class to represent nodes in an <see cref="RedBlackTree{TKey}"/> instance.
/// </summary>
/// <typeparam name="TKey">The type of key for the node.</typeparam>
public class RedBlackTreeNode<TKey>
{
    /// <summary>
    ///     Gets or sets key value of node.
    /// </summary>
    public TKey Key { get; set; }

    /// <summary>
    ///     Gets or sets the color of the node.
    /// </summary>
    public NodeColor Color { get; set; }

    /// <summary>
    ///     Gets or sets the parent of the node.
    /// </summary>
    public RedBlackTreeNode<TKey>? Parent { get; set; }

    /// <summary>
    ///     Gets or sets left child of the node.
    /// </summary>
    public RedBlackTreeNode<TKey>? Left { get; set; }

    /// <summary>
    ///     Gets or sets the right child of the node.
    /// </summary>
    public RedBlackTreeNode<TKey>? Right { get; set; }

    /// <summary>
    ///  Initializes a new instance of the <see cref="RedBlackTreeNode{TKey}"/> class.
    /// </summary>
    /// <param name="key">Key value for node.</param>
    /// <param name="parent">Parent of node.</param>
    public RedBlackTreeNode(TKey key, RedBlackTreeNode<TKey>? parent)
    {
        Key = key;
        Parent = parent;
    }
}
