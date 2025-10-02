using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Bag;

/// <summary>
/// Implementation of a Bag (or multiset) data structure using a basic linked list.
/// </summary>
/// <remarks>
/// A bag (or multiset, or mset) is a modification of the concept of a set that, unlike a set, allows for multiple instances for each of its elements.
/// The number of instances given for each element is called the multiplicity of that element in the multiset.
/// As a consequence, an infinite number of multisets exist that contain only elements a and b, but vary in the multiplicities of their elements.
/// See https://en.wikipedia.org/wiki/Multiset for more information.
/// </remarks>
/// <typeparam name="T">Generic Type.</typeparam>
public class Bag<T> : IEnumerable<T> where T : notnull
{
    private BagNode<T>? head;
    private int totalCount;

    /// <summary>
    /// Initializes a new instance of the <see cref="Bag{T}" /> class.
    /// </summary>
    public Bag()
    {
        head = null;
        totalCount = 0;
    }

    /// <summary>
    /// Adds an item to the bag. If the item already exists, increases its multiplicity.
    /// </summary>
    public void Add(T item)
    {
        // If the bag is empty, create the first node
        if (head == null)
        {
            head = new BagNode<T>(item);
            totalCount = 1;
            return;
        }

        // Check if item already exists
        var current = head;
        BagNode<T>? previous = null;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Item, item))
            {
                current.Multiplicity++;
                totalCount++;
                return;
            }

            previous = current;
            current = current.Next;
        }

        previous!.Next = new BagNode<T>(item);
        totalCount++;
    }

    /// <summary>
    /// Clears the bag.
    /// </summary>
    public void Clear()
    {
        head = null;
        totalCount = 0;
    }

    /// <summary>
    /// Gets the number of items in the bag.
    /// </summary>
    public int Count => totalCount;

    /// <summary>
    /// Returns a boolean indicating whether the bag is empty.
    /// </summary>
    public bool IsEmpty() => head == null;

    /// <summary>
    /// Returns an enumerator that iterates through the bag.
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        var current = head;

        while (current != null)
        {
            // Yield the item as many times as its multiplicity, pretending they are separate items
            for (var i = 0; i < current.Multiplicity; i++)
            {
                yield return current.Item;
            }

            current = current.Next;
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the bag.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
