using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.DataCompression
{
    public class ShannonFano
    {
        private class FanoNode
        {
            public float Probability;
            public readonly int[] Arr = new int[20];
            public int Top;
        }

        /// <summary>
        /// The input text length.
        /// </summary>
        private int Len { get; set; }

        private int Pos { get; set; }

        public string Compress(string inputText)
        {
            var str = inputText.ToLowerInvariant().Replace(" ", "#");
            Len = str.Length;

            var textAsCharArray = str.ToCharArray();
            var initCharCountArray = GetCharFrequencies(textAsCharArray);

            var (fFreqArray, fCharArray) =
                FlagCharsByFrequency(initCharCountArray, textAsCharArray);

            SortArrays(fFreqArray, fCharArray);

            var pArr = GetProbabilities(fFreqArray);

            Shannon(0, Pos - 1, pArr);

            var code = GetShannonCode(pArr);

            var finalStr = GetShannonString(str, fCharArray, code);
            return finalStr;
        }

        /// <summary>
        /// Find the frequency(# of incidences) for each caracter on the text.
        /// </summary>
        /// <returns>Updateds frequency Array</returns>
        private int[] GetCharFrequencies(IReadOnlyList<char> text)
        {
            var temp = new int[Len];
            var count = 1;

            for (var i = 0; i < Len; i++)
            {
                for (var j = i + 1; j < Len; j++)
                {
                    if (text[i] == text[j])
                    {
                        count++;
                    }
                }

                temp[i] = count;
                count = 1;
            }

            return temp;
        }

        /// <summary>
        /// Segregated chars by frequency.
        /// </summary>
        /// <param name="countArray">initial count per char</param>
        /// <param name="textAsCharArray">text message as array of chars.</param>
        /// <returns>Filtered arrays</returns>
        private Tuple<int[], char[]> FlagCharsByFrequency(
            IReadOnlyList<int> countArray, IReadOnlyList<char> textAsCharArray)
        {
            var filteredFreqArray = new int[Len];
            var filteredCharArray = new char[Len];

            var flag = false;

            for (var i = 0; i < Len; i++)
            {
                for (var j = 0; j < Len; j++)
                {
                    if (textAsCharArray[i] == filteredCharArray[j])
                    {
                        flag = true;
                    }
                }

                if (!flag)
                {
                    filteredCharArray[Pos] = textAsCharArray[i];
                    filteredFreqArray[Pos] = countArray[i];
                    Pos++;
                }

                flag = false;
            }

            return new Tuple<int[], char[]>(filteredFreqArray, filteredCharArray);
        }

        /// <summary>
        /// Sorts arrays symbols based on  frequency.
        /// </summary>
        private void SortArrays(IList<int> fFreqArray, IList<char> fCharArray)
        {
            for (var i = 0; i < Pos; i++)
            {
                for (var j = i + 1; j < Pos; j++)
                {
                    if (fFreqArray[i] <= fFreqArray[j])
                    {
                        continue;
                    }

                    var temp = fFreqArray[i];
                    var ch = fCharArray[i];
                    fFreqArray[i] = fFreqArray[j];
                    fCharArray[i] = fCharArray[j];
                    fFreqArray[j] = temp;
                    fCharArray[j] = ch;
                }
            }
        }

        /// <summary>
        /// Given the frequency array of the input text
        /// gets the arithmetic mean for each character.
        /// </summary>
        /// <param name="fFreqArray">Char freq array.</param>
        /// <returns></returns>
        private List<FanoNode> GetProbabilities(IReadOnlyList<int> fFreqArray)
        {
            var f = new List<FanoNode>();

            for (var i = 0; i < Pos; i++)
            {
                var prob = fFreqArray[i] / (double)Len;
                f.Add(new FanoNode
                {
                    Probability = (float)prob,
                    Top = -1
                });
            }
            return f;
        }

        /// <summary>
        /// Joins the encoded string
        /// </summary>
        /// <param name="text">Text message</param>
        /// <param name="fCharArray">Frequency char array</param>
        /// <param name="code">encoding</param>
        /// <returns>Shannon String</returns>
        private static string GetShannonString(string text, char[] fCharArray, string[] code)
        {
            var sbl = new StringBuilder(" ");

            foreach (var item in text)
            {
                var index = Array.IndexOf(fCharArray, item);
                sbl.Append(code.ElementAt(index));
            }

            sbl.Replace(" ", "");

            return sbl.ToString();
        }

        /// <summary>
        /// Creates code.
        /// </summary>
        /// <param name="pArr">Probability array</param>
        /// <returns></returns>
        private string[] GetShannonCode(IReadOnlyList<FanoNode> pArr)
        {
            var code = new string[Pos];

            for (var i = Pos - 1; i >= 0; i--)
            {
                var sbl = new StringBuilder(" ");

                for (var j = 0; j <= pArr[i].Top; j++)
                {
                    sbl.Append(pArr[i].Arr[j]);
                }

                code[i] = sbl.ToString();
            }

            return code;
        }

        private static void UpdateTopValue(IReadOnlyList<FanoNode> f, int h, int tempIndex)
        {
            f[h].Arr[++f[h].Top] = 0;
            f[tempIndex].Arr[++f[tempIndex].Top] = 1;
        }

        private static void UpdateTopValue(IReadOnlyList<FanoNode> f, int h, int tempIndex, int k)
        {
            int i;
            for (i = tempIndex; i <= k; i++)
            {
                f[i].Arr[++f[i].Top] = 1;
            }

            for (i = k + 1; i <= h; i++)
            {
                f[i].Arr[++f[i].Top] = 0;
            }
        }

        private static float GetInitDifference(IReadOnlyList<FanoNode> f, int h)
        {
            var set1 = f.AsEnumerable().Take(h).Sum(x => x.Probability);
            var set2 = f[h].Probability;

            var diff1 = set1 - set2;
            if (diff1 < 0)
            {
                diff1 *= -1;
            }

            return diff1;
        }

        private static void Shannon(int l, int h, IReadOnlyList<FanoNode> f)
        {
            while (true)
            {
                var tempIndex = l;
                if (tempIndex == h || tempIndex > h)
                {
                    return;
                }

                if (tempIndex + 1 == h)
                {
                    UpdateTopValue(f, h, tempIndex);
                }
                else
                {
                    var diff1 = GetInitDifference(f, h);

                    var j = 2;
                    var k = 0;

                    while (j != h - tempIndex + 1)
                    {
                        k = h - j;
                        float set2 = 0;

                        var set1 = f.AsEnumerable().Take(k + 1).Sum(x => x.Probability);

                        int i;
                        for (i = h; i > k; i--)
                        {
                            set2 += f[i].Probability;
                        }

                        var diff2 = set1 - set2;
                        if (diff2 < 0)
                        {
                            diff2 *= -1;
                        }

                        if (diff2 >= diff1)
                        {
                            break;
                        }

                        diff1 = diff2;
                        j++;
                    }

                    k++;
                    UpdateTopValue(f, h, tempIndex, k);

                    Shannon(tempIndex, k, f);
                    l = k + 1;
                    continue;
                }

                break;
            }
        }
    }
}
