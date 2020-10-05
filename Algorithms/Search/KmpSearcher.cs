using System.Collections.Generic;
using Utilities.Exceptions;

namespace Algorithms.Search
{
    /// <summary>
    /// An implementation of Knuth–Morris–Pratt Algorithm.
    /// Worst case time complexity: O(n).
    /// </summary>
    public class KmpSearcher
    {
        /// <summary>
        /// Enumerates each zero-based index of all occurrences of the string in this instance.
        /// throws ItemNotFoundException if the item couldn't be found.
        /// </summary>
        /// <param name="str">The string instance.</param>
        /// <param name="pat">The pattern to seek.</param>
        /// <returns>
        /// The zero-based index positions of value if one or more <paramref name="pat"/> are found.
        /// If <paramref name="pat"/> is empty, no indexes will be enumerated.
        /// </returns>
        /// <exception cref="ItemNotFoundException"><paramref name="str"/> or <paramref name="pat"/> is null.</exception>
        public IEnumerable<int> FindIndexes(string str, string pat)
        {
            List<int> retVal = new List<int>();
            int m = pat.Length;
            int n = str.Length;
            int i = 0;
            int j = 0;
            int[] lps = new int[m];

            ComputeLPSArray(pat, m, lps);

            while (i < n)
            {
                if (pat[j] == str[i])
                {
                    j++;
                    i++;
                }

                if (j == m)
                {
                    retVal.Add(i - j);
                    j = lps[j - 1];
                }
                else if (i < n && pat[j] != str[i])
                {
                    if (j != 0)
                    {
                        j = lps[j - 1];
                    }
                    else
                    {
                        i = i + 1;
                    }
                }
            }

            if (retVal.Count == 0)
            {
                throw new ItemNotFoundException();
            }

            return retVal;
        }

        private void ComputeLPSArray(string pat, int m, int[] lps)
        {
            int len = 0;
            int i = 1;

            lps[0] = 0;

            while (i < m)
            {
                if (pat[i] == pat[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                    {
                        len = lps[len - 1];
                    }
                    else
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }
        }
    }
}
