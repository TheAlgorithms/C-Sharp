using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Heap;

/// <summary>
///     This class implements min-max heap.
///     It provides functionality of both min-heap and max-heap with the same time complexity.
///     Therefore it provides constant time retrieval and logarithmic time removal
///     of both the minimum and maximum elements in it.
/// </summary>
/// <typeparam name="T">Generic type.</typeparam>
public class MinMaxHeap<T>
{
    private readonly List<T> heap;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MinMaxHeap{T}" /> class that contains
    ///     elements copied from a specified enumerable collection and that uses a specified comparer.
    /// </summary>
    /// <param name="collection">The enumerable collection to be copied.</param>
    /// <param name="comparer">The default comparer to use for comparing objects.</param>
    public MinMaxHeap(IEnumerable<T>? collection = null, IComparer<T>? comparer = null)
    {
        Comparer = comparer ?? Comparer<T>.Default;
        collection ??= Enumerable.Empty<T>();

        heap = collection.ToList();
        for (var i = Count / 2 - 1; i >= 0; --i)
        {
            PushDown(i);
        }
    }

    /// <summary>
    ///     Gets the  <see cref="IComparer{T}" />. object that is used to order the values in the <see cref="MinMaxHeap{T}" />.
    /// </summary>
    public IComparer<T> Comparer { get; }

    /// <summary>
    ///     Gets the number of elements in the <see cref="MinMaxHeap{T}" />.
    /// </summary>
    public int Count => heap.Count;

    /// <summary>
    ///     Adds an element to the heap.
    /// </summary>
    /// <param name="item">The element to add to the heap.</param>
    public void Add(T item)
    {
        heap.Add(item);
        PushUp(Count - 1);
    }

    /// <summary>
    ///     Removes the maximum node from the heap and returns its value.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if heap is empty.</exception>
    /// <returns>Value of the removed maximum node.</returns>
    public T ExtractMax()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("Heap is empty");
        }

        var max = GetMax();
        RemoveNode(GetMaxNodeIndex());
        return max;
    }

    /// <summary>
    ///     Removes the minimum node from the heap and returns its value.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if heap is empty.</exception>
    /// <returns>Value of the removed minimum node.</returns>
    public T ExtractMin()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("Heap is empty");
        }

        var min = GetMin();
        RemoveNode(0);
        return min;
    }

    /// <summary>
    ///     Gets the maximum value in the heap, as defined by the comparer.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if heap is empty.</exception>
    /// <returns>The maximum value in the heap.</returns>
    public T GetMax()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("Heap is empty");
        }

        return heap[GetMaxNodeIndex()];
    }

    /// <summary>
    ///     Gets the minimum value in the heap, as defined by the comparer.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if heap is empty.</exception>
    /// <returns>The minimum value in the heap.</returns>
    public T GetMin()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("Heap is empty");
        }

        return heap[0];
    }

    /// <summary>
    ///     Finds maximum value among children and grandchildren of the specified node.
    /// </summary>
    /// <param name="index">Index of the node in the Heap array.</param>
    /// <returns>Index of the maximum descendant.</returns>
    private int IndexOfMaxChildOrGrandchild(int index)
    {
        var descendants = new[]
        {
            2 * index + 1,
            2 * index + 2,
            4 * index + 3,
            4 * index + 4,
            4 * index + 5,
            4 * index + 6,
        };
        var resIndex = descendants[0];
        foreach (var descendant in descendants)
        {
            if (descendant >= Count)
            {
                break;
            }

            if (Comparer.Compare(heap[descendant], heap[resIndex]) > 0)
            {
                resIndex = descendant;
            }
        }

        return resIndex;
    }

    /// <summary>
    ///     Finds minumum value among children and grandchildren of the specified node.
    /// </summary>
    /// <param name="index">Index of the node in the Heap array.</param>
    /// <returns>Index of the minimum descendant.</returns>
    private int IndexOfMinChildOrGrandchild(int index)
    {
        var descendants = new[] { 2 * index + 1, 2 * index + 2, 4 * index + 3, 4 * index + 4, 4 * index + 5, 4 * index + 6 };
        var resIndex = descendants[0];
        foreach (var descendant in descendants)
        {
            if (descendant >= Count)
            {
                break;
            }

            if (Comparer.Compare(heap[descendant], heap[resIndex]) < 0)
            {
                resIndex = descendant;
            }
        }

        return resIndex;
    }

    private int GetMaxNodeIndex()
    {
        return Count switch
        {
            0 => throw new InvalidOperationException("Heap is empty"),
            1 => 0,
            2 => 1,
            _ => Comparer.Compare(heap[1], heap[2]) > 0 ? 1 : 2,
        };
    }

    private bool HasChild(int index) => index * 2 + 1 < Count;

    private bool IsGrandchild(int node, int grandchild) => grandchild > 2 && Grandparent(grandchild) == node;

    /// <summary>
    ///     Checks if node at index belongs to Min or Max level of the heap.
    ///     Root node belongs to Min level, its children - Max level,
    ///     its grandchildren - Min level, and so on.
    /// </summary>
    /// <param name="index">Index to check.</param>
    /// <returns>true if index is at Min level; false if it is at Max Level.</returns>
    private bool IsMinLevelIndex(int index)
    {
        // For all Min levels, value (index + 1) has the leftmost bit set to '1' at even position.
        const uint minLevelsBits = 0x55555555;
        const uint maxLevelsBits = 0xAAAAAAAA;
        return ((index + 1) & minLevelsBits) > ((index + 1) & maxLevelsBits);
    }

    private int Parent(int index) => (index - 1) / 2;

    private int Grandparent(int index) => ((index - 1) / 2 - 1) / 2;

    /// <summary>
    ///     Assuming that children sub-trees are valid heaps, pushes node to lower levels
    ///     to make heap valid.
    /// </summary>
    /// <param name="index">Node index.</param>
    private void PushDown(int index)
    {
        if (IsMinLevelIndex(index))
        {
            PushDownMin(index);
        }
        else
        {
            PushDownMax(index);
        }
    }

    private void PushDownMax(int index)
    {
        if (!HasChild(index))
        {
            return;
        }

        var maxIndex = IndexOfMaxChildOrGrandchild(index);

        // If smaller element are put at min level (as result of swaping), it doesn't affect sub-tree validity.
        // If smaller element are put at max level, PushDownMax() should be called for that node.
        if (IsGrandchild(index, maxIndex))
        {
            if (Comparer.Compare(heap[maxIndex], heap[index]) > 0)
            {
                SwapNodes(maxIndex, index);
                if (Comparer.Compare(heap[maxIndex], heap[Parent(maxIndex)]) < 0)
                {
                    SwapNodes(maxIndex, Parent(maxIndex));
                }

                PushDownMax(maxIndex);
            }
        }
        else
        {
            if (Comparer.Compare(heap[maxIndex], heap[index]) > 0)
            {
                SwapNodes(maxIndex, index);
            }
        }
    }

    private void PushDownMin(int index)
    {
        if (!HasChild(index))
        {
            return;
        }

        var minIndex = IndexOfMinChildOrGrandchild(index);

        // If bigger element are put at max level (as result of swaping), it doesn't affect sub-tree validity.
        // If bigger element are put at min level, PushDownMin() should be called for that node.
        if (IsGrandchild(index, minIndex))
        {
            if (Comparer.Compare(heap[minIndex], heap[index]) < 0)
            {
                SwapNodes(minIndex, index);
                if (Comparer.Compare(heap[minIndex], heap[Parent(minIndex)]) > 0)
                {
                    SwapNodes(minIndex, Parent(minIndex));
                }

                PushDownMin(minIndex);
            }
        }
        else
        {
            if (Comparer.Compare(heap[minIndex], heap[index]) < 0)
            {
                SwapNodes(minIndex, index);
            }
        }
    }

    /// <summary>
    ///     Having a new node in the heap, swaps this node with its ancestors to make heap valid.
    ///     For node at min level. If new node is less than its parent, then it is surely less then
    ///     all other nodes on max levels on path to the root of the heap. So node are pushed up, by
    ///     swaping with its grandparent, until they are ordered correctly.
    ///     For node at max level algorithm is analogical.
    /// </summary>
    /// <param name="index">Index of the new node.</param>
    private void PushUp(int index)
    {
        if (index == 0)
        {
            return;
        }

        var parent = Parent(index);

        if (IsMinLevelIndex(index))
        {
            if (Comparer.Compare(heap[index], heap[parent]) > 0)
            {
                SwapNodes(index, parent);
                PushUpMax(parent);
            }
            else
            {
                PushUpMin(index);
            }
        }
        else
        {
            if (Comparer.Compare(heap[index], heap[parent]) < 0)
            {
                SwapNodes(index, parent);
                PushUpMin(parent);
            }
            else
            {
                PushUpMax(index);
            }
        }
    }

    private void PushUpMax(int index)
    {
        if (index > 2)
        {
            var grandparent = Grandparent(index);
            if (Comparer.Compare(heap[index], heap[grandparent]) > 0)
            {
                SwapNodes(index, grandparent);
                PushUpMax(grandparent);
            }
        }
    }

    private void PushUpMin(int index)
    {
        if (index > 2)
        {
            var grandparent = Grandparent(index);
            if (Comparer.Compare(heap[index], heap[grandparent]) < 0)
            {
                SwapNodes(index, grandparent);
                PushUpMin(grandparent);
            }
        }
    }

    private void RemoveNode(int index)
    {
        SwapNodes(index, Count - 1);
        heap.RemoveAt(Count - 1);
        if (Count != 0)
        {
            PushDown(index);
        }
    }

    private void SwapNodes(int i, int j)
    {
        var temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
}
