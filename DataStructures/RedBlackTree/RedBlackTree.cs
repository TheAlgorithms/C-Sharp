using System;
using System.Collections.Generic;

namespace DataStructures.RedBlackTree;

/// <summary>
///     A self-balancing bindary tree.
/// </summary>
/// <remarks>
///     A red-black tree is a self-balancing binary search tree (BST) that
///     stores a color with each node. A node's color can either be red or
///     black. Several properties are maintained to ensure the tree remains
///     balanced.
///     <list type="number">
///         <item>
///             <term>A red node does not have a red child.</term>
///         </item>
///         <item>
///             <term>All null nodes are considered black.</term>
///         </item>
///         <item>
///             <term>
///                 Every path from a node to its descendant leaf nodes
///             has the same number of black nodes.
///             </term>
///         </item>
///         <item>
///             <term>(Optional) The root is always black.</term>
///         </item>
///     </list>
///     Red-black trees are generally slightly more unbalanced than an
///     AVL tree, but insertion and deletion is generally faster.
///     See https://en.wikipedia.org/wiki/Red%E2%80%93black_tree for more information.
/// </remarks>
/// <typeparam name="TKey">Type of key for the tree.</typeparam>
public class RedBlackTree<TKey>
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
    private RedBlackTreeNode<TKey>? root;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RedBlackTree{TKey}"/> class.
    /// </summary>
    public RedBlackTree()
    {
        comparer = Comparer<TKey>.Default;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="RedBlackTree{TKey}"/> class
    ///     using the specified comparer.
    /// </summary>
    /// <param name="customComparer">Comparer to use when comparing keys.</param>
    public RedBlackTree(Comparer<TKey> customComparer)
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
            // Case 3
            // New node is root
            root = new RedBlackTreeNode<TKey>(key, null)
            {
                Color = NodeColor.Black,
            };
            Count++;
            return;
        }

        // Regular binary tree insertion
        var node = Add(root, key);

        // Get which side child was added to
        var childDir = comparer.Compare(node.Key, node.Parent!.Key);

        // Set node to be new node's parent for easier handling
        node = node.Parent;

        // Return tree to valid state
        int addCase;
        do
        {
            addCase = GetAddCase(node);

            switch(addCase)
            {
                case 1:
                    break;
                case 2:
                    var oldParent = node.Parent;
                    node = AddCase2(node);

                    if (node is not null)
                    {
                        childDir = comparer.Compare(oldParent!.Key, oldParent.Parent!.Key);
                    }

                    break;
                case 4:
                    node.Color = NodeColor.Black;
                    break;
                case 56:
                    AddCase56(node, comparer.Compare(node.Key, node.Parent!.Key), childDir);
                    break;
                default:
                    throw new InvalidOperationException("It should not be possible to get here!");
            }
        }
        while (addCase == 2 && node is not null);

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
        // Search for node
        var node = Remove(root, key);

        // Simple cases
        node = RemoveSimpleCases(node);

        // Exit if deleted node was not non-root black leaf
        if (node is null)
        {
            return;
        }

        // Delete node
        DeleteLeaf(node.Parent!, comparer.Compare(node.Key, node.Parent!.Key));

        // Recolor tree
        do
        {
            node = RemoveRecolor(node);
        }
        while (node is not null && node.Parent is not null);    // Case 2: Reached root

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
        var result = new List<TKey>();
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
    ///     Perform binary tree insertion.
    /// </summary>
    /// <param name="node">Root of subtree to search from.</param>
    /// <param name="key">Key value to insert.</param>
    /// <returns>Node that was added.</returns>
    private RedBlackTreeNode<TKey> Add(RedBlackTreeNode<TKey> node, TKey key)
    {
        int compareResult;
        RedBlackTreeNode<TKey> newNode;
        while (true)
        {
            compareResult = comparer.Compare(key, node!.Key);
            if (compareResult < 0)
            {
                if (node.Left is null)
                {
                    newNode = new RedBlackTreeNode<TKey>(key, node);
                    node.Left = newNode;
                    break;
                }
                else
                {
                    node = node.Left;
                }
            }
            else if (compareResult > 0)
            {
                if (node.Right is null)
                {
                    newNode = new RedBlackTreeNode<TKey>(key, node);
                    node.Right = newNode;
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

        return newNode;
    }

    /// <summary>
    ///     Perform case 2 of insertion by pushing blackness down from parent.
    /// </summary>
    /// <param name="node">Parent of inserted node.</param>
    /// <returns>Grandparent of inserted node.</returns>
    private RedBlackTreeNode<TKey>? AddCase2(RedBlackTreeNode<TKey> node)
    {
        var grandparent = node.Parent;
        var parentDir = comparer.Compare(node.Key, node.Parent!.Key);
        var uncle = parentDir < 0 ? grandparent!.Right : grandparent!.Left;

        node.Color = NodeColor.Black;
        uncle!.Color = NodeColor.Black;
        grandparent.Color = NodeColor.Red;

        // Keep root black
        if (node.Parent.Parent is null)
        {
            node.Parent.Color = NodeColor.Black;
        }

        // Set current node as parent to move up tree
        return node.Parent.Parent;
    }

    /// <summary>
    ///     Perform rotations needed for cases 5 and 6 of insertion.
    /// </summary>
    /// <param name="node">Parent of node just inserted.</param>
    /// <param name="parentDir">The side node is on of its parent.</param>
    /// <param name="childDir">The side the child node is on.</param>
    private void AddCase56(RedBlackTreeNode<TKey> node, int parentDir, int childDir)
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
    }

    /// <summary>
    ///     Determine which add case applies to inserted node.
    /// </summary>
    /// <param name="node">Parent of inserted node.</param>
    /// <returns>Case number needed to get tree in valid state. Cases 5 and 6 are represented by 56.</returns>
    private int GetAddCase(RedBlackTreeNode<TKey> node)
    {
        if (node.Color == NodeColor.Black)
        {
            return 1;
        }
        else if (node.Parent is null)
        {
            return 4;
        }
        else
        {
            // Remaining insert cases need uncle
            var grandparent = node.Parent;
            var parentDir = comparer.Compare(node.Key, node.Parent.Key);
            var uncle = parentDir < 0 ? grandparent.Right : grandparent.Left;

            // Case 5 & 6
            if (uncle is null || uncle.Color == NodeColor.Black)
            {
                return 56;
            }

            return 2;
        }
    }

    /// <summary>
    ///     Search for the node to be deleted.
    /// </summary>
    /// <param name="node">Node to start search from.</param>
    /// <param name="key">Key to search for.</param>
    /// <returns>Node to be deleted.</returns>
    private RedBlackTreeNode<TKey> Remove(RedBlackTreeNode<TKey>? node, TKey key)
    {
        if (node is null)
        {
            throw new InvalidOperationException("Tree is empty!");
        }
        else if (!Contains(key))
        {
            throw new KeyNotFoundException($"Key {key} is not in the tree!");
        }
        else
        {
            // Find node
            int dir;
            while (true)
            {
                dir = comparer.Compare(key, node!.Key);
                if (dir < 0)
                {
                    node = node.Left;
                }
                else if (dir > 0)
                {
                    node = node.Right;
                }
                else
                {
                    break;
                }
            }

            return node;
        }
    }

    /// <summary>
    ///     Get the tree back into a valid state after removing non-root black leaf.
    /// </summary>
    /// <param name="node">Non-root black leaf being removed.</param>
    private RedBlackTreeNode<TKey>? RemoveRecolor(RedBlackTreeNode<TKey> node)
    {
        var removeCase = GetRemoveCase(node);

        var dir = comparer.Compare(node.Key, node.Parent!.Key);

        // Determine current node's sibling and nephews
        var sibling = dir < 0 ? node.Parent.Right : node.Parent.Left;
        var closeNewphew = dir < 0 ? sibling!.Left : sibling!.Right;
        var distantNephew = dir < 0 ? sibling!.Right : sibling!.Left;

        switch (removeCase)
        {
            case 1:
                sibling.Color = NodeColor.Red;
                return node.Parent;
            case 3:
                RemoveCase3(node, closeNewphew, dir);
                break;
            case 4:
                RemoveCase4(sibling);
                break;
            case 5:
                RemoveCase5(node, sibling, dir);
                break;
            case 6:
                RemoveCase6(node, distantNephew!, dir);
                break;
            default:
                throw new InvalidOperationException("It should not be possible to get here!");
        }

        return null;
    }

    /// <summary>
    ///     Simple removal cases where black height doesn't change.
    /// </summary>
    /// <param name="node">Node to remove.</param>
    /// <returns>Non-root black leaf node or null. Null indicates that removal was performed.</returns>
    private RedBlackTreeNode<TKey>? RemoveSimpleCases(RedBlackTreeNode<TKey> node)
    {
        // Node to delete is root and has no children
        if (node.Parent is null && node.Left is null && node.Right is null)
        {
            root = null;
            Count--;
            return null;
        }

        // Node has two children. Swap pointers
        if (node.Left is not null && node.Right is not null)
        {
            var successor = GetMin(node.Right);
            node.Key = successor.Key;
            node = successor;
        }

        // At this point node should have at most one child
        if (node.Color == NodeColor.Red)
        {
            // Node is red so it must have no children since it doesn't have two children
            DeleteLeaf(node.Parent!, comparer.Compare(node.Key, node.Parent!.Key));

            Count--;
            return null;
        }
        else
        {
            // Node is black and may or may not be node
            return RemoveBlackNode(node);
        }
    }

    /// <summary>
    ///     Node to delete is black. If it is a leaf then we need to recolor, otherwise remove it.
    /// </summary>
    /// <param name="node">Black node to examine.</param>
    /// <returns>Node to start recoloring from. Null if deletion occurred.</returns>
    private RedBlackTreeNode<TKey>? RemoveBlackNode(RedBlackTreeNode<TKey> node)
    {
        // Node is black and has at most one child. If it has a child it must be red.
        var child = node.Left ?? node.Right;

        // Continue to recoloring if node is leaf
        if (child is null)
        {
            return node;
        }

        // Recolor child
        child.Color = NodeColor.Black;
        child.Parent = node.Parent;

        var childDir = node.Parent is null ? 0 : comparer.Compare(node.Key, node.Parent.Key);

        // Replace node with child
        Transplant(node.Parent, child, childDir);

        Count--;
        return null;
    }

    /// <summary>
    ///     Perform case 3 of removal.
    /// </summary>
    /// <param name="node">Node that was removed.</param>
    /// <param name="closeNephew">Close nephew of removed node.</param>
    /// <param name="childDir">Side of parent the removed node was.</param>
    private void RemoveCase3(RedBlackTreeNode<TKey> node, RedBlackTreeNode<TKey>? closeNephew, int childDir)
    {
        // Rotate and recolor
        var sibling = childDir < 0 ? RotateLeft(node.Parent!) : RotateRight(node.Parent!);
        sibling.Color = NodeColor.Black;
        if (childDir < 0)
        {
            sibling.Left!.Color = NodeColor.Red;
        }
        else
        {
            sibling.Right!.Color = NodeColor.Red;
        }

        // Get new distant newphew
        sibling = closeNephew!;
        var distantNephew = childDir < 0 ? sibling.Right : sibling.Left;

        // Parent is red, sibling is black
        if (distantNephew is not null && distantNephew.Color == NodeColor.Red)
        {
            RemoveCase6(node, distantNephew, childDir);
            return;
        }

        // Get new close nephew
        closeNephew = childDir < 0 ? sibling!.Left : sibling!.Right;

        // Sibling is black, distant nephew is black
        if (closeNephew is not null && closeNephew.Color == NodeColor.Red)
        {
            RemoveCase5(node, sibling!, childDir);
            return;
        }

        // Final recoloring
        RemoveCase4(sibling!);
    }

    /// <summary>
    ///     Perform case 4 of removal.
    /// </summary>
    /// <param name="sibling">Sibling of removed node.</param>
    private void RemoveCase4(RedBlackTreeNode<TKey> sibling)
    {
        sibling.Color = NodeColor.Red;
        sibling.Parent!.Color = NodeColor.Black;
    }

    /// <summary>
    ///     Perform case 5 of removal.
    /// </summary>
    /// <param name="node">Node that was removed.</param>
    /// <param name="sibling">Sibling of removed node.</param>
    /// <param name="childDir">Side of parent removed node was on.</param>
    private void RemoveCase5(RedBlackTreeNode<TKey> node, RedBlackTreeNode<TKey> sibling, int childDir)
    {
        sibling = childDir < 0 ? RotateRight(sibling) : RotateLeft(sibling);
        var distantNephew = childDir < 0 ? sibling.Right! : sibling.Left!;

        sibling.Color = NodeColor.Black;
        distantNephew.Color = NodeColor.Red;

        RemoveCase6(node, distantNephew, childDir);
    }

    /// <summary>
    ///     Perform case 6 of removal.
    /// </summary>
    /// <param name="node">Node that was removed.</param>
    /// <param name="distantNephew">Distant nephew of removed node.</param>
    /// <param name="childDir">Side of parent removed node was on.</param>
    private void RemoveCase6(RedBlackTreeNode<TKey> node, RedBlackTreeNode<TKey> distantNephew, int childDir)
    {
        var oldParent = node.Parent!;
        node = childDir < 0 ? RotateLeft(oldParent) : RotateRight(oldParent);
        node.Color = oldParent.Color;
        oldParent.Color = NodeColor.Black;
        distantNephew.Color = NodeColor.Black;
    }

    /// <summary>
    ///     Determine which removal case is required.
    /// </summary>
    /// <param name="node">Node being removed.</param>
    /// <returns>Which removal case should be performed.</returns>
    private int GetRemoveCase(RedBlackTreeNode<TKey> node)
    {
        var dir = comparer.Compare(node.Key, node.Parent!.Key);

        // Determine current node's sibling and nephews
        var sibling = dir < 0 ? node.Parent.Right : node.Parent.Left;
        var closeNewphew = dir < 0 ? sibling!.Left : sibling!.Right;
        var distantNephew = dir < 0 ? sibling!.Right : sibling!.Left;

        if (sibling.Color == NodeColor.Red)
        {
            return 3;
        }
        else if (distantNephew is not null && distantNephew.Color == NodeColor.Red)
        {
            return 6;
        }
        else if (closeNewphew is not null && closeNewphew.Color == NodeColor.Red)
        {
            return 5;
        }
        else if (node.Parent.Color == NodeColor.Red)
        {
            return 4;
        }
        else
        {
            return 1;
        }
    }

    /// <summary>
    ///     Set child of node or delete leaf.
    /// </summary>
    /// <param name="node">Node to set child of. Set to null for root.</param>
    /// <param name="child">Node to set as child.</param>
    /// <param name="dir">Which side of node to place child.</param>
    private void Transplant(RedBlackTreeNode<TKey>? node, RedBlackTreeNode<TKey>? child, int dir)
    {
        if (node is null)
        {
            root = child;
        }
        else if (child is null)
        {
            DeleteLeaf(node, dir);
        }
        else if (dir < 0)
        {
            node.Left = child;
        }
        else
        {
            node.Right = child;
        }
    }

    /// <summary>
    ///     Delete leaf node.
    /// </summary>
    /// <param name="node">Parent of leaf node to delete.</param>
    /// <param name="dir">Side of parent leaf is on.</param>
    private void DeleteLeaf(RedBlackTreeNode<TKey> node, int dir)
    {
        if (dir < 0)
        {
            node.Left = null;
        }
        else
        {
            node.Right = null;
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

        if (node.Parent is null)
        {
            root = node;
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

        if (node.Parent is null)
        {
            root = node;
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
