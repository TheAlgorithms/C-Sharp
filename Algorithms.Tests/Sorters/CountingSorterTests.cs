using System;
using Algorithms.Sorters;
using Algorithms.Tests.Helpers;
using NUnit.Framework;

namespace Algorithms.Tests.Sorters
{
    public static class CountingSorterTests
    {
        [Test]
        public static void SortsArray([Random(0, 10000, 100, Distinct = true)]int n)
        {
            // Arrange
            var (correctArray, testArray) = RandomHelper.GetArrays(n);

            // Act
            CountingSorter.Sort(testArray);
            Array.Sort(correctArray);

            // Assert
            Assert.AreEqual(correctArray, testArray);
        }
    }
}