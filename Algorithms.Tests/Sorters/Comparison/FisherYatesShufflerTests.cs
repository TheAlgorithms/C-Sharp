using System;
using Algorithms.Sorters.Comparison;
using Algorithms.Tests.Helpers;
using NUnit.Framework;

namespace Algorithms.Tests.Sorters.Comparison
{
    public static class FisherYatesShufflerTests
    {
        [Test]
        public static void ArrayShuffled_NewArrayHaveSameSize(
            [Random(0, 1000, 100, Distinct = true)]
            int n)
        {
            // Arrange
            var shuffler = new FisherYatesShuffler<int>();
            var (correctArray, testArray) = RandomHelper.GetArrays(n);

            // Act
            shuffler.Shuffle(testArray);

            // Assert
            Assert.AreEqual(testArray.Length, correctArray.Length);
        }

        [Test]
        public static void ArrayShuffled_NewArrayHaveSameValues(
            [Random(0, 1000, 100, Distinct = true)]
            int n)
        {
            // Arrange
            var shuffler = new FisherYatesShuffler<int>();
            var (correctArray, testArray) = RandomHelper.GetArrays(n);

            // Act
            shuffler.Shuffle(testArray);
            Array.Sort(testArray);
            Array.Sort(correctArray);

            // Assert
            Assert.AreEqual(testArray, correctArray);
        }
    }
}
