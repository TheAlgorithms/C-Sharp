using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures;

/// <summary>
/// Implementation of a Bag data structure using a hashmap that allows adding items and iterating through them.
/// Items with the same value are stored as a single entry with a count.
/// </summary>
/// <typeparam name="T">Generic Type.</typeparam>
public class Bag<T> : IEnumerable<T> where T : notnull
{
    private readonly Dictionary<T, int> items;

    /// <summary>
    /// Initializes a new instance of the <see cref="Bag{T}" /> class.
    /// </summary>
    public Bag()
    {
        items = [];
    }

    /// <summary>
    /// Adds an item to the bag.
    /// </summary>
    public void Add(T item)
    {
        if (items.TryGetValue(item, out var count))
        {
            items[item] = count + 1;
        }
        else
        {
            items[item] = 1;
        }
    }

    /// <summary>
    /// Clears the bag.
    /// </summary>
    public void Clear() => items.Clear();

    /// <summary>
    /// Gets the number of items in the bag.
    /// </summary>
    public int Count => items.Values.Sum();

    /// <summary>
    /// Returns a boolean indicating whether the bag is empty.
    /// </summary>
    public bool IsEmpty() => items.Count == 0;

    /// <summary>
    /// Returns an enumerator that iterates through the bag.
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        foreach (var pair in items)
        {
            for (var i = 0; i < pair.Value; i++)
            {
                yield return pair.Key;
            }
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the bag.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
