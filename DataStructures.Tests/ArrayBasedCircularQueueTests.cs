using System;
using System.Collections.Generic;
using System.Text;
using DataStructures.ArrayBasedCircularQueue;
using NUnit.Framework;

namespace DataStructures.Tests
{
    public class ArrayBasedCircularQueueTests
    {
        [Test]
        public static void ClearQualityTest()
        {
            var circularQueue = new CircularQueue<int>(2);
            circularQueue.Enqueue(1);
            circularQueue.Enqueue(2);

            circularQueue.Clear();

            Assert.IsTrue(circularQueue.IsEmpty(), "Queue is empty");
            Assert.IsFalse(circularQueue.IsFull(), "Queue is full");

        }

        [Test]
        public static void DequeueQualityTest()
        {
            var circularQueue = new CircularQueue<int>(2);
            circularQueue.Enqueue(1);
            circularQueue.Enqueue(2);

            int afterDequeueSum = 0;

            for (int i = 0; i < 2; i++)
            {
                afterDequeueSum += circularQueue.Dequeue();
            }
            

            Assert.AreEqual(expected: 3, actual: afterDequeueSum);
            Assert.IsTrue(circularQueue.IsEmpty(), "Queue is empty");
            Assert.IsFalse(circularQueue.IsFull(), "Queue is full");
        }

        [Test]
        public static void DequeueEmptyQueueThrowsInvalidOperationException()
        {
            var circularQueue = new CircularQueue<int>(1);
            Exception? exception = null;

            try
            {
                circularQueue.Dequeue();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.AreEqual(expected: typeof(InvalidOperationException), actual: exception?.GetType());
        }

        [Test]
        public static void EnqueueQualityTest()
        {
            var circularQueue = new CircularQueue<int>(2);
            circularQueue.Enqueue(1);
            circularQueue.Enqueue(2);
            
            Assert.IsFalse(circularQueue.IsEmpty(), "Queue is not empty");
        }

        [Test]
        public static void EnqueueQualityTestThrowsInvalidOperationException()
        {
            var circularQueue = new CircularQueue<int>(1);
            circularQueue.Enqueue(0);
            Exception? exception = null;

            try
            {
                circularQueue.Enqueue(1);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.AreEqual(expected: typeof(InvalidOperationException), actual: exception?.GetType());
        }

        [Test]
        public static void EmptyQualityTest()
        {
            var circularQueue = new CircularQueue<int>(2);
            circularQueue.Enqueue(1);
            circularQueue.Enqueue(2);

            circularQueue.Dequeue();
            circularQueue.Dequeue();

            Assert.IsTrue(circularQueue.IsEmpty(), "Queue is empty");
        }

        [Test]
        public static void FullQualityTest()
        {
            var circularQueue = new CircularQueue<int>(2);
            circularQueue.Enqueue(1);
            circularQueue.Enqueue(2);

            Assert.IsTrue(circularQueue.IsFull(), "Queue is full");
        }

        [Test]
        public static void PeekWorksCorrectly()
        {
            var circularQueue = new CircularQueue<int>(1);
            circularQueue.Enqueue(1);

            int peeked = circularQueue.Peek();

            Assert.AreEqual(expected: 1, actual: peeked);
            Assert.IsFalse(circularQueue.IsEmpty(), "Queue is empty");
        }    
        
        [Test]
        public static void PeekEmptyQueueThrowsInvalidOperationException()
        {
            var circularQueue = new CircularQueue<int>(1);
            Exception? exception = null;

            try
            {
                circularQueue.Peek();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.AreEqual(expected: typeof(InvalidOperationException), actual: exception?.GetType());
        }

        public static void GetEnumeratorTypeQualityTest()
        {
            var circularQueue = new CircularQueue<int>(2);
            circularQueue.Enqueue(1);
            circularQueue.Enqueue(2);

            IEnumerator<int>? enumeratorActual = null;

            enumeratorActual = circularQueue.GetEnumerator();

            Assert.AreEqual(expected: typeof(IEnumerator<int>), actual: enumeratorActual?.GetType());
        }

        [Test]
        public static void GetEnumeratorItemQualityTest()
        {
            var circularQueue = new CircularQueue<int>(2);
            circularQueue.Enqueue(1);
            circularQueue.Enqueue(2);

            var enumerator = circularQueue.GetEnumerator();

            int firstvalue = 0;

            if (enumerator.MoveNext())
            {
                firstvalue = enumerator.Current;
            }

            Assert.AreEqual(expected: typeof(int), actual: firstvalue.GetType());
            Assert.AreEqual(expected: 1, actual: firstvalue);
        }
    }
}
