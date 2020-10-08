using System;
using System.Collections.Generic;
using System.Text;
using DataStructures.ArrayBasedQueue;
using DataStructures.ArrayBasedQueue.Enumerators;
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
            var result = new StringBuilder();

            // Act
            for (int i = 0; i < 3; i++)
            {
                result.Append(q.Dequeue());
            }

            // Assert
            Assert.AreEqual(expected: "ABC", actual: result.ToString());
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
            int peeked = 0;

            // Act
            for (int i = 0; i < 2; i++)
            {
                peeked = q.Peek();
            }

            // Assert
            Assert.AreEqual(expected: 1, actual: peeked);
            Assert.IsFalse(q.IsEmpty(), "Queue is empty");
            Assert.IsTrue(q.IsFull(), "Queue is full");
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
            Assert.AreEqual(expected: typeof(InvalidOperationException), actual: exception?.GetType());
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
            Assert.AreEqual(expected: typeof(InvalidOperationException), actual: exception?.GetType());
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
            Assert.AreEqual(expected: typeof(InvalidOperationException), actual: exception?.GetType());
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

        [Test]
        public static void GetEnumeratorTypeQualityTest()
        {
            var queue = new ArrayBasedQueue<int>(2);
            queue.Enqueue(1);
            queue.Enqueue(2);

            var enumeratorActual = queue.GetEnumerator();

            Assert.AreEqual(expected: typeof(QueueEnumerator<int>), actual: enumeratorActual?.GetType());
        }

        [Test]
        public static void GetEnumeratorItemQualityTest()
        {
            var queue = new ArrayBasedQueue<int>(2);
            queue.Enqueue(1);
            queue.Enqueue(2);

            var enumerator = queue.GetEnumerator();

            int firstvalue = 0;

            if (enumerator.MoveNext())
            {
                firstvalue = enumerator.Current;
            }

            Assert.AreEqual(expected: typeof(int), actual: firstvalue.GetType());
            Assert.AreEqual(expected: 1, actual: firstvalue);
        }

        [Test]
        public static void GetEnumeratorItemsListQualityTest()
        {
            var queue = new ArrayBasedQueue<int>(2);
            queue.Enqueue(1);
            queue.Enqueue(2);

            var enumerator = queue.GetEnumerator();

            int sumOfItems = 0;

            while (enumerator.MoveNext())
            {
                sumOfItems += enumerator.Current;
            }

            Assert.AreEqual(expected: 3, actual: sumOfItems);
        }

        [Test]
        public static void QueueEmptyQualityTest()
        {
            var queue = new ArrayBasedQueue<int>(2);
            queue.Enqueue(1);
            queue.Enqueue(2);

            queue.Dequeue();
            queue.Dequeue();

            Assert.IsTrue(queue.IsEmpty(), "Queue is empty");
        }

        [Test]
        public static void QueueFullQualityTest()
        {
            var queue = new ArrayBasedQueue<int>(2);
            queue.Enqueue(1);
            queue.Enqueue(2);

            Assert.IsTrue(queue.IsFull(), "Queue is full");
        }
    }
}
