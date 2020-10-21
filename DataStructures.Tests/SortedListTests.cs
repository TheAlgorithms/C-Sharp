using System.Collections.Generic;

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

            bool actualflag = false;
            //Act
            actualflag= sortedList.Insert(250);

            bool expectedflag = true;
            //Assert
            Assert.AreEqual(expectedflag, actualflag);
        }


        [Test]
        public static void RemoveItemFromList()
        {
            //Arrange
            List<int> list = new List<int>{ 100, 200, 300, 400, 500 };
            SortedList<int> sortedList = new SortedList<int>(list);

            bool actualflag = false;
            //Act
            actualflag = sortedList.Remove(300);

            bool expectedflag = true;
            //Assert
            Assert.AreEqual(expectedflag, actualflag);
        }
    }
}
