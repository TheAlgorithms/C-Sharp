using System;
using System.Collections.Generic;

namespace DataStructures.Cache;

/// <summary>
/// Least Frequently Used (LFU) cache implementation.
/// </summary>
/// <typeparam name="TKey">The type of the key (must be not null).</typeparam>
/// <typeparam name="TValue">The type of the value.</typeparam>
/// <remarks>
/// Cache keeps up to <c>capacity</c> items. When new item is added and cache is full,
/// one of the least frequently used item is removed (e.g. it keeps N items that were the most
/// frequently requested using <c>Get()</c> or <c>Put()</c> methods).
/// When there are multiple items with the same frequency, the least recently used item is removed.
///
/// Cache is built on top of two data structures:
/// - <c>Dictionary</c>. Allows items to be looked up by key in O(1) time. Another dictionary
///   is used to store the frequency of each key.
/// - <c>LinkedList</c> - Allows items with the same frequency to be ordered by the last
///   usage in O(1) time.
///
/// Useful links:
/// https://en.wikipedia.org/wiki/Cache_replacement_policies
/// https://www.enjoyalgorithms.com/blog/least-frequently-used-cache
/// https://www.educative.io/answers/what-is-least-frequently-used-cache-replace-policy
/// https://leetcode.com/problems/lfu-cache/ .
/// </remarks>
public class LfuCache<TKey, TValue> where TKey : notnull
{
    private class CachedItem
    {
        public TKey Key { get; set; } = default!;

        public TValue? Value { get; set; }

        public int Frequency { get; set; }
    }

    private const int DefaultCapacity = 100;

    private readonly int capacity;

    // Note that <c>Dictionary</c> stores <c>LinkedListNode</c> as it allows
    // removing the node from the <c>LinkedList</c> in O(1) time.
    private readonly Dictionary<TKey, LinkedListNode<CachedItem>> cache = new();

    // Map frequency (number of times the item was requested or updated)
    // to the LRU linked list.
    private readonly Dictionary<int, LinkedList<CachedItem>> frequencies = new();

    // Track the minimum frequency with non-empty linked list in <c>frequencies</c>.
    // When the last item with the minFrequency is promoted (after being requested or updated),
    // the <c>minFrequency</c> is increased.
    // When a new item is added, the <c>minFrequency</c> is set to 1.
    private int minFrequency = -1;

    /// <summary>
    /// Initializes a new instance of the <see cref="LfuCache{TKey, TValue}"/> class.
    /// </summary>
    /// <param name="capacity">The max number of items the cache can store.</param>
    public LfuCache(int capacity = DefaultCapacity)
    {
        this.capacity = capacity;
    }

    public bool Contains(TKey key) => cache.ContainsKey(key);

    /// <summary>
    /// Gets the cached item by key.
    /// </summary>
    /// <param name="key">The key of cached item.</param>
    /// <returns>The cached item or <c>default</c> if item is not found.</returns>
    /// <remarks> Time complexity: O(1). </remarks>
    public TValue? Get(TKey key)
    {
        if (!cache.ContainsKey(key))
        {
            return default;
        }

        var node = cache[key];
        UpdateFrequency(node, isNew: false);
        return node.Value.Value;
    }

    /// <summary>
    /// Adds or updates the value in the cache.
    /// </summary>
    /// <param name="key">The key of item to cache.</param>
    /// <param name="value">The value to cache.</param>
    /// <remarks>
    /// Time complexity: O(1).
    /// If the value is already cached, it is updated and the item is moved
    /// to the end of the LRU list.
    /// If the cache is full, one of the least frequently used items is removed.
    /// </remarks>
    public void Put(TKey key, TValue value)
    {
        if (cache.ContainsKey(key))
        {
            var existingNode = cache[key];
            existingNode.Value.Value = value;
            UpdateFrequency(existingNode, isNew: false);
            return;
        }

        if (cache.Count >= capacity)
        {
            EvictOneItem();
        }

        var item = new CachedItem { Key = key, Value = value };
        var newNode = new LinkedListNode<CachedItem>(item);
        UpdateFrequency(newNode, isNew: true);
        cache.Add(key, newNode);
    }

    private void UpdateFrequency(LinkedListNode<CachedItem> node, bool isNew)
    {
        var item = node.Value;

        if (isNew)
        {
            item.Frequency = 1;
            minFrequency = 1;
        }
        else
        {
            // Remove the existing node from the LRU list with its previous frequency.
            var lruList = frequencies[item.Frequency];
            lruList.Remove(node);
            if (lruList.Count == 0 && minFrequency == item.Frequency)
            {
                minFrequency++;
            }

            item.Frequency++;
        }

        // Insert item to the end of the LRU list that corresponds to its new frequency.
        if (!frequencies.ContainsKey(item.Frequency))
        {
            frequencies[item.Frequency] = new LinkedList<CachedItem>();
        }

        frequencies[item.Frequency].AddLast(node);
    }

    private void EvictOneItem()
    {
        var lruList = frequencies[minFrequency];
        var itemToRemove = lruList.First!.Value;
        lruList.RemoveFirst();
        cache.Remove(itemToRemove.Key);
    }
}
