using Algorithms.Shufflers;
using Algorithms.Tests.Helpers;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Algorithms.Tests.Shufflers
{
    public static class FisherYatesShufflerTests
    {
        [Test]
        public static void ArrayShuffled_NewArrayHasSameSize(
            [Random(0, 1000, 100, Distinct = true)]
            int n)
        {
            // Arrange
            var shuffler = new FisherYatesShuffler<int>();
            var (correctArray, testArray) = RandomHelper.GetArrays(n);

            // Act
            shuffler.Shuffle(testArray);

            // Assert
            testArray.Length.Should().Be(correctArray.Length);
        }

        [Test]
        public static void ArrayShuffled_NewArrayHasSameValues(
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
            testArray.Should().BeEquivalentTo(correctArray);
        }
    }
}
