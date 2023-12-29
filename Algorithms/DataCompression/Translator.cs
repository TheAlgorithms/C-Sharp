using System.Collections.Generic;
using System.Text;

namespace Algorithms.DataCompression;

/// <summary>
///     Provides method for text conversion by key mapping.
/// </summary>
public class Translator
{
    /// <summary>
    ///     Converts the input text according to the translation keys.
    /// </summary>
    /// <param name="text">Input text.</param>
    /// <param name="translationKeys">Translation keys used for text matching.</param>
    /// <returns>Converted text according to the translation keys.</returns>
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
