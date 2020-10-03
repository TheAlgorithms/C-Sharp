using System;
using System.Collections.Generic;

namespace DataStructures.BinarySearchTree
{
    /// <summary>
    /// An ordered tree with efficient insertion, removal, and lookup.
    /// </summary>
    /// <remarks>
    /// A Binary Search Tree (BST) is a tree that satisfies the following properties:
    /// <list type="bullet">
    /// <item>All nodes in the tree contain two children, usually called Left and Right.</item>
    /// <item>All nodes in the Left subtree contain keys that are less than the node's key.</item>
    /// <item>All nodes in the Right subtree contain keys that are greater than the node's key.</item>
    /// </list>
    /// A BST will have an average complexity of O(log n) for insertion, removal, and lookup operations.
    /// </remarks>
    /// <typeparam name="TKey">Type of key for the BST. Keys must implement IComparable.</typeparam>
    public class BinarySearchTree<TKey> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// Private class member to hold the root of the BST.
        /// </summary>
        private BinarySearchTreeNode<TKey>? root;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTree{TKey}"/> class.
        /// </summary>
        public BinarySearchTree()
        {
            root = null;
            Count = 0;
        }

        /// <summary>
        /// Gets the number nodes currently in the BST.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Insert a key into the BST.
        /// </summary>
        /// <param name="key">The key to insert.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if key is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if key is already in BST.
        /// </exception>
        public void Add(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("Key cannot be null!");
            }

            if (root == null)
            {
                root = new BinarySearchTreeNode<TKey>(key);
            }
            else
            {
                Add(root, key);
            }

            Count++;
        }

        /// <summary>
        /// Insert multiple keys into the BST.
        /// Keys are inserted in the order they appear in the sequence.
        /// </summary>
        /// <param name="keys">Sequence of keys to insert.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if a key in the sequence is already in BST.
        /// </exception>
        public void AddRange(IEnumerable<TKey> keys)
        {
            foreach (TKey key in keys)
            {
                Add(key);
            }
        }

        /// <summary>
        /// Find a node with the specified key.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <returns>The node with the specified key if it exists, otherwise a default value is returned.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if key is null.
        /// </exception>
        public BinarySearchTreeNode<TKey>? Search(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("Key cannot be null!");
            }

            return Search(root, key);
        }

        /// <summary>
        /// Checks if the specified key is in the BST.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <returns>true if the key is in the BST, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if key is null.
        /// </exception>
        public bool Contains(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("Key cannot be null!");
            }

            return Search(root, key) != null;
        }

        /// <summary>
        /// Removes a node with the specified key from the BST.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        /// <returns>true if the operation was successful, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if key is null.
        /// </exception>
        public bool Remove(TKey key)
        {
            /* Removing a node from the BST can be split into three cases:
             * 0. The node to be removed has no children. In this case, the node can just be removed from the tree.
             * 1. The node to be removed has one child. In this case, the node's child is moved to the node's parent,
             * then the node is removed from the tree.
             * 2. The node to be removed has two children. In this case, we must find a suitable node among the children
             * subtrees to replace the node. This can be done with either the in-order predecessor or the in-order successor.
             * The in-order predecessor is the largest node in Left subtree, or the largest node that is still smaller then the
             * current node. The in-order successor is the smallest node in the Right subtree, or the smallest node that is
             * still larger than the current node. Either way, once this suitable node is found, remove it from the tree (it
             * should be either a case 1 or 2 node) and replace the node to be removed with this suitable node.
             * More information: https://en.wikipedia.org/wiki/Binary_search_tree#Deletion
             */

            if (key == null)
            {
                throw new ArgumentNullException("Key cannot be null!");
            }

            BinarySearchTreeNode<TKey>? node = Search(root, key);
            if (node == null)
            {
                return false;
            }

            // Case 0: Node has no children
            if (node.Left == null && node.Right == null)
            {
                if (node == root)
                {
                    root = null;
                }
                else
                {
                    if (node.Parent!.Left == node)
                    {
                        node.Parent!.Left = null;
                    }
                    else if (node.Parent!.Right == node)
                    {
                        node.Parent!.Right = null;
                    }
                }
            }

            // Case 1: Node has one child.
            else if (node.Left == null || node.Right == null)
            {
                var childNode = node.Left ?? node.Right;

                if (node.Parent!.Left == node)
                {
                    node.Parent!.Left = childNode;
                }
                else if (node.Parent!.Right == node)
                {
                    node.Parent!.Right = childNode;
                }
            }

            // Case 2: Node has two children. (This implementation uses the in-order predecessor to replace node.)
            else
            {
                var replacementNode = GetMax(node.Left);
                Remove(replacementNode.Key);

                // Removing the replacementNode decrements the counter, but since it's getting added back, increment the count.
                Count++;
                var newNode = new BinarySearchTreeNode<TKey>(replacementNode.Key);
                newNode.Parent = node.Parent;
                newNode.Left = node.Left;
                newNode.Right = node.Right;

                if (node.Parent!.Left == node)
                {
                    node.Parent!.Left = newNode;
                }
                else if (node.Parent!.Right == node)
                {
                    node.Parent!.Right = newNode;
                }
            }

            Count--;
            return true;
        }

        /// <summary>
        /// Returns a node with the smallest key.
        /// </summary>
        /// <returns>The node if possible, a default value otherwise.</returns>
        public BinarySearchTreeNode<TKey>? GetMin()
        {
            if (root == null)
            {
                return default;
            }

            return GetMin(root);
        }

        /// <summary>
        /// Returns a node with the largest key.
        /// </summary>
        /// <returns>The node if possible, a default value otherwise.</returns>
        public BinarySearchTreeNode<TKey>? GetMax()
        {
            if (root == null)
            {
                return default;
            }

            return GetMax(root);
        }

        /// <summary>
        /// Returns all the keys in the BST, sorted In-Order.
        /// </summary>
        /// <returns>A list of keys in the BST.</returns>
        public ICollection<TKey> GetKeysInOrder() => GetKeysInOrder(root);

        /// <summary>
        /// Returns all the keys in the BST, sorted Pre-Order.
        /// </summary>
        /// <returns>A list of keys in the BST.</returns>
        public ICollection<TKey> GetKeysPreOrder() => GetKeysPreOrder(root);

        /// <summary>
        /// Returns all the keys in the BST, sorted Post-Order.
        /// </summary>
        /// <returns>A list of keys in the BST.</returns>
        public ICollection<TKey> GetKeysPostOrder() => GetKeysPostOrder(root);

        public override string ToString() => $"BinarySearchTree<{typeof(TKey)}>: Count={Count}";

        /// <summary>
        /// Recursive method to add a key to the BST.
        /// </summary>
        /// <param name="node">Node to search from.</param>
        /// <param name="key">Key to add.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if key is already in the BST.
        /// </exception>
        private void Add(BinarySearchTreeNode<TKey> node, TKey key)
        {
            int compareResult = node.Key.CompareTo(key);
            if (compareResult > 0)
            {
                if (node.Left != null)
                {
                    Add(node.Left, key);
                }
                else
                {
                    var newNode = new BinarySearchTreeNode<TKey>(key);
                    newNode.Parent = node;
                    node.Left = newNode;
                }
            }
            else if (compareResult < 0)
            {
                if (node.Right != null)
                {
                    Add(node.Right, key);
                }
                else
                {
                    var newNode = new BinarySearchTreeNode<TKey>(key);
                    newNode.Parent = node;
                    node.Right = newNode;
                }
            }
            else if (compareResult == 0)
            {
                throw new ArgumentException($"Key \"{key}\" already exists in tree!");
            }
        }

        /// <summary>
        /// Recursive method to get node with largest key.
        /// </summary>
        /// <param name="node">Node to search from.</param>
        /// <returns>Node with largest value.</returns>
        private BinarySearchTreeNode<TKey> GetMax(BinarySearchTreeNode<TKey> node)
        {
            if (node.Right == null)
            {
                return node;
            }

            return GetMax(node.Right);
        }

        /// <summary>
        /// Recursive method to get node with smallest key.
        /// </summary>
        /// <param name="node">Node to search from.</param>
        /// <returns>Node with smallest value.</returns>
        private BinarySearchTreeNode<TKey> GetMin(BinarySearchTreeNode<TKey> node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return GetMin(node.Left);
        }

        /// <summary>
        /// Recursive method to get a list with the keys sorted in in-order order.
        /// </summary>
        /// <param name="node">Node to traverse from.</param>
        /// <returns>List of keys in in-order order.</returns>
        private IList<TKey> GetKeysInOrder(BinarySearchTreeNode<TKey>? node)
        {
            if (node == null)
            {
                return new List<TKey>();
            }

            var result = new List<TKey>();
            result.AddRange(GetKeysInOrder(node.Left));
            result.Add(node.Key);
            result.AddRange(GetKeysInOrder(node.Right));
            return result;
        }

        /// <summary>
        /// Recursive method to get a list with the keys sorted in pre-order order.
        /// </summary>
        /// <param name="node">Node to traverse from.</param>
        /// <returns>List of keys in pre-order order.</returns>
        private IList<TKey> GetKeysPreOrder(BinarySearchTreeNode<TKey>? node)
        {
            if (node == null)
            {
                return new List<TKey>();
            }

            var result = new List<TKey>();
            result.Add(node.Key);
            result.AddRange(GetKeysPreOrder(node.Left));
            result.AddRange(GetKeysPreOrder(node.Right));
            return result;
        }

        /// <summary>
        /// Recursive method to get a list with the keys sorted in post-order order.
        /// </summary>
        /// <param name="node">Node to traverse from.</param>
        /// <returns>List of keys in post-order order.</returns>
        private IList<TKey> GetKeysPostOrder(BinarySearchTreeNode<TKey>? node)
        {
            if (node == null)
            {
                return new List<TKey>();
            }

            var result = new List<TKey>();
            result.AddRange(GetKeysPostOrder(node.Left));
            result.AddRange(GetKeysPostOrder(node.Right));
            result.Add(node.Key);
            return result;
        }

        /// <summary>
        /// Recursive method to find a node with a matching key.
        /// </summary>
        /// <param name="node">Node to search from.</param>
        /// <param name="key">Key to find.</param>
        /// <returns>The node with the specified if it exists, a default value otherwise.</returns>
        private BinarySearchTreeNode<TKey>? Search(BinarySearchTreeNode<TKey>? node, TKey key)
        {
            if (node == null)
            {
                return default;
            }

            int compareResult = node.Key.CompareTo(key);
            if (compareResult > 0)
            {
                return Search(node.Left, key);
            }
            else if (compareResult < 0)
            {
                return Search(node.Right, key);
            }

            return node;
        }
    }
}
