namespace Algorithms.Encoders
{
    /// <summary>
    /// Encodes and decodes text based on specified key.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    public interface IEncoder<TKey>
    {
        /// <summary>
        /// Encodes text using specified key.
        /// </summary>
        /// <param name="text">Text to be encoded.</param>
        /// <param name="key">Key that will be used to encode the text.</param>
        /// <returns>Encoded text.</returns>
        string Encode(string text, TKey key);

        /// <summary>
        /// Decodes text that was encoded using specified key.
        /// </summary>
        /// <param name="text">Text to be decoded.</param>
        /// <param name="key">Key that was used to encode the text.</param>
        /// <returns>Decoded text.</returns>
        string Decode(string text, TKey key);
    }
}
