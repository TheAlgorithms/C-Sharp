using System;

namespace Algorithms.Strings
{
    /// <summary>
    ///     Implementations to reverse a string:
    ///     1. using Reverse method in Array class;
    ///     2. using for loop;
    ///     3. using recursion.
    /// </summary>
    public static class ReverseString
    {
        /// <summary>
        ///     Reverse a string using Reverse method in Array class.
        /// </summary>
        /// <param name="s">String to be reversed.</param>
        /// <returns>The reversed string.</returns>
        public static string ReverseByArrayReverseMethod(string s)
        {
            char[] chars = s.ToCharArray();
            Array.Reverse(chars);

            return new string(chars);
        }

        /// <summary>
        ///     Reverse a string using for loop.
        /// </summary>
        /// <param name="s">String to be reversed.</param>
        /// <returns>The reversed string.</returns>
        public static string ReverseByForLoop(string s)
        {
            char[] chars = s.ToCharArray();
            string result = string.Empty;

            for (int i = s.Length - 1; i >= 0; i--)
            {
                result += chars[i];
            }

            return result;
        }

        /// <summary>
        ///     Reverse a string recursively.
        /// </summary>
        /// <param name="s">String to be reversed.</param>
        /// <param name="index">
        ///     Index of the characters in chars,
        ///     must be initialized to 0 when function is called.
        /// </param>
        /// <returns>The reversed string.</returns>
        public static string ReverseByRecursion(string s, int index)
        {
            char[] chars = s.ToCharArray();
            int len = chars.Length;

            if (index < len / 2)
            {
                char c = chars[index];
                chars[index] = chars[len - index - 1];
                chars[len - index - 1] = c;
                index++;
                return ReverseByRecursion(new string(chars), index);
            }
            else
            {
                return new string(chars);
            }
        }
    }
}
