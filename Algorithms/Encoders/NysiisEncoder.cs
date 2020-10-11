using System;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace Algorithms.Encoders
{
    /// <summary>
    /// Class for NYSIIS encoding strings.
    /// </summary>
    public class NysiisEncoder
    {
        private static char[] Vowels = { 'A', 'E', 'I', 'O', 'U' };

        /// <summary>
        /// Encodes a string using the NYSIIS Algorithm
        /// </summary>
        /// <param name="text">The string to encode</param>
        /// <returns>The NYSIIS encoded string (all uppercase)</returns>
        public string Encode(string text)
        {
            text = text.ToUpper(CultureInfo.CurrentCulture);
            text = StartReplace(text, new (string, string)[] {
                ("MAC", "MCC"), ("KN", "NN"), ("K", "C"),
                ("PH", "FF"), ("PF", "FF"), ("SCH", "SSS")
            });
            text = EndReplace(text, new (string, string)[] {
                ("EE", "Yb"), ("IE", "Yb"),
                ("DT", "Db"), ("RT", "Db"), ("NT", "Db"),
                ("ND", "Db")
            });

            StringBuilder nysiis = new StringBuilder($"{text[0]}");
            for(int i = 1; i < text.Length; i++)
            {
                if(Vowels.Contains(text[i]))
                {
                    //Vowel: EV -> AF, otherwise ? -> A
                    text = (i < text.Length - 1 && text[i] == 'E' && text[i + 1] == 'V') ?
                        text.ReplaceAt(i , "AF", 2) :
                        text.ReplaceAt(i, "A", 1);
                }
                text = RepWith(text, i, new (char, char)[] {
                    ('Q', 'G'), ('Z', 'S'), ('M', 'N')
                });

                if(text[i] == 'K')
                {
                    //KN -> NN, otherwise K -> C
                    text = text.ReplaceSingle(i,
                        (i < text.Length - 1 && text[i + 1] == 'N') ? 'N' : 'C');
                }

                text = RepArr(text, i, new (string, string)[] {
                    ("SCH", "SSS"), ("PH", "FF")
                });

                //H[vowel] or [vowel]H -> text[i-1]
                if(text[i] == 'H')
                {
                    if(Vowels.Contains(text[i - 1]))
                        text = text.ReplaceSingle(i, text[i - 1]);
                    else if(i < text.Length - 1 && Vowels.Contains(text[i + 1]))
                        text = text.ReplaceSingle(i, text[i - 1]);
                    else {/*do nothing, codacy complained...*/}
                }

                //[vowel]W -> [vowel]
                if(text[i] == 'W' && Vowels.Contains(text[i - 1]))
                    text = text.ReplaceSingle(i, text[i - 1]);

                if(nysiis.Last() != text[i] && text[i] != ' ' && char.IsUpper(text[i]))
                    nysiis.Append(text[i]);
            }

            //ends in S => remove
            if(nysiis.Last() == 'S') nysiis.RemoveLast();
            //ends in AY => replace with single Y
            if(nysiis.Last() == 'Y' && nysiis[nysiis.Length - 2] == 'A')
            {
                nysiis[nysiis.Length - 2] = 'Y';
                nysiis.RemoveLast();
            }
            //ends in A => remove
            if(nysiis.Last() == 'A')
                nysiis.RemoveLast();

            return nysiis.ToString();
        }

        private string RepWith(string start, int index, (char, char)[] opts)
        {
            for(int i = 0; i < opts.Length; i++)
            {
                if(start[index] == opts[i].Item1) return start.ReplaceSingle(index, opts[i].Item2);
            }
            return start;
        }

        private string RepArr(string start, int index, (string, string)[] opts)
        {
            for(int i = 0; i < opts.Length; i++)
            {
                string check = opts[i].Item1;
                string repl = opts[i].Item2;
                if(start.Length >= index + check.Length && start.Substring(index, check.Length) == check)
                    return start.ReplaceAt(index, repl, check.Length);
            }
            return start;
        }

        private string StartReplace(string start, (string, string)[] checks)
        {
            checks.ForEach(tup => {
                string pref = tup.Item1;
                string rep = tup.Item2;
                if(start.StartsWith(pref))
                    start = start.ReplaceAt(0, rep, pref.Length);
            });
            return start;
        }

        private string EndReplace(string end, (string, string)[] checks)
        {
            checks.ForEach(tup => {
                string suff = tup.Item1;
                string rep = tup.Item2;
                if(end.EndsWith(suff))
                    end = end.ReplaceAt(end.Length - suff.Length, rep, suff.Length);
            });
            return end;
        }
    }

    /// <summary>
    /// Static class containing Extension Methods used by the NYSIIS algorithm.
    /// </summary>
    public static class NysiisExtensions
    {
        /// <summary>
        /// Gets the last character of a StringBuilder.
        /// </summary>
        /// <param name="b">The StringBuilder to query</param>
        /// <returns>The last character in the given StringBuilder</returns>
        public static char Last(this StringBuilder b) =>
            b[b.Length - 1];

            /// <summary>
            /// Removes the last character of a StringBuilder.
            /// </summary>
            /// <param name="b">The StringBuilder to remove from</param>
        public static void RemoveLast(this StringBuilder b) =>
            b.Remove(b.Length - 1, 1);

        /// <summary>
        /// Replaces a number of characters starting at a given index with another set of characters.
        /// </summary>
        /// <param name="torep">The string in which to replace</param>
        /// <param name="index">The index where the substring to replace starts</param>
        /// <param name="repl">The characters with which to replace the substring</param>
        /// <param name="len">The length of the substring to replace</param>
        /// <returns>The given string where the given substring was replaced by the given characters</returns>
        public static string ReplaceAt(this string torep, int index, string repl, int len) =>
            $"{torep.Substring(0,index)}{repl}{(index + len < torep.Length ? torep.Substring(index + len) : "")}";

        /// <summary>
        /// Replaces one character at a given index with another character.
        /// </summary>
        /// <param name="torep">The string in which to replace</param>
        /// <param name="index">The index of the character to replace</param>
        /// <param name="rep">The character with which to replace</param>
        /// <returns>The given string where the given character was replaced</returns>
        public static string ReplaceSingle(this string torep, int index, char rep) =>
            torep.ReplaceAt(index, $"{rep}", 1);

        /// <summary>
        /// Enumerates over an array using indices and values.
        /// </summary>
        /// <param name="arr">The array over which to enumerate</param>
        /// <param name="consumer">The action to execute for each pair (element, index).</param>
        public static void Enumerate<T>(this T[] arr, Action<T, int> consumer)
        {
            for(int i = 0; i < arr.Length; i++) consumer(arr[i], i);
        }

        /// <summary>
        /// Enumerates over an IEnumerable.
        /// </summary>
        /// <param name="en">The IEnumerable over which to enumerate</param>
        /// <param name="consumer">The action to execute for each element.</param>
        public static void ForEach<T>(this IEnumerable<T> en, Action<T> consumer)
        {
            foreach(T t in en) consumer(t);
        }

        /// <summary>
        /// Checks whether a given array contains a given value.
        /// </summary>
        /// <param name="arr">The array to check</param>
        /// <param name="eq">The value to check against</param>
        /// <returns>True if the array contains the value, otherwise false</returns>
        public static bool Contains<T>(this T[] arr, T eq)
        {
            foreach(T t in arr)
            {
                if(t.Equals(eq)) return true;
            }
            return false;
        }
    }
}
