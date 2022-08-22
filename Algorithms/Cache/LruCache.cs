using System;
using System.Collections.Generic;

namespace Algorithms.Cache
{
    /// <summary>
    /// Least Recently Used (LRU) cache implementation.
    /// </summary>
    /// <typeparam name="TKey">The type of the key (must be not null).</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <remarks>
    /// Cache keeps up to <c>capacity</c> items. When new item is added and cache is full,
    /// the least recently used item is removed (e.g. it keeps N items that were recently requested
    /// using <c>Get()</c> or <c>TryGet()</c> methods).
    ///
    /// Cache is built on top of two data structures:
    /// - <c>Dictionary</c> - allows items to be looked up by key in O(1) time.
    /// - <c>LinkedList</c> - allows items to be ordered by last usage time in O(1) time.
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
        private readonly Dictionary<TKey, LinkedListNode<CachedItem>> map = new();
        private readonly LinkedList<CachedItem> lruList = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="LruCache{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="capacity">The max number of items the cache can store.</param>
        public LruCache(int capacity = DefaultCapacity)
        {
            this.capacity = capacity;
        }

        /// <summary>
        /// Gets the cached item by key.
        /// </summary>
        /// <param name="key">The key of cached item.</param>
        /// <returns>The cached item or <c>null</c> if item is not found.</returns>
        /// <remarks> Time complexity: O(1). </remarks>
        public TValue? Get(TKey key)
        {
            if (!map.ContainsKey(key))
            {
                return default;
            }

            var node = map[key];
            lruList.Remove(node);
            lruList.AddLast(node);

            return node.Value.Value;
        }

        /// <summary>
        /// Tries to get the cached item by key.
        /// </summary>
        /// <param name="key">The key of cached item.</param>
        /// <param name="value">The cached item or <c>null</c> if item is not found.</param>
        /// <returns><c>true</c> if item is found, <c>false</c> otherwise.</returns>
        /// <remarks> Time complexity: O(1). </remarks>
        public bool TryGet(TKey key, out TValue? value)
        {
            if (!map.ContainsKey(key))
            {
                value = default;
                return false;
            }

            value = Get(key);
            return true;
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
            if (map.ContainsKey(key))
            {
                var node = map[key];
                node.Value.Value = value;
                lruList.Remove(node);
                lruList.AddLast(node);
            }
            else
            {
                var item = new CachedItem { Key = key, Value = value };
                var node = lruList.AddLast(item);
                map.Add(key, node);

                if (map.Count > capacity)
                {
                    var first = lruList.First!;
                    lruList.RemoveFirst();
                    map.Remove(first.Value.Key);
                }
            }
        }
    }
}
