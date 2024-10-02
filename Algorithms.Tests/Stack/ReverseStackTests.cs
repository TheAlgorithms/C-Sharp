using Algorithms.Stack;
using NUnit.Framework;
using System.Collections.Generic;


namespace Algorithms.Tests.Stack
{
    public class ReverseStackTests
    {
        public static void Reverse<T>(Stack<T> stack)
        {
            var obj = new ReverseStack();
            obj.Reverse(stack);
        }

        [Test]
        public void Reverse_EmptyStack_DoesNotChangeStack()
        {
            // Arrange
            Stack<int> stack = new Stack<int>();

            // Act
            Reverse(stack);

            // Assert
            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Reverse_SingleElementStack_DoesNotChangeStack()
        {
            // Arrange
            Stack<int> stack = new Stack<int>();
            stack.Push(1);

            // Act
            Reverse(stack);

            // Assert
            Assert.That(stack.Count, Is.EqualTo(1));
            Assert.That(stack.Peek(), Is.EqualTo(1));
        }

        [Test]
        public void Reverse_MultipleElementStack_ReturnsCorrectOrder()
        {
            // Arrange
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            // The stack is now [3, 2, 1] (top to bottom)

            // Act
            Reverse(stack);

            // Assert
            Assert.That(stack.Count, Is.EqualTo(3));
            Assert.That(stack.Pop(), Is.EqualTo(1)); // Should return 1
            Assert.That(stack.Pop(), Is.EqualTo(2)); // Should return 2
            Assert.That(stack.Pop(), Is.EqualTo(3)); // Should return 3
        }

        [Test]
        public void Reverse_StackWithDuplicates_ReturnsCorrectOrder()
        {
            // Arrange
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(1);
            // The stack is now [1, 2, 1] (top to bottom)

            // Act
            Reverse(stack);

            // Assert
            Assert.That(stack.Count, Is.EqualTo(3));
            Assert.That(stack.Pop(), Is.EqualTo(1)); // Should return 1
            Assert.That(stack.Pop(), Is.EqualTo(2)); // Should return 2
            Assert.That(stack.Pop(), Is.EqualTo(1)); // Should return 1
        }
    }
}
