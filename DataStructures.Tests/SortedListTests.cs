using System.Collections.Generic;
using System.Linq;

using DataStructures.Helpers;
using DataStructures.SortedList;

using NUnit.Framework;

namespace DataStructures.Tests
{
    public static class SortedListTests
    {
        [Test]
        public static void ListIsSorted([Random(0, 1000, 100, Distinct = true)] int n)
        {
            // Arrange
            var sorter = new SortedList<int>();
            var intComparer = new IntComparer();
            var (correctList, testList) = RandomHelper.GetLists(n);

            // Act
            sorter.Sort(testList, intComparer);
            correctList.Sort(0, correctList.Count, intComparer);

            //Assert
            Assert.AreEqual(testList, correctList);

        }

        [Test]
        public static void InsertItemIntoList()
        {
            //Arrange
            List<int> list = new List<int>{ 100, 200, 300, 400, 500 };
            SortedList<int> sortedList = new SortedList<int>(list);
            //Act
            sortedList.Insert(200);
            sortedList.Insert(350);
            list.Add(200);
            list.Add(350);
            //Assert
            Assert.AreEqual(sortedList.Count<int>(), list.Count<int>());
        }


        [Test]
        public static void RemoveItemFromList()
        {
            //Arrange
            List<int> list = new List<int>{ 100, 200, 300, 400, 500 };
            SortedList<int> sortedList = new SortedList<int>(list);           
            //Act
            sortedList.Remove(300);
            list.Remove(300);
            //Assert
            Assert.AreEqual(list,sortedList);
        }

        [Test]
        public static void IsItemOnTheList()
        {
            //Arrange
            List<int> list = new List<int> { 100, 200, 300, 400, 500 };
            SortedList<int> sortedList = new SortedList<int>(list);
            //Act
            int actual = sortedList.Search(500);
            int expected = 4;
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
