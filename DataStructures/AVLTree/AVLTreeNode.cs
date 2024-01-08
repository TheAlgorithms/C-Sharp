using System;

namespace DataStructures.AVLTree;

/// <summary>
///     Generic class to represent nodes in an <see cref="AvlTree{TKey}"/>
///     instance.
/// </summary>
/// <typeparam name="TKey">The type of key for the node.</typeparam>
internal class AvlTreeNode<TKey>
{
    /// <summary>
    ///     Gets or sets key value of node.
    /// </summary>
    public TKey Key { get; set; }

    /// <summary>
    ///     Gets the balance factor of the node.
    /// </summary>
    public int BalanceFactor { get; private set; }

    /// <summary>
    ///     Gets or sets the left child of the node.
    /// </summary>
    public AvlTreeNode<TKey>? Left { get; set; }

    /// <summary>
    ///     Gets or sets the right child of the node.
    /// </summary>
    public AvlTreeNode<TKey>? Right { get; set; }

    /// <summary>
    ///     Gets or sets the height of the node.
    /// </summary>
    private int Height { get; set; }

    /// <summary>
    ///     Initializes a new instance of the
    ///     <see cref="AvlTreeNode{TKey}"/> class.
    /// </summary>
    /// <param name="key">Key value for node.</param>
    public AvlTreeNode(TKey key)
    {
        Key = key;
    }

    /// <summary>
    ///     Update the node's height and balance factor.
    /// </summary>
    public void UpdateBalanceFactor()
    {
        if (Left is null && Right is null)
        {
            Height = 0;
            BalanceFactor = 0;
        }
        else if (Left is null)
        {
            Height = Right!.Height + 1;
            BalanceFactor = Height;
        }
        else if (Right is null)
        {
            Height = Left!.Height + 1;
            BalanceFactor = -Height;
        }
        else
        {
            Height = Math.Max(Left.Height, Right.Height) + 1;
            BalanceFactor = Right.Height - Left.Height;
        }
    }
}
