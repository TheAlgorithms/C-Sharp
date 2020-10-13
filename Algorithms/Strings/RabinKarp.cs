using System;
using System.Collections.Generic;

namespace Algorithms.Strings
{
    /// <summary>
    /// The idea: You calculate the hash for the pattern <c>p</c> and the hash values for all the prefixes of the text <c>t</c>.
    /// Now, you can compare a substring in constant time using the calculated hashes.
    /// time complexity: O(p + t),
    /// space complexity: O(t),
    /// where   t - text length
    ///         p - pattern length.
    /// </summary>
    public static class RabinKarp
    {
        /// <summary>
        /// Finds the index of all occurrences of the pattern <c>p</c> int <c>t</c>.
        /// </summary>
        /// <param name="t">Input text.</param>
        /// <param name="p">Search pattern.</param>
        /// <returns>List of starting indices of the pattern in the text.</returns>
        public static List<int> FindAllOccurrences(string t, string p)
        {
            // Prime number
            const ulong P = 65537;

            // Modulo coefficient
            const ulong M = (ulong)1e9 + 7;

            // p_pow[i] = P^i mod M
            ulong[] p_pow = new ulong[Math.Max(p.Length, t.Length)];
            p_pow[0] = 1;
            for (int i = 1; i < p_pow.Length; i++)
            {
                p_pow[i] = (p_pow[i - 1] * P) % M;
            }

            // hash_t[i] is the sum of the previous hash values of the letters (t[0], t[1], ..., t[i-1]) and the hash value of t[i] itself (mod M).
            // The hash value of a letter t[i] is equal to the product of t[i] and p_pow[i] (mod M).
            ulong[] hash_t = new ulong[t.Length + 1];
            for (int i = 0; i < t.Length; i++)
            {
                hash_t[i + 1] = (hash_t[i] + (ulong)t[i] * p_pow[i]) % M;
            }

            // hash_s is equal to sum of the hash values of the pattern (mod M).
            ulong hash_s = 0;
            for (int i = 0; i < p.Length; i++)
            {
                hash_s = (hash_s + p[i] * p_pow[i]) % M;
            }

            // In the next step you iterate over the text with the pattern.
            List<int> occurences = new List<int>();
            for (int i = 0; i + p.Length - 1 < t.Length; i++)
            {
                // In each step you calculate the hash value of the substring to be tested.
                // By storing the hash values of the letters as a prefixes you can do this in constant time.
                ulong current_hash = (hash_t[i + p.Length] + M - hash_t[i]) % M;

                // Now you can compare the hash value of the substring with the product of the hash value of the pattern and p_pow[i].
                if (current_hash == hash_s * p_pow[i] % M)
                {
                    // If the hash values are identical, do a double-check in case a hash collision occurs.
                    int j = 0;
                    while (j < p.Length && t[i + j] == p[j])
                    {
                        ++j;
                    }

                    if (j == p.Length)
                    {
                        // If the hash values are identical and the double-check passes, a substring was found that matches the pattern.
                        // In this case you add the index i to the list of occurences.
                        occurences.Add(i);
                    }
                }
            }

            return occurences;
        }
    }
}
