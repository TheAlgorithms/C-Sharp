using System;
using System.Collections.Generic;

namespace DataStructures.AATree
{
    /// <summary>
    /// A simple self-balancing binary search tree.
    /// </summary>
    /// <remarks>
    /// AA Trees are a form of self-balancing binary search tree named after their inventor
    /// Arne Anderson. AA Trees are designed to be simple to understand and implement.
    /// This is accomplished by limiting how nodes can be added to the tree.
    /// This simplifies rebalancing operations.
    /// More information: https://en.wikipedia.org/wiki/AA_tree .
    /// </remarks>
    /// <typeparam name="TKey">The type of key for the AA tree.</typeparam>
    public class AATree<TKey>
    {
        /// <summary>
        /// The comparer function to use to compare the keys.
        /// </summary>
        private readonly Comparer<TKey> comparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AATree{TKey}"/> class.
        /// </summary>
        public AATree()
            : this(Comparer<TKey>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AATree{TKey}"/> class with a custom comparer.
        /// </summary>
        /// <param name="customComparer">The custom comparer to use to compare keys.</param>
        public AATree(Comparer<TKey> customComparer) => comparer = customComparer;

        /// <summary>
        /// Gets the root of the tree.
        /// </summary>
        public AATreeNode<TKey>? Root { get; private set; }

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
            Root = Add(key, Root);
            Count++;
        }

        /// <summary>
        /// Add multiple elements to the tree.
        /// </summary>
        /// <param name="keys">The elements to add to the tree.</param>
        public void AddRange(IEnumerable<TKey> keys)
        {
            foreach (var key in keys)
            {
                Root = Add(key, Root);
                Count++;
            }
        }

        /// <summary>
        /// Remove a single element from the tree.
        /// </summary>
        /// <param name="key">Element to remove.</param>
        public void Remove(TKey key)
        {
            if (!Contains(key, Root))
            {
                throw new InvalidOperationException($"{nameof(key)} is not in the tree");
            }

            Root = Remove(key, Root);
            Count--;
        }

        /// <summary>
        /// Checks if the specified element is in the tree.
        /// </summary>
        /// <param name="key">The element to look for.</param>
        /// <returns>true if the element is in the tree, false otherwise.</returns>
        public bool Contains(TKey key) => Contains(key, Root);

        /// <summary>
        /// Gets the largest element in the tree. (ie. the element in the right most node).
        /// </summary>
        /// <returns>The largest element in the tree according to the stored comparer.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the tree is empty.</exception>
        public TKey GetMax()
        {
            if (Root is null)
            {
                throw new InvalidOperationException("Tree is empty!");
            }

            return GetMax(Root).Key;
        }

        /// <summary>
        /// Gets the smallest element in the tree. (ie. the element in the left most node).
        /// </summary>
        /// <returns>The smallest element in the tree according to the stored comparer.</returns>
        /// <throws>InvalidOperationException if the tree is empty.</throws>
        public TKey GetMin()
        {
            if (Root is null)
            {
                throw new InvalidOperationException("Tree is empty!");
            }

            return GetMin(Root).Key;
        }

        /// <summary>
        /// Gets all the elements in the tree in in-order order.
        /// </summary>
        /// <returns>Sequence of elements in in-order order.</returns>
        public IEnumerable<TKey> GetKeysInOrder()
        {
            var result = new List<TKey>();
            InOrderWalk(Root);
            return result;

            void InOrderWalk(AATreeNode<TKey>? node)
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
        /// Gets all the elements in the tree in pre-order order.
        /// </summary>
        /// <returns>Sequence of elements in pre-order order.</returns>
        public IEnumerable<TKey> GetKeysPreOrder()
        {
            var result = new List<TKey>();
            PreOrderWalk(Root);
            return result;

            void PreOrderWalk(AATreeNode<TKey>? node)
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
        /// Gets all the elements in the tree in post-order order.
        /// </summary>
        /// <returns>Sequence of elements in post-order order.</returns>
        public IEnumerable<TKey> GetKeysPostOrder()
        {
            var result = new List<TKey>();
            PostOrderWalk(Root);
            return result;

            void PostOrderWalk(AATreeNode<TKey>? node)
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
        /// Recursive function to add an element to the tree.
        /// </summary>
        /// <param name="key">The element to add.</param>
        /// <param name="node">The node to search for a empty spot.</param>
        /// <returns>The node with the added element.</returns>
        /// <exception cref="ArgumentException">Thrown if key is already in the tree.</exception>
        private AATreeNode<TKey> Add(TKey key, AATreeNode<TKey>? node)
        {
            if (node is null)
            {
                return new AATreeNode<TKey>(key, 1);
            }

            if (comparer.Compare(key, node.Key) < 0)
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
            if (node is null)
            {
                return null;
            }

            if (comparer.Compare(key, node.Key) < 0)
            {
                node.Left = Remove(key, node.Left);
            }
            else if (comparer.Compare(key, node.Key) > 0)
            {
                node.Right = Remove(key, node.Right);
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

            if (node.Right is not null)
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
        private bool Contains(TKey key, AATreeNode<TKey>? node) =>
            node is { }
            && comparer.Compare(key, node.Key) is { } v
            && v switch
            {
                { } when v > 0 => Contains(key, node.Right),
                { } when v < 0 => Contains(key, node.Left),
                _ => true,
            };

        /// <summary>
        /// Recursive to find the maximum/right-most element.
        /// </summary>
        /// <param name="node">The node to traverse from.</param>
        /// <returns>The node with the maximum/right-most element.</returns>
        private AATreeNode<TKey> GetMax(AATreeNode<TKey> node)
        {
            while (true)
            {
                if (node.Right is null)
                {
                    return node;
                }

                node = node.Right;
            }
        }

        /// <summary>
        /// Recursive to find the minimum/left-most element.
        /// </summary>
        /// <param name="node">The node to traverse from.</param>
        /// <returns>The node with the minimum/left-most element.</returns>
        private AATreeNode<TKey> GetMin(AATreeNode<TKey> node)
        {
            while (true)
            {
                if (node.Left is null)
                {
                    return node;
                }

                node = node.Left;
            }
        }

        /// <summary>
        /// Remove right-horizontal links and replaces them with left-horizontal links.
        /// Accomplishes this by performing a right rotation.
        /// </summary>
        /// <param name="node">The node to rebalance from.</param>
        /// <returns>The rebalanced node.</returns>
        private AATreeNode<TKey>? Skew(AATreeNode<TKey>? node)
        {
            if (node?.Left is null || node.Left.Level != node.Level)
            {
                return node;
            }

            var left = node.Left;
            node.Left = left.Right;
            left.Right = node;
            return left;
        }

        /// <summary>
        /// Reduces the number of right-horizontal links.
        /// Accomplishes this by performing a left rotation, and incrementing level.
        /// </summary>
        /// <param name="node">The node to rebalance from.</param>
        /// <returns>The rebalanced node.</returns>
        private AATreeNode<TKey>? Split(AATreeNode<TKey>? node)
        {
            if (node?.Right?.Right is null || node.Level != node.Right.Right.Level)
            {
                return node;
            }

            var right = node.Right;
            node.Right = right.Left;
            right.Left = node;
            right.Level++;
            return right;
        }

        /// <summary>
        /// Decreases the level of node if necessary.
        /// </summary>
        /// <param name="node">The node to decrease level from.</param>
        /// <returns>The node with modified level.</returns>
        private AATreeNode<TKey> DecreaseLevel(AATreeNode<TKey> node)
        {
            var newLevel = Math.Min(GetLevel(node.Left), GetLevel(node.Right)) + 1;
            if (newLevel >= node.Level)
            {
                return node;
            }

            node.Level = newLevel;
            if (node.Right is { } && newLevel < node.Right.Level)
            {
                node.Right.Level = newLevel;
            }

            return node;

            static int GetLevel(AATreeNode<TKey>? x) => x?.Level ?? 0;
        }
    }
}
