using System;
using System.Collections.Generic;

/// <summary>
///This is a stack-based utility in C-Sharp that can be used by other developers in their solutions. 
///This utility provides commonly used operations related to stacks, which can be beneficial for solving 
///various problems such as expression evaluations, balanced parentheses, or maintaining a history of operations.

///MinStack: This custom stack keeps track of the minimum element so that getMin() can return the minimum in O(1) time.
///Next Greater Element: For each element in an array, the utility finds the next greater element on the right side using a stack.
///Balanced Parentheses Checker: It checks if an expression has matching and balanced parentheses.
///Reverse a Stack: This utility reverses the elements in a stack using recursion.

///@author Mohit Singh
///@author <a href="https://github.com/mohit-gogitter">mohit-gogitter<a>
/// </summary>

namespace DataStructures.Stack
{
    public class StackUtils
    {
        private static readonly Dictionary<char, char> parenthesesMap = new Dictionary<char, char>
        {
            { '(', ')' },
            { '{', '}' },
            { '[', ']' }
        };
            
        // 1. MinStack - Stack supporting push, pop, and retrieving minimum in O(1) time
        public class MinStack
        {
            private readonly Stack<int> mainStack;
            private readonly Stack<int> minStack;
           

            public MinStack()
            {
                mainStack = new Stack<int>();
                minStack = new Stack<int>();
            }
            /// <summary>
            ///     Adds an item on top of the stack.
            /// </summary>
            /// <param name="value">Item to be added on top of stack.</param>
            public void Push(int value)
            {
                mainStack.Push(value);
                if (minStack.Count == 0 || value <= minStack.Peek())
                {
                    minStack.Push(value);
                }
            }

            /// <summary>
            ///     Removes an item from top of the stack.
            /// </summary>
            public void Pop()
            {
                if (mainStack.Count > 0)
                {
                    int poppedValue = mainStack.Pop();
                    if (poppedValue == minStack.Peek())
                    {
                        minStack.Pop();
                    }
                }
            }

            /// <summary>
            ///     Fetches the minimum item from the stack and returns it.
            /// </summary>
            /// <returns>minimum item from the stack</returns>
            public int GetMin()
            {
                return minStack.Count == 0 ? int.MaxValue : minStack.Peek();
            }
            /// <summary>
            ///     Removes an item from top of the stack and returns it.
            ///  </summary>
            /// <returns>item on top of stack.</returns>
            public int Top()
            {
                return mainStack.Count == 0 ? -1 : mainStack.Peek();
            }
            /// <summary>
            ///     Checks whether the stack is empty. Returns True if found empty else False.
            ///  </summary>
            /// <returns>True or False</returns>
            public bool IsEmpty()
            {
                return mainStack.Count == 0;
            }
        }

        // 2. Next Greater Element for each element in an array
        /// <summary>
        ///     For each element in an array, the utility finds the next greater element on the right side using a stack.
        /// </summary>
        /// <param name="nums">Integer Array for which NextGreaterElement needs to be computed</param>
        /// <returns>Interger Array</returns>
        public static int[] NextGreaterElement(int[] nums)
        {
            Stack<int> stack = new Stack<int>();
            int[] result = new int[nums.Length];

            for (int i = nums.Length - 1; i >= 0; i--)
            {
                while (stack.Count > 0 && stack.Peek() <= nums[i])
                {
                    stack.Pop();
                }
                result[i] = stack.Count == 0 ? -1 : stack.Peek();
                stack.Push(nums[i]);
            }
            return result;
        }

        // 3. Balanced Parentheses Checker
        /// <summary>
        ///     It checks if an expression has matching and balanced parentheses.
        /// </summary>
        /// <param name="expression">string containing parenthesis</param>
        /// <returns>True or False</returns>
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
            return parenthesesMap.ContainsKey(open) && parenthesesMap[open] == close;
        }

        // 4. Reverse a Stack
        /// <summary>
        ///    This utility reverses the elements in a stack using recursion.
        /// </summary>
        /// <param name="stack">A Stack of Generic Type</param>
        public static void ReverseStack<T>(Stack<T> stack)
        {
            if (stack.Count == 0) 
            {
                return;
            }
            T temp = stack.Pop();
            ReverseStack(stack);
            InsertAtBottom(stack, temp);
        }

        private static void InsertAtBottom<T>(Stack<T> stack, T value)
        {
            if (stack.Count == 0)
            {
                stack.Push(value);
            }
            else
            {
                T temp = stack.Pop();
                InsertAtBottom(stack, value);
                stack.Push(temp);
            }
        }
    }
}