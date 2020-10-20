using System;
using System.Collections.Generic;

namespace DataStructures.AATree
{
    /// <summary>
    /// A simple self-balencing binary search tree.
    /// </summary>
    /// <remarks>
    /// AA Trees are a form of self-balencing binary search tree named after their inventor
    /// Arne Andersson. AA Trees are designed to be simple to understand and implement.
    /// This is accomplished by limiting how nodes can be added to the tree.
    /// This simplifies rebalencing operations.
    /// More information: https://en.wikipedia.org/wiki/AA_tree .
    /// </remarks>
    /// <typeparam name="TKey">The type of key for the AA tree.</typeparam>
    public class AATree<TKey>
    {
        /// <summary>
        /// The root of the tree; has the highest level.
        /// </summary>
        private AATreeNode<TKey>? root;

        /// <summary>
        /// The comparer function to use to compare the keys.
        /// </summary>
        private Comparer<TKey> comparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AATree{TKey}"/> class.
        /// </summary>
        public AATree()
        {
            root = null;
            Count = 0;
            comparer = Comparer<TKey>.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AATree{TKey}"/> class with a custom comparer.
        /// </summary>
        /// <param name="customComparer">The custom comparer to use to compare keys.</param>
        public AATree(Comparer<TKey> customComparer)
        {
            root = null;
            Count = 0;
            comparer = customComparer;
        }

        /// <summary>
        /// Gets the number of elements in the tree.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Add a single element to the tree.
        /// </summary>
        /// <param name="key">The element to add to the tree.</param>
        public void Add(TKey key)
        {
            root = Add(key, root);
            Count++;
        }

        /// <summary>
        /// Add multiple elements to the tree.
        /// </summary>
        /// <param name="keys">The elements to add to the tree.</param>
        public void AddRange(IEnumerable<TKey> keys)
        {
            foreach (TKey key in keys)
            {
                root = Add(key, root);
                Count++;
            }
        }

        /// <summary>
        /// Remove a single element from the tree.
        /// </summary>
        /// <param name="key">Element to remove.</param>
        /// <returns>true if the element was successfully removed, false otherwise.</returns>
        public bool Remove(TKey key)
        {
            if (!Contains(key, root))
            {
                return false;
            }

            root = Remove(key, root);
            Count--;
            return true;
        }

        /// <summary>
        /// Checks if the specified element is in the tree.
        /// </summary>
        /// <param name="key">The element to look for.</param>
        /// <returns>true if the element is in the tree, false otherwise.</returns>
        public bool Contains(TKey key)
        {
            return Contains(key, root);
        }

        /// <summary>
        /// Gets the largest element in the tree. (ie. the element in the right most node).
        /// </summary>
        /// <returns>The largest element in the tree according to the stored comparer.</returns>
        /// <throws>InvalidOperationException if the tree is empty.</throws>
        public TKey GetMax()
        {
            if (root == null)
            {
                throw new InvalidOperationException("Tree is empty!");
            }

            return GetMax(root).Key;
        }

        /// <summary>
        /// Gets the smallest element in the tree. (ie. the element in the left most node).
        /// </summary>
        /// <returns>The smallest element in the tree according to the stored comparer.</returns>
        /// <throws>InvalidOperationException if the tree is empty.</throws>
        public TKey GetMin()
        {
            if (root == null)
            {
                throw new InvalidOperationException("Tree is empty!");
            }

            return GetMin(root).Key;
        }

        /// <summary>
        /// Gets all the elements in the tree in in-order order.
        /// </summary>
        /// <returns>Sequence of elements in in-order order.</returns>
        public IEnumerable<TKey> GetKeysInOrder()
        {
            var result = new List<TKey>();

            Action<AATreeNode<TKey>?>? inOrderWalk = null;

            inOrderWalk = (node) =>
            {
                if (node == null)
                {
                    return;
                }

                inOrderWalk!(node.Left);
                result.Add(node.Key);
                inOrderWalk(node.Right);
            };

            inOrderWalk(root);
            return result;
        }

        /// <summary>
        /// Gets all the elements in the tree in pre-order order.
        /// </summary>
        /// <returns>Sequence of elements in pre-order order.</returns>
        public IEnumerable<TKey> GetKeysPreOrder()
        {
            var result = new List<TKey>();

            Action<AATreeNode<TKey>?>? preOrderWalk = null;

            preOrderWalk = (node) =>
            {
                if (node == null)
                {
                    return;
                }

                result.Add(node.Key);
                preOrderWalk!(node.Left);
                preOrderWalk(node.Right);
            };

            preOrderWalk(root);
            return result;
        }

        /// <summary>
        /// Gets all the elements in the tree in post-order order.
        /// </summary>
        /// <returns>Sequence of elements in post-order order.</returns>
        public IEnumerable<TKey> GetKeysPostOrder()
        {
            var result = new List<TKey>();

            Action<AATreeNode<TKey>?>? postOrderWalk = null;

            postOrderWalk = (node) =>
            {
                if (node == null)
                {
                    return;
                }

                postOrderWalk!(node.Left);
                postOrderWalk(node.Right);
                result.Add(node.Key);
            };

            postOrderWalk(root);
            return result;
        }

        /// <summary>
        /// Checks if the tree is a valid AA Tree.
        /// </summary>
        public void Validate()
        {
            ValidateTree(root);
        }

        /// <summary>
        /// Recursive function to add an element to the tree.
        /// </summary>
        /// <param name="key">The element to add.</param>
        /// <param name="node">The node to search for a empty spot.</param>
        /// <returns>The node with the added element.</returns>
        /// <throws>ArgumentException if element is already in the tree.</throws>
        private AATreeNode<TKey> Add(TKey key, AATreeNode<TKey>? node)
        {
            if (node == null)
            {
                return new AATreeNode<TKey>(key, 1);
            }
            else if (comparer.Compare(key, node.Key) < 0)
            {
                node.Left = Add(key, node.Left);
            }
            else if (comparer.Compare(key, node.Key) > 0)
            {
                node.Right = Add(key, node.Right);
            }
            else
            {
                throw new ArgumentException($"Key \"{key}\" already in tree!", nameof(key));
            }

            return Split(Skew(node)) !;
        }

        /// <summary>
        /// Recursive function to remove an element from the tree.
        /// </summary>
        /// <param name="key">The element to remove.</param>
        /// <param name="node">The node to search from.</param>
        /// <returns>The node with the specified element removed.</returns>
        private AATreeNode<TKey>? Remove(TKey key, AATreeNode<TKey>? node)
        {
            if (node == null)
            {
                return node;
            }
            else if (comparer.Compare(key, node.Key) < 0)
            {
                node.Left = Remove(key, node.Left);
            }
            else if (comparer.Compare(key, node.Key) > 0)
            {
                node.Right = Remove(key, node.Right);
            }
            else
            {
                if (node.Left == null && node.Right == null)
                {
                    return node.Right;
                }
                else if (node.Left == null)
                {
                    var successor = GetMin(node.Right!);
                    node.Right = Remove(successor.Key, node.Right);
                    node.Key = successor.Key;
                }
                else
                {
                    var predecessor = GetMax(node.Left);
                    node.Left = Remove(predecessor.Key, node.Left);
                    node.Key = predecessor.Key;
                }
            }

            node = DecreaseLevel(node);
            node = Skew(node);
            node!.Right = Skew(node.Right);

            if (node.Right != null)
            {
                node.Right.Right = Skew(node.Right.Right);
            }

            node = Split(node);
            node!.Right = Split(node.Right);
            return node;
        }

        /// <summary>
        /// Recursive function to check if the element exists in the tree.
        /// </summary>
        /// <param name="key">The element to check for.</param>
        /// <param name="node">The node to search from.</param>
        /// <returns>true if the element exists in the tree, false otherwise.</returns>
        private bool Contains(TKey key, AATreeNode<TKey>? node)
        {
            if (node == null)
            {
                return false;
            }

            if (comparer.Compare(key, node.Key) < 0)
            {
                return Contains(key, node.Left);
            }
            else if (comparer.Compare(key, node.Key) > 0)
            {
                return Contains(key, node.Right);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Recursive to find the maximum/right-most element.
        /// </summary>
        /// <param name="node">The node to traverse from.</param>
        /// <returns>The node with the maximum/right-most element.</returns>
        private AATreeNode<TKey> GetMax(AATreeNode<TKey> node)
        {
            if (node.Right == null)
            {
                return node;
            }
            else
            {
                return GetMax(node.Right);
            }
        }

        /// <summary>
        /// Recursive to find the minimum/left-most element.
        /// </summary>
        /// <param name="node">The node to traverse from.</param>
        /// <returns>The node with the minimum/left-most element.</returns>
        private AATreeNode<TKey> GetMin(AATreeNode<TKey> node)
        {
            if (node.Left == null)
            {
                return node;
            }
            else
            {
                return GetMin(node.Left);
            }
        }

        /// <summary>
        /// Checks various properties to determine if the tree is a valid AA Tree.
        /// Throws exceptions if properties are violated.
        /// Useful for debugging.
        /// </summary>
        /// <remarks>
        /// The properties that are checked are:
        /// <list type="number">
        /// <item>The level of every leaf node is one.</item>
        /// <item>The level of every left child is exactly one less than that of its parent.</item>
        /// <item>The level of every right child is equal to or one less than that of its parent.</item>
        /// <item>The level of every right grandchild is strictly less than that of its grandparent.</item>
        /// <item>Every node of level greater than one has two children.</item>
        /// </list>
        /// More information: https://en.wikipedia.org/wiki/AA_tree .
        /// </remarks>
        /// <param name="node">The node to check from.</param>
        private void ValidateTree(AATreeNode<TKey>? node)
        {
            if (node == null)
            {
                return;
            }

            if (node.Left == null && node.Right == null)
            {
                if (node.Level != 1)
                {
                    throw new Exception($"Node {node.Key} - leaf: expected level of 1, found {node.Level} instead.");
                }
            }

            if (node.Left != null)
            {
                if (node.Level - node.Left.Level != 1)
                {
                    throw new Exception($"Node {node.Key} - left child: expected level of {node.Level - 1}, found {node.Left.Level} instead.");
                }
            }

            if (node.Right != null)
            {
                if (node.Level - node.Right.Level != 1 && node.Level != node.Right.Level)
                {
                    throw new Exception($"Node {node.Key} - right child: expected level of {node.Level - 1} or {node.Level}, found {node.Right.Level} instead.");
                }
            }

            if (node.Right != null && node.Right.Right != null)
            {
                if (node.Right.Level < node.Right.Right.Level)
                {
                    throw new Exception($"Node {node.Key} - right grandchild: right grandchild has level of {node.Right.Right.Level}, which is greater than node level of {node.Level}.");
                }
            }

            if (node.Level > 1)
            {
                if (node.Left == null || node.Right == null)
                {
                    throw new Exception($"Node {node.Key}: node level is greater than one but has less than two children.");
                }
            }

            ValidateTree(node.Left);
            ValidateTree(node.Right);
        }

        /// <summary>
        /// Remove right-horizontal links and replaces them with left-horizontal links.
        /// Acccomplishes this by performing a right rotation.
        /// </summary>
        /// <param name="node">The node to rebalence from.</param>
        /// <returns>The rebalenced node.</returns>
        private AATreeNode<TKey>? Skew(AATreeNode<TKey>? node)
        {
            if (node == null)
            {
                return null;
            }
            else if (node.Left == null)
            {
                return node;
            }
            else if (node.Left.Level == node.Level)
            {
                var left = node.Left;
                node.Left = left.Right;
                left.Right = node;
                return left;
            }
            else
            {
                return node;
            }
        }

        /// <summary>
        /// Reduces the number of right-horizontal links.
        /// Accomplishes this by performing a lefft rotation, and incrementing level.
        /// </summary>
        /// <param name="node">The node to rebalence from.</param>
        /// <returns>The rebalenced node.</returns>
        private AATreeNode<TKey>? Split(AATreeNode<TKey>? node)
        {
            if (node == null)
            {
                return null;
            }
            else if (node.Right == null || node.Right.Right == null)
            {
                return node;
            }
            else if (node.Level == node.Right.Right.Level)
            {
                var right = node.Right;
                node.Right = right.Left;
                right.Left = node;
                right.Level++;
                return right;
            }
            else
            {
                return node;
            }
        }

        /// <summary>
        /// Decreases the level of node if nesscessary.
        /// </summary>
        /// <param name="node">The node to decrease level from.</param>
        /// <returns>The node with modified level.</returns>
        private AATreeNode<TKey> DecreaseLevel(AATreeNode<TKey> node)
        {
            Func<AATreeNode<TKey>?, int> getLvl = (x) => x == null ? 0 : x.Level;
            var newLevel = Math.Min(getLvl(node.Left), getLvl(node.Right)) + 1;
            if (newLevel < node.Level)
            {
                node.Level = newLevel;

                if (node.Right != null && newLevel < node.Right.Level)
                {
                    node.Right.Level = newLevel;
                }
            }

            return node;
        }
    }
}
