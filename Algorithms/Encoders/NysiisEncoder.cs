using System.Globalization;
using System.Linq;
using System.Text;

namespace Algorithms.Encoders
{
    /// <summary>
    /// Class for NYSIIS encoding strings.
    /// </summary>
    public class NysiisEncoder
    {
        private static readonly char[] Vowels = { 'A', 'E', 'I', 'O', 'U' };

        /// <summary>
        /// Encodes a string using the NYSIIS Algorithm.
        /// </summary>
        /// <param name="text">The string to encode.</param>
        /// <returns>The NYSIIS encoded string (all uppercase).</returns>
        public string Encode(string text)
        {
            text = text.ToUpper(CultureInfo.CurrentCulture);
            text = StartReplace(text);
            text = EndReplace(text);

            var nysiis = new StringBuilder($"{text[0]}");
            for (var i = 1; i < text.Length; i++)
            {
                if (Vowels.Contains(text[i]))
                {
                    // Vowel: EV -> AF, otherwise ? -> A
                    text = (i < text.Length - 1 && text[i] == 'E' && text[i + 1] == 'V') ?
                        text.ReplaceAt(i, "AF", 2) :
                        text.ReplaceAt(i, "A", 1);
                }

                var replacements =
                    new[] { ('Q', 'G'), ('Z', 'S'), ('M', 'N'), };
                text = RepWith(text, i, replacements);

                if (text[i] == 'K')
                {
                    // KN -> NN, otherwise K -> C
                    text = text.ReplaceSingle(
                        i,
                        (i < text.Length - 1 && text[i + 1] == 'N') ? 'N' : 'C');
                }

                var stringReplacements = new[]
                {
                    ("SCH", "SSS"),
                    ("PH", "FF"),
                };
                text = RepArr(text, i, stringReplacements);

                // H[vowel] or [vowel]H -> text[i-1]
                if (text[i] == 'H')
                {
                    if (!Vowels.Contains(text[i - 1]))
                    {
                        text = text.ReplaceSingle(i, text[i - 1]);
                    }
                    else if (i < text.Length - 1 && !Vowels.Contains(text[i + 1]))
                    {
                        text = text.ReplaceSingle(i, text[i - 1]);
                    }
                    else
                    {
                        // do nothing
                    }
                }

                // [vowel]W -> [vowel]
                if (text[i] == 'W' && Vowels.Contains(text[i - 1]))
                {
                    text = text.ReplaceSingle(i, text[i - 1]);
                }

                if (nysiis[^1] != text[i] && text[i] != ' ' && char.IsUpper(text[i]))
                {
                    nysiis.Append(text[i]);
                }
            }

            // ends in S => remove
            if (nysiis[^1] == 'S')
            {
                nysiis.RemoveLast();
            }

            // ends in AY => replace with single Y
            if (nysiis[^1] == 'Y' && nysiis[nysiis.Length - 2] == 'A')
            {
                nysiis[nysiis.Length - 2] = 'Y';
                nysiis.RemoveLast();
            }

            // ends in A => remove
            if (nysiis[^1] == 'A')
            {
                nysiis.RemoveLast();
            }

            return nysiis.ToString();
        }

        private string RepWith(string start, int index, (char, char)[] opts)
        {
            for (var i = 0; i < opts.Length; i++)
            {
                if (start[index] == opts[i].Item1)
                {
                    return start.ReplaceSingle(index, opts[i].Item2);
                }
            }

            return start;
        }

        private string RepArr(string start, int index, (string, string)[] opts)
        {
            for (var i = 0; i < opts.Length; i++)
            {
                var check = opts[i].Item1;
                var repl = opts[i].Item2;
                if (start.Length >= index + check.Length && start.Substring(index, check.Length) == check)
                {
                    return start.ReplaceAt(index, repl, check.Length);
                }
            }

            return start;
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
                start = start.ReplaceAt(0, to, from.Length);
            }

            return start;
        }

        private string EndReplace(string end)
        {
            var checks = new (string from, string to)?[]
            {
                ("EE", "Yb"),
                ("IE", "Yb"),
                ("DT", "Db"),
                ("RT", "Db"),
                ("NT", "Db"),
                ("ND", "Db"),
            };
            var replacement = checks.FirstOrDefault(t => end.EndsWith(t!.Value.from));
            if (replacement is { })
            {
                var (from, to) = replacement!.Value;
                end = end.ReplaceAt(end.Length - from.Length, to, from.Length);
            }

            return end;
        }
    }
}
