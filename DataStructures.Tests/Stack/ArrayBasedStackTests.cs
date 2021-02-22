using DataStructures.Stack;

using NUnit.Framework;

namespace DataStructures.Tests.Stack
{
    public static class ArrayBasedStackTests
    {
        const string StackEmptyErrorMessage = "Stack is empty";

        [Test]
        public static void CountTest()
        {
            var stack = new ArrayBasedStack<int>(new[] { 0, 1, 2, 3, 4 });
            Assert.IsTrue(stack.Top == 4);
        }

        [Test]
        public static void ClearTest()
        {
            var stack = new ArrayBasedStack<int>(new[] { 0, 1, 2, 3, 4 });
            stack.Clear();
            Assert.IsTrue(stack.Top == -1);
        }

        [Test]
        public static void ContainsTest()
        {
            var stack = new ArrayBasedStack<int>(new[] { 0, 1, 2, 3, 4 });
            Assert.IsTrue(stack.Contains(0));
            Assert.IsTrue(stack.Contains(1));
            Assert.IsTrue(stack.Contains(2));
            Assert.IsTrue(stack.Contains(3));
            Assert.IsTrue(stack.Contains(4));
        }

        [Test]
        public static void PeekTest()
        {
            var stack = new ArrayBasedStack<int>(new[] { 0, 1, 2, 3, 4 });
            Assert.IsTrue(stack.Peek() == 4);
            Assert.IsTrue(stack.Peek() == 4);
            Assert.IsTrue(stack.Peek() == 4);
        }

        [Test]
        public static void PopTest()
        {
            var stack = new ArrayBasedStack<int>(new[] { 0, 1, 2, 3, 4 });
            Assert.IsTrue(stack.Pop() == 4);
            Assert.IsTrue(stack.Pop() == 3);
            Assert.IsTrue(stack.Pop() == 2);
            Assert.IsTrue(stack.Pop() == 1);
            Assert.IsTrue(stack.Pop() == 0);
        }

        [Test]
        public static void PushTest()
        {
            var stack = new ArrayBasedStack<int>();
            stack.Push(0);
            Assert.IsTrue(stack.Peek() == 0);
            stack.Push(1);
            Assert.IsTrue(stack.Peek() == 1);
            stack.Push(2);
            Assert.IsTrue(stack.Peek() == 2);
            stack.Push(3);
            Assert.IsTrue(stack.Peek() == 3);
            stack.Push(4);
            Assert.IsTrue(stack.Peek() == 4);
        }

        [Test]
        public static void AutomaticResizesTest()
        {
            var stack = new ArrayBasedStack<int>();
            stack.Capacity = 2;
            stack.Push(0);
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            Assert.IsTrue(stack.Capacity > 2);
        }

        [Test]
        public static void ShouldThrowStackEmptyExceptionOnEmptyPopTest()
        {
            var stack = new ArrayBasedStack<int>();

            Assert.Catch(() => stack.Pop(), StackEmptyErrorMessage);
        }

        [Test]
        public static void ShouldThrowStackEmptyExceptionOnEmptyPeekTest()
        {
            var stack = new ArrayBasedStack<int>();

            Assert.Catch(() => stack.Peek(), StackEmptyErrorMessage);
        }

    }
}