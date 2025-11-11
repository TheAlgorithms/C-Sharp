using System;
using System.Collections.Generic;

namespace Algorithms.Strings;

/// <summary>
/// Knuth-Morris-Pratt (KMP) algorithm for pattern matching in strings.
/// Uses a failure function to avoid unnecessary comparisons, achieving O(n+m) time complexity.
/// </summary>
public static class KnuthMorrisPratt
{
    /// <summary>
    /// Finds all occurrences of a pattern in the given text using KMP algorithm.
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="pattern">The pattern to search for.</param>
    /// <returns>List of starting indices where pattern is found.</returns>
    /// <exception cref="ArgumentNullException">Thrown when text or pattern is null.</exception>
    /// <exception cref="ArgumentException">Thrown when pattern is empty.</exception>
    public static List<int> FindAllOccurrences(string text, string pattern)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text), "Text cannot be null.");
        }

        if (pattern == null)
        {
            throw new ArgumentNullException(nameof(pattern), "Pattern cannot be null.");
        }

        if (pattern.Length == 0)
        {
            throw new ArgumentException("Pattern cannot be empty.", nameof(pattern));
        }

        var matches = new List<int>();

        if (pattern.Length > text.Length)
        {
            return matches;
        }

        // Build the failure function (LPS array)
        int[] lps = BuildFailureFunction(pattern);

        int textIndex = 0;
        int patternIndex = 0;

        while (textIndex < text.Length)
        {
            if (text[textIndex] == pattern[patternIndex])
            {
                textIndex++;
                patternIndex++;

                // Pattern found
                if (patternIndex == pattern.Length)
                {
                    matches.Add(textIndex - patternIndex);
                    patternIndex = lps[patternIndex - 1];
                }
            }
            else
            {
                if (patternIndex != 0)
                {
                    patternIndex = lps[patternIndex - 1];
                }
                else
                {
                    textIndex++;
                }
            }
        }

        return matches;
    }

    /// <summary>
    /// Finds the first occurrence of a pattern in the text.
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="pattern">The pattern to search for.</param>
    /// <returns>Index of first occurrence, or -1 if not found.</returns>
    public static int FindFirst(string text, string pattern)
    {
        var matches = FindAllOccurrences(text, pattern);
        return matches.Count > 0 ? matches[0] : -1;
    }

    /// <summary>
    /// Checks if a pattern exists in the text.
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="pattern">The pattern to search for.</param>
    /// <returns>True if pattern is found, false otherwise.</returns>
    public static bool Contains(string text, string pattern)
    {
        return FindFirst(text, pattern) != -1;
    }

    /// <summary>
    /// Counts the number of occurrences of a pattern in the text.
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="pattern">The pattern to search for.</param>
    /// <returns>Number of occurrences.</returns>
    public static int CountOccurrences(string text, string pattern)
    {
        return FindAllOccurrences(text, pattern).Count;
    }

    /// <summary>
    /// Builds the Longest Proper Prefix which is also Suffix (LPS) array.
    /// This is the failure function used by KMP algorithm.
    /// </summary>
    /// <param name="pattern">The pattern to build LPS array for.</param>
    /// <returns>LPS array.</returns>
    public static int[] BuildFailureFunction(string pattern)
    {
        int[] lps = new int[pattern.Length];
        int length = 0;
        int i = 1;

        lps[0] = 0; // First element is always 0

        while (i < pattern.Length)
        {
            if (pattern[i] == pattern[length])
            {
                length++;
                lps[i] = length;
                i++;
            }
            else
            {
                if (length != 0)
                {
                    length = lps[length - 1];
                }
                else
                {
                    lps[i] = 0;
                    i++;
                }
            }
        }

        return lps;
    }

    /// <summary>
    /// Finds all occurrences of a pattern and returns their end indices.
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="pattern">The pattern to search for.</param>
    /// <returns>List of ending indices (exclusive) where pattern is found.</returns>
    public static List<int> FindAllEndIndices(string text, string pattern)
    {
        var startIndices = FindAllOccurrences(text, pattern);
        var endIndices = new List<int>();

        foreach (var start in startIndices)
        {
            endIndices.Add(start + pattern.Length);
        }

        return endIndices;
    }

    /// <summary>
    /// Checks if text starts with the given pattern.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <param name="pattern">The pattern to look for.</param>
    /// <returns>True if text starts with pattern.</returns>
    public static bool StartsWith(string text, string pattern)
    {
        var firstIndex = FindFirst(text, pattern);
        return firstIndex == 0;
    }

    /// <summary>
    /// Checks if text ends with the given pattern.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <param name="pattern">The pattern to look for.</param>
    /// <returns>True if text ends with pattern.</returns>
    public static bool EndsWith(string text, string pattern)
    {
        if (text == null || pattern == null || pattern.Length > text.Length)
        {
            return false;
        }

        var matches = FindAllOccurrences(text, pattern);
        return matches.Count > 0 && matches[matches.Count - 1] == text.Length - pattern.Length;
    }
}
