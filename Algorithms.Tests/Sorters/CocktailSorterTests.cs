using System;

using Algorithms.Sorters;
using Algorithms.Tests.Helpers;

using NUnit.Framework;

namespace Algorithms.Tests.Sorters
{
    public static class CocktailSorterTests
    {
        [Test]
        public static void SortsArray([Random(0, 1000, 100, Distinct = true)]int n)
        {
            // Arrange
            var sorter = new CocktailSorter<int>();
            var intComparer = new IntComparer();
            var (correctArray, testArray) = RandomHelper.GetArrays(n);

            // Act
            sorter.Sort(testArray, intComparer);
            Array.Sort(correctArray);

            // Assert
            Assert.AreEqual(correctArray, testArray);
        }
    }
}
