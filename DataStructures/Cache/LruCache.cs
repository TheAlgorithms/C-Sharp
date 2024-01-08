using System;
using System.Collections.Generic;

namespace DataStructures.Cache;

/// <summary>
/// Least Recently Used (LRU) cache implementation.
/// </summary>
/// <typeparam name="TKey">The type of the key (must be not null).</typeparam>
/// <typeparam name="TValue">The type of the value.</typeparam>
/// <remarks>
/// Cache keeps up to <c>capacity</c> items. When new item is added and cache is full,
/// the least recently used item is removed (e.g. it keeps N items that were recently requested
/// using <c>Get()</c> or <c>Put()</c> methods).
///
/// Cache is built on top of two data structures:
/// - <c>Dictionary</c> - allows items to be looked up by key in O(1) time.
/// - <c>LinkedList</c> - allows items to be ordered by last usage time in O(1) time.
///
/// Useful links:
/// https://en.wikipedia.org/wiki/Cache_replacement_policies
/// https://www.educative.io/m/implement-least-recently-used-cache
/// https://leetcode.com/problems/lru-cache/
///
/// In order to make the most recently used (MRU) cache, when the cache is full,
/// just remove the last node from the linked list in the method <c>Put</c>
/// (replace <c>RemoveFirst</c> with <c>RemoveLast</c>).
/// </remarks>
public class LruCache<TKey, TValue> where TKey : notnull
{
    private class CachedItem
    {
        public TKey Key { get; set; } = default!;

        public TValue? Value { get; set; }
    }

    private const int DefaultCapacity = 100;

    private readonly int capacity;

    // Note that <c>Dictionary</c> stores <c>LinkedListNode</c> as it allows
    // removing the node from the <c>LinkedList</c> in O(1) time.
    private readonly Dictionary<TKey, LinkedListNode<CachedItem>> cache = new();
    private readonly LinkedList<CachedItem> lruList = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="LruCache{TKey, TValue}"/> class.
    /// </summary>
    /// <param name="capacity">The max number of items the cache can store.</param>
    public LruCache(int capacity = DefaultCapacity)
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
        lruList.Remove(node);
        lruList.AddLast(node);

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
    /// If the cache is full, the least recently used item is removed.
    /// </remarks>
    public void Put(TKey key, TValue value)
    {
        if (cache.ContainsKey(key))
        {
            var existingNode = cache[key];
            existingNode.Value.Value = value;
            lruList.Remove(existingNode);
            lruList.AddLast(existingNode);
            return;
        }

        if (cache.Count >= capacity)
        {
            var first = lruList.First!;
            lruList.RemoveFirst();
            cache.Remove(first.Value.Key);
        }

        var item = new CachedItem { Key = key, Value = value };
        var newNode = lruList.AddLast(item);
        cache.Add(key, newNode);
    }
}
