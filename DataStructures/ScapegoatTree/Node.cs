using System;

namespace DataStructures.ScapegoatTree
{
    public class Node<TKey> where TKey : IComparable
    {
        private Node<TKey>? right;
        private Node<TKey>? left;

        public TKey Key { get; }

        public Node<TKey>? Right
        {
            get => right;
            set
            {
                if (value != null && !value.IsGreaterThanOrSameAs(Key))
                {
                    throw new ArgumentException("The value's key is smaller than or equal to node's right child's key.", nameof(value));
                }

                right = value;
            }
        }

        public Node<TKey>? Left
        {
            get => left;
            set
            {
                if (value != null && value.IsGreaterThanOrSameAs(Key))
                {
                    throw new ArgumentException("The value's key is greater than or equal to node's left child's key.", nameof(value));
                }

                left = value;
            }
        }

        public Node(TKey key) => Key = key;

        public Node(TKey key, Node<TKey>? right, Node<TKey>? left)
            : this(key)
        {
            Right = right;
            Left = left;
        }

        /// <summary>
        /// Returns number of elements in the tree.
        /// </summary>
        /// <returns>Number of elements in the tree.</returns>
        public int GetSize() => (Left?.GetSize() ?? 0) + 1 + (Right?.GetSize() ?? 0);

        public double GetAlphaHeight(double alpha) => Math.Floor(Math.Log(this.GetSize(), 1.0 / alpha));

        public Node<TKey> GetSmallestKeyNode() => Left?.GetSmallestKeyNode() ?? this;

        public Node<TKey> GetLargestKeyNode() => Right?.GetLargestKeyNode() ?? this;

        public bool IsAlphaWeightBalanced(double a)
        {
            var isLeftBalanced = (Left?.GetSize() ?? 0) <= a * GetSize();
            var isRightBalanced = (Right?.GetSize() ?? 0) <= a * GetSize();

            return isLeftBalanced && isRightBalanced;
        }

        private bool IsGreaterThanOrSameAs(TKey key)
        {
            return this.Key.CompareTo(key) >= 0;
        }
    }
}
