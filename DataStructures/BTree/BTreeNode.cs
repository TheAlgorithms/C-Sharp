using System.Collections.Generic;

namespace DataStructures.BTree
{
    /// <summary>
    /// Generic class to represent nodes in an <see cref="BTree{TK}"/>
    /// instance.
    /// </summary>
    /// <typeparam name="TK">Type of BTree Key.</typeparam>
    public class BTreeNode<TK>
    {
        private int degree;

        /// <summary>
        /// Initializes a new instance of the <see cref="BTreeNode{TK}"/> class.
        /// </summary>
        /// <param name="degree">the minimum number of keys in a non-root node.</param>
        public BTreeNode(int degree)
        {
            this.degree = degree;
            Children = new List<BTreeNode<TK>>(degree);
            Entries = new List<TK>(degree);
        }

        /// <summary>
        /// Gets or sets children node.
        /// </summary>
        public List<BTreeNode<TK>> Children { get; set; }

        /// <summary>
        /// Gets or sets current entries.
        /// </summary>
        public List<TK> Entries { get; set; }

        /// <summary>
        /// Gets a value indicating whether this node the actual data objects.
        /// </summary>
        public bool IsLeaf => Children.Count == 0;

        /// <summary>
        /// Gets a value indicating whether the minimum value of entries.
        /// </summary>
        public bool HasReachedMaxEntries => Entries.Count == 2 * degree - 1;

        /// <summary>
        /// Gets a value indicating whether the maximum value of entries.
        /// </summary>
        public bool HasReachedMinEntries => Entries.Count == degree - 1;
    }
}
