using System;
using System.Text.RegularExpressions;

namespace Algorithms.Strings;

/// <summary>
///     Palindrome a series of characters or a string that when reversed,
///     equals the original string.
/// </summary>
public static class Palindrome
{
    /// <summary>
    ///     Function to check if a string is a palindrome.
    /// </summary>
    /// <param name="word">String being checked.</param>
    public static bool IsStringPalindrome(string word) =>
        TypifyString(word).Equals(TypifyString(ReverseString(word)));

    /// <summary>
    ///     Typify string to lower and remove white spaces.
    /// </summary>
    /// <param name="word">String to remove spaces.</param>
    /// <returns>Returns original string without spaces.</returns>
    private static string TypifyString(string word) =>
        Regex.Replace(word.ToLowerInvariant(), @"\s+", string.Empty);

    /// <summary>
    ///     Helper function that returns a reversed string inputed.
    /// </summary>
    /// <param name="s">String to be reversed.</param>
    /// <returns>Returns s reversed.</returns>
    private static string ReverseString(string s)
    {
        var arr = s.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }
}
