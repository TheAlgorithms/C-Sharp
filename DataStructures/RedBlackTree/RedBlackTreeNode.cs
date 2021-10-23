using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.RedBlackTree
{
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

    public class RedBlackTreeNode<TKey>
    {
        public TKey Key { get; set; }

        public NodeColor Color { get; set; }

        public RedBlackTreeNode<TKey>? Parent { get; set; }

        public RedBlackTreeNode<TKey>? Left { get; set; }

        public RedBlackTreeNode<TKey>? Right { get; set; }

        public RedBlackTreeNode(TKey key, RedBlackTreeNode<TKey>? parent)
        {
            Key = key;
            Parent = parent;
        }
    }
}
