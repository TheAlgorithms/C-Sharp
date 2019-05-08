/// <summary>
/// Encodes and decodes value based on specified key
/// </summary>
public interface IEncoder<TKey>
{
    string Encode(string value, TKey key);
    string Decode(string value, TKey key);
}
