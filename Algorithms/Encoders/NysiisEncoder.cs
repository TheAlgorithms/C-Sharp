using System.Globalization;
using System.Linq;
using System.Text;

namespace Algorithms.Encoders;

/// <summary>
///     Class for NYSIIS encoding strings.
/// </summary>
public class NysiisEncoder
{
    private static readonly char[] Vowels = { 'A', 'E', 'I', 'O', 'U' };

    /// <summary>
    ///     Encodes a string using the NYSIIS Algorithm.
    /// </summary>
    /// <param name="text">The string to encode.</param>
    /// <returns>The NYSIIS encoded string (all uppercase).</returns>
    public string Encode(string text)
    {
        text = text.ToUpper(CultureInfo.CurrentCulture);
        text = TrimSpaces(text);
        text = StartReplace(text);
        text = EndReplace(text);

        for (var i = 1; i < text.Length; i++)
        {
            text = ReplaceStep(text, i);
        }

        text = RemoveDuplicates(text);
        return TrimEnd(text);
    }

    private string TrimSpaces(string text) => text.Replace(" ", string.Empty);

    private string RemoveDuplicates(string text)
    {
        var sb = new StringBuilder();
        sb.Append(text[0]);
        foreach (var c in text)
        {
            if (sb[^1] != c)
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }

    private string TrimEnd(string text)
    {
        var checks = new (string from, string to)?[]
        {
            ("S", string.Empty),
            ("AY", "Y"),
            ("A", string.Empty),
        };
        var replacement = checks.FirstOrDefault(t => text.EndsWith(t!.Value.from));
        if (replacement is { })
        {
            var (from, to) = replacement!.Value;
            text = Replace(text, text.Length - from.Length, from.Length, to);
        }

        return text;
    }

    private string ReplaceStep(string text, int i)
    {
        (string from, string to)[] replacements =
        {
            ("EV", "AF"),
            ("E", "A"),
            ("I", "A"),
            ("O", "A"),
            ("U", "A"),
            ("Q", "G"),
            ("Z", "S"),
            ("M", "N"),
            ("KN", "NN"),
            ("K", "C"),
            ("SCH", "SSS"),
            ("PH", "FF"),
        };
        var replaced = TryReplace(text, i, replacements, out text);
        if (replaced)
        {
            return text;
        }

        // H[vowel] or [vowel]H -> text[i-1]
        if (text[i] == 'H')
        {
            if (!Vowels.Contains(text[i - 1]))
            {
                return ReplaceWithPrevious();
            }

            if (i < text.Length - 1 && !Vowels.Contains(text[i + 1]))
            {
                return ReplaceWithPrevious();
            }
        }

        // [vowel]W -> [vowel]
        if (text[i] == 'W' && Vowels.Contains(text[i - 1]))
        {
            return ReplaceWithPrevious();
        }

        return text;

        string ReplaceWithPrevious() => Replace(text, i, 1, text[i - 1].ToString());
    }

    private bool TryReplace(string text, int index, (string, string)[] opts, out string result)
    {
        for (var i = 0; i < opts.Length; i++)
        {
            var check = opts[i].Item1;
            var repl = opts[i].Item2;
            if (text.Length >= index + check.Length && text.Substring(index, check.Length) == check)
            {
                result = Replace(text, index, check.Length, repl);
                return true;
            }
        }

        result = text;
        return false;
    }

    private string StartReplace(string start)
    {
        var checks = new (string from, string to)?[]
        {
            ("MAC", "MCC"),
            ("KN", "NN"),
            ("K", "C"),
            ("PH", "FF"),
            ("PF", "FF"),
            ("SCH", "SSS"),
        };
        var replacement = checks.FirstOrDefault(t => start.StartsWith(t!.Value.from));
        if (replacement is { })
        {
            var (from, to) = replacement!.Value;
            start = Replace(start, 0, from.Length, to);
        }

        return start;
    }

    private string EndReplace(string end)
    {
        var checks = new (string from, string to)?[]
        {
            ("EE", "Y"),
            ("IE", "Y"),
            ("DT", "D"),
            ("RT", "D"),
            ("NT", "D"),
            ("ND", "D"),
        };
        var replacement = checks.FirstOrDefault(t => end.EndsWith(t!.Value.from));
        if (replacement is { })
        {
            var (from, to) = replacement!.Value;
            end = Replace(end, end.Length - from.Length, from.Length, to);
        }

        return end;
    }

    private string Replace(string text, int index, int length, string substitute) =>
        text[..index] + substitute + text[(index + length) ..];
}
