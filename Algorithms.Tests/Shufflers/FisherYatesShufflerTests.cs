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

        [Test]
        public static void ArrayShuffled_SameArraySameSeed(
            [Random(0, 1000, 10, Distinct = true)]
            int n,
            [Random(0, 1000000, 10, Distinct = true)]
            int seed
            )
        {
            // Arrange
            var shuffler = new FisherYatesShuffler<int>();
            var (arr1, arr2) = RandomHelper.GetArrays(n);


            // Act
            shuffler.Shuffle(arr1, seed);
            shuffler.Shuffle(arr2, seed);

            // Assert
            arr1.Should().Equal(arr2);
        }

        [Test]
        public static void ArrayShuffled_SameArrayDiferentSeed(
            [Random(0, 1000, 5, Distinct = true)]
            int n,
            [Random(0, 1000000, 5, Distinct = true)]
            int seed1,
             [Random(0, 1000000, 5, Distinct = true)]
            int seed2
            )
        {
            // Arrange
            var shuffler = new FisherYatesShuffler<int>();
            var (arr1, arr2) = RandomHelper.GetArrays(n);


            // Act
            shuffler.Shuffle(arr1, seed1);
            shuffler.Shuffle(arr2, seed2);

            // Assert
            arr1.Should().NotEqual(arr2);
        }
    }
}
