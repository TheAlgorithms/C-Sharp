using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.RedBlackTree
{
    public class RedBlackTree<TKey>
    {
        public int Count { get; private set; }

        private readonly Comparer<TKey> comparer;

        private RedBlackTreeNode<TKey>? root;

        public RedBlackTree()
        {
            comparer = Comparer<TKey>.Default;
        }

        public RedBlackTree(Comparer<TKey> customComparer)
        {
            comparer = customComparer;
        }

        public void Add(TKey key)
        {
            if (root is null)
            {
                // Case 3
                // New node is root
                root = new RedBlackTreeNode<TKey>(key, null);
                root.Color = NodeColor.Black;
            }
            else
            {
                // Regular binary tree insertion
                var node = root;
                int childDir;
                while (true)
                {
                    childDir = comparer.Compare(key, node!.Key);
                    if (childDir < 0)
                    {
                        if (node.Left is null)
                        {
                            node.Left = new RedBlackTreeNode<TKey>(key, node);
                            break;
                        }
                        else
                        {
                            node = node.Left;
                        }
                    }
                    else if (childDir > 0)
                    {
                        if (node.Right is null)
                        {
                            node.Right = new RedBlackTreeNode<TKey>(key, node);
                            break;
                        }
                        else
                        {
                            node = node.Right;
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"Key \"{key}\" already exists in tree!");
                    }
                }

                // Return tree to valid state
                do
                {
                    // Case 1
                    if (node.Color == NodeColor.Black)
                    {
                        break;
                    }

                    // Case 4
                    if (node.Parent is null)
                    {
                        node.Color = NodeColor.Black;
                        break;
                    }

                    // Remaining insert cases need uncle
                    var grandparent = node.Parent;
                    var parentDir = comparer.Compare(node.Key, node.Parent.Key);
                    RedBlackTreeNode<TKey>? uncle;
                    if (parentDir < 0)
                    {
                        uncle = grandparent.Right;
                    }
                    else
                    {
                        uncle = grandparent.Left;
                    }

                    // Case 5 & 6
                    if (uncle is null || uncle.Color == NodeColor.Black)
                    {
                        if (parentDir < 0)
                        {
                            // Case 5
                            if (childDir > 0)
                            {
                                node = RotateLeft(node);
                            }

                            // Case 6
                            node = RotateRight(node.Parent!);
                            node.Color = NodeColor.Black;
                            node.Right!.Color = NodeColor.Red;
                        }
                        else
                        {
                            // Case 5
                            if (childDir < 0)
                            {
                                node = RotateRight(node);
                            }

                            // Case 6
                            node = RotateLeft(node.Parent!);
                            node.Color = NodeColor.Black;
                            node.Left!.Color = NodeColor.Red;
                        }

                        // Update root if it changed
                        if (node.Parent is null)
                        {
                            root = node;
                        }

                        break;
                    }

                    // Case 2
                    node.Color = NodeColor.Black;
                    uncle!.Color = NodeColor.Black;
                    grandparent.Color = NodeColor.Red;

                    // Set current node as parent to move up tree
                    node = node.Parent.Parent;
                }
                while (node is not null);
            }

            Count++;
        }

        public void AddRange(IEnumerable<TKey> keys)
        {
            foreach (TKey key in keys)
            {
                Add(key);
            }
        }

        public void Remove(TKey key)
        {
            if (root is null)
            {
                throw new InvalidOperationException("Tree is empty!");
            }
            else if (!Contains(key))
            {
                throw new KeyNotFoundException($"Key {key} is not in the tree!");
            }

            var node = root;
            int compareResult;
            while (true)
            {
                compareResult = comparer.Compare(key, node!.Key);
                if (compareResult < 0)
                {
                    node = node.Left;
                }
                else if (compareResult > 0)
                {
                    node = node.Right;
                }
                else
                {
                    break;
                }
            }

            Count--;
        }

        /// <summary>
        ///     Check if given node is in the tree.
        /// </summary>
        /// <param name="key">Key value to search for.</param>
        /// <returns>Whether or not the node is in the tree.</returns>
        public bool Contains(TKey key)
        {
            var node = root;
            while (node is not null)
            {
                var compareResult = comparer.Compare(key, node.Key);
                if (compareResult < 0)
                {
                    node = node.Left;
                }
                else if (compareResult > 0)
                {
                    node = node.Right;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Get the minimum value in the tree.
        /// </summary>
        /// <returns>Minimum value in tree.</returns>
        public TKey GetMin()
        {
            if (root is null)
            {
                throw new InvalidOperationException("Tree is empty!");
            }

            return GetMin(root).Key;
        }

        /// <summary>
        ///     Get the maximum value in the tree.
        /// </summary>
        /// <returns>Maximum value in tree.</returns>
        public TKey GetMax()
        {
            if (root is null)
            {
                throw new InvalidOperationException("Tree is empty!");
            }

            return GetMax(root).Key;
        }

        /// <summary>
        ///     Get keys in order from smallest to largest as defined by the comparer.
        /// </summary>
        /// <returns>Keys in tree in order from smallest to largest.</returns>
        public IEnumerable<TKey> GetKeysInOrder()
        {
            List<TKey> result = new List<TKey>();
            InOrderWalk(root);
            return result;

            void InOrderWalk(RedBlackTreeNode<TKey>? node)
            {
                if (node is null)
                {
                    return;
                }

                InOrderWalk(node.Left);
                result.Add(node.Key);
                InOrderWalk(node.Right);
            }
        }

        /// <summary>
        ///     Get keys in the pre-order order.
        /// </summary>
        /// <returns>Keys in pre-order order.</returns>
        public IEnumerable<TKey> GetKeysPreOrder()
        {
            var result = new List<TKey>();
            PreOrderWalk(root);
            return result;

            void PreOrderWalk(RedBlackTreeNode<TKey>? node)
            {
                if (node is null)
                {
                    return;
                }

                result.Add(node.Key);
                PreOrderWalk(node.Left);
                PreOrderWalk(node.Right);
            }
        }

        /// <summary>
        ///     Get keys in the post-order order.
        /// </summary>
        /// <returns>Keys in the post-order order.</returns>
        public IEnumerable<TKey> GetKeysPostOrder()
        {
            var result = new List<TKey>();
            PostOrderWalk(root);
            return result;

            void PostOrderWalk(RedBlackTreeNode<TKey>? node)
            {
                if (node is null)
                {
                    return;
                }

                PostOrderWalk(node.Left);
                PostOrderWalk(node.Right);
                result.Add(node.Key);
            }
        }

        /// <summary>
        ///     Perform a left (counter-clockwise) rotation.
        /// </summary>
        /// <param name="node">Node to rotate about.</param>
        /// <returns>New node with rotation applied.</returns>
        private RedBlackTreeNode<TKey> RotateLeft(RedBlackTreeNode<TKey> node)
        {
            var temp1 = node;
            var temp2 = node!.Right!.Left;

            node = node.Right;
            node.Parent = temp1.Parent;
            if (node.Parent is not null)
            {
                var nodeDir = comparer.Compare(node.Key, node.Parent.Key);
                if (nodeDir < 0)
                {
                    node.Parent.Left = node;
                }
                else
                {
                    node.Parent.Right = node;
                }
            }

            node.Left = temp1;
            node.Left.Parent = node;

            node.Left.Right = temp2;
            if (temp2 is not null)
            {
                node.Left.Right!.Parent = temp1;
            }

            return node;
        }

        /// <summary>
        ///     Perform a right (clockwise) rotation.
        /// </summary>
        /// <param name="node">Node to rotate about.</param>
        /// <returns>New node with rotation applied.</returns>
        private RedBlackTreeNode<TKey> RotateRight(RedBlackTreeNode<TKey> node)
        {
            var temp1 = node;
            var temp2 = node!.Left!.Right;

            node = node.Left;
            node.Parent = temp1.Parent;
            if (node.Parent is not null)
            {
                var nodeDir = comparer.Compare(node.Key, node.Parent.Key);
                if (nodeDir < 0)
                {
                    node.Parent.Left = node;
                }
                else
                {
                    node.Parent.Right = node;
                }
            }

            node.Right = temp1;
            node.Right.Parent = node;

            node.Right.Left = temp2;
            if (temp2 is not null)
            {
                node.Right.Left!.Parent = temp1;
            }

            return node;
        }

        /// <summary>
        ///     Helper function to get node instance with minimum key value
        ///     in the specified subtree.
        /// </summary>
        /// <param name="node">Node specifying root of subtree.</param>
        /// <returns>Minimum value in node's subtree.</returns>
        private RedBlackTreeNode<TKey> GetMin(RedBlackTreeNode<TKey> node)
        {
            while (node.Left is not null)
            {
                node = node.Left;
            }

            return node;
        }

        /// <summary>
        ///     Helper function to get node instance with maximum key value
        ///     in the specified subtree.
        /// </summary>
        /// <param name="node">Node specifyng root of subtree.</param>
        /// <returns>Maximum value in node's subtree.</returns>
        private RedBlackTreeNode<TKey> GetMax(RedBlackTreeNode<TKey> node)
        {
            while (node.Right is not null)
            {
                node = node.Right;
            }

            return node;
        }
    }
}
