namespace DataStructures.Deque;

/// <summary>
///     Implementation of a Deque (Double-Ended Queue) data structure.
///     A deque allows insertion and deletion of elements from both ends (front and rear).
///     This implementation uses a circular array for efficient operations.
///     
///     Key Features:
///     - O(1) time complexity for AddFront, AddRear, RemoveFront, RemoveRear operations
///     - O(1) amortized time for insertions (due to dynamic resizing)
///     - Space efficient with circular array implementation
///     - Automatic capacity doubling when full
///     
///     Use Cases:
///     - Implementing sliding window algorithms
///     - Palindrome checking
///     - Undo/Redo functionality
///     - Task scheduling with priority at both ends
///     
///     Reference: "Data Structures and Algorithms in C#" by Michael T. Goodrich.
/// </summary>
/// <typeparam name="T">The type of elements in the deque.</typeparam>
public class Deque<T>
{
    // Internal circular array to store elements
    private T[] items;
    
    // Index of the front element (next element to remove from front)
    private int front;
    
    // Index where the next element will be added at rear
    private int rear;
    
    // Current number of elements in the deque
    private int count;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Deque{T}" /> class with default capacity.
    ///     Default capacity is 16 elements, which provides a good balance between
    ///     memory usage and avoiding early resizing for typical use cases.
    /// </summary>
    public Deque() : this(16)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Deque{T}" /> class with specified capacity.
    /// </summary>
    /// <param name="capacity">The initial capacity of the deque.</param>
    /// <exception cref="ArgumentException">Thrown when capacity is less than 1.</exception>
    public Deque(int capacity)
    {
        if (capacity < 1)
        {
            throw new ArgumentException("Capacity must be at least 1.", nameof(capacity));
        }

        items = new T[capacity];
        front = 0;
        rear = 0;
        count = 0;
    }

    /// <summary>
    ///     Gets the number of elements in the deque.
    /// </summary>
    public int Count => count;

    /// <summary>
    ///     Gets a value indicating whether the deque is empty.
    /// </summary>
    public bool IsEmpty => count == 0;

    /// <summary>
    ///     Adds an element to the front of the deque.
    ///     This operation is O(1) time complexity (amortized due to occasional resizing).
    /// </summary>
    /// <param name="item">The item to add.</param>
    /// <example>
    ///     deque.AddFront(5);  // Deque: [5]
    ///     deque.AddFront(3);  // Deque: [3, 5]
    /// </example>
    public void AddFront(T item)
    {
        // Check if we need to resize before adding
        if (count == items.Length)
        {
            Resize();
        }

        // Move front pointer backward in circular fashion
        // Adding items.Length ensures the result is always positive
        front = (front - 1 + items.Length) % items.Length;
        items[front] = item;
        count++;
    }

    /// <summary>
    ///     Adds an element to the rear of the deque.
    ///     This operation is O(1) time complexity (amortized due to occasional resizing).
    /// </summary>
    /// <param name="item">The item to add.</param>
    /// <example>
    ///     deque.AddRear(5);  // Deque: [5]
    ///     deque.AddRear(7);  // Deque: [5, 7]
    /// </example>
    public void AddRear(T item)
    {
        // Check if we need to resize before adding
        if (count == items.Length)
        {
            Resize();
        }

        // Add item at rear position
        items[rear] = item;
        // Move rear pointer forward in circular fashion
        rear = (rear + 1) % items.Length;
        count++;
    }

    /// <summary>
    ///     Removes and returns the element at the front of the deque.
    ///     This operation is O(1) time complexity.
    /// </summary>
    /// <returns>The element at the front of the deque.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the deque is empty.</exception>
    /// <example>
    ///     // Deque: [3, 5, 7]
    ///     int value = deque.RemoveFront();  // Returns 3, Deque: [5, 7]
    /// </example>
    public T RemoveFront()
    {
        // Validate that deque is not empty
        if (IsEmpty)
        {
            throw new InvalidOperationException("Deque is empty.");
        }

        // Retrieve the front element
        T item = items[front];
        // Clear the reference to help garbage collection
        items[front] = default!;
        // Move front pointer forward in circular fashion
        front = (front + 1) % items.Length;
        count--;

        return item;
    }

    /// <summary>
    ///     Removes and returns the element at the rear of the deque.
    ///     This operation is O(1) time complexity.
    /// </summary>
    /// <returns>The element at the rear of the deque.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the deque is empty.</exception>
    /// <example>
    ///     // Deque: [3, 5, 7]
    ///     int value = deque.RemoveRear();  // Returns 7, Deque: [3, 5]
    /// </example>
    public T RemoveRear()
    {
        // Validate that deque is not empty
        if (IsEmpty)
        {
            throw new InvalidOperationException("Deque is empty.");
        }

        // Move rear pointer backward to the last element
        rear = (rear - 1 + items.Length) % items.Length;
        // Retrieve the rear element
        T item = items[rear];
        // Clear the reference to help garbage collection
        items[rear] = default!;
        count--;

        return item;
    }

    /// <summary>
    ///     Returns the element at the front of the deque without removing it.
    ///     This operation is O(1) time complexity and does not modify the deque.
    /// </summary>
    /// <returns>The element at the front of the deque.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the deque is empty.</exception>
    /// <example>
    ///     // Deque: [3, 5, 7]
    ///     int value = deque.PeekFront();  // Returns 3, Deque unchanged: [3, 5, 7]
    /// </example>
    public T PeekFront()
    {
        // Validate that deque is not empty
        if (IsEmpty)
        {
            throw new InvalidOperationException("Deque is empty.");
        }

        return items[front];
    }

    /// <summary>
    ///     Returns the element at the rear of the deque without removing it.
    ///     This operation is O(1) time complexity and does not modify the deque.
    /// </summary>
    /// <returns>The element at the rear of the deque.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the deque is empty.</exception>
    /// <example>
    ///     // Deque: [3, 5, 7]
    ///     int value = deque.PeekRear();  // Returns 7, Deque unchanged: [3, 5, 7]
    /// </example>
    public T PeekRear()
    {
        // Validate that deque is not empty
        if (IsEmpty)
        {
            throw new InvalidOperationException("Deque is empty.");
        }

        // Calculate the index of the last element (rear - 1 in circular array)
        int rearIndex = (rear - 1 + items.Length) % items.Length;
        return items[rearIndex];
    }

    /// <summary>
    ///     Removes all elements from the deque.
    ///     This operation is O(n) where n is the capacity of the internal array.
    ///     After clearing, the deque can be reused without reallocation.
    /// </summary>
    public void Clear()
    {
        // Clear all references in the array to help garbage collection
        Array.Clear(items, 0, items.Length);
        // Reset pointers to initial state
        front = 0;
        rear = 0;
        count = 0;
    }

    /// <summary>
    ///     Converts the deque to an array.
    ///     This operation is O(n) where n is the number of elements.
    ///     The resulting array maintains the order from front to rear.
    /// </summary>
    /// <returns>An array containing all elements in the deque from front to rear.</returns>
    /// <example>
    ///     // Deque: [3, 5, 7]
    ///     int[] array = deque.ToArray();  // Returns [3, 5, 7]
    /// </example>
    public T[] ToArray()
    {
        // Create result array with exact size needed
        T[] result = new T[count];
        int index = front;

        // Copy elements from front to rear, handling circular wrap-around
        for (int i = 0; i < count; i++)
        {
            result[i] = items[index];
            index = (index + 1) % items.Length;
        }

        return result;
    }

    /// <summary>
    ///     Determines whether the deque contains a specific element.
    ///     This operation is O(n) where n is the number of elements.
    ///     Uses the default equality comparer for type T.
    /// </summary>
    /// <param name="item">The item to locate in the deque.</param>
    /// <returns>true if the item is found; otherwise, false.</returns>
    /// <example>
    ///     // Deque: [3, 5, 7]
    ///     bool exists = deque.Contains(5);  // Returns true
    ///     bool missing = deque.Contains(9);  // Returns false
    /// </example>
    public bool Contains(T item)
    {
        int index = front;

        // Iterate through all elements in order from front to rear
        for (int i = 0; i < count; i++)
        {
            // Use default equality comparer to compare elements
            if (EqualityComparer<T>.Default.Equals(items[index], item))
            {
                return true;
            }

            // Move to next element in circular array
            index = (index + 1) % items.Length;
        }

        return false;
    }

    /// <summary>
    ///     Resizes the internal array to accommodate more elements.
    ///     This is a private helper method called automatically when capacity is reached.
    ///     Doubles the capacity and reorganizes elements to start at index 0.
    ///     Time complexity: O(n) where n is the current number of elements.
    /// </summary>
    private void Resize()
    {
        // Double the capacity to reduce frequency of future resizes
        int newCapacity = items.Length * 2;
        T[] newItems = new T[newCapacity];

        // Copy all elements to new array starting from index 0
        // This "unwraps" the circular structure for simplicity
        int index = front;
        for (int i = 0; i < count; i++)
        {
            newItems[i] = items[index];
            index = (index + 1) % items.Length;
        }

        // Replace old array with new larger array
        items = newItems;
        // Reset pointers: front at 0, rear at position after last element
        front = 0;
        rear = count;
    }
}
