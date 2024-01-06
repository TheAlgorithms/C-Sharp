using System;
using System.Collections.Generic;

namespace DataStructures.ScapegoatTree;

/// <summary>
/// A scapegoat implementation class.
/// See https://en.wikipedia.org/wiki/Scapegoat_tree for more information about scapegoat tree.
/// </summary>
/// <typeparam name="TKey">The scapegoat tree key type.</typeparam>
public class ScapegoatTree<TKey> where TKey : IComparable
{
    /// <summary>
    /// Gets the α (alpha) value of the tree.
    /// </summary>
    public double Alpha { get; private set; }

    /// <summary>
    /// Gets the root node of the tree.
    /// </summary>
    public Node<TKey>? Root { get; private set; }

    /// <summary>
    /// Gets the number of nodes in the tree.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// Gets the maximal value of the tree Size since the last time the tree was completely rebuilt.
    /// </summary>
    public int MaxSize { get; private set; }

    /// <summary>
    /// Gets an event handler which will fire when tree is being balanced.
    /// </summary>
    public event EventHandler? TreeIsUnbalanced;

    public ScapegoatTree()
        : this(alpha: 0.5, size: 0)
    {
    }

    public ScapegoatTree(double alpha)
        : this(alpha, size: 0)
    {
    }

    public ScapegoatTree(Node<TKey> node, double alpha)
        : this(alpha, size: node.GetSize())
    {
        Root = node;
    }

    public ScapegoatTree(TKey key, double alpha = 0.5)
        : this(alpha, size: 1)
    {
        Root = new Node<TKey>(key);
    }

    private ScapegoatTree(double alpha, int size)
    {
        CheckAlpha(alpha);

        Alpha = alpha;

        Size = size;
        MaxSize = size;
    }

    /// <summary>
    /// Checks if current instance of the scapegoat tree is alpha weight balanced.
    /// </summary>
    /// <returns>True - if tree is alpha weight balanced. Otherwise, false.</returns>
    public bool IsAlphaWeightBalanced()
    {
        return Root?.IsAlphaWeightBalanced(Alpha) ?? true;
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
    /// Searches current instance of the scapegoat tree for specified key.
    /// </summary>
    /// <param name="key">Key value.</param>
    /// <returns>Node with the specified key or null.</returns>
    public Node<TKey>? Search(TKey key)
    {
        if (Root == null)
        {
            return null;
        }

        var current = Root;

        while (true)
        {
            var result = current.Key.CompareTo(key);

            switch (result)
            {
                case 0:
                    return current;
                case > 0 when current.Left != null:
                    current = current.Left;
                    break;
                case < 0 when current.Right != null:
                    current = current.Right;
                    break;
                default:
                    return null;
            }
        }
    }

    /// <summary>
    /// Inserts a new key into current instance of the scapegoat tree. Rebuilds tree if it's unbalanced.
    /// </summary>
    /// <param name="key">Key value.</param>
    /// <returns>True - if insertion is successful, false - if the key is already present in the tree.</returns>
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

        var current = Root;

        var found = false;

        while (!found)
        {
            path.Push(current);

            var result = current.Key.CompareTo(node.Key);

            switch (result)
            {
                case < 0 when current.Right != null:
                    current = current.Right;
                    continue;
                case < 0:
                    current.Right = node;
                    found = true;
                    break;
                case > 0 when current.Left != null:
                    current = current.Left;
                    continue;
                case > 0:
                    current.Left = node;
                    found = true;
                    break;
                default:
                    return false;
            }
        }

        UpdateSizes();

        if (path.Count > Root.GetAlphaHeight(Alpha))
        {
            TreeIsUnbalanced?.Invoke(this, EventArgs.Empty);

            BalanceFromPath(path);

            MaxSize = Math.Max(MaxSize, Size);
        }

        return true;
    }

    /// <summary>
    /// Removes the specified key from the current instance of the scapegoat tree. Rebuilds tree if it's unbalanced.
    /// </summary>
    /// <param name="key">Key value.</param>
    /// <returns>True - if key was successfully removed, false - if the key wasn't found in the tree.</returns>
    public bool Delete(TKey key)
    {
        if (Root == null)
        {
            return false;
        }

        if (Remove(Root, Root, key))
        {
            Size--;

            if (Root != null && Size < Alpha * MaxSize)
            {
                TreeIsUnbalanced?.Invoke(this, EventArgs.Empty);

                var list = new List<Node<TKey>>();

                Extensions.FlattenTree(Root, list);

                Root = Extensions.RebuildFromList(list, 0, list.Count - 1);

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
        Alpha = value;
    }

    /// <summary>
    /// Searches for a scapegoat node in provided stack.
    /// </summary>
    /// <param name="path">Stack instance with nodes, starting with root node.</param>
    /// <returns>Scapegoat node with its parent node. Parent can be null if scapegoat node is root node.</returns>
    /// <exception cref="ArgumentException">Thrown if path stack is empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown if scapegoat wasn't found.</exception>
    public (Node<TKey>? parent, Node<TKey> scapegoat) FindScapegoatInPath(Stack<Node<TKey>> path)
    {
        if (path.Count == 0)
        {
            throw new ArgumentException("The path collection should not be empty.", nameof(path));
        }

        var depth = 1;

        while (path.TryPop(out var next))
        {
            if (depth > next.GetAlphaHeight(Alpha))
            {
                return path.TryPop(out var parent) ? (parent, next) : (null, next);
            }

            depth++;
        }

        throw new InvalidOperationException("Scapegoat node wasn't found. The tree should be unbalanced.");
    }

    private static void CheckAlpha(double alpha)
    {
        if (alpha is < 0.5 or > 1.0)
        {
            throw new ArgumentException("The alpha parameter's value should be in 0.5..1.0 range.", nameof(alpha));
        }
    }

    private bool Remove(Node<TKey>? parent, Node<TKey>? node, TKey key)
    {
        if (node is null || parent is null)
        {
            return false;
        }

        var compareResult = node.Key.CompareTo(key);

        if (compareResult > 0)
        {
            return Remove(node, node.Left, key);
        }

        if (compareResult < 0)
        {
            return Remove(node, node.Right, key);
        }

        Node<TKey>? replacementNode;

        // Case 0: Node has no children.
        // Case 1: Node has one child.
        if (node.Left is null || node.Right is null)
        {
            replacementNode = node.Left ?? node.Right;
        }

        // Case 2: Node has two children. (This implementation uses the in-order predecessor to replace node.)
        else
        {
            var predecessorNode = node.Left.GetLargestKeyNode();
            Remove(Root, Root, predecessorNode.Key);
            replacementNode = new Node<TKey>(predecessorNode.Key)
            {
                Left = node.Left,
                Right = node.Right,
            };
        }

        // Replace the relevant node with a replacement found in the previous stages.
        // Special case for replacing the root node.
        if (node == Root)
        {
            Root = replacementNode;
        }
        else if (parent.Left == node)
        {
            parent.Left = replacementNode;
        }
        else
        {
            parent.Right = replacementNode;
        }

        return true;
    }

    private void BalanceFromPath(Stack<Node<TKey>> path)
    {
        var (parent, scapegoat) = FindScapegoatInPath(path);

        var list = new List<Node<TKey>>();

        Extensions.FlattenTree(scapegoat, list);

        var tree = Extensions.RebuildFromList(list, 0, list.Count - 1);

        if (parent == null)
        {
            Root = tree;
        }
        else
        {
            var result = parent.Key.CompareTo(tree.Key);

            if (result < 0)
            {
                parent.Right = tree;
            }
            else
            {
                parent.Left = tree;
            }
        }
    }

    private void UpdateSizes()
    {
        Size += 1;
        MaxSize = Math.Max(Size, MaxSize);
    }
}
