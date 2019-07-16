using System;
using Algorithms.Sorters;
using Algorithms.Tests.Helpers;
using NUnit.Framework;

namespace Algorithms.Tests.Sorters
{
    public class SelectionSorterTests
    {
        [Test]
        public void ArraySorted([Random(0, 1000, 100, Distinct = true)]int n)
        {
            // Arrange
            var sorter = new SelectionSorter<int>();
            var intComparer = new IntComparer();
            var (correctArray, testArray) = RandomHelper.GetArrays(n);

            // Act
            sorter.Sort(testArray, intComparer);
            Array.Sort(correctArray, intComparer);

            // Assert
            Assert.AreEqual(testArray, correctArray);
        }
    }
}
