using System;
using System.Collections.Generic;

namespace DataStructures.ScapegoatTree
{
    public class ScapegoatTreeImplementation<TKey> : IScapegoatTreeImplementable<TKey>
        where TKey : IComparable
    {
        /// <summary>
        /// Searches for the key in the subtree starting from the root.
        /// </summary>
        /// <param name="root">Root of subtree.</param>
        /// <param name="key">Key to search for.</param>
        /// <returns>Node or null if not found.</returns>
        public Node<TKey>? SearchWithRoot(Node<TKey> root, TKey key)
        {
            while (true)
            {
                var result = root.Key.CompareTo(key);

                switch (result)
                {
                    case 0:
                        return root;
                    case > 0 when root.Left != null:
                        root = root.Left;
                        break;
                    case < 0 when root.Right != null:
                        root = root.Right;
                        break;
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Searches for the key in the subtree starting with root.
        /// </summary>
        /// <param name="root">Root of the subtree.</param>
        /// <param name="key">Key to search for.</param>
        /// <returns>True if deletion is successful, false if key not found.</returns>
        /// <exception cref="InvalidOperationException">Throws exception if the tree is invalid for some reason.</exception>
        public bool TryDeleteWithRoot(Node<TKey> root, TKey key)
        {
            Node<TKey> previous = root;
            ref Node<TKey>? current = ref root!;

            while (true)
            {
                var result = current.Key.CompareTo(key);

                if (result == 0)
                {
                    break;
                }

                previous = current;

                switch (result)
                {
                    case < 0 when current.Right != null:
                        current = current.Right;
                        continue;
                    case > 0 when current.Left != null:
                        current = current.Left;
                        continue;
                    default:
                        return false;
                }
            }

            Node<TKey>? replacementNode;

            // Case 0: Node has no children.
            // Case 1: Node has one child.
            if (current.Left is null || current.Right is null)
            {
                replacementNode = current.Left ?? current.Right;
            }

            // Case 2: Node has two children. (This implementation uses the in-order predecessor to replace node.)
            else
            {
                var predecessorNode = current.Left.GetLargestKeyNode();
                TryDeleteWithRoot(root, predecessorNode.Key);

                replacementNode = new Node<TKey>(predecessorNode.Key)
                {
                    Left = current.Left,
                    Right = current.Right,
                };
            }

            // Replace the relevant node with a replacement found in the previous stages.
            // Special case for replacing the root node.
            if (current == root)
            {
                current = ref replacementNode;
            }
            else if (previous.Left == current)
            {
                previous.Left = replacementNode;
            }
            else
            {
                previous.Right = replacementNode;
            }

            return true;
        }

        /// <summary>
        /// Tries to insert a node into subtee started with root.
        /// Keeps the traversed path in stack.
        /// Won't insert duplicate key.
        /// </summary>
        /// <param name="root">Root of the subtree.</param>
        /// <param name="node">Node to insert.</param>
        /// <param name="path">Stack-based path.</param>
        /// <returns>True - if key is inserted, False - if key is duplicate.</returns>
        public bool TryInsertWithRoot(Node<TKey> root, Node<TKey> node, Stack<Node<TKey>> path)
        {
            while (true)
            {
                path.Push(root);

                var result = root.Key.CompareTo(node.Key);

                switch (result)
                {
                    case < 0 when root.Right != null:
                        root = root.Right;
                        continue;
                    case < 0:
                        root.Right = node;
                        return true;
                    case > 0 when root.Left != null:
                        root = root.Left;
                        continue;
                    case > 0:
                        root.Left = node;
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Rebuilds the subtree using stack-based path (obtained during insert opration).
        /// Searches for scapegoat node in path, rebuilds a subtree with scapegoat as it's root.
        /// </summary>
        /// <param name="alpha">Alpha value.</param>
        /// <param name="path">Stack-based path.</param>
        /// <returns>Returns rebuilded subtree and it's parent node.</returns>
        public (Node<TKey>? parent, Node<TKey> subtree) RebuildFromPath(double alpha, Stack<Node<TKey>> path)
        {
            var (parent, scapegoat) = FindScapegoatInPath(path, alpha);

            var list = new List<Node<TKey>>();

            FlattenTree(scapegoat, list);

            return (parent, RebuildFromList(list, 0, list.Count - 1));
        }

        /// <summary>
        /// Rebuilds a subtree.
        /// </summary>
        /// <param name="root">Root of the subtree.</param>
        /// <returns>Returns a node which is a root of rebuilded subtree.</returns>
        public Node<TKey> RebuildWithRoot(Node<TKey> root)
        {
            var list = new List<Node<TKey>>();

            FlattenTree(root, list);

            return RebuildFromList(list, 0, list.Count - 1);
        }

        /// <summary>
        /// Searches for scapegoat node in stack-based path.
        /// </summary>
        /// <param name="path">Stack-based bath (obtained from insert operation).</param>
        /// <param name="alpha">Alpha value.</param>
        /// <returns>Scapegoat node and it's parent. Parent can be null if scapegoat is root.</returns>
        /// <exception cref="ArgumentException">Throws if path is empty.</exception>
        /// <exception cref="InvalidOperationException">Throws if scapegoat node wasn't found.</exception>
        public (Node<TKey>? parent, Node<TKey> scapegoat) FindScapegoatInPath(Stack<Node<TKey>> path, double alpha)
        {
            if (path.Count == 0)
            {
                throw new ArgumentException("The path collection should not be empty.", nameof(path));
            }

            var depth = 1;

            while (path.TryPop(out var next))
            {
                if (depth > next.GetAlphaHeight(alpha))
                {
                    return path.TryPop(out var parent) ? (parent, next) : (null, next);
                }

                depth++;
            }

            throw new InvalidOperationException("Scapegoat node wasn't found. The tree should be unbalanced.");
        }

        /// <summary>
        /// Flattens the tree inroder into a list of nodes.
        /// </summary>
        /// <param name="root">Root node of a tree.</param>
        /// <param name="list">Resulting list of nodes.</param>
        public void FlattenTree(Node<TKey> root, List<Node<TKey>> list)
        {
            if (root.Left != null)
            {
                FlattenTree(root.Left, list);
            }

            list.Add(root);

            if (root.Right != null)
            {
                FlattenTree(root.Right, list);
            }
        }

        /// <summary>
        /// Rebuilds a tree from list of nodes (obtained from FlattenTree method).
        /// </summary>
        /// <param name="list">List of nodes.</param>
        /// <param name="start">Staring index in the list.</param>
        /// <param name="end">Ending oindex in the list.</param>
        /// <returns>Root node of rebuilded tree.</returns>
        /// <exception cref="ArgumentException">Throws if start value is greater than end value.</exception>
        public Node<TKey> RebuildFromList(IList<Node<TKey>> list, int start, int end)
        {
            if (start > end)
            {
                throw new ArgumentException("The parameter's value is invalid.", nameof(start));
            }

            var pivot = Convert.ToInt32(Math.Ceiling(start + (end - start) / 2.0));

            return new Node<TKey>(list[pivot].Key)
            {
                Left = start > (pivot - 1) ? null : RebuildFromList(list, start, pivot - 1),
                Right = (pivot + 1) > end ? null : RebuildFromList(list, pivot + 1, end),
            };
        }
    }
}
