using System;

namespace Algorithms.Strings
{
    /// <summary>
    /// The idea:   You compare the pattern with the text from right to left.
    ///             If the text symbol that is compared with the rightmost pattern symbol
    ///             does not occur in the pattern at all, then the pattern can be shifted
    ///             by m positions behind this text symbol.
    /// Complexity:
    ///     Time:   Preprocessing: O(m²)
    ///             Comparison: O(mn)
    ///     Space:  O(m + a)
    ///     where   m - pattern length
    ///             n - text length
    ///             a - alphabet length.
    /// Source:     https://www.inf.hs-flensburg.de/lang/algorithmen/pattern/bmen.htm
    ///             https://en.wikipedia.org/wiki/Boyer%E2%80%93Moore_string-search_algorithm.
    /// </summary>
    public static class BoyerMoore
    {
        /// <summary>
        /// Finds the index of the first occurrence of the pattern <c>p</c> in <c>t</c>.
        /// </summary>
        /// <param name="t">Input text.</param>
        /// <param name="p">Search pattern.</param>
        /// <returns>Index of the pattern in text or -1 if the pattern  was not found.</returns>
        public static int FindFirstOccurrence(string t, string p)
        {
            // Pattern length
            var m = p.Length;

            // Text length
            var n = t.Length;

            // For each symbol of the alphabet, the position of its rightmost occurrence in the pattern,
            // or -1 if the symbol does not occur in the pattern.
            int[] badChar = BadCharacterRule(p, m);

            // Each entry goodSuffix[i] contains the shift distance of the pattern
            // if a mismatch at position i – 1 occurs, i.e. if the suffix of the pattern starting at position i has matched.
            int[] goodSuffix = GoodSuffixRule(p, m);

            // Index in text
            var i = 0;

            // Index in pattern
            int j;

            while (i <= n - m)
            {
                // Starting at end of pattern
                j = m - 1;

                // While matching
                while (j >= 0 && p[j] == t[i + j])
                {
                    j--;
                }

                // Pattern found
                if (j < 0)
                {
                    return i;
                }

                // Pattern is shifted by the maximum of the values
                // given by the good-suffix and the bad-character heuristics
                i += Math.Max(goodSuffix[j + 1], j - badChar[t[i + j]]);
            }

            // Pattern not found
            return -1;
        }

        /// <summary>
        /// Finds out the position of its rightmost occurrence in the pattern for each symbol of the alphabet,
        /// or -1 if the symbol does not occur in the pattern.
        /// </summary>
        /// <param name="p">Search pattern.</param>
        /// <param name="m">Length of the pattern.</param>
        /// <returns>Array of the named postition for each symbol of the alphabet.</returns>
        private static int[] BadCharacterRule(string p, int m)
        {
            // For each character (note that there are more than 256 characters)
            int[] badChar = new int[256];
            Array.Fill<int>(badChar, -1);

            // Iterate from left to right over the pattern
            for (var j = 0; j < m; j++)
            {
                badChar[p[j]] = j;
            }

            return badChar;
        }

        /// <summary>
        /// Finds out the shift distance of the pattern if a mismatch at position i – 1 occurs
        /// for each character of the pattern, i.e. if the suffix of the pattern starting at position i has matched.
        /// </summary>
        /// <param name="p">Search pattern.</param>
        /// <param name="m">Length of the pattern.</param>
        /// <returns>Array of the named shift distance for each character of the pattern.</returns>
        private static int[] GoodSuffixRule(string p, int m)
        {
            // CASE 1
            // The matching suffix occurs somewhere else in the pattern
            // --> matching suffix is a border of a suffix of the pattern

            // f[i] contains starting position of the widest border of the suffix of the pattern beginning at position i
            int[] f = new int[p.Length + 1];

            // Suffix of p[m] has no border --> f[m] = m+1
            f[m] = m + 1;

            // Corresponding shift distance
            int[] s = new int[p.Length + 1];

            // Start of suffix including border of the pattern
            // (hint: https://www.inf.hs-flensburg.de/lang/algorithmen/pattern/kmpen.htm#section2)
            var i = m;

            // Start of suffix of the pattern
            var j = m + 1;

            while (i > 0)
            {
                // checking if a shorter border that is already known can be extended to the left by the same symbol
                while (j <= m && p[i - 1] != p[j - 1])
                {
                    if (s[j] == 0)
                    {
                        s[j] = j - i;
                    }

                    j = f[j];
                }

                --i;
                --j;
                f[i] = j;
            }

            // CASE 2
            // Only a part of the matching suffix occurs at the beginning of the pattern
            // (filling remaining entries)
            j = f[0];
            for (i = 0; i <= m; i++)
            {
                // Starting postition of the greates border
                if (s[i] == 0)
                {
                    s[i] = j;
                }

                // From position i = j, it switches to the next narrower border f[j]
                if (i == j)
                {
                    j = f[j];
                }
            }

            return s;
        }
    }
}
