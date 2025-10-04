namespace DataStructures.Hashing;

/// <summary>
/// Entry in the hash table.
/// </summary>
/// <typeparam name="TKey">Type of the key.</typeparam>
/// <typeparam name="TValue">Type of the value.</typeparam>
/// <remarks>
/// This class is used to store the key-value pairs in the hash table.
/// </remarks>
public class Entry<TKey, TValue>(TKey key, TValue value)
{
    public TKey? Key { get; set; } = key;

    public TValue? Value { get; set; } = value;
}
