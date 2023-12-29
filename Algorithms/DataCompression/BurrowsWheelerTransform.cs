using System;
using System.Linq;

namespace Algorithms.DataCompression;

/// <summary>
///     The Burrows–Wheeler transform (BWT) rearranges a character string into runs of similar characters.
///     This is useful for compression, since it tends to be easy to compress a string that has runs of repeated
///     characters.
///     See <a href="https://en.wikipedia.org/wiki/Burrows%E2%80%93Wheeler_transform">here</a> for more info.
/// </summary>
public class BurrowsWheelerTransform
{
    /// <summary>
    ///     Encodes the input string using BWT and returns encoded string and the index of original string in the sorted
    ///     rotation matrix.
    /// </summary>
    /// <param name="s">Input string.</param>
    public (string encoded, int index) Encode(string s)
    {
        if (s.Length == 0)
        {
            return (string.Empty, 0);
        }

        var rotations = GetRotations(s);
        Array.Sort(rotations, StringComparer.Ordinal);
        var lastColumn = rotations
            .Select(x => x[^1])
            .ToArray();
        var encoded = new string(lastColumn);
        return (encoded, Array.IndexOf(rotations, s));
    }

    /// <summary>
    ///     Decodes the input string and returns original string.
    /// </summary>
    /// <param name="s">Encoded string.</param>
    /// <param name="index">Index  of original string in the sorted rotation matrix.</param>
    public string Decode(string s, int index)
    {
        if (s.Length == 0)
        {
            return string.Empty;
        }

        var rotations = new string[s.Length];

        for (var i = 0; i < s.Length; i++)
        {
            for (var j = 0; j < s.Length; j++)
            {
                rotations[j] = s[j] + rotations[j];
            }

            Array.Sort(rotations, StringComparer.Ordinal);
        }

        return rotations[index];
    }

    private string[] GetRotations(string s)
    {
        var result = new string[s.Length];

        for (var i = 0; i < s.Length; i++)
        {
            result[i] = s.Substring(i) + s.Substring(0, i);
        }

        return result;
    }
}
