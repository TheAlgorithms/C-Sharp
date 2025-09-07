namespace Algorithms.Stack;

/// <summary>
///     Reverses the elements in a stack using recursion.
/// @author Mohit Singh. <a href="https://github.com/mohit-gogitter">mohit-gogitter</a>
/// </summary>
public class ReverseStack
{
    /// <summary>
    /// Recursively reverses the elements of the specified stack.
    /// </summary>
    /// <typeparam name="T">The type of elements in the stack.</typeparam>
    /// <param name="stack">The stack to be reversed. This parameter cannot be null.</param>
    /// <exception cref="ArgumentNullException">Thrown when the stack parameter is null.</exception>
    public void Reverse<T>(Stack<T> stack)
    {
        if (stack.Count == 0)
        {
            return;
        }

        T temp = stack.Pop();
        Reverse(stack);
        InsertAtBottom(stack, temp);
    }

    private void InsertAtBottom<T>(Stack<T> stack, T value)
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
