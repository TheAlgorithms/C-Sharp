using System;
using System.Collections.Generic;

namespace Algorithms.Strings;

/// <summary>
/// Rabin-Karp algorithm for pattern matching in strings.
/// Uses rolling hash to find all occurrences of a pattern in O(n+m) average time.
/// </summary>
public static class RabinKarp
{
    private const int Prime = 101; // Prime number for hash calculation

    /// <summary>
    /// Finds all occurrences of a pattern in the given text using Rabin-Karp algorithm.
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="pattern">The pattern to search for.</param>
    /// <returns>List of starting indices where pattern is found.</returns>
    /// <exception cref="ArgumentNullException">Thrown when text or pattern is null.</exception>
    /// <exception cref="ArgumentException">Thrown when pattern is empty or longer than text.</exception>
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

        if (pattern.Length > text.Length)
        {
            return new List<int>();
        }

        var matches = new List<int>();
        int patternLength = pattern.Length;
        int textLength = text.Length;

        // Calculate hash value for pattern and first window of text
        long patternHash = CalculateHash(pattern, patternLength);
        long textHash = CalculateHash(text, patternLength);

        // Calculate the value of h = pow(256, patternLength-1) % Prime
        long h = 1;
        for (int i = 0; i < patternLength - 1; i++)
        {
            h = (h * 256) % Prime;
        }

        // Slide the pattern over text one by one
        for (int i = 0; i <= textLength - patternLength; i++)
        {
            // Check if hash values match and verify character by character
            if (patternHash == textHash && VerifyMatch(text, pattern, i))
            {
                matches.Add(i);
            }

            // Calculate hash for next window of text
            if (i < textLength - patternLength)
            {
                textHash = RecalculateHash(text, i, i + patternLength, textHash, h);
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
    /// Calculates hash value for a string.
    /// </summary>
    /// <param name="str">String to hash.</param>
    /// <param name="length">Length of string to hash.</param>
    /// <returns>Hash value.</returns>
    private static long CalculateHash(string str, int length)
    {
        long hash = 0;
        for (int i = 0; i < length; i++)
        {
            hash = (hash * 256 + str[i]) % Prime;
        }

        return hash;
    }

    /// <summary>
    /// Recalculates hash using rolling hash technique.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="oldIndex">Old starting index.</param>
    /// <param name="newIndex">New ending index.</param>
    /// <param name="oldHash">Previous hash value.</param>
    /// <param name="h">Precomputed value for hash calculation.</param>
    /// <returns>New hash value.</returns>
    private static long RecalculateHash(string text, int oldIndex, int newIndex, long oldHash, long h)
    {
        long newHash = oldHash - (text[oldIndex] * h);
        newHash = (newHash * 256 + text[newIndex]) % Prime;

        // Handle negative hash
        if (newHash < 0)
        {
            newHash += Prime;
        }

        return newHash;
    }

    /// <summary>
    /// Verifies if pattern matches text at given position.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="pattern">The pattern.</param>
    /// <param name="startIndex">Starting index in text.</param>
    /// <returns>True if pattern matches at position.</returns>
    private static bool VerifyMatch(string text, string pattern, int startIndex)
    {
        for (int i = 0; i < pattern.Length; i++)
        {
            if (text[startIndex + i] != pattern[i])
            {
                return false;
            }
        }

        return true;
    }
}
