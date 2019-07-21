using System;

using Algorithms.Sorters;
using Algorithms.Tests.Helpers;

using NUnit.Framework;

namespace Algorithms.Tests.Sorters
{
    public static class RadixSorterTests
    {
        [Test]
        public static void SortsArray([Random(0, 1000, 100, Distinct = true)]int n)
        {
            // Arrange
            const int bits = 4;
            var (correctArray, testArray) = RandomHelper.GetArrays(n);

            // Act
            RadixSorter.Sort(testArray, bits);
            Array.Sort(correctArray);

            // Assert
            Assert.AreEqual(correctArray, testArray);
        }
    }
}
