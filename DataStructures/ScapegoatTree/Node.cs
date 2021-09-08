using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.ScapegoatTree
{
    public class Node<TKey> : IEnumerable<TKey>, IComparable<TKey> where TKey : IComparable
    {
        public TKey Key { get; }

        public Node<TKey>? Right { get; set; }

        public Node<TKey>? Left { get; set; }

        public Node(TKey key) => Key = key;

        public Node(TKey key, Node<TKey>? right, Node<TKey> left)
            : this(key)
        {
            Right = right;
            Left = left;
        }

        public int CompareTo(TKey? other) => Key.CompareTo(other);

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

        public IEnumerator<TKey> GetEnumerator()
        {
            if (Left != null)
            {
                foreach (var value in Left)
                {
                    yield return value;
                }
            }

            yield return Key;

            if (Right != null)
            {
                foreach (var value in Right)
                {
                    yield return value;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
