using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Heap.PairingHeap;

/// <summary>
/// A pairing minMax heap implementation.
/// </summary>
/// <typeparam name="T">Base type.</typeparam>
public class PairingHeap<T> : IEnumerable<T> where T : IComparable
{
    private readonly Sorting sorting;
    private readonly IComparer<T> comparer;
    private readonly Dictionary<T, List<PairingHeapNode<T>>> mapping = new();

    private PairingHeapNode<T> root = null!;

    public int Count { get; private set; }

    public PairingHeap(Sorting sortDirection = Sorting.Ascending)
    {
        sorting = sortDirection;
        comparer = new PairingNodeComparer<T>(sortDirection, Comparer<T>.Default);
    }

    /// <summary>
    /// Insert a new Node [O(1)].
    /// </summary>
    public void Insert(T newItem)
    {
        var newNode = new PairingHeapNode<T>(newItem);

        root = RebuildHeap(root, newNode);
        Map(newItem, newNode);

        Count++;
    }

    /// <summary>
    /// Get the element from heap [O(log(n))].
    /// </summary>
    public T Extract()
    {
        var minMax = root;

        RemoveMapping(minMax.Value, minMax);
        RebuildHeap(root.ChildrenHead);

        Count--;
        return minMax.Value;
    }

    /// <summary>
    /// Update heap key [O(log(n))].
    /// </summary>
    public void UpdateKey(T currentValue, T newValue)
    {
        if(!mapping.ContainsKey(currentValue))
        {
            throw new ArgumentException("Current value is not present in this heap.");
        }

        var node = mapping[currentValue]?.Where(x => x.Value.Equals(currentValue)).FirstOrDefault();

        if (comparer.Compare(newValue, node!.Value) > 0)
        {
            throw new ArgumentException($"New value is not {(sorting != Sorting.Descending ? "less" : "greater")} than old value.");
        }

        UpdateNodeValue(currentValue, newValue, node);

        if (node == root)
        {
            return;
        }

        DeleteChild(node);

        root = RebuildHeap(root, node);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return mapping.SelectMany(x => x.Value).Select(x => x.Value).GetEnumerator();
    }

    /// <summary>
    /// Rebuild heap on action [O(log(n))].
    /// </summary>
    private void RebuildHeap(PairingHeapNode<T> headNode)
    {
        if (headNode == null)
        {
            return;
        }

        var passOneResult = new List<PairingHeapNode<T>>();
        var current = headNode;

        if (current.Next == null)
        {
            headNode.Next = null!;
            headNode.Previous = null!;
            passOneResult.Add(headNode);
        }
        else
        {
            while (true)
            {
                if (current == null)
                {
                    break;
                }

                if (current.Next != null)
                {
                    var next = current.Next;
                    var nextNext = next.Next;
                    passOneResult.Add(RebuildHeap(current, next));
                    current = nextNext;
                }
                else
                {
                    var lastInserted = passOneResult[^1];
                    passOneResult[^1] = RebuildHeap(lastInserted, current);
                    break;
                }
            }
        }

        var passTwoResult = passOneResult[^1];

        if (passOneResult.Count == 1)
        {
            root = passTwoResult;
            return;
        }

        for (var i = passOneResult.Count - 2; i >= 0; i--)
        {
            current = passOneResult[i];
            passTwoResult = RebuildHeap(passTwoResult, current);
        }

        root = passTwoResult;
    }

    private PairingHeapNode<T> RebuildHeap(PairingHeapNode<T> node1, PairingHeapNode<T> node2)
    {
        if (node2 != null)
        {
            node2.Previous = null!;
            node2.Next = null!;
        }

        if (node1 == null)
        {
            return node2!;
        }

        node1.Previous = null!;
        node1.Next = null!;

        if (node2 != null && comparer.Compare(node1.Value, node2.Value) <= 0)
        {
            AddChild(ref node1, node2);
            return node1;
        }

        AddChild(ref node2!, node1);
        return node2;
    }

    private void AddChild(ref PairingHeapNode<T> parent, PairingHeapNode<T> child)
    {
        if (parent.ChildrenHead == null)
        {
            parent.ChildrenHead = child;
            child.Previous = parent;
            return;
        }

        var head = parent.ChildrenHead;

        child.Previous = head;
        child.Next = head.Next;

        if (head.Next != null)
        {
            head.Next.Previous = child;
        }

        head.Next = child;
    }

    private void DeleteChild(PairingHeapNode<T> node)
    {
        if (node.IsHeadChild)
        {
            var parent = node.Previous;

            if (node.Next != null)
            {
                node.Next.Previous = parent;
            }

            parent.ChildrenHead = node.Next!;
        }
        else
        {
            node.Previous.Next = node.Next;

            if (node.Next != null)
            {
                node.Next.Previous = node.Previous;
            }
        }
    }

    private void Map(T newItem, PairingHeapNode<T> newNode)
    {
        if (mapping.ContainsKey(newItem))
        {
            mapping[newItem].Add(newNode);
        }
        else
        {
            mapping[newItem] = new List<PairingHeapNode<T>>(new[] { newNode });
        }
    }

    private void UpdateNodeValue(T currentValue, T newValue, PairingHeapNode<T> node)
    {
        RemoveMapping(currentValue, node);
        node.Value = newValue;
        Map(newValue, node);
    }

    private void RemoveMapping(T currentValue, PairingHeapNode<T> node)
    {
        mapping[currentValue].Remove(node);
        if (mapping[currentValue].Count == 0)
        {
            mapping.Remove(currentValue);
        }
    }
}
