using System;
using System.Linq;
using DataStructures.SinglyLinkedList;
using NUnit.Framework;

namespace DataStructures.Tests
{
    public static class LinkedListTests
    {
        [Test]
        public static void LengthWorksCorrectly([Random(0, 1000, 100)]int quantity)
        {
            // Arrange
            var a = new SinglyLinkedList<int>();

            // Act
            var r = TestContext.CurrentContext.Random;
            for (var i = 0; i < quantity; i++)
            {
                _ = a.AddFirst(r.Next());
            }

            // Assert
            Assert.AreEqual(quantity, a.Length());
        }

        [Test]
        public static void LengthOnEmptyListIsZero()
        {
            // Arrange
            var a = new SinglyLinkedList<int>();

            // Act

            // Assert
            Assert.AreEqual(0, a.Length());
        }

        [Test]
        public static void GetItemsFromLinkedList()
        {
            // Arrange
            var testObj = new SinglyLinkedList<string>();
            _ = testObj.AddLast("H");
            _ = testObj.AddLast("E");
            _ = testObj.AddLast("L");
            _ = testObj.AddLast("L");
            _ = testObj.AddLast("O");

            // Act
            var items = testObj.GetListData();

            // Assert
            Assert.AreEqual(5, items.Count());
            Assert.AreEqual("O", testObj.GetElementByIndex(4));
        }

        [Test]
        public static void GetElementByIndex_IndexOutOfRange_ArgumentOutOfRangeExceptionThrown()
        {
            // Arrange
            var list = new SinglyLinkedList<int>();

            // Act
            _ = list.AddFirst(1);
            _ = list.AddFirst(2);
            _ = list.AddFirst(3);

            // Assert
            _ = Assert.Throws<ArgumentOutOfRangeException>(() => list.GetElementByIndex(-1));
            _ = Assert.Throws<ArgumentOutOfRangeException>(() => list.GetElementByIndex(3));
        }


        [Test]
        public static void RemoveItemsFromList()
        {
            // Arrange
            var testObj = new SinglyLinkedList<string>();
            _ = testObj.AddLast("X");
            _ = testObj.AddLast("H");
            _ = testObj.AddLast("E");
            _ = testObj.AddLast("L");
            _ = testObj.AddLast("L");
            _ = testObj.AddLast("I");
            _ = testObj.AddLast("O");

            // Act
            var xRemoveSucess = testObj.DeleteElement("X");
            var oRemoveSucess = testObj.DeleteElement("O");
            var eRemoveSucess = testObj.DeleteElement("E");
            var lRemoveSucess = testObj.DeleteElement("L");
            var l2RemoveSucess = testObj.DeleteElement("L");
            var l3RemoveSucess = testObj.DeleteElement("L");
            var nonExistantRemoveSucess = testObj.DeleteElement("F");

            var resultString = testObj.GetElementByIndex(0) + testObj.GetElementByIndex(1);

            // Assert
            Assert.AreEqual("HI", resultString);
            Assert.IsTrue(xRemoveSucess);
            Assert.IsTrue(oRemoveSucess);
            Assert.IsTrue(eRemoveSucess);
            Assert.IsTrue(lRemoveSucess);
            Assert.IsTrue(l2RemoveSucess);
            Assert.IsFalse(l3RemoveSucess);
            Assert.IsFalse(nonExistantRemoveSucess);
        }
    }
}
