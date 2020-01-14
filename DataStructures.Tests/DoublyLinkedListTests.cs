using System;
using System.Linq;
using DataStructures.DoublyLinkedList;
using NUnit.Framework;

namespace DataStructures.Tests
{
    public static class DoublyLinkedListTests
    {
        [Test]
        public static void TestGetData()
        {
            var dll = new DoublyLinkedList<int>(new [] { 0, 1, 2, 3, 4 });
            var arr = dll.GetData().ToArray();

            Assert.AreEqual(dll.Count, 5);
            Assert.AreEqual(new [] { 0, 1, 2, 3, 4 }, arr);
        }

        [Test]
        public static void TestGetAt()
        {
            var dll = new DoublyLinkedList<int>(new [] { 0, 1, 2, 3, 4 });

            var one = dll.GetAt(1);
            var three = dll.GetAt(3);

            Assert.AreEqual(one.Data, 1);
            Assert.AreEqual(three.Data, 3);
            Assert.Throws<ArgumentOutOfRangeException>(
                () => dll.GetAt(-1)
            );
            Assert.Throws<ArgumentOutOfRangeException>(
                () => dll.GetAt(5)
            );
        }

        [Test]
        public static void TestAddtion()
        {
            var dll = new DoublyLinkedList<int>(0);
            var one = dll.Add(1);

            dll.Add(3);
            dll.AddAfter(2, one);
            dll.Add(4);

            var arr = dll.GetData().ToArray();

            Assert.AreEqual(dll.Count, 5);
            Assert.AreEqual(new [] { 0, 1, 2, 3, 4 }, arr);
        }

        [Test]
        public static void TestRemove()
        {
            var dll = new DoublyLinkedList<int>(new [] { 0, 1, 2, 3, 4 });

            dll.RemoveNode(dll.Find(2));
            dll.RemoveHead();
            dll.Remove();

            var arr = dll.GetData().ToArray();

            Assert.AreEqual(dll.Count, 2);
            Assert.AreEqual(new [] { 1, 3 }, arr);
        }

        [Test]
        public static void TestFind()
        {
            var dll = new DoublyLinkedList<int>(new [] { 0, 1, 2, 3, 4 });

            var one = dll.Find(1);
            var three = dll.Find(3);

            Assert.AreEqual(one.Data, 1);
            Assert.AreEqual(three.Data, 3);
        }

        [Test]
        public static void TestIndexOf()
        {
            var dll = new DoublyLinkedList<int>(new [] { 0, 1, 2, 3, 4 });

            var one = dll.IndexOf(1);
            var three = dll.IndexOf(3);

            Assert.AreEqual(one, 1);
            Assert.AreEqual(three, 3);
        }

        [Test]
        public static void TestContains()
        {
            var dll = new DoublyLinkedList<int>(new [] { 0, 1, 2, 3, 4 });

            var one = dll.Contains(1);
            var six = dll.Contains(6);

            Assert.IsTrue(one);
            Assert.IsFalse(six);
        }

        [Test]
        public static void TestReverse()
        {
            var dll = new DoublyLinkedList<int>(new [] { 0, 1, 2, 3, 4 });
            dll.Reverse();
            var arr = dll.GetData().ToArray();

            var empty = new DoublyLinkedList<int>(new int[] {});
            empty.Reverse();
            var emptyArr = empty.GetData().ToArray();

            Assert.AreEqual(arr, new [] { 4, 3, 2, 1, 0 });
            Assert.AreEqual(emptyArr, new int[] {});
        }

        [Test]
        public static void TestGetDataReversed()
        {
            var dll = new DoublyLinkedList<int>(new [] { 0, 1, 2, 3, 4 });
            var arr = dll.GetData().ToArray();
            var reversedArr = dll.GetDataReversed().ToArray();

            Assert.AreEqual(arr, new [] { 0, 1, 2, 3, 4 });
            Assert.AreEqual(reversedArr, new [] { 4, 3, 2, 1, 0 });
        }
    }
}
