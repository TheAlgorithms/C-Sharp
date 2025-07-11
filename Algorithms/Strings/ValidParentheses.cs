using System;
using System.Collections.Generic;

namespace Algorithms.Strings
{
    /// <summary>
    ///     This is a class for checking if the parentheses is valid.
    ///     A valid parentheses should have opening brace and closing brace.
    /// </summary>
    public static class ValidParentheses
    {
        /// <summary>
        ///     Function to check if the parentheses is valid.
        /// </summary>
        /// <param name="parentheses">String to be checked.</param>
        public static bool IsValidParentheses(string parentheses)
        {
            if (parentheses.Length % 2 != 0)
            {
                return false;
            }

            Dictionary<char, char> bracketPairs = new Dictionary<char, char>
        {
            { ')', '(' },
            { '}', '{' },
            { ']', '[' },
        };

            Stack<char> stack = new Stack<char>();

            foreach (char c in parentheses)
            {
                if (bracketPairs.ContainsValue(c))
                {
                    stack.Push(c);
                }
                else if (bracketPairs.ContainsKey(c))
                {
                    if (stack.Count == 0 || stack.Pop() != bracketPairs[c])
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return stack.Count == 0;
        }
    }
}
