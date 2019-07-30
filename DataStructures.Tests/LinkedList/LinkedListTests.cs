using System;
using System.Linq;
using DataStructures.LinkedList;
using NUnit.Framework;

namespace DataStructures.Tests.LinkedList
{
    public static class LinkedListTests
    {
        [Test]
        [TestCase(5, 2)]
        public static void AddsElements(int q, int firstElement)
        {
            // Arrange
            var a = new LinkedList<int>();

            // Act
            var r = new Random();
            a.AddListElement(firstElement);
            for (var i = 0; i < q; i++)
            {
                a.AddListElement(r.Next(0, 100));
            }

            var d = a.GetListData();

            // Assert
            Assert.AreEqual(6, a.Length());
            Assert.AreEqual(firstElement, d.First());
            Assert.AreEqual(firstElement, a.GetElementByIndex(0));
        }

        [Test]
        public static void LengthOnZeroElements()
        {
            // Arrange
            var a = new LinkedList<int>();

            // Act

            // Assert
            Assert.AreEqual(0, a.Length());
        }

        [Test]
        public static void GetItemsFromLinkedList()
        {
            // Arrange
            var testObj = new LinkedList<string>();
            testObj.AddListElement("H");
            testObj.AddListElement("E");
            testObj.AddListElement("L");
            testObj.AddListElement("L");
            testObj.AddListElement("O");

            // Act
            var items = testObj.GetListData();

            // Assert
            Assert.AreEqual(5, items.Count());
            Assert.AreEqual("O", testObj.GetElementByIndex(4));
        }

        [Test]
        public static void OutOfRangeIndexGetsFirstOrLastItem()
        {
            // Arrange
            var testObj = new LinkedList<string>();
            testObj.AddListElement("H");
            testObj.AddListElement("E");
            testObj.AddListElement("L");
            testObj.AddListElement("L");
            testObj.AddListElement("O");

            // Act
            var firstItem = testObj.GetElementByIndex(-1);
            var lastItem = testObj.GetElementByIndex(6);

            // Assert
            Assert.AreEqual("O", lastItem);
            Assert.AreEqual("H", firstItem);
        }


        [Test]
        public static void RemoveItemsFromList()
        {
            // Arrange
            var testObj = new LinkedList<string>();
            testObj.AddListElement("X");
            testObj.AddListElement("H");
            testObj.AddListElement("E");
            testObj.AddListElement("L");
            testObj.AddListElement("L");
            testObj.AddListElement("I");
            testObj.AddListElement("O");

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
