namespace DataStructures.AATree;

/// <summary>
///     Generic node class for AATree.
/// </summary>
/// <typeparam name="TKey">Type of key for node.</typeparam>
/// <remarks>
///     Initializes a new instance of the <see cref="AaTreeNode{TKey}" /> class.
/// </remarks>
/// <param name="key">The initial key of this node.</param>
/// <param name="level">The level of this node. See <see cref="AaTree{TKey}" /> for more details.</param>
public class AaTreeNode<TKey>(TKey key, int level)
{
    /// <summary>
    ///     Gets or Sets key for this node.
    /// </summary>
    public TKey Key { get; set; } = key;

    /// <summary>
    ///     Gets or Sets level for this node.
    /// </summary>
    public int Level { get; set; } = level;

    /// <summary>
    ///     Gets or sets the left subtree of this node.
    /// </summary>
    public AaTreeNode<TKey>? Left { get; set; }

    /// <summary>
    ///     Gets or sets the right subtree of this node.
    /// </summary>
    public AaTreeNode<TKey>? Right { get; set; }
}
