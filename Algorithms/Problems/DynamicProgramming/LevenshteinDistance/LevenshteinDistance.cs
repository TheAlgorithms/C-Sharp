using System;

namespace Algorithms.Problems.DynamicProgramming;

/// <summary>
///     <para>
///         Levenshtein distance between two words is the minimum number of single-character edits (insertions, deletions or substitutions) required to change one word into the other.
///     </para>
///     <para>
///         Wikipedia: https://en.wikipedia.org/wiki/Levenshtein_distance.
///     </para>
/// </summary>
public static class LevenshteinDistance
{
    /// <summary>
    ///     Calculates Levenshtein distance.
    ///     Time and space complexity is O(ab) where a and b are the lengths of the source and target strings.
    /// </summary>
    /// <param name="source">Source string.</param>
    /// <param name="target">Target string.</param>
    /// <returns>Levenshtein distance between source and target strings.</returns>
    public static int Calculate(string source, string target)
    {
        var distances = new int[source.Length + 1, target.Length + 1];

        for(var i = 0; i <= source.Length; i++)
        {
            distances[i, 0] = i;
        }

        for (var i = 0; i <= target.Length; i++)
        {
            distances[0, i] = i;
        }

        for (var i = 1; i <= source.Length; i++)
        {
            for (var j = 1; j <= target.Length; j++)
            {
                var substitionCost = source[i - 1] == target[j - 1] ? 0 : 1;
                distances[i, j] = Math.Min(distances[i - 1, j] + 1, Math.Min(distances[i, j - 1] + 1, distances[i - 1, j - 1] + substitionCost));
            }
        }

        return distances[source.Length, target.Length];
    }
}
