using System.Linq;

namespace Algorithms.Strings.Similarity;

/// <summary>
///     <para>
///         Jaro–Winkler distance is a string metric measuring an edit distance between two sequences.
///         The score is normalized such that 1 means an exact match and 0 means there is no similarity.
///         Time complexity is O(a*b) where a is the length of the first string and b is the length of the second string.
///     </para>
///     <para>
///         Wikipedia: https://en.wikipedia.org/wiki/Jaro%E2%80%93Winkler_distance.
///     </para>
/// </summary>
public static class JaroWinklerDistance
{
    /// <summary>
    ///     Calculates Jaro–Winkler distance.
    /// </summary>
    /// <param name="s1">First string.</param>
    /// <param name="s2">Second string.</param>
    /// <param name="scalingFactor">Scaling factor for how much the score is adjusted upwards for having common prefixes. Default is 0.1.</param>
    /// <returns>Distance between two strings.</returns>
    public static double Calculate(string s1, string s2, double scalingFactor = 0.1)
    {
        var jaroSimilarity = JaroSimilarity.Calculate(s1, s2);
        var commonPrefixLength = s1.Zip(s2).Take(4).TakeWhile(x => x.First == x.Second).Count();
        var jaroWinklerSimilarity = jaroSimilarity + commonPrefixLength * scalingFactor * (1 - jaroSimilarity);

        return 1 - jaroWinklerSimilarity;
    }
}
