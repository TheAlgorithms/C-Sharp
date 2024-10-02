using System;
namespace DataStructures.Stack
{
    /// <summary>
    ///     Reverses the elements in a stack using recursion.
    /// @author Mohit Singh
    /// @author <a href="https://github.com/mohit-gogitter">mohit-gogitter</a>
    /// </summary>
    public class ReverseStack
    {
        /// <summary>
        ///    This method reverses the elements in a stack using recursion.
        /// </summary>
        /// <param name="stack">A Stack of Generic Type</param>
        public static void Reverse<T>(Stack<T> stack)
        {
            if (stack.Count == 0)
            {
                return;
            }
            T temp = stack.Pop();
            Reverse(stack);
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