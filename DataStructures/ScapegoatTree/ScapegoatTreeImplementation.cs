using System;
using System.Collections.Generic;

namespace DataStructures.ScapegoatTree
{
    public class ScapegoatTreeImplementation<TKey> : ScapegoatTreeImplementationBase<TKey>
        where TKey : IComparable
    {
        public override Node<TKey>? SearchWithRoot(Node<TKey> root, TKey key)
        {
            while (true)
            {
                switch (root.CompareTo(key))
                {
                    case > 0 when root.Left != null:
                        root = root.Left;
                        continue;
                    case < 0 when root.Right != null:
                        root = root.Right;
                        continue;
                    case > 0:
                        return null;
                    case < 0:
                        return null;
                    case 0:
                        return root;
                }
            }
        }

        public override bool TryDeleteWithRoot(Node<TKey> root, TKey key)
        {
            var previous = root;
            var current = root;
            var found = false;

            while (!found)
            {
                var result = current.CompareTo(key);

                if (result == 0)
                {
                    found = true;
                }
                else
                {
                    previous = current;

                    switch (result)
                    {
                        case < 0 when current.Right != null:
                            current = current.Right;
                            continue;
                        case > 0 when current.Left != null:
                            current = current.Left;
                            continue;
                        case < 0:
                        case > 0:
                            return false;
                    }
                }
            }

            // case 0: Node has no children. Action - delete node:
            if (current.Left == null && current.Right == null)
            {
                if (previous.Left == current)
                {
                    previous.Left = null;
                }
                else
                {
                    previous.Right = null;
                }

                return true;
            }

            // case 1: Node ahs only one child. Action - add it to parent
            if (current.Left == null || current.Right == null)
            {
                if (previous.Left == current)
                {
                    previous.Left = current.Left ?? current.Right;
                }
                else
                {
                    previous.Right = current.Left ?? current.Right;
                }

                return true;
            }

            // case 2: node ahs two children. Action -
            else
            {
                var predecessor = current.Left.GetLargestKeyNode();

                if (!TryDeleteWithRoot(root, predecessor.Key))
                {
                    throw new InvalidOperationException("Error while trying to delete a key. The subtree is invalid.");
                }

                var tmp = new Node<TKey>(predecessor.Key, current.Right, current.Left);

                if (previous.Left == current)
                {
                    previous.Left = tmp;
                }
                else
                {
                    predecessor.Right = tmp;
                }

                return true;
            }
        }

        public override bool TryInsertWithRoot(Node<TKey> root, Node<TKey> node, Queue<Node<TKey>> path)
        {
            while (true)
            {
                path.Enqueue(root);

                switch (root.CompareTo(node.Key))
                {
                    case < 0 when root.Right != null:
                        root = root.Right;
                        continue;
                    case > 0 when root.Left != null:
                        root = root.Left;
                        continue;
                    case < 0:
                        root.Right = node;
                        return true;
                    case > 0:
                        root.Left = node;
                        return true;
                    case 0:
                        return false;
                }
            }
        }

        public override (Node<TKey>? parent, Node<TKey> subtree) RebuildFromPath(double alpha, Queue<Node<TKey>> path)
        {
            var (parent, scapegoat) = FindScapegoatInPath(null, path, alpha);

            var list = new List<Node<TKey>>();

            FlattenTree(scapegoat, list);

            return (parent, RebuildFromList(list, 0, list.Count - 1));
        }

        public override Node<TKey> RebuildWithRoot(Node<TKey> root)
        {
            var list = new List<Node<TKey>>();

            FlattenTree(root, list);

            return RebuildFromList(list, 0, list.Count - 1);
        }

        public (Node<TKey>? parent, Node<TKey> scapegoat) FindScapegoatInPath(
            Node<TKey>? parent, Queue<Node<TKey>> path, double alpha)
        {
            if (path.Count == 0)
            {
                throw new ArgumentException("The path collection should not be empty.", nameof(path));
            }

            while (true)
            {
                var depth = path.Count;

                if (path.TryDequeue(out var next) && next != null)
                {
                    if (depth > next.GetAlphaHeight(alpha))
                    {
                        return (parent, next);
                    }

                    parent = next;
                }
                else
                {
                    throw new ArgumentException("No scapegoat node was found. Check if the tree is unbalanced.");
                }
            }
        }

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

        public Node<TKey> RebuildFromList(IList<Node<TKey>> list, int start, int end)
        {
            if (start > end)
            {
                throw new ArgumentException(null, nameof(start));
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
