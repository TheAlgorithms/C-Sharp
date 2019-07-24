using System;
using System.Text.RegularExpressions;

namespace Algorithms.Strings
{
    /// <summary>
    /// TODO.
    /// </summary>
    public static class Palindrome
    {
        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="word">TODO. 2.</param>
        /// <returns>TODO. 3.</returns>
        public static bool IsStringPalindrome(string word) =>
            TypifyString(word).Equals(TypifyString(ReverseString(word)));

        private static string TypifyString(string word)
        {
            // Typify string to lower and remove white spaces.
            return Regex.Replace(word.ToLowerInvariant(), @"\s+", string.Empty);
        }

        private static string ReverseString(string s)
        {
            var arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
