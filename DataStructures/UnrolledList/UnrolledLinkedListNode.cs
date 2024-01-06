using System;

namespace DataStructures.UnrolledList;

/// <summary>
/// Single node with array buffer for unrolled list.
/// </summary>
public class UnrolledLinkedListNode
{
    private readonly int[] array;

    public UnrolledLinkedListNode(int nodeSize)
    {
        Next = null!;

        Count = 0;
        array = new int[nodeSize];
    }

    public UnrolledLinkedListNode Next { get; set; }

    public int Count { get; set; }

    /// <summary>
    /// Set new item in array buffer.
    /// </summary>
    /// <param name="pos">Index in array.</param>
    /// <param name="val">The entered value.</param>
    /// <exception cref="ArgumentException">Index is out of scope.</exception>
    public void Set(int pos, int val)
    {
        if (pos < 0 || pos > array.Length - 1)
        {
            throw new ArgumentException("Position is out of size", nameof(pos));
        }

        array[pos] = val;
        Count++;
    }

    /// <summary>
    /// Get item from array buffer.
    /// </summary>
    /// <param name="pos">Index in array.</param>
    /// <exception cref="ArgumentException">Index is out of scope.</exception>
    public int Get(int pos)
    {
        if (pos < 0 || pos > array.Length - 1)
        {
            throw new ArgumentException("Position is out of size", nameof(pos));
        }

        return array[pos];
    }
}
