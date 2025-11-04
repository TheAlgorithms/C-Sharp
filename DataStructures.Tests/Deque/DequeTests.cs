using DataStructures.Deque;
using NUnit.Framework;
using System;
using System.Linq;

namespace DataStructures.Tests.Deque;

/// <summary>
///     Comprehensive test suite for the Deque (Double-Ended Queue) data structure.
///     Tests cover:
///     - Constructor validation and initialization
///     - Basic operations (AddFront, AddRear, RemoveFront, RemoveRear)
///     - Peek operations (non-destructive reads)
///     - Edge cases (empty deque, single element, capacity overflow)
///     - Type flexibility (int, string, tuples)
///     - Circular array behavior and automatic resizing
///     - Mixed operations maintaining correct order
/// </summary>
public static class DequeTests
{
    [Test]
    public static void Constructor_WithDefaultCapacity_CreatesEmptyDeque()
    {
        // Arrange & Act: Create deque with default capacity (16)
        var deque = new Deque<int>();

        // Assert: Should be empty initially
        Assert.That(deque.Count, Is.EqualTo(0));
        Assert.That(deque.IsEmpty, Is.True);
    }

    [Test]
    public static void Constructor_WithSpecifiedCapacity_CreatesEmptyDeque()
    {
        // Arrange & Act
        var deque = new Deque<int>(10);

        // Assert
        Assert.That(deque.Count, Is.EqualTo(0));
        Assert.That(deque.IsEmpty, Is.True);
    }

    [Test]
    public static void Constructor_WithInvalidCapacity_ThrowsArgumentException()
    {
        // Arrange, Act & Assert: Capacity must be at least 1
        // Zero capacity should throw
        Assert.Throws<ArgumentException>(() => new Deque<int>(0));
        // Negative capacity should throw
        Assert.Throws<ArgumentException>(() => new Deque<int>(-1));
    }

    [Test]
    public static void AddFront_AddsElementToFront()
    {
        // Arrange
        var deque = new Deque<int>();

        // Act: Add elements to front (each becomes new front)
        deque.AddFront(1);  // Deque: [1]
        deque.AddFront(2);  // Deque: [2, 1]
        deque.AddFront(3);  // Deque: [3, 2, 1]

        // Assert: Most recently added element should be at front
        Assert.That(deque.Count, Is.EqualTo(3));
        Assert.That(deque.PeekFront(), Is.EqualTo(3));
    }

    [Test]
    public static void AddRear_AddsElementToRear()
    {
        // Arrange
        var deque = new Deque<int>();

        // Act
        deque.AddRear(1);
        deque.AddRear(2);
        deque.AddRear(3);

        // Assert
        Assert.That(deque.Count, Is.EqualTo(3));
        Assert.That(deque.PeekRear(), Is.EqualTo(3));
    }

    [Test]
    public static void RemoveFront_RemovesAndReturnsElementFromFront()
    {
        // Arrange: Build deque [1, 2, 3]
        var deque = new Deque<int>();
        deque.AddRear(1);
        deque.AddRear(2);
        deque.AddRear(3);

        // Act: Remove front element
        int result = deque.RemoveFront();

        // Assert: Should return 1 and deque becomes [2, 3]
        Assert.That(result, Is.EqualTo(1));
        Assert.That(deque.Count, Is.EqualTo(2));
        Assert.That(deque.PeekFront(), Is.EqualTo(2));
    }

    [Test]
    public static void RemoveRear_RemovesAndReturnsElementFromRear()
    {
        // Arrange
        var deque = new Deque<int>();
        deque.AddRear(1);
        deque.AddRear(2);
        deque.AddRear(3);

        // Act
        int result = deque.RemoveRear();

        // Assert
        Assert.That(result, Is.EqualTo(3));
        Assert.That(deque.Count, Is.EqualTo(2));
        Assert.That(deque.PeekRear(), Is.EqualTo(2));
    }

    [Test]
    public static void RemoveFront_OnEmptyDeque_ThrowsInvalidOperationException()
    {
        // Arrange: Create empty deque
        var deque = new Deque<int>();

        // Act & Assert: Cannot remove from empty deque
        Assert.Throws<InvalidOperationException>(() => deque.RemoveFront());
    }

    [Test]
    public static void RemoveRear_OnEmptyDeque_ThrowsInvalidOperationException()
    {
        // Arrange
        var deque = new Deque<int>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => deque.RemoveRear());
    }

    [Test]
    public static void PeekFront_ReturnsElementWithoutRemoving()
    {
        // Arrange
        var deque = new Deque<int>();
        deque.AddRear(1);
        deque.AddRear(2);

        // Act
        int result = deque.PeekFront();

        // Assert
        Assert.That(result, Is.EqualTo(1));
        Assert.That(deque.Count, Is.EqualTo(2));
    }

    [Test]
    public static void PeekRear_ReturnsElementWithoutRemoving()
    {
        // Arrange
        var deque = new Deque<int>();
        deque.AddRear(1);
        deque.AddRear(2);

        // Act
        int result = deque.PeekRear();

        // Assert
        Assert.That(result, Is.EqualTo(2));
        Assert.That(deque.Count, Is.EqualTo(2));
    }

    [Test]
    public static void PeekFront_OnEmptyDeque_ThrowsInvalidOperationException()
    {
        // Arrange
        var deque = new Deque<int>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => deque.PeekFront());
    }

    [Test]
    public static void PeekRear_OnEmptyDeque_ThrowsInvalidOperationException()
    {
        // Arrange
        var deque = new Deque<int>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => deque.PeekRear());
    }

    [Test]
    public static void Clear_RemovesAllElements()
    {
        // Arrange
        var deque = new Deque<int>();
        deque.AddRear(1);
        deque.AddRear(2);
        deque.AddRear(3);

        // Act
        deque.Clear();

        // Assert
        Assert.That(deque.Count, Is.EqualTo(0));
        Assert.That(deque.IsEmpty, Is.True);
    }

    [Test]
    public static void ToArray_ReturnsElementsInCorrectOrder()
    {
        // Arrange
        var deque = new Deque<int>();
        deque.AddRear(1);
        deque.AddRear(2);
        deque.AddRear(3);

        // Act
        int[] result = deque.ToArray();

        // Assert
        Assert.That(result, Is.EqualTo(new[] { 1, 2, 3 }));
    }

    [Test]
    public static void ToArray_WithMixedOperations_ReturnsCorrectOrder()
    {
        // Arrange: Build deque using both front and rear operations
        var deque = new Deque<int>();
        deque.AddFront(2);  // Deque: [2]
        deque.AddFront(1);  // Deque: [1, 2]
        deque.AddRear(3);   // Deque: [1, 2, 3]
        deque.AddRear(4);   // Deque: [1, 2, 3, 4]

        // Act
        int[] result = deque.ToArray();

        // Assert: Array should maintain front-to-rear order
        Assert.That(result, Is.EqualTo(new[] { 1, 2, 3, 4 }));
    }

    [Test]
    public static void Contains_WithExistingElement_ReturnsTrue()
    {
        // Arrange
        var deque = new Deque<int>();
        deque.AddRear(1);
        deque.AddRear(2);
        deque.AddRear(3);

        // Act
        bool result = deque.Contains(2);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public static void Contains_WithNonExistingElement_ReturnsFalse()
    {
        // Arrange
        var deque = new Deque<int>();
        deque.AddRear(1);
        deque.AddRear(2);
        deque.AddRear(3);

        // Act
        bool result = deque.Contains(5);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public static void Deque_WithStringType_WorksCorrectly()
    {
        // Arrange
        var deque = new Deque<string>();

        // Act
        deque.AddRear("Hello");
        deque.AddFront("World");
        deque.AddRear("!");

        // Assert
        Assert.That(deque.Count, Is.EqualTo(3));
        Assert.That(deque.PeekFront(), Is.EqualTo("World"));
        Assert.That(deque.PeekRear(), Is.EqualTo("!"));
    }

    [Test]
    public static void Deque_AutomaticallyResizes_WhenCapacityExceeded()
    {
        // Arrange: Create deque with small capacity of 2
        var deque = new Deque<int>(2);

        // Act: Add more elements than initial capacity
        deque.AddRear(1);  // Capacity: 2, Count: 1
        deque.AddRear(2);  // Capacity: 2, Count: 2 (full)
        deque.AddRear(3);  // Should trigger resize to capacity 4, Count: 3
        deque.AddRear(4);  // Capacity: 4, Count: 4

        // Assert: All elements should be present in correct order
        Assert.That(deque.Count, Is.EqualTo(4));
        Assert.That(deque.ToArray(), Is.EqualTo(new[] { 1, 2, 3, 4 }));
    }

    [Test]
    public static void Deque_MixedOperations_MaintainsCorrectOrder()
    {
        // Arrange
        var deque = new Deque<int>();

        // Act: Perform complex sequence of operations
        deque.AddRear(3);      // Deque: [3]
        deque.AddFront(2);     // Deque: [2, 3]
        deque.AddFront(1);     // Deque: [1, 2, 3]
        deque.AddRear(4);      // Deque: [1, 2, 3, 4]
        deque.RemoveFront();   // Deque: [2, 3, 4] (removed 1)
        deque.RemoveRear();    // Deque: [2, 3] (removed 4)
        deque.AddRear(5);      // Deque: [2, 3, 5]

        // Assert: Final order should be correct after all operations
        Assert.That(deque.ToArray(), Is.EqualTo(new[] { 2, 3, 5 }));
    }

    [Test]
    public static void Deque_WithComplexType_WorksCorrectly()
    {
        // Arrange
        var deque = new Deque<(int, string)>();

        // Act
        deque.AddRear((1, "One"));
        deque.AddRear((2, "Two"));
        deque.AddFront((0, "Zero"));

        // Assert
        Assert.That(deque.Count, Is.EqualTo(3));
        Assert.That(deque.PeekFront(), Is.EqualTo((0, "Zero")));
        Assert.That(deque.PeekRear(), Is.EqualTo((2, "Two")));
    }

    [Test]
    public static void Deque_AfterMultipleResizes_MaintainsIntegrity()
    {
        // Arrange: Start with very small capacity
        var deque = new Deque<int>(2);

        // Act: Add many elements to trigger multiple resizes
        // Capacity progression: 2 -> 4 -> 8 -> 16 -> 32 -> 64 -> 128
        for (int i = 0; i < 100; i++)
        {
            deque.AddRear(i);
        }

        // Assert: All elements should be intact after multiple resizes
        Assert.That(deque.Count, Is.EqualTo(100));
        Assert.That(deque.PeekFront(), Is.EqualTo(0));
        Assert.That(deque.PeekRear(), Is.EqualTo(99));
        Assert.That(deque.ToArray(), Is.EqualTo(Enumerable.Range(0, 100).ToArray()));
    }

    [Test]
    public static void Deque_CircularBehavior_WorksCorrectly()
    {
        // Arrange: Create deque with capacity 4
        var deque = new Deque<int>(4);

        // Act: Test circular wrap-around behavior
        deque.AddRear(1);      // Internal: [1, _, _, _], front=0, rear=1
        deque.AddRear(2);      // Internal: [1, 2, _, _], front=0, rear=2
        deque.RemoveFront();   // Internal: [_, 2, _, _], front=1, rear=2
        deque.RemoveFront();   // Internal: [_, _, _, _], front=2, rear=2
        deque.AddRear(3);      // Internal: [_, _, 3, _], front=2, rear=3
        deque.AddRear(4);      // Internal: [_, _, 3, 4], front=2, rear=0 (wrapped)
        deque.AddRear(5);      // Internal: [5, _, 3, 4], front=2, rear=1 (wrapped)

        // Assert: Elements should be in correct logical order despite circular storage
        Assert.That(deque.ToArray(), Is.EqualTo(new[] { 3, 4, 5 }));
    }
}
