using System;
using System.Diagnostics.CodeAnalysis;

namespace Algorithms.Stack
{
    /// <summary>
    /// The Code focuses on Converting an Infix Expression to a Postfix Expression and Evaluates the value for the expression.
    /// @author Aalok Choudhari. <a href="https://github.com/kaloa2025">kaloa2025</a>
    /// </summary>
    public static class InfixToPostfix
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
            ValidateInfix(initialInfixExpression);

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

                ProcessInfixCharacter(c, stack, postfixExpression);
            }

            EmptyRemainingStack(stack, postfixExpression);
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
            ValidatePostfix(postfixExpression);

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
                    continue;
                }

                if (IsOperator(ch))
                {
                    EvaluateOperator(stack, ch);
                    continue;
                }

                throw new InvalidOperationException($"Invalid character in expression: {ch}");
            }

            if (stack.Count != 1)
            {
                throw new InvalidOperationException("Invalid postfix expression: Leftover operands.");
            }

            return stack.Pop();
        }

        private static void ProcessInfixCharacter(char c, Stack<char> stack, StringBuilder postfixExpression)
        {
            if (IsOperand(c))
            {
                postfixExpression.Append(c);
                return;
            }

            if (c == '(')
            {
                stack.Push(c);
                return;
            }

            if (c == ')')
            {
                ProcessClosingParenthesis(stack, postfixExpression);
                return;
            }

            ProcessOperator(c, stack, postfixExpression);
        }

        private static void ProcessClosingParenthesis(Stack<char> stack, StringBuilder postfixExpression)
        {
            while (stack.Count > 0 && stack.Peek() != '(')
            {
                postfixExpression.Append(stack.Pop());
            }

            if (stack.Count == 0)
            {
                throw new InvalidOperationException("Mismatched parentheses in expression.");
            }

            stack.Pop();
        }

        private static void ProcessOperator(char c, Stack<char> stack, StringBuilder postfixExpression)
        {
            while (stack.Count > 0 && stack.Peek() != '(' && Precedence(stack.Peek()) >= Precedence(c))
            {
                postfixExpression.Append(stack.Pop());
            }

            stack.Push(c);
        }

        private static void EmptyRemainingStack(Stack<char> stack, StringBuilder postfix)
        {
            while (stack.Count > 0)
            {
                if (stack.Peek() is '(' or ')')
                {
                    throw new InvalidOperationException("Mismatched parentheses.");
                }

                postfix.Append(stack.Pop());
            }
        }

        private static void EvaluateOperator(Stack<int> stack, char op)
        {
            if (stack.Count < 2)
            {
                throw new InvalidOperationException("Insufficient operands");
            }

            int b = stack.Pop();
            int a = stack.Pop();

            if (op == '/' && b == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero");
            }

            int result = op switch
            {
                '+' => a + b,
                '-' => a - b,
                '*' => a * b,
                '/' => a / b,
                '^' => (int)Math.Pow(a, b),
                _ => throw new InvalidOperationException($"Unknown operator {op}"),
            };

            stack.Push(result);
        }

        private static void ValidateInfix(string expr)
        {
            if (string.IsNullOrEmpty(expr) || string.IsNullOrWhiteSpace(expr))
            {
                throw new ArgumentException("Infix cannot be null or empty.");
            }
        }

        private static void ValidatePostfix(string expr)
        {
            if (string.IsNullOrEmpty(expr) || string.IsNullOrWhiteSpace(expr))
            {
                throw new ArgumentException("Postfix cannot be null or empty.");
            }
        }

        /// <summary>
        /// Decided Operator Precedence.
        /// <param name="operatorChar"> Operator character whose precedence is asked.</param>
        /// <returns>Precedence rank of parameter operator character.</returns>
        /// </summary>
        [ExcludeFromCodeCoverage]
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
        /// <param name="ch"> Character asked to verify whether its an operand.</param>
        /// <returns>True if its a digit or a Letter.</returns>
        /// </summary>
        private static bool IsOperand(char ch) => char.IsLetterOrDigit(ch);

        private static readonly HashSet<char> Operators = new() { '+', '-', '*', '/', '^' };

        /// <summary>
        /// Checks Operator.
        /// <param name="ch"> Character asked to verify whether its an operator.</param>
        /// <returns>True if its allowded operator character.</returns>
        /// </summary>
        private static bool IsOperator(char ch) => Operators.Contains(ch);

        /// <summary>
        /// Checks Valid Character.
        /// <param name="c"> Character asked to verify whether its an valid Character for expression.</param>
        /// <returns>True if its allowded character.</returns>
        /// </summary>
        private static bool IsValidCharacter(char c) => IsOperand(c) || IsOperator(c) || IsParenthesis(c);

        private static bool IsParenthesis(char c) => c == '(' || c == ')';
    }
}
