using System;

namespace DataStructures.Queue;

/// <summary>
///     Implementation of an array based queue. FIFO style.
/// </summary>
/// <typeparam name="T">Generic Type.</typeparam>
public class ArrayBasedQueue<T>
{
    private readonly T[] queue;
    private int endIndex;
    private bool isEmpty;
    private bool isFull;
    private int startIndex;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ArrayBasedQueue{T}" /> class.
    /// </summary>
    public ArrayBasedQueue(int capacity)
    {
        queue = new T[capacity];
        Clear();
    }

    /// <summary>
    ///     Clears the queue.
    /// </summary>
    public void Clear()
    {
        startIndex = 0;
        endIndex = 0;
        isEmpty = true;
        isFull = false;
    }

    /// <summary>
    ///     Returns the first item in the queue and removes it from the queue.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
    public T Dequeue()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("There are no items in the queue.");
        }

        var dequeueIndex = endIndex;
        endIndex++;
        if (endIndex >= queue.Length)
        {
            endIndex = 0;
        }

        isFull = false;
        isEmpty = startIndex == endIndex;

        return queue[dequeueIndex];
    }

    /// <summary>
    ///     Returns a boolean indicating whether the queue is empty.
    /// </summary>
    public bool IsEmpty() => isEmpty;

    /// <summary>
    ///     Returns a boolean indicating whether the queue is full.
    /// </summary>
    public bool IsFull() => isFull;

    /// <summary>
    ///     Returns the first item in the queue and keeps it in the queue.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
    public T Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("There are no items in the queue.");
        }

        return queue[endIndex];
    }

    /// <summary>
    ///     Adds an item at the last position in the queue.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the queue is full.</exception>
    public void Enqueue(T item)
    {
        if (IsFull())
        {
            throw new InvalidOperationException("The queue has reached its capacity.");
        }

        queue[startIndex] = item;

        startIndex++;
        if (startIndex >= queue.Length)
        {
            startIndex = 0;
        }

        isEmpty = false;
        isFull = startIndex == endIndex;
    }
}
