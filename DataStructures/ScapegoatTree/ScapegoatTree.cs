using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.ScapegoatTree
{
    /// <summary>
    /// Self-balancing binary search tree. Will balance itself during delete and insert operations.
    /// Worst-case O(log n) lookup time, and O(log n) amortized insertion and deletion time.
    /// No additional pre-node memory overhead - each node only stores a key and two pointers to child nodes.
    /// Balancing logic depends on <see cref="Alpha"/> value, which should be in (0.5 .. 1) range.
    /// </summary>
    /// <typeparam name="TKey">Type of the tree node value.</typeparam>
    public class ScapegoatTree<TKey> : IEnumerable<TKey> where TKey : IComparable
    {
        public double Alpha { get; private set; }

        public Node<TKey>? Root { get; private set; }

        public int Size { get; private set; }

        public int MaxSize { get; private set; }

        public ScapegoatTreeImplementationBase<TKey> Base { get; }

        public event EventHandler? TreeIsUnbalanced;

        public ScapegoatTree()
            : this(alpha: 0.5, size: 0, implementation: default)
        {
        }

        public ScapegoatTree(double alpha)
            : this(alpha, size: 0, implementation: default)
        {
        }

        public ScapegoatTree(TKey key)
            : this(key, alpha: 0.5, implementation: default)
        {
        }

        public ScapegoatTree(ScapegoatTreeImplementationBase<TKey> implementation)
            : this(alpha: 0.5, size: 0, implementation)
        {
        }

        public ScapegoatTree(TKey key, double alpha, ScapegoatTreeImplementationBase<TKey>? implementation = default)
            : this(alpha, size: 1, implementation)
        {
            this.Root = new Node<TKey>(key);
        }

        private ScapegoatTree(double alpha, int size, ScapegoatTreeImplementationBase<TKey>? implementation)
        {
            CheckAlpha(alpha);

            this.Base = implementation ?? new ScapegoatTreeImplementation<TKey>();

            this.Alpha = alpha;

            this.Size = size;
            this.MaxSize = size;
        }

        public bool IsAlphaWeightBalanced()
        {
            return Root?.IsAlphaWeightBalanced(Alpha) ?? true;
        }

        /// <summary>
        /// Searches specified key value in the tree.
        /// Classic binary search algorithm.
        /// </summary>
        /// <param name="key">Key value.</param>
        /// <returns>Returns node or null if tree is empty or node does not exists.</returns>
        public Node<TKey>? Search(TKey key)
        {
            return Root == null ? null : Base.SearchWithRoot(Root, key);
        }

        /// <summary>
        /// Check if any node in the tree has specified key value.
        /// </summary>
        /// <param name="key">Key value.</param>
        /// <returns>Returns true if node exists, false if not.</returns>
        public bool Contains(TKey key)
        {
            return Search(key) != null;
        }

        /// <summary>
        /// Inserts new node with specified key value.
        /// Doesn't insert duplicate keys. If the key already exists in the tree, method returns false.
        /// Uses binary search and insert algorithm to insert new node.
        /// On successful insertion, checks if the tree became unbalanced.
        /// Uses scapegoat algorithm to find a scapegoat node which subtree is unbalanced and balances it.
        /// </summary>
        /// <param name="key">Key value.</param>
        /// <returns>Returns true if insertion is successful. False, if not.</returns>
        public bool Insert(TKey key)
        {
            var node = new Node<TKey>(key);

            if (Root == null)
            {
                Root = node;

                UpdateSizes();

                return true;
            }

            var path = new Stack<Node<TKey>>();

            if (Base.TryInsertWithRoot(Root, node, path))
            {
                UpdateSizes();

                if (path.Count > Root.GetAlphaHeight(Alpha))
                {
                    TreeIsUnbalanced?.Invoke(this, EventArgs.Empty);

                    RebalanceFromPath(path);

                    MaxSize = Math.Max(MaxSize, Size);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes a node with specified key from the tree.
        /// Uses binary search and delete algorithm to find and remove the node.
        /// After successful removal checks if the tree is unbalanced.
        /// </summary>
        /// <param name="key">Key value.</param>
        /// <returns>Returns true if removal is successful, otherwise false.</returns>
        public bool Delete(TKey key)
        {
            if (Root == null)
            {
                return false;
            }

            if (Base.TryDeleteWithRoot(Root, key))
            {
                Size--;

                if (Root != null && Size < Alpha * MaxSize)
                {
                    TreeIsUnbalanced?.Invoke(this, EventArgs.Empty);

                    Root = Base.RebuildWithRoot(Root);
                    MaxSize = Size;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Clears the tree.
        /// </summary>
        public void Clear()
        {
            Size = 0;
            MaxSize = 0;
            Root = null;
        }

        /// <summary>
        /// Changes <see cref="Alpha"/> value to adjust balancing.
        /// </summary>
        /// <param name="value">New alpha value.</param>
        public void Tune(double value)
        {
            CheckAlpha(value);
            this.Alpha = value;
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return (Root ?? Enumerable.Empty<TKey>()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void RebalanceFromPath(Stack<Node<TKey>> path)
        {
            var (parent, tree) = Base.RebuildFromPath(Alpha, path);

            if (parent == null)
            {
                Root = tree;
            }
            else
            {
                var result = parent.CompareTo(tree.Key);

                if (result < 0)
                {
                    parent.Right = tree;
                    return;
                }

                if (result > 0)
                {
                    parent.Left = tree;
                    return;
                }
            }
        }

        private void UpdateSizes()
        {
            Size += 1;
            MaxSize = Math.Max(Size, MaxSize);
        }

        private void CheckAlpha(double alpha)
        {
            if (alpha is < 0.5 or > 1.0)
            {
                throw new ArgumentException("The alpha parameter's value should be in 0.5..1.0 range.", nameof(alpha));
            }
        }
    }
}
