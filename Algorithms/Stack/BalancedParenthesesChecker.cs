using System;
namespace DataStructures.Stack
{
    /// <summary>
    ///     It checks if an expression has matching and balanced parentheses.
    /// @author Mohit Singh
    /// @author <a href="https://github.com/mohit-gogitter">mohit-gogitter</a>
    /// </summary>
    public class BalancedParenthesesChecker
    {
        private static readonly Dictionary<char, char> ParenthesesMap = new Dictionary<char, char>()
        {
            { '(', ')' },
            { '{', '}' },
            { '[', ']' },
        };
        /// <summary>
        ///     This method checks if an expression has matching and balanced parentheses.
        /// </summary>
        /// <param name="expression">string containing parenthesis</param>
        /// <returns>Boolean value</returns>
        public static bool IsBalanced(string expression)
        {
            Stack<char> stack = new Stack<char>();
            foreach (char c in expression)
            {
                if (c == '(' || c == '{' || c == '[')
                {
                    stack.Push(c);
                }
                else if (c == ')' || c == '}' || c == ']')
                {
                    if (stack.Count == 0)
                    {
                        return false;
                    }
                    char open = stack.Pop();

                    if (!IsMatchingPair(open, c))
                    {
                        return false;
                    }
                }
                else
                {
                    //since there are no other brackets, this is unreachable code
                }
            }
            return stack.Count == 0;
        }
        private static bool IsMatchingPair(char open, char close)
        {
            return ParenthesesMap.ContainsKey(open) && ParenthesesMap[open] == close;
        }
    }
}