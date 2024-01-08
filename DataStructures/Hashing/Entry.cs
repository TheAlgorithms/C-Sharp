using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Hashing.NumberTheory;

namespace DataStructures.Hashing;

/// <summary>
/// Entry in the hash table.
/// </summary>
/// <typeparam name="TKey">Type of the key.</typeparam>
/// <typeparam name="TValue">Type of the value.</typeparam>
/// <remarks>
/// This class is used to store the key-value pairs in the hash table.
/// </remarks>
public class Entry<TKey, TValue>
{
    public TKey? Key { get; set; }

    public TValue? Value { get; set; }

    public Entry(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}
