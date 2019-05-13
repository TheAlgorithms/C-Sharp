using System;
using System.Text;

namespace Algorithms.Encoders
{
    /// <summary>
    /// Encodes using caesar cypher
    /// </summary>
    public class CaesarEncoder : IEncoder<int>
    {
        /// <summary>
        /// Encodes text using specified key
        /// </summary>
        /// <param name="text">Text to be encoded</param>
        /// <param name="key">Key that will be used to encode text</param>
        /// <returns>Encoded text</returns>
        public string Encode(string text, int key)
        {
            if (key < 1 || key > 25)
            {
                throw new ArgumentException($"{nameof(key)} must be between 1 and 25", nameof(key));
            }

            return Cipher(text, key);
        }

        /// <summary>
        /// Decodes text that was encoded using specified key
        /// </summary>
        /// <param name="text">Text to be decoded</param>
        /// <param name="key">Key that was used to encode text</param>
        /// <returns>Decoded text</returns>
        public string Decode(string text, int key)
        {
            if (key < 1 || key > 25)
            {
                throw new ArgumentException($"{nameof(key)} must be between 1 and 25", nameof(key));
            }

            return Cipher(text, -key);
        }

        private string Cipher(string text, int key)
        {
            var newText = new StringBuilder(text.Length);
            for (var i = 0; i < text.Length; i++)
            {
                if (!char.IsLetter(text[i]))
                {
                    newText.Append(text[i]);
                    continue;
                }

                var letterA = char.IsUpper(text[i]) ? 'A' : 'a';
                var letterZ = char.IsUpper(text[i]) ? 'Z' : 'z';

                var c = text[i] + key;
                c -= c > letterZ ? 26 : 0;
                c += c < letterA ? 26 : 0;

                newText.Append((char)c);
            }

            return newText.ToString();
        }
    }
}