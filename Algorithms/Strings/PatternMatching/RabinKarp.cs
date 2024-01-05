using System;
using System.Collections.Generic;

namespace Algorithms.Strings.PatternMatching;

/// <summary>
///     The idea: You calculate the hash for the pattern <c>p</c> and the hash values for all the prefixes of the text
///     <c>t</c>.
///     Now, you can compare a substring in constant time using the calculated hashes.
///     time complexity: O(p + t),
///     space complexity: O(t),
///     where   t - text length
///     p - pattern length.
/// </summary>
public static class RabinKarp
{
    /// <summary>
    ///     Finds the index of all occurrences of the pattern <c>p</c> int <c>t</c>.
    /// </summary>
    /// <returns>List of starting indices of the pattern in the text.</returns>
    public static List<int> FindAllOccurrences(string text, string pattern)
    {
        // Prime number
        const ulong p = 65537;

        // Modulo coefficient
        const ulong m = (ulong)1e9 + 7;

        // p_pow[i] = P^i mod M
        ulong[] pPow = new ulong[Math.Max(pattern.Length, text.Length)];
        pPow[0] = 1;
        for (var i = 1; i < pPow.Length; i++)
        {
            pPow[i] = pPow[i - 1] * p % m;
        }

        // hash_t[i] is the sum of the previous hash values of the letters (t[0], t[1], ..., t[i-1]) and the hash value of t[i] itself (mod M).
        // The hash value of a letter t[i] is equal to the product of t[i] and p_pow[i] (mod M).
        ulong[] hashT = new ulong[text.Length + 1];
        for (var i = 0; i < text.Length; i++)
        {
            hashT[i + 1] = (hashT[i] + text[i] * pPow[i]) % m;
        }

        // hash_s is equal to sum of the hash values of the pattern (mod M).
        ulong hashS = 0;
        for (var i = 0; i < pattern.Length; i++)
        {
            hashS = (hashS + pattern[i] * pPow[i]) % m;
        }

        // In the next step you iterate over the text with the pattern.
        List<int> occurrences = new();
        for (var i = 0; i + pattern.Length - 1 < text.Length; i++)
        {
            // In each step you calculate the hash value of the substring to be tested.
            // By storing the hash values of the letters as a prefixes you can do this in constant time.
            var currentHash = (hashT[i + pattern.Length] + m - hashT[i]) % m;

            // Now you can compare the hash value of the substring with the product of the hash value of the pattern and p_pow[i].
            if (currentHash == hashS * pPow[i] % m)
            {
                // If the hash values are identical, do a double-check in case a hash collision occurs.
                var j = 0;
                while (j < pattern.Length && text[i + j] == pattern[j])
                {
                    ++j;
                }

                if (j == pattern.Length)
                {
                    // If the hash values are identical and the double-check passes, a substring was found that matches the pattern.
                    // In this case you add the index i to the list of occurences.
                    occurrences.Add(i);
                }
            }
        }

        return occurrences;
    }
}
