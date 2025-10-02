using DataStructures.Queue;

namespace DataStructures.Tests.Queue;

public static class ArrayBasedQueueTests
{
    [Test]
    public static void DequeueWorksCorrectly()
    {
        // Arrange
        var q = new ArrayBasedQueue<char>(3);
        q.Enqueue('A');
        q.Enqueue('B');
        q.Enqueue('C');
        var result = new StringBuilder();

        // Act
        for (var i = 0; i < 3; i++)
        {
            result.Append(q.Dequeue());
        }

        // Assert
        Assert.That("ABC", Is.EqualTo(result.ToString()));
        Assert.That(q.IsEmpty(), Is.True, "Queue is empty");
        Assert.That(q.IsFull(), Is.False, "Queue is full");
    }

    [Test]
    public static void PeekWorksCorrectly()
    {
        // Arrange
        var q = new ArrayBasedQueue<int>(2);
        q.Enqueue(1);
        q.Enqueue(2);
        var peeked = 0;

        // Act
        for (var i = 0; i < 3; i++)
        {
            peeked = q.Peek();
        }

        // Assert
        Assert.That(1, Is.EqualTo(peeked));
        Assert.That(q.IsEmpty(), Is.False, "Queue is empty");
        Assert.That(q.IsFull(), Is.True, "Queue is full");
    }

    [Test]
    public static void DequeueEmptyQueueThrowsInvalidOperationException()
    {
        // Arrange
        var q = new ArrayBasedQueue<int>(1);
        Exception? exception = null;

        // Act
        try
        {
            q.Dequeue();
        }
        catch (Exception ex)
        {
            exception = ex;
        }

        // Assert
        Assert.That(typeof(InvalidOperationException), Is.EqualTo(exception?.GetType()));
    }

    [Test]
    public static void EnqueueFullQueueThrowsInvalidOperationException()
    {
        // Arrange
        var q = new ArrayBasedQueue<int>(1);
        q.Enqueue(0);
        Exception? exception = null;

        // Act
        try
        {
            q.Enqueue(1);
        }
        catch (Exception ex)
        {
            exception = ex;
        }

        // Assert
        Assert.That(typeof(InvalidOperationException), Is.EqualTo(exception?.GetType()));
    }

    [Test]
    public static void PeekEmptyQueueThrowsInvalidOperationException()
    {
        // Arrange
        var q = new ArrayBasedQueue<int>(1);
        Exception? exception = null;

        // Act
        try
        {
            q.Peek();
        }
        catch (Exception ex)
        {
            exception = ex;
        }

        // Assert
        Assert.That(typeof(InvalidOperationException), Is.EqualTo(exception?.GetType()));
    }

    [Test]
    public static void ClearWorksCorrectly()
    {
        // Arrange
        var q = new ArrayBasedQueue<int>(2);
        q.Enqueue(1);
        q.Enqueue(2);

        // Act
        q.Clear();

        // Assert
        Assert.That(q.IsEmpty(), Is.True, "Queue is empty");
        Assert.That(q.IsFull(), Is.False, "Queue is full");
    }
}
