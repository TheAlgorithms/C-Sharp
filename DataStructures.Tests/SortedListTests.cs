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
    }
}
