using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Heap.FibonacciHeap;

/// <summary>
///     A generic implementation of a Fibonacci heap.
/// </summary>
/// <remarks>
///     <para>
///         A Fibonacci heap is similar to a standard binary heap
///         <see cref="DataStructures.Heap.BinaryHeap{T}" />, however it uses concepts
///         of amortized analysis to provide theoretical speedups on common operations like
///         insert, union, and decrease-key while maintaining the same speed on all other
///         operations.
///     </para>
///     <para>
///         In practice, Fibonacci heaps are more complicated than binary heaps and require
///         a large input problems before the benifits of the theoretical speed up
///         begin to show.
///     </para>
///     <para>
///         References:
///         [1] Thomas H. Cormen, Charles E. Leiserson, Ronald L. Rivest,
///         and Clifford Stein. 2009. Introduction to Algorithms, Third Edition (3rd. ed.).
///         The MIT Press.
///     </para>
/// </remarks>
/// <typeparam name="T">Type of elements in binary heap.</typeparam>
public class FibonacciHeap<T> where T : IComparable
{
    /// <summary>
    ///     Gets or sets the count of the number of nodes in the Fibonacci heap.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    ///     Gets or sets a reference to the MinItem. The MinItem and all of its siblings
    ///     comprise the root list, a list of trees that satisfy the heap property and
    ///     are joined in a circularly doubly linked list.
    /// </summary>
    private FHeapNode<T>? MinItem { get; set; }

    /// <summary>
    ///     Add item <c>x</c> to this Fibonacci heap.
    /// </summary>
    /// <remarks>
    ///     To add an item to a Fibonacci heap, we simply add it to the "root list",
    ///     a circularly doubly linked list where our minimum item sits. Since adding
    ///     items to a linked list takes O(1) time, the overall time to perform this
    ///     operation is O(1).
    /// </remarks>
    /// <param name="x">An item to push onto the heap.</param>
    /// <returns>
    ///     A reference to the item as it is in the heap. This is used for
    ///     operations like decresing key.
    /// </returns>
    public FHeapNode<T> Push(T x)
    {
        Count++;

        var newItem = new FHeapNode<T>(x);

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
    ///     Combines all the elements of two fibonacci heaps.
    /// </summary>
    /// <remarks>
    ///     To union two Fibonacci heaps is a single fibonacci heap is a single heap
    ///     that contains all the elements of both heaps. This can be done in O(1) time
    ///     by concatenating the root lists together.
    ///     For more details on how two circularly linked lists are concatenated, see
    ///     <see cref="FHeapNode{T}.ConcatenateRight" />
    ///     Finally, check to see which of <c>this.MinItem</c> and <c>other.MinItem</c>
    ///     is smaller, and set <c>this.MinItem</c> accordingly
    ///     This operation destroys <c>other</c>.
    /// </remarks>
    /// <param name="other">
    ///     Another heap whose elements we wish to add to this heap.
    ///     The other heap will be destroyed as a result.
    /// </param>
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
    ///     Return the MinItem and remove it from the heap.
    /// </summary>
    /// <remarks>
    ///     This function (with all of its helper functions) is the most complicated
    ///     part of the Fibonacci Heap. However, it can be broken down into a few steps.
    ///     <list type="number">
    ///         <item>
    ///             Add the children of MinItem to the root list. Either one of these children,
    ///             or another of the items in the root list is a candidate to become the new
    ///             MinItem.
    ///         </item>
    ///         <item>
    ///             Remove the MinItem from the root list and appoint a new MinItem temporarily.
    ///         </item>
    ///         <item>
    ///             <see cref="Consolidate" /> what's left
    ///             of the heap.
    ///         </item>
    ///     </list>
    /// </remarks>
    /// <returns>The minimum item from the heap.</returns>
    public T Pop()
    {
        FHeapNode<T>? z = null;
        if (MinItem == null)
        {
            throw new InvalidOperationException("Heap is empty!");
        }

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
            Count = 0;
            return z.Key;
        }

        // Temporarily reassign MinItem to an arbitrary item in the root
        // list
        MinItem = MinItem.Right;

        // Remove the old MinItem from the root list altogether
        z.Remove();

        // Consolidate the heap
        Consolidate();

        Count -= 1;

        return z.Key;
    }

    /// <summary>
    ///     A method to see what's on top of the heap without changing its structure.
    /// </summary>
    /// <returns>
    ///     Returns the top element without popping it from the structure of
    ///     the heap.
    /// </returns>
    public T Peek()
    {
        if (MinItem == null)
        {
            throw new InvalidOperationException("The heap is empty");
        }

        return MinItem.Key;
    }

    /// <summary>
    ///     Reduce the key of x to be k.
    /// </summary>
    /// <remarks>
    ///     k must be less than x.Key, increasing the key of an item is not supported.
    /// </remarks>
    /// <param name="x">The item you want to reduce in value.</param>
    /// <param name="k">The new value for the item.</param>
    public void DecreaseKey(FHeapNode<T> x, T k)
    {
        if (MinItem == null)
        {
            throw new ArgumentException($"{nameof(x)} is not from the heap");
        }

        if (x.Key == null)
        {
            throw new ArgumentException("x has no value");
        }

        if (k.CompareTo(x.Key) > 0)
        {
            throw new InvalidOperationException("Value cannot be increased");
        }

        x.Key = k;
        var y = x.Parent;
        if (y != null && x.Key.CompareTo(y.Key) < 0)
        {
            Cut(x, y);
            CascadingCut(y);
        }

        if (x.Key.CompareTo(MinItem.Key) < 0)
        {
            MinItem = x;
        }
    }

    /// <summary>
    ///     Remove x from the child list of y.
    /// </summary>
    /// <param name="x">A child of y we just decreased the value of.</param>
    /// <param name="y">The now former parent of x.</param>
    protected void Cut(FHeapNode<T> x, FHeapNode<T> y)
    {
        if (MinItem == null)
        {
            throw new InvalidOperationException("Heap malformed");
        }

        if (y.Degree == 1)
        {
            y.Child = null;
            MinItem.AddRight(x);
        }
        else if (y.Degree > 1)
        {
            x.Remove();
        }
        else
        {
            throw new InvalidOperationException("Heap malformed");
        }

        y.Degree--;
        x.Mark = false;
        x.Parent = null;
    }

    /// <summary>
    ///     Rebalances the heap after the decrease operation takes place.
    /// </summary>
    /// <param name="y">An item that may no longer obey the heap property.</param>
    protected void CascadingCut(FHeapNode<T> y)
    {
        var z = y.Parent;
        if (z != null)
        {
            if (!y.Mark)
            {
                y.Mark = true;
            }
            else
            {
                Cut(y, z);
                CascadingCut(z);
            }
        }
    }

    /// <summary>
    ///     <para>
    ///         Consolidate is analogous to Heapify in <see cref="DataStructures.Heap.BinaryHeap{T}" />.
    ///     </para>
    ///     <para>
    ///         First, an array <c>A</c> [0...D(H.n)] is created where H.n is the number
    ///         of items in this heap, and D(x) is the max degree any node can have in a
    ///         Fibonacci heap with x nodes.
    ///     </para>
    ///     <para>
    ///         For each node <c>x</c> in the root list, try to add it to <c>A[d]</c> where
    ///         d is the degree of <c>x</c>.
    ///         If there is already a node in <c>A[d]</c>, call it <c>y</c>, and make
    ///         <c>y</c> a child of <c>x</c>. (Swap <c>x</c> and <c>y</c> beforehand if
    ///         <c>x</c> is greater than <c>y</c>). Now that <c>x</c> has one more child,
    ///         remove if from <c>A[d]</c> and add it to <c>A[d+1]</c> to reflect that its
    ///         degree is one more. Loop this behavior until we find an empty spot to put
    ///         <c>x</c>.
    ///     </para>
    ///     <para>
    ///         With <c>A</c> all filled, empty the root list of the heap. And add each item
    ///         from <c>A</c> into the root list, one by one, making sure to keep track of
    ///         which is smallest.
    ///     </para>
    /// </summary>
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
        var maxDegree = 1 + (int)Math.Log(Count, (1 + Math.Sqrt(5)) / 2);

        // Create slots for every possible node degree of x
        var a = new FHeapNode<T>?[maxDegree];
        var siblings = SiblingIterator(MinItem).ToList();
        foreach (var w in siblings)
        {
            var x = w;
            var d = x.Degree;

            var y = a[d];

            // While A[d] is not empty, we can't blindly put x here
            while (y != null)
            {
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
                a[d] = null;

                // Add 1 to x's degree before going back into the loop
                d++;

                y = a[d];
            }

            // Now that there's an empty spot for x, place it there
            a[d] = x;
        }

        ReconstructHeap(a);
    }

    /// <summary>
    ///     Reconstructs the heap based on the array of node degrees created by the consolidate step.
    /// </summary>
    /// <param name="a">An array of FHeapNodes where a[i] represents a node of degree i.</param>
    private void ReconstructHeap(FHeapNode<T>?[] a)
    {
        // Once all items are in A, empty out the root list
        MinItem = null;

        for (var i = 0; i < a.Length; i++)
        {
            var r = a[i];
            if (r == null)
            {
                continue;
            }

            if (MinItem == null)
            {
                // If the root list is completely empty, make this the new
                // MinItem
                MinItem = r;

                // Make a new root list with just this item. Make sure to make
                // it its own list.
                MinItem.SetSiblings(MinItem, MinItem);
                MinItem.Parent = null;
            }
            else
            {
                // Add A[i] to the root list
                MinItem.AddRight(r);

                // If this item is smaller, make it the new min item
                if (MinItem.Key.CompareTo(r.Key) > 0)
                {
                    MinItem = a[i];
                }
            }
        }
    }

    /// <summary>
    ///     Make y a child of x.
    /// </summary>
    /// <param name="y">A node to become the child of x.</param>
    /// <param name="x">A node to become the parent of y.</param>
    private void FibHeapLink(FHeapNode<T> y, FHeapNode<T> x)
    {
        y.Remove();
        x.AddChild(y);
        y.Mark = false;
    }

    /// <summary>
    ///     A helper function to iterate through all the siblings of this node in the
    ///     circularly doubly linked list.
    /// </summary>
    /// <param name="node">A node we want the siblings of.</param>
    /// <returns>An iterator over all of the siblings.</returns>
    private IEnumerable<FHeapNode<T>> SiblingIterator(FHeapNode<T> node)
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
}
