using Algorithms.Shufflers;
using Algorithms.Tests.Helpers;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Algorithms.Tests.Shufflers
{
    public static class RecursiveShufflerTests
    {
        [Test]
        public static void ArrayShuffled_NewArraySameSize(
            [Random(10, 1000, 100, Distinct = true)]
            int n)
        {
            // Arrange
            var shuffler = new RecursiveShuffler<int>();
            var (correctArray, testArray) = RandomHelper.GetArrays(n);

            // Act
            shuffler.Shuffle(testArray);

            // Assert
            testArray.Length.Should().Be(correctArray.Length);
        }

        [Test]
        public static void ArrayShuffled_NewArraySameValues(
            [Random(10, 1000, 100, Distinct = true)]
            int n)
        {
            // Arrange
            var shuffler = new RecursiveShuffler<int>();
            var (correctArray, testArray) = RandomHelper.GetArrays(n);

            // Act
            shuffler.Shuffle(testArray);

            // Assert
            testArray.Should().BeEquivalentTo(correctArray);
        }

        [Test]
        public static void ArrayShuffled_NewArraySameShuffle(
            [Random(0, 1000, 2, Distinct = true)] int n,
            [Random(1000, 10000, 5, Distinct = true)] int seed)
        {
            // Arrange
            var shuffler = new RecursiveShuffler<int>();
            var (correctArray, testArray) = RandomHelper.GetArrays(n);

            // Act
            shuffler.Shuffle(testArray, seed);
            shuffler.Shuffle(correctArray, seed);

            // Assert
            correctArray.Should().BeEquivalentTo(testArray, options => options.WithStrictOrdering());
        }
    }
}
