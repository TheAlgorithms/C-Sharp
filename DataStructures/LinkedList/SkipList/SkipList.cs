using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DataStructures.LinkedList.SkipList;

/// <summary>
/// Skip list implementation that is based on the singly linked list,
/// but offers O(log n) time complexity on most operations.
/// </summary>
/// <typeparam name="TValue">The type of the values in the list.</typeparam>
/// <remarks>
/// Skip list nodes sorted by key.
/// The "skip lanes" allow searching for a node in O(log n) time on average.
/// The worst case performence is O(n) when the height of all nodes is 1 (very
/// unluckily to happen on any decent list size).
/// These two properties make the skip list an excellent data structure for
/// implementing additional operations like finding min/max value in the list,
/// finding values with the key in a given range, etc.
///
/// Sourses:
/// - "Skip Lists: A Probabilistic Alternative to Balanced Trees" by William Pugh.
/// - https://en.wikipedia.org/wiki/Skip_list
/// - https://iq.opengenus.org/skip-list/
/// - https://medium.com/simple-computer-science/data-structures-basics-skip-list-8b8c69f9a044
/// - https://github.com/TheAlgorithms/Java/blob/master/src/main/java/com/thealgorithms/datastructures/lists/SkipList.java
///
/// The key is hardcoded to be of type <c>int</c> to simplify the implementation,
/// but it can be easily an any generic type that implements <c>IComparable</c>.
/// </remarks>
[DebuggerDisplay("Count = {Count}")]
public class SkipList<TValue>
{
    private const double Probability = 0.5;
    private readonly int maxLevels;
    private readonly SkipListNode<TValue> head;
    private readonly SkipListNode<TValue> tail;
    private readonly Random random = new Random();

    /// <summary>
    /// Initializes a new instance of the <see cref="SkipList{TValue}"/> class.
    /// </summary>
    /// <param name="capacity">Expected number of elements the list might contain.</param>
    public SkipList(int capacity = 255)
    {
        maxLevels = (int)Math.Log2(capacity) + 1;

        head = new(int.MinValue, default(TValue), maxLevels);
        tail = new(int.MaxValue, default(TValue), maxLevels);

        for(int i = 0; i < maxLevels; i++)
        {
            head.Next[i] = tail;
        }
    }

    /// <summary>
    /// Gets the number of elements currently in the list.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    /// Gets or sets the element with the specified key.
    /// </summary>
    /// <exception cref="KeyNotFoundException">The key is not present in the list.</exception>
    public TValue this[int key]
    {
        get
        {
            var previousNode = GetSkipNodes(key).First();
            if(previousNode.Next[0].Key == key)
            {
                return previousNode.Next[0].Value!;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        set => AddOrUpdate(key, value);
    }

    /// <summary>
    /// Adds an element with the specified key and value to the list.
    /// If an element with the same key already exists, updates its value.
    /// </summary>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">The value of the element to add.</param>
    /// <remarks>
    /// Time complexity: O(log n) where n is the number of elements in the list.
    /// </remarks>
    public void AddOrUpdate(int key, TValue value)
    {
        var skipNodes = GetSkipNodes(key);

        var previousNode = skipNodes.First();
        if (previousNode.Next[0].Key == key)
        {
            // Node with the given key already exists.
            // Update its value.
            previousNode.Next[0].Value = value;
            return;
        }

        // Node with the given key does not exist.
        // Insert the new one and update the skip nodes.
        var newNode = new SkipListNode<TValue>(key, value, GetRandomHeight());
        for (var level = 0; level < newNode.Height; level++)
        {
            newNode.Next[level] = skipNodes[level].Next[level];
            skipNodes[level].Next[level] = newNode;
        }

        Count++;
    }

    /// <summary>
    /// Returns whether a value with the given key exists in the list.
    /// </summary>
    /// <remarks>
    /// Time complexity: O(log n) where n is the number of elements in the list.
    /// </remarks>
    public bool Contains(int key)
    {
        var previousNode = GetSkipNodes(key).First();
        return previousNode.Next[0].Key == key;
    }

    /// <summary>
    /// Removes the value with the given key from the list.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the value was removed; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Time complexity: O(log n) where n is the number of elements in the list.
    /// </remarks>
    public bool Remove(int key)
    {
        var skipNodes = GetSkipNodes(key);
        var previousNode = skipNodes.First();
        if (previousNode.Next[0].Key != key)
        {
            return false;
        }

        // Key exists in the list, remove it and update the skip nodes.
        var nodeToRemove = previousNode.Next[0];
        for (var level = 0; level < nodeToRemove.Height; level++)
        {
            skipNodes[level].Next[level] = nodeToRemove.Next[level];
        }

        Count--;

        return true;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the list.
    /// </summary>
    /// <remarks>
    /// Order of values is the ascending order of their keys.
    /// Time complexity: O(n) where n is the number of elements in the list.
    /// </remarks>
    public IEnumerable<TValue> GetValues()
    {
        var current = head.Next[0];
        while (current.Key != tail.Key)
        {
            yield return current.Value!;
            current = current.Next[0];
        }
    }

    /// <summary>
    /// Builds a list of skip nodes on each level that
    /// are closest, but smaller than the given key.
    /// </summary>
    /// <remarks>
    /// The node on level 0 will point to the node with the given key, if it exists.
    /// Time complexity: O(log n) where n is the number of elements in the list.
    /// </remarks>
    private SkipListNode<TValue>[] GetSkipNodes(int key)
    {
        var skipNodes = new SkipListNode<TValue>[maxLevels];
        var current = head;
        for (var level = head.Height - 1; level >= 0; level--)
        {
            while (current.Next[level].Key < key)
            {
                current = current.Next[level];
            }

            skipNodes[level] = current;
        }

        return skipNodes;
    }

    /// <summary>
    /// Determines the height of skip levels for the new node.
    /// </summary>
    /// <remarks>
    /// Probability of the next level is 1/(2^level).
    /// </remarks>
    private int GetRandomHeight()
    {
        int height = 1;
        while (random.NextDouble() < Probability && height < maxLevels)
        {
            height++;
        }

        return height;
    }
}
