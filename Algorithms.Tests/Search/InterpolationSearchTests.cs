using Algorithms.Search;
using NUnit.Framework.Internal;
using NUnit.Framework;
using System;
using System.Linq;

namespace Algorithms.Tests.Search
{
    public static class InterpolationSearchTests
    {
        [Test]
        public static void FindIndex_ItemPresent_IndexCorrect([Random(1, 1000, 100)] int n)
        {
            // Arrange
            var random = Randomizer.CreateRandomizer();
            var arrayToSearch = Enumerable.Range(0, n).Select(_ => random.Next(0, 1000)).OrderBy(x => x).ToArray();
            var selectedIndex = random.Next(0, n);

            // Act
            var actualIndex = InterpolationSearch.FindIndex(arrayToSearch, arrayToSearch[selectedIndex]);

            // Assert
            Assert.AreEqual(arrayToSearch[selectedIndex], arrayToSearch[actualIndex]);
        }

        [Test]
        public static void FindIndex_ItemMissing_MinusOneReturned(
            [Random(0, 1000, 10)] int n,
            [Random(-100, 1100, 10)] int missingItem)
        {
            // Arrange
            var random = Randomizer.CreateRandomizer();
            var arrayToSearch = Enumerable.Range(0, n)
                .Select(_ => random.Next(0, 1000))
                .Where(x => x != missingItem)
                .OrderBy(x => x).ToArray();

            // Act
            var actualIndex = InterpolationSearch.FindIndex(arrayToSearch, missingItem);

            // Assert
            Assert.AreEqual(-1, actualIndex);
        }

        [Test]
        public static void FindIndex_ArrayEmpty_MinusOneReturned([Random(100)] int itemToSearch)
        {
            // Arrange
            var arrayToSearch = new int[0];

            // Act
            var actualIndex = InterpolationSearch.FindIndex(arrayToSearch, itemToSearch);

            // Assert
            Assert.AreEqual(-1, actualIndex);
        }
    }
}
