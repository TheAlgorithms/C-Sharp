using System;
using System.Collections.Generic;
using DataStructures.BinarySearchTree;

namespace Algorithms.Graph
{
    /// <summary>
    ///     Breadth first tree traversal traverses through a binary tree
    ///     by iterating through each level first.
    /// </summary>
    /// <typeparam name="TKey">Type of key held in binary search tree.</typeparam>
    public static class BreadthFirstTreeTraversal<TKey>
    {
        /// <summary>
        ///     Level Order Traversal returns an array of integers in order
        ///     of each level of a binary tree. It uses a queue to iterate
        ///     through each node following breadth first search traversal.
        /// </summary>
        /// <param name="tree">Passes the binary tree to traverse.</param>
        /// <returns>Returns level order traversal.</returns>
        public static TKey[] LevelOrderTraversal(BinarySearchTree<TKey> tree)
        {
            BinarySearchTreeNode<TKey>? root = tree.Root;
            TKey[] levelOrder = new TKey[tree.Count];
            if (root is null)
            {
                return Array.Empty<TKey>();
            }

            Queue<BinarySearchTreeNode<TKey>> breadthTraversal = new Queue<BinarySearchTreeNode<TKey>>();
            breadthTraversal.Enqueue(root);
            for (int i = 0; i < levelOrder.Length; i++)
            {
                BinarySearchTreeNode<TKey> current = breadthTraversal.Dequeue();
                levelOrder[i] = current.Key;
                if (current.Left is not null)
                {
                    breadthTraversal.Enqueue(current.Left);
                }

                if (current.Right is not null)
                {
                    breadthTraversal.Enqueue(current.Right);
                }
            }

            return levelOrder;
        }
    }
}
