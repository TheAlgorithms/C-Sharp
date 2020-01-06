using System;
using System.Linq;
using DataStructures.DoublyLinkedList;
using NUnit.Framework;

namespace DataStructures.Tests {
    public static class DoublyLinkedListTests {
        [Test]
        public static void TestGetData() {
            var dll = new DoublyLinkedList<int>(new int[] { 0, 1, 2, 3, 4 });
            var arr = dll.GetData().ToArray();
            Assert.AreEqual(dll.Count, 5);
            Assert.AreEqual(new int[] { 0, 1, 2, 3, 4 }, arr);
        }

        [Test]
        public static void TestGetAt() {
            var dll = new DoublyLinkedList<int>(new int[] { 0, 1, 2, 3, 4 });
            
            var one = dll.GetAt(1);
            var three = dll.GetAt(3);
           
            Assert.AreEqual(one.Data, 1);
            Assert.AreEqual(three.Data, 3);
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>  dll.GetAt(-1)
            );
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>  dll.GetAt(5)
            );
        }

        [Test]
        public static void TestAddtion() {
            var dll = new DoublyLinkedList<int>(0);
            var one = dll.Add(1);
            dll.Add(3);
            dll.AddAfter(2, one);
            dll.Add(4);

            var arr = dll.GetData().ToArray();

            Assert.AreEqual(dll.Count, 5);
            Assert.AreEqual(new int[] { 0, 1, 2, 3, 4 }, arr);
        }

        [Test]
        public static void TestRemove() {
            var dll = new DoublyLinkedList<int>(new int[] { 0, 1, 2, 3, 4 });
            dll.RemoveHead();
            dll.Remove();

            var arr = dll.GetData().ToArray();

            Assert.AreEqual(dll.Count, 3);
            Assert.AreEqual(new int[] { 1, 2, 3 }, arr);
        }

        [Test]
        public static void TestFind() {
            var dll = new DoublyLinkedList<int>(new int[] { 0, 1, 2, 3, 4 });

            var one = dll.Find(1);
            var three = dll.Find(3);

            Assert.AreEqual(one.Data, 1);
            Assert.AreEqual(three.Data, 3);
        }

        [Test]
        public static void TestIndexOf() {
            var dll = new DoublyLinkedList<int>(new int[] { 0, 1, 2, 3, 4 });

            var one = dll.IndexOf(1);
            var three = dll.IndexOf(3);

            Assert.AreEqual(one, 1);
            Assert.AreEqual(three, 3);
        }

        [Test]
        public static void TestContains() {
            var dll = new DoublyLinkedList<int>(new int[] { 0, 1, 2, 3, 4 });

            var one = dll.Contains(1);
            var six = dll.Contains(6);

            Assert.IsTrue(one);
            Assert.IsFalse(six);
        }
    }
}