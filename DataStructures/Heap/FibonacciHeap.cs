using System;
using System.Collections.Generic;

namespace DataStructures.Heap
{
    /// <summary>
    /// A generic implementation of a Fibonacci heap.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A Fibonacci heap is similar to a standard binary heap
    /// <see cref="DataStructures.Heap.BinaryHeap{T}"/>, however it uses concepts
    /// of amortized analysis to provide theoretical speedups on common operations like
    /// insert, union, and decrease-key while maintaining the same speed on all other
    /// operations.
    /// </para>
    /// <para>
    /// In practice, Fibonacci heaps are more complicated than binary heaps and require
    /// a large input problems before the benifits of the theoretical speed up 
    /// begin to show. 
    /// </para>
    /// <para>
    /// References:
    /// [1] Thomas H. Cormen, Charles E. Leiserson, Ronald L. Rivest, 
    /// and Clifford Stein. 2009. Introduction to Algorithms, Third Edition (3rd. ed.). 
    /// The MIT Press.
    /// </para>
    /// </remarks>
    /// <typeparam name="T">Type of elements in binary heap.</typeparam>
    public class FibonacciHeap<T> where T : IComparable, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataStructures.Heap.FibonacciHeap{T}"/> class.
        /// </summary>
        public FibonacciHeap()
        {
            MinItem = null;
        }

        /// <summary>
        /// Gets or sets the count of the number of nodes in the Fibonacci heap.
        /// </summary>
        public int Count { get; set; } = 0;

        /// <summary>
        /// Gets or sets a reference to the MinItem. The MinItem and all of its siblings 
        /// comprise the root list, a list of trees that satisfy the heap property and 
        /// are joined in a circularly doubly linked list.
        /// </summary>
        protected FHeapNode? MinItem { get; set; }

        /// <summary>
        /// Add item <c>x</c> to this Fibonacci heap.
        /// </summary>
        /// <remarks>
        /// To add an item to a Fibonacci heap, we simply add it to the "root list", 
        /// a circularly doubly linked list where our minimum item sits. Since adding 
        /// items to a linked list takes O(1) time, the overall time to perform this 
        /// operation is O(1).
        /// </remarks>
        /// <param name="x">An item to push onto the heap.</param>
        /// <returns>A reference to the item as it is in the heap. This is used for 
        /// operations like decresing key.</returns>
        public FHeapNode Push(T x)
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
        /// Combines all the elements of two fibonacci heaps.
        /// </summary>
        /// <remarks>
        /// To union two Fibonacci heaps is a single fibonacci heap is a single heap
        /// that contains all the elements of both heaps. This can be done in O(1) time
        /// by concatenating the root lists together. 
        /// 
        /// For more details on how two circularly linked lists are concatenated, see
        /// <see cref="DataStructures.Heap.FibonacciHeap{T}.FHeapNode.ConcatenateRight(DataStructures.Heap.FibonacciHeap{T}.FHeapNode)"/>
        /// 
        /// Finally, check to see which of <c>this.MinItem</c> and <c>other.MinItem</c>
        /// is smaller, and set <c>this.MinItem</c> accordingly
        /// 
        /// This operation destroys <c>other</c>.
        /// </remarks>
        /// <param name="other">Another heap whose elements we wish to add to this heap.
        /// The other heap will be destroyed as a result.</param>
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

        /// <summary>
        /// Return the MinItem and remove it from the heap.
        /// </summary>
        /// <remarks>
        /// This function (with all of its helper functions) is the most complicated 
        /// part of the Fibonacci Heap. However, it can be broken down into a few steps.
        /// <list type="number">
        /// <item>
        /// Add the children of MinItem to the root list. Either one of these children, 
        /// or another of the items in the root list is a candidate to become the new
        /// MinItem.
        /// </item>
        /// <item>
        /// Remove the MinItem from the root list and appoint a new MinItem temporarily.
        /// </item>
        /// <item>
        /// <see cref="DataStructures.Heap.FibonacciHeap{T}.Consolidate"/> what's left 
        /// of the heap
        /// </item>
        /// </list>
        /// </remarks>
        /// <returns>The minimum item from the heap.</returns>
        public T Pop()
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

            if (z == null)
            {
                throw new InvalidOperationException("Heap is empty!");
            }

            return z.Key;
        }

        /// <summary>
        /// <para>
        /// Consolidate is analogous to Heapify in <see cref="DataStructures.Heap.BinaryHeap{T}"/>.
        /// </para>
        /// <para>
        /// First, an array <c>A</c> of length D(H.n) is created where H.n is the number of items
        /// in this heap, and D(x) is the max degree any node can have in a Fibonacci 
        /// heap with x nodes.
        /// </para>
        /// <para>
        /// For each node <c>x</c> in the root list, try to add it to <c>A[d]</c> where 
        /// d is the degree of <c>x</c>.
        /// If there is already a node in <c>A[d]</c>, call it <c>y</c>, and make 
        /// <c>y</c> a child of <c>x</c>. (Swap <c>x</c> and <c>y</c> beforehand if 
        /// <c>x</c> is greater than <c>y</c>). Now that <c>x</c> has one more child, 
        /// remove if from <c>A[d]</c> and add it to <c>A[d+1]</c> to reflect that it's 
        /// degree is one more. Loop this behavior until we find an empty spot to put 
        /// <c>x</c>.
        /// </para>
        /// <para>
        /// With <c>A</c> all filled, empty the root list of the heap. And add each item
        /// from <c>A</c> into the root list, one by one, making sure to keep track of
        /// which is smallest. 
        /// </para>
        /// </summary>
        public T Peek()
        {
            if (MinItem == null)
            {
                throw new InvalidOperationException("The heap is Empty");
            }

            return MinItem.Key;
        }

        protected void Consolidate()
        {
            if (MinItem == null)
            {
                return;
            }

            // There's a fact in Intro to Algorithms: 
            // "the max degree of any node in an n-node fibonacci heap is O(lg(n)). 
            // In particular, we shall show that D(n) <= floor(log_phi(n)) where phi is 
            // the golden ratio, defined in equation 3.24 as phi = (1 + sqrt(5))/2"
            //
            // For a proof, see [1]
            var maxDegree = (int)Math.Log(Count, (1 + Math.Sqrt(5)) / 2);

            // Create slots for every possible node degree of x
            var A = new FHeapNode?[maxDegree];
            foreach (var w in SiblingIterator(MinItem))
            {
                var x = w;
                var d = x.Degree;

                // While A[d] is not empty, we can't blindly put x here
                while (A[d] != null)
                {
                    var y = A[d];

                    // This is just here to satisfy C#, otherwise it complains that y 
                    // might be null further below.
                    if (y == null)
                    {
                        throw new NullReferenceException("y is null");
                    }

                    // if (x.Key > y.Key) {Exchange x with y}
                    if (x.Key.CompareTo(y.Key) > 0)
                    {
                        // Exchange x and y
                        var temp = x;
                        x = y;
                        y = temp;
                    }

                    // Make y a child of x
                    FibHeapLink(y, x);

                    // Empty out this spot since x now has a higher degree
                    A[d] = null;

                    // Add 1 to x's degree before going back into the loop
                    d++;
                }

                // Now that there's an empty spot for x, place it there
                A[d] = x;
            }

            // Once all items are in A, empty out the root list
            MinItem = null;

            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] != null)
                {
                    if (MinItem == null)
                    {
                        // If the root list is completely empty, make this the new 
                        // MinItem
                        MinItem = A[i];

                        // Similarly to up above, this lets C# know MinItem won't be
                        // null below
                        if (MinItem == null)
                        {
                            throw new NullReferenceException("MinItem is null");
                        }

                        // Make a new root list with just this item. Make sure to make
                        // it its own list.
                        MinItem.SetSiblings(MinItem, MinItem);
                        MinItem.Parent = null;
                    }

                    else
                    {
                        var r = A[i];

                        // Similarly to up above, this lets C# know A[i] won't be
                        // null below
                        if (r == null)
                        {
                            throw new NullReferenceException("a[i] is null");
                        }

                        // Add A[i] to the root list
                        MinItem.AddRight(r);

                        // If this item is smaller, make it the new min item
                        if (MinItem.Key.CompareTo(r.Key) > 0)
                        {
                            MinItem = A[i];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Make y a child of x.
        /// </summary>
        /// <param name="y">A node to become the child of x.</param>
        /// <param name="x">A node to become the parent of y.</param>
        protected void FibHeapLink(FHeapNode y, FHeapNode x)
        {
            y.Remove();
            x.AddChild(y);
        }

        /// <summary>
        /// A helper function to iterate through all the siblings of this node in the
        /// circularly doubly linked list.
        /// </summary>
        /// <param name="node">A node we want the siblings of.</param>
        /// <returns>An iterator over all of the siblings.</returns>
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
        /// These FHeapNodes are the bulk of the data structure. The have pointers to
        /// their parent, a left and right sibling, and to a child. A node and its
        /// siblings comprise a circularly doubly linked list.
        /// </summary>
        public class FHeapNode
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="FHeapNode"/>.
            /// </summary>
            /// <param name="key">An item in the Fibonacci heap.</param>
            public FHeapNode(T key)
            {
                Key = key;

                // Since even a single node must form a circularly doubly linked list, 
                // initialize it as such
                Left = Right = this;
                Parent = Child = null;
            }

            /// <summary>
            /// Gets or sets the data of this node.
            /// </summary>
            public T Key { get; set; }

            /// <summary>
            /// Gets or sets a reference to the parent.
            /// </summary>
            public FHeapNode? Parent { get; set; }

            /// <summary>
            /// Gets or sets a reference to the left sibling.
            /// </summary>
            public FHeapNode Left { get; set; }

            /// <summary>
            /// Gets or sets a reference to the right sibling.
            /// </summary>
            public FHeapNode Right { get; set; }

            /// <summary>
            /// Gets or sets a reference to one of the children, there may be more that 
            /// are siblings the this child, however this structure only maintains a 
            /// reference to one of them.
            /// </summary>
            public FHeapNode? Child { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this node has been marked, 
            /// used in some operations.
            /// </summary>
            public bool Mark { get; set; } = false;

            /// <summary>
            /// Gets or sets the number of nodes in the child linked list. 
            /// </summary>
            public int Degree { get; set; } = 0;

            public void SetSiblings(FHeapNode left, FHeapNode right)
            {
                Left = left;
                Right = right;
            }

            /// <summary>
            /// A helper function to add a node to the right of this one in the current
            /// circularly doubly linked list
            /// </summary>
            /// <param name="node">A node to go in the linked list</param>
            public void AddRight(FHeapNode node)
            {
                Right.Left = node;
                node.Right = Right;
                node.Left = this;
                Right = node;
            }

            /// <summary>
            /// Similar to AddRight, but adds the node as a sibling to the child node
            /// </summary>
            /// <param name="node"></param>
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

            /// <summary>
            /// Remove this item from the linked list it's in
            /// </summary>
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
    }
}