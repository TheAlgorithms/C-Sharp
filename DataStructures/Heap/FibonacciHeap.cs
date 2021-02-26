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
        public class FHeapNode
        {
            public T Key { get; set; }
            public FHeapNode? Parent { get; set; }
            public FHeapNode Left { get; set; }

            public FHeapNode Right { get; set; }

            public FHeapNode? Child { get; set; }

            public bool Mark { get; set; } = false;

            public int Degree { get; set; } = 0;

            public FHeapNode(T key)
            {
                Key = key;

                Left = Right = this;
                Parent = Child = null;
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

            public void AddChild(FHeapNode node)
            {
                Degree++;

                if (Child == null)
                {
                    Child = node;
                    Child.Parent = this;
                    Child.Left = Child.Right = Child;

                    return;
                }

                Child.AddRight(node);
            }

            public void Remove()
            {
                Left.Right = Right;
                Right.Left = Left;
            }

            /// <summary>
            /// Combine the linked list that <c>otherList</c> sits inside, with the
            /// linked list this is in. Do this by cutting the link between this node, 
            /// and the node to the right of this, and inserting the contents of the 
            /// otherList in between
            /// </summary>
            /// <param name="otherList"></param>
            public void ConcatenateRight(FHeapNode otherList)
            {
                Right.Left = otherList.Left;
                otherList.Left.Right = Right;

                Right = otherList;
                otherList.Left = this;
            }
        }

        public int Count { get; set; } = 0;

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



        /// <summary>
        /// Add item <c>x</c> to this Fibonacci heap.
        /// </summary>
        /// <remarks>
        /// To add an item to a Fibonacci heap, we simply add it to the "root list", 
        /// a circularly doubly linked list where our minimum item sits. Since adding 
        /// items to a linked list takes O(1) time, the overall time to perform this 
        /// operation is O(1)
        /// </remarks>
        /// <param name="x"></param>
        /// <returns></returns>
        public FHeapNode Insert(T x)
        {
            Count++;

            var newItem = new FHeapNode(x);

            if (MinItem == null)
            {
                MinItem = newItem;
            }
            else
            {
                MinItem.AddRight(newItem);

                if (newItem.Key.CompareTo(MinItem.Key) < 0)
                {
                    MinItem = newItem;
                }
            }

            return newItem;
        }

        /// <summary>
        /// Combines all the elements of two fibonacci heaps
        /// </summary>
        /// <remarks>
        /// To union two Fibonacci heaps is a single fibonacci heap is a single heap
        /// that contains all the elements of both heaps. This can be done in O(1) time
        /// by concatenating the root lists together. 
        /// 
        /// For more details on how two circularly linked lists are concatenated, see
        /// <see cref="DataStructures.FibonacciHeap{T}.FHeapNode.ConcatenateRight(DataStructures.FibonacciHeap{T}.FHeapNode)"/>
        /// 
        /// Finally, check to see which of <c>this.MinItem</c> and <c>other.MinItem</c>
        /// is smaller, and set <c>this.MinItem</c> accordingly
        /// 
        /// This operation destroys <c>other</c>
        /// </remarks>
        /// <param name="other"></param>
        public void Union(FibonacciHeap<T> other)
        {
            // If there are no items in the other heap, then there is nothing to do.
            if (other.MinItem == null)
            {
                return;
            }

            // If this heap is empty, simply set it equal to the other heap
            if (MinItem == null)
            {
                // Set this heap to the other one
                MinItem = other.MinItem;
                Count = other.Count;

                // Destroy the other heap
                other.MinItem = null;
                other.Count = 0;

                return;
            }

            Count += other.Count;
            // <see cref="DataStructures.FibonacciHeap{T}.FHeapNode.ConcatenateRight(DataStructures.FibonacciHeap{T}.FHeapNode)"/>
            MinItem.ConcatenateRight(other.MinItem);

            // Set the MinItem to the smaller of the two MinItems
            if (other.MinItem.Key.CompareTo(MinItem.Key) < 0)
            {
                MinItem = other.MinItem;
            }

            other.MinItem = null;
            other.Count = 0;
        }

        public FHeapNode? Pop()
        {
            FHeapNode? z = null;

            if (MinItem != null)
            {
                z = MinItem;
                // Since z is leaving the heap, add its children to the root list
                if (z.Child != null)
                {
                    foreach (var x in SiblingIterator(z.Child))
                    {
                        x.Parent = null;
                    }

                    // This effectively adds each child x to the root list
                    z.ConcatenateRight(z.Child);
                }

                if (Count == 1)
                {
                    MinItem = null;
                }
                else
                {
                    // Temporarily reassign MinItem to an arbitrary item in the root 
                    // list
                    MinItem = MinItem.Right;

                    // Remove the old MinItem from the root list altogether
                    z.Remove();

                    // Consolidate the heap
                    Consolidate();
                }

                Count -= 1;
            }

            return z;
        }

        protected void Consolidate()
        {
            if (MinItem == null) { return; }

            // There's a fact in Intro to Algorithms: 
            // "the max degree of any node in an n-node fibonacci heap is O(lg(n)). 
            // In particular, we shall show that D(n) <= floor(log_phi(n)) where phi is 
            // the golden ratio, defined in equation 3.24 as phi = (1 + sqrt(5))/2"
            //
            // For a proof, see [1]
            var maxDegree = (int)Math.Log(Count, (1 + Math.Sqrt(5)) / 2);

            var A = new FHeapNode?[maxDegree];
            foreach (var w in SiblingIterator(MinItem))
            {
                var x = w;
                var d = x.Degree;

                while (A[d] != null)
                {
                    var y = A[d];
                    if (y == null) { throw new NullReferenceException("y is null"); }

                    // if (x.Key > y.Key) {Exchange x with y}
                    if (x.Key.CompareTo(y.Key) > 0)
                    {
                        // Exchange x and y
                        var temp = x;
                        x = y;
                        y = temp;
                    }
                    FibHeapLink(y, x);
                    A[d] = null;
                    d++;
                }
                A[d] = x;
            }

            MinItem = null;
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] != null)
                {
                    if (MinItem == null)
                    {
                        MinItem = A[i];
                        if (MinItem == null) { throw new NullReferenceException("MinItem is null"); }
                        MinItem.SetSiblings(MinItem, MinItem);
                        MinItem.Parent = null;
                    }

                    else
                    {
                        var r = A[i];
                        if (r == null) { throw new NullReferenceException("A[i] is null"); }
                        MinItem.AddRight(r);
                        if (MinItem.Key.CompareTo(r.Key) > 0)
                        {
                            MinItem = A[i];
                        }
                    }
                }
            }
        }

        protected void FibHeapLink(FHeapNode y, FHeapNode x)
        {
            y.Remove();
            x.AddChild(y);
        }
    }
}
