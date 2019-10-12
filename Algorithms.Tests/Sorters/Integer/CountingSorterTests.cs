using System;
using Algorithms.Sorters.Integer;
using Algorithms.Tests.Helpers;
using NUnit.Framework;

namespace Algorithms.Tests.Sorters.Integer
{
    public static class CountingSorterTests
    {
        [Test]
        public static void SortsArray([Random(0, 10000, 100, Distinct = true)]int n)
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
    }
}