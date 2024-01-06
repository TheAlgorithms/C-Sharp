using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Hashing.NumberTheory;

namespace DataStructures.Hashing;

/// <summary>
/// Hash table implementation.
/// </summary>
/// <typeparam name="TKey">Type of the key.</typeparam>
/// <typeparam name="TValue">Type of the value.</typeparam>
public class HashTable<TKey, TValue>
{
    private const int DefaultCapacity = 16;
    private const float DefaultLoadFactor = 0.75f;

    private readonly float loadFactor;
    private int capacity;
    private int size;
    private int threshold;
    private int version;

    private Entry<TKey, TValue>?[] entries;

    /// <summary>
    /// Gets the number of elements in the hash table.
    /// </summary>
    public int Count => size;

    /// <summary>
    /// Gets the capacity of the hash table.
    /// </summary>
    public int Capacity => capacity;

    /// <summary>
    /// Gets the load factor of the hash table.
    /// </summary>
    public float LoadFactor => loadFactor;

    /// <summary>
    /// Gets the keys in the hash table.
    /// </summary>
    public IEnumerable<TKey> Keys => entries.Where(e => e != null).Select(e => e!.Key!);

    /// <summary>
    /// Gets the values in the hash table.
    /// </summary>
    public IEnumerable<TValue> Values => entries.Where(e => e != null).Select(e => e!.Value!);

    /// <summary>
    /// Gets or sets the value associated with the specified key.
    /// </summary>
    /// <param name="key">Key to get or set.</param>
    /// <returns>Value associated with the key.</returns>
    public TValue this[TKey? key]
    {
        get
        {
            if (EqualityComparer<TKey>.Default.Equals(key, default(TKey)))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var entry = FindEntry(key);
            if (entry == null)
            {
                throw new KeyNotFoundException();
            }

            return entry.Value!;
        }

        set
        {
            if (EqualityComparer<TKey>.Default.Equals(key, default(TKey)))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var entry = FindEntry(key);
            if (entry == null)
            {
                throw new KeyNotFoundException();
            }

            entry.Value = value;
            version++;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HashTable{TKey, TValue}"/> class.
    /// </summary>
    /// <param name="capacity">Initial capacity of the hash table.</param>
    /// <param name="loadFactor">Load factor of the hash table.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="capacity"/> is less than or equal to 0.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="loadFactor"/> is less than or equal to 0.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="loadFactor"/> is greater than 1.</exception>
    /// <remarks>
    /// <paramref name="capacity"/> is rounded to the next prime number.
    /// </remarks>
    /// <see cref="PrimeNumber.NextPrime(int, int, bool)"/>
    /// <see cref="PrimeNumber.IsPrime(int)"/>
    public HashTable(int capacity = DefaultCapacity, float loadFactor = DefaultLoadFactor)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than 0");
        }

        if (loadFactor <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(loadFactor), "Load factor must be greater than 0");
        }

        if (loadFactor > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(loadFactor), "Load factor must be less than or equal to 1");
        }

        this.capacity = PrimeNumber.NextPrime(capacity);
        this.loadFactor = loadFactor;
        threshold = (int)(this.capacity * loadFactor);
        entries = new Entry<TKey, TValue>[this.capacity];
    }

    /// <summary>
    /// Adds a key-value pair to the hash table.
    /// </summary>
    /// <param name="key">Key to add.</param>
    /// <param name="value">Value to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="key"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="key"/> already exists in the hash table.</exception>
    /// <remarks>
    /// If the number of elements in the hash table is greater than or equal to the threshold, the hash table is resized.
    /// </remarks>
    public void Add(TKey? key, TValue? value)
    {
        if (EqualityComparer<TKey>.Default.Equals(key, default(TKey)))
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (size >= threshold)
        {
            Resize();
        }

        var index = GetIndex(key);
        if (
            entries[index] != null &&
            EqualityComparer<TKey>.Default.Equals(entries[index] !.Key!, key))
        {
            throw new ArgumentException("Key already exists");
        }

        if (EqualityComparer<TValue>.Default.Equals(value, default(TValue)))
        {
            throw new ArgumentNullException(nameof(value));
        }

        entries[index] = new Entry<TKey, TValue>(key!, value!);
        size++;
        version++;
    }

    /// <summary>
    /// Removes the key-value pair associated with the specified key.
    /// </summary>
    /// <param name="key">Key to remove.</param>
    /// <returns>True if the key-value pair was removed, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="key"/> is null.</exception>
    /// <remarks>
    /// If the number of elements in the hash table is less than or equal to the threshold divided by 4, the hash table is resized.
    /// </remarks>
    public bool Remove(TKey? key)
    {
        if (EqualityComparer<TKey>.Default.Equals(key, default(TKey)))
        {
            throw new ArgumentNullException(nameof(key));
        }

        var index = GetIndex(key);
        if (entries[index] == null)
        {
            return false;
        }

        entries[index] = null;
        size--;
        version++;

        if (size <= threshold / 4)
        {
            Resize();
        }

        return true;
    }

    /// <summary>
    /// Find the index of the entry associated with the specified key.
    /// </summary>
    /// <param name="key">Key to find.</param>
    /// <returns>Index of the entry associated with the key.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="key"/> is null.</exception>
    public int GetIndex(TKey? key)
    {
        if (EqualityComparer<TKey>.Default.Equals(key, default(TKey)))
        {
            throw new ArgumentNullException(nameof(key));
        }

        var hash = key!.GetHashCode();
        var index = hash % capacity;

        if (index < 0)
        {
            index += capacity;
        }

        return index;
    }

    /// <summary>
    /// Finds the entry associated with the specified key.
    /// </summary>
    /// <param name="key">Key to find.</param>
    /// <returns>Entry associated with the key.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="key"/> is null.</exception>
    /// <remarks>
    /// This method uses <see cref="GetIndex(TKey)"/> internally.
    /// </remarks>
    public Entry<TKey, TValue>? FindEntry(TKey? key)
    {
        if (EqualityComparer<TKey>.Default.Equals(key, default(TKey)))
        {
            throw new ArgumentNullException(nameof(key));
        }

        var index = GetIndex(key);
        return entries[index];
    }

    /// <summary>
    /// Checks if the hash table contains the specified key.
    /// </summary>
    /// <param name="key">Key to check.</param>
    /// <returns>True if the hash table contains the key, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="key"/> is null.</exception>
    /// <remarks>
    /// This method uses <see cref="FindEntry(TKey)"/> internally.
    /// </remarks>
    public bool ContainsKey(TKey? key)
    {
        if (EqualityComparer<TKey>.Default.Equals(key, default(TKey)))
        {
            throw new ArgumentNullException(nameof(key));
        }

        return FindEntry(key) != null;
    }

    /// <summary>
    /// Checks if the hash table contains the specified value.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>True if the hash table contains the value, false otherwise.</returns>
    public bool ContainsValue(TValue? value)
    {
        if (EqualityComparer<TValue>.Default.Equals(value, default(TValue)))
        {
            throw new ArgumentNullException(nameof(value));
        }

        return entries.Any(e => e != null && e.Value!.Equals(value));
    }

    /// <summary>
    /// Clears the hash table.
    /// </summary>
    /// <remarks>
    /// This method resets the capacity of the hash table to the default capacity.
    /// </remarks>
    public void Clear()
    {
        capacity = DefaultCapacity;
        threshold = (int)(capacity * loadFactor);
        entries = new Entry<TKey, TValue>[capacity];
        size = 0;
        version++;
    }

    /// <summary>
    /// Resizes the hash table.
    /// </summary>
    /// <remarks>
    /// This method doubles the capacity of the hash table and rehashes all the elements.
    /// </remarks>
    public void Resize()
    {
        var newCapacity = capacity * 2;
        var newEntries = new Entry<TKey, TValue>[newCapacity];

        foreach (var entry in entries)
        {
            if (entry == null)
            {
                continue;
            }

            var index = entry.Key!.GetHashCode() % newCapacity;
            if (index < 0)
            {
                index += newCapacity;
            }

            newEntries[index] = entry;
        }

        capacity = newCapacity;
        threshold = (int)(capacity * loadFactor);
        entries = newEntries;
        version++;
    }
}
