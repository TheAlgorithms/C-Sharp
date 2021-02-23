using System;
using System.Text;

namespace Algorithms.Encoders
{
    /// <summary>
    /// Encodes using vigenere cypher.
    /// </summary>
    public class VigenereEncoder : IEncoder<string>
    {
        private readonly CaesarEncoder caesarEncoder = new ();

        /// <summary>
        /// Encodes text using specified key,
        /// time complexity: O(n),
        /// space complexity: O(n),
        /// where n - text length.
        /// </summary>
        /// <param name="text">Text to be encoded.</param>
        /// <param name="key">Key that will be used to encode the text.</param>
        /// <returns>Encoded text.</returns>
        public string Encode(string text, string key) => Cipher(text, key, caesarEncoder.Encode);

        /// <summary>
        /// Decodes text that was encoded using specified key,
        /// time complexity: O(n),
        /// space complexity: O(n),
        /// where n - text length.
        /// </summary>
        /// <param name="text">Text to be decoded.</param>
        /// <param name="key">Key that was used to encode the text.</param>
        /// <returns>Decoded text.</returns>
        public string Decode(string text, string key) => Cipher(text, key, caesarEncoder.Decode);

        private string Cipher(string text, string key, Func<string, int, string> symbolCipher)
        {
            key = AppendKey(key, text.Length);
            var encodedTextBuilder = new StringBuilder(text.Length);
            for (var i = 0; i < text.Length; i++)
            {
                if (!char.IsLetter(text[i]))
                {
                    _ = encodedTextBuilder.Append(text[i]);
                    continue;
                }

                var letterZ = char.IsUpper(key[i]) ? 'Z' : 'z';
                var encodedSymbol = symbolCipher(text[i].ToString(), letterZ - key[i]);
                _ = encodedTextBuilder.Append(encodedSymbol);
            }

            return encodedTextBuilder.ToString();
        }

        private string AppendKey(string key, int length)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentOutOfRangeException($"{nameof(key)} must be non-empty string");
            }

            var keyBuilder = new StringBuilder(key, length);
            while (keyBuilder.Length < length)
            {
                _ = keyBuilder.Append(key);
            }

            return keyBuilder.ToString();
        }
    }
}
