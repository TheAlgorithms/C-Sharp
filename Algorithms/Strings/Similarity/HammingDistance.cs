using System;

namespace Algorithms.Strings.Similarity;

/// <summary>
///     <para>
///         Hamming distance between two strings of equal length is the number of positions at which the corresponding symbols are different.
///         Time complexity is O(n) where n is the length of the string.
///     </para>
///     <para>
///         Wikipedia: https://en.wikipedia.org/wiki/Hamming_distance.
///     </para>
/// </summary>
public static class HammingDistance
{
    /// <summary>
    ///     Calculates Hamming distance between two strings of equal length.
    /// </summary>
    /// <param name="s1">First string.</param>
    /// <param name="s2">Second string.</param>
    /// <returns>Levenshtein distance between source and target strings.</returns>
    public static int Calculate(string s1, string s2)
    {
        if(s1.Length != s2.Length)
        {
            throw new ArgumentException("Strings must be equal length.");
        }

        var distance = 0;
        for (var i = 0; i < s1.Length; i++)
        {
            distance += s1[i] != s2[i] ? 1 : 0;
        }

        return distance;
    }
}
