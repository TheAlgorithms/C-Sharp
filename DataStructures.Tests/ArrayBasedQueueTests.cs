using System;
using NUnit.Framework;

namespace DataStructures.Tests
{
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

            // Act
            string result = "";
            for (int i = 0; i < 3; i++)
            {
                result += q.Dequeue();
            }

            // Assert
            Assert.AreEqual(result, "ABC");
            Assert.IsTrue(q.IsEmpty(), "Queue is empty");
            Assert.IsFalse(q.IsFull(), "Queue is full");
        }

        [Test]
        public static void PeekWorksCorrectly()
        {
            // Arrange
            var q = new ArrayBasedQueue<int>(2);
            q.Enqueue(1);
            q.Enqueue(2);

            // Act
            int peeked = 0;
            for (int i = 0; i < 3; i++)
            {
                peeked = q.Peek();
            }

            // Assert
            Assert.AreEqual(peeked, 1);
            Assert.IsFalse(q.IsEmpty(), "Queue is empty");
            Assert.IsTrue(q.IsFull(), "Queue is full");
        }

        [Test]
        public static void DequeueEmptyQueueThrowsInvalidOperationException()
        {
            // Arrange
            var q = new ArrayBasedQueue<int>(1);

            // Act
            Exception? exception = null;
            try
            {
                q.Dequeue();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Assert
            Assert.AreEqual(typeof(InvalidOperationException), exception?.GetType());
        }

        [Test]
        public static void PeekEmptyQueueThrowsInvalidOperationException()
        {
            // Arrange
            var q = new ArrayBasedQueue<int>(1);

            // Act
            Exception? exception = null;
            try
            {
                q.Peek();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Assert
            Assert.AreEqual(typeof(InvalidOperationException), exception?.GetType());
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
            Assert.IsTrue(q.IsEmpty(), "Queue is empty");
            Assert.IsFalse(q.IsFull(), "Queue is full");
        }
    }
}
