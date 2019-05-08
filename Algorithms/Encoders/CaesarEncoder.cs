using System.Text;

namespace Algorithms.Encoders
{
    /// <summary>
    /// Encoder that uses caesar cypher
    /// </summary>
    public class CaesarEncoder : IEncoder<int>
    {
        public string Encode(string text, int key) => Cipher(text, key);

        public string Decode(string text, int key) => Cipher(text, -key);

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
                if (c > letterZ)
                {
                    c -= 26 * (1 + (c - letterZ) / 26);
                }
                if (c < letterA)
                {
                    c += 26 * (1 + (letterA - c) / 26);
                }

                newText.Append((char)c);
            }

            return newText.ToString();
        }
    }
}