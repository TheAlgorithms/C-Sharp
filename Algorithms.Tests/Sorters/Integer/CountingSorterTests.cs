using System;
using Algorithms.Sorters.Integer;
using Algorithms.Tests.Helpers;
using NUnit.Framework;

namespace Algorithms.Tests.Sorters.Integer
{
    public static class CountingSorterTests
    {
        [Test]
        public static void SortsNonEmptyArray(
            [Random(1, 10000, 100, Distinct = true)]
            int n)
        {
            // Arrange
            var sorter = new CountingSorter();
            var (correctArray, testArray) = RandomHelper.GetArrays(n);

            // Act
            sorter.Sort(testArray);
            Array.Sort(correctArray);

            // Assert
            Assert.AreEqual(correctArray, testArray);
        }

        [Test]
        public static void SortsEmptyArray()
        {
            var sorter = new CountingSorter();
            sorter.Sort(Array.Empty<int>());
        }
    }
}
