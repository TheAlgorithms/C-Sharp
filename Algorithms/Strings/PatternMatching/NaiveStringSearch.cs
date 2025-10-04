// Implements the traditional naive string matching algorithm in C# for TheAlgorithms/C-Sharp.
namespace Algorithms.Strings.PatternMatching;

/// <summary>
///     Implements the traditional naive string matching algorithm in C#.
/// </summary>
public static class NaiveStringSearch
{
    /// <summary>
    ///     NaiveSearch(Content, Pattern) will return an array containing each index of Content in which Pattern appears.
    ///     Cost:  O(n*m).
    /// </summary>
    /// <param name="content">The text body across which to search for a given pattern.</param>
    /// <param name="pattern">The pattern against which to check the given text body.</param>
    /// <returns>Array containing each index of Content in which Pattern appears.</returns>
    public static int[] NaiveSearch(string content, string pattern)
    {
        var m = pattern.Length;
        var n = content.Length;
        List<int> indices = [];
        for (var e = 0; e <= n - m; e++)
        {
            int j;
            for (j = 0; j < m; j++)
            {
                if (content[e + j] != pattern[j])
                {
                    break;
                }
            }

            if (j == m)
            {
                indices.Add(e);
            }
        }

        return indices.ToArray();
    }
}
