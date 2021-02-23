using DataStructures.Stack;

using NUnit.Framework;

namespace DataStructures.Tests.Stack
{
    public static class ListBasedStackTests
    {
        [Test]
        public static void CountTest()
        {
            var stack = new ListBasedStack<int>(new[] { 0, 1, 2, 3, 4 });
            Assert.IsTrue(stack.Count == 5);
        }

        [Test]
        public static void ClearTest()
        {
            var stack = new ListBasedStack<int>(new[] { 0, 1, 2, 3, 4 });
            stack.Clear();
            Assert.IsTrue(stack.Count == 0);
        }

        [Test]
        public static void ContainsTest()
        {
            var stack = new ListBasedStack<int>(new[] { 0, 1, 2, 3, 4 });
            Assert.IsTrue(stack.Contains(0));
            Assert.IsTrue(stack.Contains(1));
            Assert.IsTrue(stack.Contains(2));
            Assert.IsTrue(stack.Contains(3));
            Assert.IsTrue(stack.Contains(4));
        }

        [Test]
        public static void PeekTest()
        {
            var stack = new ListBasedStack<int>(new[] { 0, 1, 2, 3, 4 });
            Assert.IsTrue(stack.Peek() == 4);
            Assert.IsTrue(stack.Peek() == 4);
            Assert.IsTrue(stack.Peek() == 4);
        }

        [Test]
        public static void PopTest()
        {
            var stack = new ListBasedStack<int>(new[] { 0, 1, 2, 3, 4 });
            Assert.IsTrue(stack.Pop() == 4);
            Assert.IsTrue(stack.Pop() == 3);
            Assert.IsTrue(stack.Pop() == 2);
            Assert.IsTrue(stack.Pop() == 1);
            Assert.IsTrue(stack.Pop() == 0);
        }

        [Test]
        public static void PushTest()
        {
            var stack = new ListBasedStack<int>();
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
    }
}