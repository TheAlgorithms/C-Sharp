using System;
using System.Collections.Generic;

namespace Utilities.Extensions;

public static class DictionaryExtensions
{
    /// <summary>
    ///     Adds the specified key value tuples to the dictionary.
    /// </summary>
    /// <param name="keys">The dictionary.</param>
    /// <param name="enumerable">The collection of key value tuples to add.</param>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <exception cref="ArgumentException">
    ///     A key from the <paramref name="enumerable"/> already exists in <paramref name="keys"/>.
    /// </exception>
    public static void AddMany<TKey, TValue>(
        this Dictionary<TKey, TValue> keys,
        IEnumerable<(TKey, TValue)> enumerable) where TKey : notnull
    {
        foreach (var (key, value) in enumerable)
        {
            keys.Add(key, value);
        }
    }
}
