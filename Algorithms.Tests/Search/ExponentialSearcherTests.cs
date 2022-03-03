using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Search;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Search
{
    public static class ExponentialSearcherTests
    {
        [Test]
        public static void FindIndex_ItemPresent_IndexCorrect([Random(1, 1000, 100)] int n)
        {
            // Arrange
            var searcher = new ExponentialSearcher();
            var random = Randomizer.CreateRandomizer();
            var arrayToSearch = Enumerable.Range(0, n).Select(_ => random.Next(0, 1000)).OrderBy(x => x).ToArray();
            var selectedIndex = random.Next(0, n);

            // Act
            var actualIndex = searcher.FindIndex(arrayToSearch, arrayToSearch[selectedIndex]);

            // Assert
            sortedArray[actualIndex].Should().Be(sortedArray[selectedIndex]);
        }
        [Test]
        public static void FindIndex_ItemMissing_MinusOneReturned(
            [Random(0, 1000, 10)] int n,
            [Random(-100, 1100, 10)] int missingItem)
        {
            // Arrange
            var subject = new ExponentialSearcherr<int>();
            var random = Randomizer.CreateRandomizer();
            var collection = Enumerable.Range(0, n)
                .Select(_ => random.Next(0, 1000))
                .Where(x => x != missingItem)
                .OrderBy(x => x).ToList();

            // Act
            var actualIndex = subject.FindIndex(collection, missingItem);

            // Assert
            actualIndex.Should().Be(-1);
        }

        [Test]
        public static void FindIndex_ArrayEmpty_MinusOneReturned([Random(100)] int itemToSearch)
        {
            // Arrange
            var searcher = new ExponentialSearcher<int>();
            var arrayToSearch = new int[0];

            // Act
            var actualIndex = searcher.FindIndex(arrayToSearch, itemToSearch);

            // Assert
            actualIndex.Should().Be(-1);
        }
    }
}
