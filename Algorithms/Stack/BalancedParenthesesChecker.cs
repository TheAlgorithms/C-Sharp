namespace Algorithms.Stack;

/// <summary>
///     It checks if an expression has matching and balanced parentheses.
/// @author Mohit Singh. <a href="https://github.com/mohit-gogitter">mohit-gogitter</a>
/// </summary>
public class BalancedParenthesesChecker
{
    private static readonly Dictionary<char, char> ParenthesesMap = new Dictionary<char, char>
        {
            { '(', ')' },
            { '{', '}' },
            { '[', ']' },
        };

    /// <summary>
    /// Determines if a given string expression containing brackets is balanced.
    /// A string is considered balanced if all opening brackets have corresponding closing brackets
    /// in the correct order. The supported brackets are '()', '{}', and '[]'.
    /// </summary>
    /// <param name="expression">
    /// The input string expression containing the brackets to check for balance.
    /// </param>
    /// <returns>
    /// <c>true</c> if the brackets in the expression are balanced; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the input expression contains invalid characters or is null/empty.
    /// Only '(', ')', '{', '}', '[', ']' characters are allowed.
    /// </exception>
    public bool IsBalanced(string expression)
    {
        if (string.IsNullOrEmpty(expression))
        {
            throw new ArgumentException("The input expression cannot be null or empty.");
        }

        Stack<char> stack = new Stack<char>();
        foreach (char c in expression)
        {
            if (IsOpeningParenthesis(c))
            {
                stack.Push(c);
            }
            else if (IsClosingParenthesis(c))
            {
                if (!IsBalancedClosing(stack, c))
                {
                    return false;
                }
            }
            else
            {
                throw new ArgumentException($"Invalid character '{c}' found in the expression.");
            }
        }

        return stack.Count == 0;
    }

    private static bool IsOpeningParenthesis(char c)
    {
        return c == '(' || c == '{' || c == '[';
    }

    private static bool IsClosingParenthesis(char c)
    {
        return c == ')' || c == '}' || c == ']';
    }

    private static bool IsBalancedClosing(Stack<char> stack, char close)
    {
        if (stack.Count == 0)
        {
            return false;
        }

        char open = stack.Pop();
        return IsMatchingPair(open, close);
    }

    private static bool IsMatchingPair(char open, char close)
    {
        return ParenthesesMap.ContainsKey(open) && ParenthesesMap[open] == close;
    }
}
