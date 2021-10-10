using System;
using System.Collections.Generic;

namespace DataStructures.AVLTree
{
    /// <summary>
    ///     A simple self-balancing binary tree.
    /// </summary>
    /// <remarks>
    ///     An AVL tree is a self-balancing binary search tree (BST) named after
    ///     its inventors: Adelson, Velsky, and Landis. It is the first self-
    ///     balancing BST invented. The primary property of an AVL tree is that
    ///     the height of both child subtrees for any node only differ by one.
    ///     Due to the balanced nature of the tree, its time complexities for
    ///     insertion, deletion, and search all have a worst-case time complexity
    ///     of O(log n). Which is an improvement over the worst-case O(n) for a
    ///     regular BST.
    ///     <br></br><br></br>
    ///     See https://en.wikipedia.org/wiki/AVL_tree for more information.
    /// </remarks>
    /// <typeparam name="TKey">Type of key for the tree.</typeparam>
    public class AVLTree<TKey>
    {
        public int Count { get; private set; }

        private readonly Comparer<TKey> comparer;
        private AVLTreeNode<TKey>? root;

        public AVLTree()
        {
            root = null;
            comparer = Comparer<TKey>.Default;
            Count = 0;
        }

        public AVLTree(Comparer<TKey> customComparer)
        {
            root = null;
            comparer = customComparer;
            Count = 0;
        }

        public void Add(TKey key)
        {
            if (root is null)
            {
                root = new AVLTreeNode<TKey>(key);
            }
            else
            {
                root = Add(root, key);
            }

            Count++;
        }

        public void AddRange(IEnumerable<TKey> keys)
        {
            foreach(var key in keys)
            {
                Add(key);
            }
        }

        public bool Remove(TKey key)
        {
            if (root is null)
            {
                return false;
            }

            return true;
        }

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

        public TKey GetMin()
        {
            if(root is null)
            {
                throw new InvalidOperationException("Tree is empty!");
            }

            return GetMin(root).Key;
        }

        public TKey GetMax()
        {
            if(root is null)
            {
                throw new InvalidOperationException("Tree is empty!");
            }

            return GetMax(root).Key;
        }

        public IEnumerable<TKey> GetKeysInOrder()
        {
            List<TKey> result = new List<TKey>();
            InOrderWalk(root);
            return result;

            void InOrderWalk(AVLTreeNode<TKey>? node)
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

        public IEnumerable<TKey> GetKeysPreOrder()
        {
            var result = new List<TKey>();
            PreOrderWalk(root);
            return result;

            void PreOrderWalk(AVLTreeNode<TKey>? node)
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

        public IEnumerable<TKey> GetKeysPostOrder()
        {
            var result = new List<TKey>();
            PostOrderWalk(root);
            return result;

            void PostOrderWalk(AVLTreeNode<TKey>? node)
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

        private AVLTreeNode<TKey> Add(AVLTreeNode<TKey> node, TKey key)
        {
            int compareResult = comparer.Compare(key, node.Key);
            if (compareResult < 0)
            {
                if (node.Left is null)
                {
                    var newNode = new AVLTreeNode<TKey>(key);
                    node.Left = newNode;
                }
                else
                {
                    node.Left = Add(node.Left, key);
                }
            }
            else if (compareResult > 0)
            {
                if (node.Right is null)
                {
                    var newNode = new AVLTreeNode<TKey>(key);
                    node.Right = newNode;
                }
                else
                {
                    node.Right = Add(node.Right, key);
                }
            }
            else
            {
                throw new ArgumentException($"Key \"{key}\" already exists in tree!");
            }

            node.UpdateBalanceFactor();

            return Rebalance(node);
        }

        private AVLTreeNode<TKey> Rebalance(AVLTreeNode<TKey> node)
        {
            if (node.BalanceFactor > 1)
            {
                if (node.Right!.BalanceFactor == 1)
                {
                    node = RotateLeft(node);
                }
                else if (node.Right!.BalanceFactor == -1)
                {
                    node.Right = RotateRight(node.Right);
                    node = RotateLeft(node);
                }
            }
            else if (node.BalanceFactor < -1)
            {
                if (node.Left!.BalanceFactor == 1)
                {
                    node.Left = RotateLeft(node.Left);
                    node = RotateRight(node);
                }
                else if (node.Left!.BalanceFactor == -1)
                {
                    node = RotateRight(node);
                }
            }

            return node;
        }

        private AVLTreeNode<TKey> RotateLeft(AVLTreeNode<TKey> node)
        {
            var temp1 = node;
            var temp2 = node!.Right!.Left;
            node = node.Right;
            node.Left = temp1;
            node.Left.Right = temp2;

            node.Left.UpdateBalanceFactor();
            node.UpdateBalanceFactor();

            return node;
        }

        private AVLTreeNode<TKey> RotateRight(AVLTreeNode<TKey> node)
        {
            var temp1 = node;
            var temp2 = node!.Left!.Right;
            node = node.Left;
            node.Right = temp1;
            node.Right.Left = temp2;

            node.Right.UpdateBalanceFactor();
            node.UpdateBalanceFactor();

            return node;
        }

        private AVLTreeNode<TKey> GetMin(AVLTreeNode<TKey> node)
        {
            while (node.Left is not null)
            {
                node = node.Left;
            }

            return node;
        }

        private AVLTreeNode<TKey> GetMax(AVLTreeNode<TKey> node)
        {
            while (node.Right is not null)
            {
                node = node.Right;
            }

            return node;
        }
    }
}
