using System;
using System.Collections.Generic;

namespace Algorithms.Strings
{
    /// <summary>
    ///     This is a class for checking if the parentheses is valid.
    ///     A valid parentheses should have opening brace and closing brace.
    /// </summary>
    public class ValidParentheses
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

            Stack<char> stack = new Stack<char>();

            foreach(char c in parentheses.ToCharArray())
            {
                if(c == '(' || c == '{' || c == '[')
                {
                    stack.Push(c);
                }
                else if (c == ')' && stack.Count != 0 && stack.Peek() == '(')
                {
                    stack.Pop();
                }
                else if (c == '}' && stack.Count != 0 && stack.Peek() == '{')
                {
                    stack.Pop();
                }
                else if (c == ']' && stack.Count != 0 && stack.Peek() == '[')
                {
                    stack.Pop();
                }
                else
                {
                    stack.Push(c);
                }
            }

            return stack.Count == 0;
        }
    }
}
