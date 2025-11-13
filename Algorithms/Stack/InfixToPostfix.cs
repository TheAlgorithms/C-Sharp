using System;

namespace Algorithms.Stack
{
    /// <summary>
    /// The Code focuses on Converting an Infix Expression to a Postfix Expression and Evaluates the value for the expression.
    /// @author Aalok Choudhari. <a href="https://github.com/kaloa2025">kaloa2025</a>
    /// </summary>
    public class InfixToPostfix
    {
        /// <summary>
        /// <param name="initialInfixExpression"> Infix Expression String to Convert.</param>
        /// <returns>Postfix Expression.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the input expression contains invalid characters or is null/empty.
        /// Only following characters are allowed : Parentheses['(',')'], Operands[a-z,A-Z,0-9], Operators['+','-','*','/','^'].
        /// </exception>
        /// </summary>
        public static string InfixToPostfixConversion(string initialInfixExpression)
        {
            if (string.IsNullOrEmpty(initialInfixExpression))
            {
                throw new ArgumentException("The input infix expression cannot be null or empty.");
            }

            Stack<char> stack = new Stack<char>();
            StringBuilder postfixExpression = new StringBuilder();

            foreach (char c in initialInfixExpression)
            {
                if (char.IsWhiteSpace(c))
                {
                    continue;
                }

                if (!IsValidCharacter(c))
                {
                    throw new ArgumentException($"Invalid character {c}.");
                }

                if (IsOperand(c))
                {
                    postfixExpression.Append(c);
                }
                else if (c == '(')
                {
                    stack.Push(c);
                }
                else if (c == ')')
                {
                    while(stack.Count > 0 && stack.Peek() != '(')
                    {
                        postfixExpression.Append(stack.Pop());
                    }

                    if(stack.Count == 0)
                    {
                        throw new InvalidOperationException("Mismatched parentheses in expression.");
                    }

                    stack.Pop();
                }
                else
                {
                    while(stack.Count > 0 && stack.Peek() != '(' && Precedence(stack.Peek()) >= Precedence(c))
                    {
                        postfixExpression.Append(stack.Pop());
                    }

                    stack.Push(c);
                }
            }

            while (stack.Count > 0)
            {
                if(stack.Peek() == '(' || stack.Peek() == ')')
                {
                    throw new InvalidOperationException("Mismatched Parentheses in expression.");
                }

                postfixExpression.Append(stack.Pop());
            }

            return postfixExpression.ToString();
        }

        /// <summary>
        /// <param name="postfixExpression"> Postfix Expression String to Evaluate.</param>
        /// <returns>Postfix Expression's Calculated value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the input expression contains invalid characters or is null/empty.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Validates expression to have sufficient operands for performing operation.
        /// </exception>
        /// </summary>
        public static int PostfixExpressionEvaluation(string postfixExpression)
        {
            if (string.IsNullOrEmpty(postfixExpression))
            {
                throw new ArgumentException("The input postfix expression cannot be null or empty.");
            }

            Stack<int> stack = new Stack<int>();
            foreach (char ch in postfixExpression)
            {
                if(char.IsWhiteSpace(ch))
                {
                    continue;
                }

                if(char.IsDigit(ch))
                {
                    stack.Push(ch - '0');
                }
                else if (IsOperator(ch))
                {
                    if(stack.Count < 2)
                    {
                        throw new InvalidOperationException("Invalid Postfix Expression: Insufficient Operands");
                    }

                    int b = stack.Pop();
                    int a = stack.Pop();

                    if(ch == '/' && b == 0)
                    {
                        throw new DivideByZeroException("Cannot divide by zero");
                    }

                    int result = ch switch
                    {
                        '+' => a + b,
                        '-' => a - b,
                        '*' => a * b,
                        '/' => a / b,
                        '^' => (int)Math.Pow(a, b),
                        _ => throw new InvalidOperationException("Unknown operator."),
                    };

                    stack.Push(result);
                }
                else
                {
                    throw new InvalidOperationException($"Invalid character in expression: {ch}");
                }
            }

            if (stack.Count != 1)
            {
                throw new InvalidOperationException("Invalid postfix expression: Leftover operands.");
            }

            return stack.Pop();
        }

        /// <summary>
        /// Decided Operator Precedence.
        /// <param name="operatorChar"> Operator character whose precedence is asked.</param>
        /// <returns>Precedence rank of parameter operator character.</returns>
        /// </summary>
        private static int Precedence(char operatorChar)
        {
            if (operatorChar == '^')
            {
                return 3;
            }

            if (operatorChar == '*' || operatorChar == '/')
            {
                return 2;
            }

            if (operatorChar == '+' || operatorChar == '-')
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Checks for character if its an Operand.
        /// <param name="isOperand"> Character asked to verify whether its an operand.</param>
        /// <returns>True if its a digit or a Letter.</returns>
        /// </summary>
        private static bool IsOperand(char isOperand) => char.IsLetterOrDigit(isOperand);

        /// <summary>
        /// Checks Operator.
        /// <param name="isOperator"> Character asked to verify whether its an operator.</param>
        /// <returns>True if its allowded operator character.</returns>
        /// </summary>
        private static bool IsOperator(char isOperator) => isOperator == '+' || isOperator == '-' || isOperator == '*' || isOperator == '/' || isOperator == '^';

        /// <summary>
        /// Checks Valid Character.
        /// <param name="c"> Character asked to verify whether its an valid Character for expression.</param>
        /// <returns>True if its allowded character.</returns>
        /// </summary>
        private static bool IsValidCharacter(char c) => IsOperand(c) || IsOperator(c) || c == '(' || c == ')';
    }
}
