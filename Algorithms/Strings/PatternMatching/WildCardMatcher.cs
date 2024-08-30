using System;

namespace Algorithms.Strings.PatternMatching;

/// <summary>
///     Implentation of regular expression matching with support for '.' and '*'.
///     '.' Matches any single character.
///     '*' Matches zero or more of the preceding element.
///     The matching should cover the entire input string (not partial).
/// </summary>
public static class WildCardMatcher
{
    /// <summary>
    ///    Using bottom-up dynamic programming for matching the input string with the pattern.
    ///
    ///    Time complexity: O(n*m), where n is the length of the input string and m is the length of the pattern.
    ///
    ///    Constrain: The pattern cannot start with '*'.
    /// </summary>
    /// <param name="inputString">The input string to match.</param>
    /// <param name="pattern">The pattern to match.</param>
    /// <returns>True if the input string matches the pattern, false otherwise.</returns>
    /// <exception cref="ArgumentException">Thrown when the pattern starts with '*'.</exception>
    public static bool MatchPattern(string inputString, string pattern)
    {
        if (pattern.Length > 0 && pattern[0] == '*')
        {
            throw new ArgumentException("Pattern cannot start with *");
        }

        var inputLength = inputString.Length + 1;
        var patternLength = pattern.Length + 1;

        // DP 2d matrix, where dp[i, j] is true if the first i characters in the input string match the first j characters in the pattern
        // This DP is initialized to all falses, as it is the default value for a boolean.
        var dp = new bool[inputLength, patternLength];

        // Empty string and empty pattern are a match
        dp[0, 0] = true;

        // Since the empty string can only match a pattern that has a * in it, we need to initialize the first row of the DP matrix
        for (var j = 1; j < patternLength; j++)
        {
            if (pattern[j - 1] == '*')
            {
                dp[0, j] = dp[0, j - 2];
            }
        }

        // Now using bottom-up approach to find for all remaining lenghts of input and pattern
        for (var i = 1; i < inputLength; i++)
        {
            for (var j = 1; j < patternLength; j++)
            {
                // If the characters match or the pattern has a ., then the result is the same as the previous positions.
                if (inputString[i - 1] == pattern[j - 1] || pattern[j - 1] == '.')
                {
                    dp[i, j] = dp[i - 1, j - 1];
                }
                else if (pattern[j - 1] == '*')
                {
                    if (dp[i, j - 2])
                    {
                        dp[i, j] = true;
                    }
                    else if (inputString[i - 1] == pattern[j - 2] || pattern[j - 2] == '.')
                    {
                        dp[i, j] = dp[i - 1, j];
                    }
                }
            }
        }

        return dp[inputLength - 1, patternLength - 1];
    }
}
