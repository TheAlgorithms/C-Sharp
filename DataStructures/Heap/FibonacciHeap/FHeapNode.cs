using System;

namespace DataStructures.Heap.FibonacciHeap;

/// <summary>
///     These FHeapNodes are the bulk of the data structure. The have pointers to
///     their parent, a left and right sibling, and to a child. A node and its
///     siblings comprise a circularly doubly linked list.
/// </summary>
/// <typeparam name="T">A type that can be compared.</typeparam>
public class FHeapNode<T> where T : IComparable
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FHeapNode{T}" /> class.
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
    ///     Gets or sets the data of this node.
    /// </summary>
    public T Key { get; set; }

    /// <summary>
    ///     Gets or sets a reference to the parent.
    /// </summary>
    public FHeapNode<T>? Parent { get; set; }

    /// <summary>
    ///     Gets or sets a reference to the left sibling.
    /// </summary>
    public FHeapNode<T> Left { get; set; }

    /// <summary>
    ///     Gets or sets a reference to the right sibling.
    /// </summary>
    public FHeapNode<T> Right { get; set; }

    /// <summary>
    ///     Gets or sets a reference to one of the children, there may be more that
    ///     are siblings the this child, however this structure only maintains a
    ///     reference to one of them.
    /// </summary>
    public FHeapNode<T>? Child { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether this node has been marked,
    ///     used in some operations.
    /// </summary>
    public bool Mark { get; set; }

    /// <summary>
    ///     Gets or sets the number of nodes in the child linked list.
    /// </summary>
    public int Degree { get; set; }

    public void SetSiblings(FHeapNode<T> left, FHeapNode<T> right)
    {
        Left = left;
        Right = right;
    }

    /// <summary>
    ///     A helper function to add a node to the right of this one in the current
    ///     circularly doubly linked list.
    /// </summary>
    /// <param name="node">A node to go in the linked list.</param>
    public void AddRight(FHeapNode<T> node)
    {
        Right.Left = node;
        node.Right = Right;
        node.Left = this;
        Right = node;
    }

    /// <summary>
    ///     Similar to AddRight, but adds the node as a sibling to the child node.
    /// </summary>
    /// <param name="node">A node to add to the child list of this node.</param>
    public void AddChild(FHeapNode<T> node)
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
    ///     Remove this item from the linked list it's in.
    /// </summary>
    public void Remove()
    {
        Left.Right = Right;
        Right.Left = Left;
    }

    /// <summary>
    ///     Combine the linked list that <c>otherList</c> sits inside, with the
    ///     linked list this is in. Do this by cutting the link between this node,
    ///     and the node to the right of this, and inserting the contents of the
    ///     otherList in between.
    /// </summary>
    /// <param name="otherList">
    ///     A node from another list whose elements we want
    ///     to concatenate to this list.
    /// </param>
    public void ConcatenateRight(FHeapNode<T> otherList)
    {
        Right.Left = otherList.Left;
        otherList.Left.Right = Right;

        Right = otherList;
        otherList.Left = this;
    }
}
