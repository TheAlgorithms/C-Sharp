using System;

namespace DataStructures.AVLTree
{
    /// <summary>
    ///     Generic class to represent nodes in an <see cref="AvlTree{TKey}"/> instance.
    /// </summary>
    /// <typeparam name="TKey">The type of key for the node.</typeparam>
    public class AVLTreeNode<TKey>
    {
        /// <summary>
        ///     Gets or sets key value of node.
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        ///     Gets the height of the node.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        ///     Gets the balance factor of the node.
        /// </summary>
        public int BalanceFactor { get; private set; }

        /// <summary>
        ///     Gets or sets the left child of the node.
        /// </summary>
        public AVLTreeNode<TKey>? Left { get; set; }

        /// <summary>
        ///     Gets or sets the right child of the node.
        /// </summary>
        public AVLTreeNode<TKey>? Right { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AVLTreeNode{TKey}"/> class.
        /// </summary>
        /// <param name="key">Key value for node.</param>
        public AVLTreeNode(TKey key)
        {
            Key = key;
            Height = 0;
            Left = null;
            Right = null;
        }

        /// <summary>
        ///     Update the node's height and balance factor.
        /// </summary>
        public void UpdateBalanceFactor()
        {
            if(Left is null && Right is null)
            {
                Height = 0;
                BalanceFactor = 0;
            }
            else if(Left is null)
            {
                Height = Right!.Height + 1;
                BalanceFactor = Height;
            }
            else if(Right is null)
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
}
