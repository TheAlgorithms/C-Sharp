using System;
using System.Collections.Generic;

// Node class representing a node in a binomial tree
public class BinomialNode<T> where T : IComparable<T>
{
    public T Key { get; set; }
    public int Degree { get; set; }
    public BinomialNode<T> Parent { get; set; }
    public BinomialNode<T> Child { get; set; }
    public BinomialNode<T> Sibling { get; set; }

    public BinomialNode(T key)
    {
        Key = key;
        Degree = 0;
        Parent = Child = Sibling = null;
    }
}

// BinomialHeap class representing the binomial heap
public class BinomialHeap<T> where T : IComparable<T>
{
    private BinomialNode<T> root;

    public BinomialHeap()
    {
        root = null;
    }

    public bool IsEmpty()
    {
        return root == null;
    }

    // Insert a key into the binomial heap
    public void Insert(T key)
    {
        BinomialHeap<T> newHeap = new BinomialHeap<T>();
        newHeap.root = new BinomialNode<T>(key);
        Union(newHeap);
    }

    // Union two binomial heaps
    private void Union(BinomialHeap<T> heap)
    {
        Merge(heap);
        BinomialNode<T> prev = null;
        BinomialNode<T> x = root;
        BinomialNode<T> next = root.Sibling;

        while (next != null)
        {
            if (x.Degree != next.Degree || (next.Sibling != null && next.Sibling.Degree == x.Degree))
            {
                prev = x;
                x = next;
            }
            else
            {
                if (x.Key.CompareTo(next.Key) <= 0)
                {
                    x.Sibling = next.Sibling;
                    Link(next, x);
                }
                else
                {
                    if (prev == null)
                        root = next;
                    else
                        prev.Sibling = next;
                    Link(x, next);
                    x = next;
                }
            }
            next = x.Sibling;
        }
    }

    // Merge two binomial heaps
    private void Merge(BinomialHeap<T> heap)
    {
        BinomialNode<T> thisRoot = root;
        BinomialNode<T> thatRoot = heap.root;
        BinomialNode<T> dummy = new BinomialNode<T>(default(T));
        BinomialNode<T> current = dummy;

        while (thisRoot != null || thatRoot != null)
        {
            if (thisRoot == null)
            {
                current.Sibling = thatRoot;
                thatRoot = thatRoot.Sibling;
            }
            else if (thatRoot == null)
            {
                current.Sibling = thisRoot;
                thisRoot = thisRoot.Sibling;
            }
            else
            {
                if (thisRoot.Degree <= thatRoot.Degree)
                {
                    current.Sibling = thisRoot;
                    thisRoot = thisRoot.Sibling;
                }
                else
                {
                    current.Sibling = thatRoot;
                    thatRoot = thatRoot.Sibling;
                }
            }
            current = current.Sibling;
        }

        root = dummy.Sibling;
    }

    // Link two binomial trees
    private void Link(BinomialNode<T> child, BinomialNode<T> parent)
    {
        child.Parent = parent;
        child.Sibling = parent.Child;
        parent.Child = child;
        parent.Degree++;
    }

    // Extract the minimum key from the binomial heap
    public T ExtractMin()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Heap is empty");
        }

        BinomialNode<T> minNode = FindMinNode();
        RemoveNode(minNode);
        BinomialHeap<T> childHeap = new BinomialHeap<T>();
        childHeap.root = ReverseList(minNode.Child);
        Union(childHeap);
        return minNode.Key;
    }

    // Find the node with the minimum key in the binomial heap
    private BinomialNode<T> FindMinNode()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Heap is empty");
        }

        BinomialNode<T> minNode = root;
        BinomialNode<T> current = root.Sibling;

        while (current != null)
        {
            if (current.Key.CompareTo(minNode.Key) < 0)
            {
                minNode = current;
            }
            current = current.Sibling;
        }

        return minNode;
    }

    // Remove a node from the binomial heap
    private void RemoveNode(BinomialNode<T> node)
    {
        DecreaseKey(node, default(T));
        ExtractMin();
    }

    // Decrease the key of a node in the binomial heap
    public void DecreaseKey(BinomialNode<T> node, T newKey)
    {
        if (newKey.CompareTo(node.Key) > 0)
        {
            throw new ArgumentException("New key is greater than the current key");
        }

        node.Key = newKey;
        BinomialNode<T> current = node;
        BinomialNode<T> parent = node.Parent;

        while (parent != null && current.Key.CompareTo(parent.Key) < 0)
        {
            // Swap the keys of current and parent
            T temp = current.Key;
            current.Key = parent.Key;
            parent.Key = temp;

            current = parent;
            parent = parent.Parent;
        }
    }

    // Reverse the sibling list of a binomial tree
    private BinomialNode<T> ReverseList(BinomialNode<T> node)
    {
        BinomialNode<T> prev = null;
        BinomialNode<T> current = node;
        BinomialNode<T> next = null;

        while (current != null)
        {
            next = current.Sibling;
            current.Sibling = prev;
            prev = current;
            current = next;
        }

        return prev;
    }
}


