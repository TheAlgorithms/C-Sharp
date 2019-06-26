using System.Collections.Generic;
using System.Text;

namespace Algorithms.Compressors
{
    public class Translator
    {
        public string Translate(string text, Dictionary<string, string> translationKeys)
        {
            var sb = new StringBuilder();

            var start = 0;
            for (var i = 0; i < text.Length; i++)
            {
                var key = text.Substring(start, i - start + 1);
                if (translationKeys.ContainsKey(key))
                {
                    sb.Append(translationKeys[key]);
                    start = i + 1;
                }
            }

            return sb.ToString();
        }
    }
}
