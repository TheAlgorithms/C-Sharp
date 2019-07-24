using System.Collections.Generic;
using System.Text;

namespace Algorithms.DataCompression
{
    /// <summary>
    /// TODO.
    /// </summary>
    public class Translator
    {
        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="text">TODO. 2.</param>
        /// <param name="translationKeys">TODO. 3.</param>
        /// <returns>TODO. 4.</returns>
        public string Translate(string text, Dictionary<string, string> translationKeys)
        {
            var sb = new StringBuilder();

            var start = 0;
            for (var i = 0; i < text.Length; i++)
            {
                var key = text.Substring(start, i - start + 1);
                if (translationKeys.ContainsKey(key))
                {
                    _ = sb.Append(translationKeys[key]);
                    start = i + 1;
                }
            }

            return sb.ToString();
        }
    }
}
