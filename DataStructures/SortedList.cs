using System.Collections;
using System.Collections.Generic;

namespace DataStructures;

/// <summary>
///     Implementation of SortedList using binary search.
/// </summary>
/// <typeparam name="T">Generic Type.</typeparam>
public class SortedList<T> : IEnumerable<T>
{
    private readonly IComparer<T> comparer;
    private readonly List<T> memory;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SortedList{T}" /> class. Uses a Comparer.Default for type T.
    /// </summary>
    public SortedList()
        : this(Comparer<T>.Default)
    {
    }

    /// <summary>
    ///     Gets the number of elements containing in <see cref="SortedList{T}" />.
    /// </summary>
    public int Count => memory.Count;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SortedList{T}" /> class.
    /// </summary>
    /// <param name="comparer">Comparer user for binary search.</param>
    public SortedList(IComparer<T> comparer)
    {
        memory = new List<T>();
        this.comparer = comparer;
    }

    /// <summary>
    ///     Adds new item to <see cref="SortedList{T}" /> instance, maintaining the order.
    /// </summary>
    /// <param name="item">An element to insert.</param>
    public void Add(T item)
    {
        var index = IndexFor(item, out _);
        memory.Insert(index, item);
    }

    /// <summary>
    ///     Gets an element of <see cref="SortedList{T}" /> at specified index.
    /// </summary>
    /// <param name="i">Index.</param>
    public T this[int i] => memory[i];

    /// <summary>
    /// Removes all elements from <see cref="SortedList{T}" />.
    /// </summary>
    public void Clear()
        => memory.Clear();

    /// <summary>
    /// Indicates whether a <see cref="SortedList{T}" /> contains a certain element.
    /// </summary>
    /// <param name="item">An element to search.</param>
    /// <returns>true - <see cref="SortedList{T}" /> contains an element, otherwise - false.</returns>
    public bool Contains(T item)
    {
        _ = IndexFor(item, out var found);
        return found;
    }

    /// <summary>
    /// Removes a certain element from <see cref="SortedList{T}" />.
    /// </summary>
    /// <param name="item">An element to remove.</param>
    /// <returns>true - element is found and removed, otherwise false.</returns>
    public bool TryRemove(T item)
    {
        var index = IndexFor(item, out var found);

        if (found)
        {
            memory.RemoveAt(index);
        }

        return found;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the <see cref="SortedList{T}" />.
    /// </summary>
    /// <returns>A Enumerator for the <see cref="SortedList{T}" />.</returns>
    public IEnumerator<T> GetEnumerator()
        => memory.GetEnumerator();

    /// <inheritdoc cref="IEnumerable.GetEnumerator"/>
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <summary>
    /// Binary search algorithm for finding element index in <see cref="SortedList{T}" />.
    /// </summary>
    /// <param name="item">Element.</param>
    /// <param name="found">Indicates whether the equal value was found in <see cref="SortedList{T}" />.</param>
    /// <returns>Index for the Element.</returns>
    private int IndexFor(T item, out bool found)
    {
        var left = 0;
        var right = memory.Count;

        while (right - left > 0)
        {
            var mid = (left + right) / 2;

            switch (comparer.Compare(item, memory[mid]))
            {
                case > 0:
                    left = mid + 1;
                    break;
                case < 0:
                    right = mid;
                    break;
                default:
                    found = true;
                    return mid;
            }
        }

        found = false;
        return left;
    }
}
