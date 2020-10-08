using System;
using System.Linq;
using System.Collections.Generic;

namespace Algorithms.Encoders
{
    public class NYSIISEncoder
    {
        private static char[] Vowels = new char[] { 'A', 'E', 'I', 'O', 'U' };

        public NYSIISEncoder() {}

        public string Encode(string text)
        {
            text = text.ToUpper();
            text = StartReplace(text, new (string, string)[] {
                ("MAC", "MCC"), ("KN", "NN"), ("K", "C"),
                ("PH", "FF"), ("PF", "FF"), ("SCH", "SSS")
            });
            text = EndReplace(text, new (string, string)[] {
                ("EE", "Yb"), ("IE", "Yb"),
                ("DT", "Db"), ("RT", "Db"), ("NT", "Db"),
                ("ND", "Db")
            });

            string nysiis = $"{text[0]}";
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
                }

                //[vowel]W -> [vowel]
                if(text[i] == 'W' && Vowels.Contains(text[i - 1]))
                    text = text.ReplaceSingle(i, text[i - 1]);

                if(nysiis.Last() != text[i] && text[i] != ' ' && char.IsUpper(text[i]))
                    nysiis += text[i];
            }

            //ends in S => remove
            if(nysiis.Last() == 'S') nysiis = nysiis.Substring(0, nysiis.Length - 1);
            //ends in AY => replace with single Y
            if(nysiis.Last() == 'Y' && nysiis[nysiis.Length - 2] == 'A')
            {
                nysiis = nysiis.ReplaceAt(nysiis.Length - 2, "Y", 2);
            }
            //ends in A => remove
            if(nysiis.Last() == 'A') nysiis = nysiis.Substring(0, nysiis.Length - 1);

            return nysiis;
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

    public static class NYSIISExtensions
    {
        public static string ReplaceAt(this string torep, int index, string repl, int len) =>
            $"{torep.Substring(0,index)}{repl}{(index + len < torep.Length ? torep.Substring(index + len) : "")}";

        public static string ReplaceSingle(this string torep, int index, char rep) =>
            torep.ReplaceAt(index, $"{rep}", 1);

        public static void Enumerate<T>(this T[] arr, Action<T, int> consumer)
        {
            for(int i = 0; i < arr.Length; i++) consumer(arr[i], i);
        }

        public static void ForEach<T>(this IEnumerable<T> en, Action<T> consumer)
        {
            foreach(T t in en) consumer(t);
        }

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
