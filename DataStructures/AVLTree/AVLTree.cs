using System;
using System.Collections.Generic;

namespace DataStructures.AVLTree;

/// <summary>
///     A simple self-balancing binary tree.
/// </summary>
/// <remarks>
///     An AVL tree is a self-balancing binary search tree (BST) named after
///     its inventors: Adelson, Velsky, and Landis. It is the first self-
///     balancing BST invented. The primary property of an AVL tree is that
///     the height of both child subtrees for any node only differ by one.
///     Due to the balanced nature of the tree, its time complexities for
///     insertion, deletion, and search all have a worst-case time
///     complexity of O(log n). Which is an improvement over the worst-case
///     O(n) for a regular BST.
///     See https://en.wikipedia.org/wiki/AVL_tree for more information.
///     Visualizer: https://visualgo.net/en/bst.
/// </remarks>
/// <typeparam name="TKey">Type of key for the tree.</typeparam>
public class AvlTree<TKey>
{
    /// <summary>
    ///     Gets the number of nodes in the tree.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    ///     Comparer to use when comparing key values.
    /// </summary>
    private readonly Comparer<TKey> comparer;

    /// <summary>
    ///     Reference to the root node.
    /// </summary>
    private AvlTreeNode<TKey>? root;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AvlTree{TKey}"/>
    ///     class.
    /// </summary>
    public AvlTree()
    {
        comparer = Comparer<TKey>.Default;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="AvlTree{TKey}"/>
    ///     class using the specified comparer.
    /// </summary>
    /// <param name="customComparer">
    /// Comparer to use when comparing keys.
    /// </param>
    public AvlTree(Comparer<TKey> customComparer)
    {
        comparer = customComparer;
    }

    /// <summary>
    ///     Add a single node to the tree.
    /// </summary>
    /// <param name="key">Key value to add.</param>
    public void Add(TKey key)
    {
        if (root is null)
        {
            root = new AvlTreeNode<TKey>(key);
        }
        else
        {
            root = Add(root, key);
        }

        Count++;
    }

    /// <summary>
    ///     Add multiple nodes to the tree.
    /// </summary>
    /// <param name="keys">Key values to add.</param>
    public void AddRange(IEnumerable<TKey> keys)
    {
        foreach (var key in keys)
        {
            Add(key);
        }
    }

    /// <summary>
    ///     Remove a node from the tree.
    /// </summary>
    /// <param name="key">Key value to remove.</param>
    public void Remove(TKey key)
    {
        root = Remove(root, key);
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
            throw new InvalidOperationException("AVL tree is empty.");
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
            throw new InvalidOperationException("AVL tree is empty.");
        }

        return GetMax(root).Key;
    }

    /// <summary>
    ///     Get keys in order from smallest to largest as defined by the
    ///     comparer.
    /// </summary>
    /// <returns>Keys in tree in order from smallest to largest.</returns>
    public IEnumerable<TKey> GetKeysInOrder()
    {
        List<TKey> result = new();
        InOrderWalk(root);
        return result;

        void InOrderWalk(AvlTreeNode<TKey>? node)
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

        void PreOrderWalk(AvlTreeNode<TKey>? node)
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

        void PostOrderWalk(AvlTreeNode<TKey>? node)
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
    ///     Helper function to rebalance the tree so that all nodes have a
    ///     balance factor in the range [-1, 1].
    /// </summary>
    /// <param name="node">Node to rebalance.</param>
    /// <returns>New node that has been rebalanced.</returns>
    private static AvlTreeNode<TKey> Rebalance(AvlTreeNode<TKey> node)
    {
        if (node.BalanceFactor > 1)
        {
            if (node.Right!.BalanceFactor == -1)
            {
                node.Right = RotateRight(node.Right);
            }

            return RotateLeft(node);
        }

        if (node.BalanceFactor < -1)
        {
            if (node.Left!.BalanceFactor == 1)
            {
                node.Left = RotateLeft(node.Left);
            }

            return RotateRight(node);
        }

        return node;
    }

    /// <summary>
    ///     Perform a left (counter-clockwise) rotation.
    /// </summary>
    /// <param name="node">Node to rotate about.</param>
    /// <returns>New node with rotation applied.</returns>
    private static AvlTreeNode<TKey> RotateLeft(AvlTreeNode<TKey> node)
    {
        var temp1 = node;
        var temp2 = node.Right!.Left;
        node = node.Right;
        node.Left = temp1;
        node.Left.Right = temp2;

        node.Left.UpdateBalanceFactor();
        node.UpdateBalanceFactor();

        return node;
    }

    /// <summary>
    ///     Perform a right (clockwise) rotation.
    /// </summary>
    /// <param name="node">Node to rotate about.</param>
    /// <returns>New node with rotation applied.</returns>
    private static AvlTreeNode<TKey> RotateRight(AvlTreeNode<TKey> node)
    {
        var temp1 = node;
        var temp2 = node.Left!.Right;
        node = node.Left;
        node.Right = temp1;
        node.Right.Left = temp2;

        node.Right.UpdateBalanceFactor();
        node.UpdateBalanceFactor();

        return node;
    }

    /// <summary>
    ///     Helper function to get node instance with minimum key value
    ///     in the specified subtree.
    /// </summary>
    /// <param name="node">Node specifying root of subtree.</param>
    /// <returns>Minimum value in node's subtree.</returns>
    private static AvlTreeNode<TKey> GetMin(AvlTreeNode<TKey> node)
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
    /// <param name="node">Node specifying root of subtree.</param>
    /// <returns>Maximum value in node's subtree.</returns>
    private static AvlTreeNode<TKey> GetMax(AvlTreeNode<TKey> node)
    {
        while (node.Right is not null)
        {
            node = node.Right;
        }

        return node;
    }

    /// <summary>
    ///     Recursively function to add a node to the tree.
    /// </summary>
    /// <param name="node">Node to check for null leaf.</param>
    /// <param name="key">Key value to add.</param>
    /// <returns>New node with key inserted.</returns>
    private AvlTreeNode<TKey> Add(AvlTreeNode<TKey> node, TKey key)
    {
        // Regular binary search tree insertion
        var compareResult = comparer.Compare(key, node.Key);
        if (compareResult < 0)
        {
            if (node.Left is null)
            {
                var newNode = new AvlTreeNode<TKey>(key);
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
                var newNode = new AvlTreeNode<TKey>(key);
                node.Right = newNode;
            }
            else
            {
                node.Right = Add(node.Right, key);
            }
        }
        else
        {
            throw new ArgumentException(
                $"Key \"{key}\" already exists in AVL tree.");
        }

        // Check all of the new node's ancestors for inbalance and perform
        // necessary rotations
        node.UpdateBalanceFactor();

        return Rebalance(node);
    }

    /// <summary>
    ///     Recursive function to remove node from tree.
    /// </summary>
    /// <param name="node">Node to check for key.</param>
    /// <param name="key">Key value to remove.</param>
    /// <returns>New node with key removed.</returns>
    private AvlTreeNode<TKey>? Remove(AvlTreeNode<TKey>? node, TKey key)
    {
        if (node == null)
        {
            throw new KeyNotFoundException(
                $"Key \"{key}\" is not in the AVL tree.");
        }

        // Normal binary search tree removal
        var compareResult = comparer.Compare(key, node.Key);
        if (compareResult < 0)
        {
            node.Left = Remove(node.Left, key);
        }
        else if (compareResult > 0)
        {
            node.Right = Remove(node.Right, key);
        }
        else
        {
            if (node.Left is null && node.Right is null)
            {
                return null;
            }

            if (node.Left is null)
            {
                var successor = GetMin(node.Right!);
                node.Right = Remove(node.Right!, successor.Key);
                node.Key = successor.Key;
            }
            else
            {
                var predecessor = GetMax(node.Left!);
                node.Left = Remove(node.Left!, predecessor.Key);
                node.Key = predecessor.Key;
            }
        }

        // Check all of the removed node's ancestors for rebalance and
        // perform necessary rotations.
        node.UpdateBalanceFactor();

        return Rebalance(node);
    }
}
