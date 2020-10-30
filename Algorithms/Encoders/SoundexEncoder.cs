using System.Globalization;
using System.Linq;
using System.Text;

namespace Algorithms.Encoders
{
    /// <summary>
    /// Class for Soundex encoding strings.
    /// </summary>
    public class SoundexEncoder
    {
        /// <summary>
        /// Encodes a string using the Soundex Algorithm.
        /// </summary>
        /// <param name="text">The string to encode.</param>
        /// <returns>The Soundex encoded string (one uppercase character and three digits).</returns>
        public string Encode(string text)
        {
            StringBuilder soundex = new StringBuilder($"{text.ToUpper(CultureInfo.CurrentCulture)[0]}");
            text = new string(text.Select(ch =>
            {
                switch (char.ToLower(ch))
                {
                    case 'a':
                    case 'e':
                    case 'i':
                    case 'o':
                    case 'u':
                    case 'y':
                        return '0';

                    case 'h':
                    case 'w':
                        return '8';

                    case 'b':
                    case 'f':
                    case 'p':
                    case 'v':
                        return '1';

                    case 'c':
                    case 'g':
                    case 'j':
                    case 'k':
                    case 'q':
                    case 's':
                    case 'x':
                    case 'z':
                        return '2';

                    case 'd':
                    case 't':
                        return '3';

                    case 'l': return '4';

                    case 'm':
                    case 'n':
                        return '5';

                    case 'r': return '6';

                    default: return '0';
                }
            }).ToArray());

            // remove doubles, retain up to 4 chars (first letter + 3 digits)
            for (var i = 1; i < text.Length; i++)
            {
                if (text[i] != text[i - 1] && text[i] != '0' && text[i] != '8' &&
                    !(i > 1 && text[i - 1] == '8' && text[i] == text[i - 2]))
                {
                    soundex.Append(text[i]);
                }

                if (soundex.Length == 4)
                {
                    break;
                }
            }

            // append to 3 digits if shorter
            while (soundex.Length < 4)
            {
                soundex.Append('0');
            }

            return soundex.ToString();
        }
    }
}
