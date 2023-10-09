using DataStructures.Stack;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Tests.Stack
{
    public static class QueueBasedStackTests
    {
        [Test]
        public static void PopWorksCorrectly()
        {
            //Arrange
            QueueBasedStack<char> s = new QueueBasedStack<char>();
            s.Push('A');
            s.Push('B');
            s.Push('C');
            var result = new StringBuilder();

            //Act
            for (int i = 0; i < 3; i++)
            {
                result.Append(s.Pop());
            }


            //Assert
            Assert.That("CBA", Is.EqualTo(result.ToString()));
            Assert.IsTrue(s.IsEmpty(), "Stack is Empty");
        }
        [Test]
        public static void PeekWorksCorrectly()
        {
            //Arrange
            QueueBasedStack<int> s = new QueueBasedStack<int>();
            s.Push(1);
            s.Push(2);
            s.Push(3);
            var peeked = 0;

            //Act
            for (int i = 0; i < 3; i++)
            {
                peeked = s.Peek();
            }


            //Assert
            Assert.That(3, Is.EqualTo(peeked));
            Assert.IsFalse(s.IsEmpty(), "Stack is Empty");
        }
        [Test]
        public static void PopEmptyStackThrowsInvalidOperationException()
        {
            //Arrange
            var s = new QueueBasedStack<int>();
            Exception? exception = null;

            //Act
            try
            {
                s.Pop();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.That(exception?.GetType(), Is.EqualTo(typeof(InvalidOperationException)));
        }
        [Test]
        public static void PeekEmptyStackThrowsInvalidOperationException()
        {
            //Arrange
            var s = new QueueBasedStack<int>();
            Exception? exception = null;

            //Act
            try
            {
                s.Peek();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.That(exception?.GetType(), Is.EqualTo(typeof(InvalidOperationException)));
        }
        [Test]
        public static void ClearWorksCorrectly()
        {
            // Arrange
            var s = new QueueBasedStack<int>();
            s.Push(1);
            s.Push(2);

            // Act
            s.Clear();

            // Assert
            Assert.IsTrue(s.IsEmpty(), "Queue is empty");

        }
        [Test]
        public static void LengthWorksCorrectly()
        {
            // Arrange
            var s = new QueueBasedStack<int>();
            s.Push(1);
            s.Push(2);
            var length = 0;

            // Act
            length = s.Length();

            // Assert
            Assert.That(2, Is.EqualTo(length));

        }
    }
}
