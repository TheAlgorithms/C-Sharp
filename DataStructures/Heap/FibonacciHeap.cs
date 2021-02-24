using System;
using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    /// A generic implementation of a binary heap.
    /// </summary>
    /// <remarks>
    /// A binary heap is a complete binary tree that satisfies the heap property;
    /// that is every node in the tree compares greater/less than or equal to its left and right
    /// child nodes. Note that this is different from a binary search tree, where every node
    /// must be the largest/smallest node of all of its children.
    /// Although binary heaps are not very efficient, they are (probably) the simpliest heaps
    /// to understand and implement.
    /// More information: https://en.wikipedia.org/wiki/Binary_heap .
    /// </remarks>
    /// <typeparam name="T">Type of elements in binary heap.</typeparam>
    public class FibonacciHeap<T> where T : IComparable, IEquatable<T>
    {
        protected class FHeapNode
        {
            public T Key { get; set; }
            public FHeapNode Parent { get; set; }
            public FHeapNode Left { get; set; }

            public FHeapNode Right { get; set; }

            public FHeapNode Child { get; set; }

            public bool Mark { get; set; } = false;

            public int Degree { get; set; } = 0;

            public FHeapNode(T key)
            {
                Key = key;

                Parent = Left = Right = Child = this;
            }

            public void SetSiblings(FHeapNode left, FHeapNode right)
            {
                Left = left;
                Right = right;
            }

            public void AddRight(FHeapNode node)
            {
                Right.Left = node;
                node.Right = Right;
                node.Left = this;
                Right = node;
            }

            public void Concatenate(FHeapNode otherList)
            {

            }
        }

        public int Count { get; protected set; } = 0;

        protected FHeapNode? MinItem { get; set; }
        public FibonacciHeap()
        {
            MinItem = null;
        }

        protected IEnumerable<FHeapNode> SiblingIterator(FHeapNode node)
        {
            var currentNode = node;
            yield return currentNode;

            currentNode = node.Right;
            while (currentNode != node)
            {
                yield return currentNode;
                currentNode = currentNode.Right;
            }
        }

        public void Insert(T x)
        {
            Count++;

            if (MinItem == null)
            {
                MinItem = new FHeapNode(x);
            }
            else
            {
                var newItem = new FHeapNode(x);
                MinItem.AddRight(newItem);

                if (newItem.Key.CompareTo(MinItem.Key) < 0)
                {
                    MinItem = newItem;
                }
            }
        }

        public void Union(FibonacciHeap<T> other)
        {

        }
    }
}
