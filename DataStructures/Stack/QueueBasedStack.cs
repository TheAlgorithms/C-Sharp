using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Stack;

public class QueueBasedStack<T>
{
    private readonly Queue<T> queue;

    public QueueBasedStack() => queue = new Queue<T>();

    /// <summary>
    ///     Clears the stack.
    /// </summary>
    public void Clear() => queue.Clear();

    public bool IsEmpty() => queue.Count == 0;

    /// <summary>
    ///     Adds an item on top of the stack.
    /// </summary>
    /// <param name="item">Item to be added on top of stack.</param>
    public void Push(T item) => queue.Enqueue(item);

    /// <summary>
    ///     Removes an item from  top of the stack and returns it.
    ///  </summary>
    /// <returns>item on top of stack.</returns>
    /// <exception cref="InvalidOperationException">Throw if stack is empty.</exception>
    public T Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("The stack contains no items.");
        }

        for (int i = 0; i < queue.Count - 1; i++)
        {
            queue.Enqueue(queue.Dequeue());
        }

        return queue.Dequeue();
    }

    /// <summary>
    ///     return an item from the top of the stack without removing it.
    /// </summary>
    /// <returns>item on top of the stack.</returns>
    /// <exception cref="InvalidOperationException">Throw if stack is empty.</exception>
    public T Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("The stack contains no items.");
        }

        for (int i = 0; i < queue.Count - 1; i++)
        {
            queue.Enqueue(queue.Dequeue());
        }

        var item = queue.Peek();
        queue.Enqueue(queue.Dequeue());
        return item;
    }

    /// <summary>
    ///     returns the count of items on the stack.
    /// </summary>
    /// <returns>number of items on the stack.</returns>
    public int Length() => queue.Count;
}
