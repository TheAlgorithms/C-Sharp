using System;
using System.Linq;
using Algorithms.Search;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Search
{
    public class LinearSearcherTests
    {
        [Test]
        public void Find_ItemPresent_ItemCorrect([Random(0, 1_000_000, 1_000)]int n)
        {
            // Arrange
            var searcher = new LinearSearcher<int>();
            var random = Randomizer.CreateRandomizer();
            var arrayToSearch = Enumerable.Range(0, n).Select(x => random.Next(0, 1000)).ToArray();

            // Act
            var expectedItem = Array.Find(arrayToSearch, x => x == arrayToSearch[n / 2]);
            var actualItem = searcher.Find(arrayToSearch, x => x == arrayToSearch[n / 2]);

            // Assert
            Assert.AreEqual(expectedItem, actualItem);
        }

        [Test]
        public void FindIndex_ItemPresent_IndexCorrect([Random(0, 1_000_000, 1_000)]int n)
        {
            // Arrange
            var searcher = new LinearSearcher<int>();
            var random = Randomizer.CreateRandomizer();
            var arrayToSearch = Enumerable.Range(0, n).Select(x => random.Next(0, 1000)).ToArray();

            // Act
            var expectedIndex = Array.FindIndex(arrayToSearch, x => x == arrayToSearch[n / 2]);
            var actualIndex = searcher.FindIndex(arrayToSearch, x => x == arrayToSearch[n / 2]);

            // Assert
            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [Test]
        public void Find_ItemMissing_ItemNotFoundExceptionThrown([Random(0, 1_000_000, 1_000)]int n)
        {
            // Arrange
            var searcher = new LinearSearcher<int>();
            var random = Randomizer.CreateRandomizer();
            var arrayToSearch = Enumerable.Range(0, n).Select(x => random.Next(0, 1000)).ToArray();

            // Act
            // Assert
            _ = Assert.Throws(typeof(ItemNotFoundException), () => searcher.Find(arrayToSearch, x => false));
        }

        [Test]
        public void FindIndex_ItemMissing_MinusOneReturned([Random(0, 1_000_000, 1_000)]int n)
        {
            // Arrange
            var searcher = new LinearSearcher<int>();
            var random = Randomizer.CreateRandomizer();
            var arrayToSearch = Enumerable.Range(0, n).Select(x => random.Next(0, 1000)).ToArray();
            var expectedIndex = -1;

            // Act
            var actualIndex = searcher.FindIndex(arrayToSearch, x => false);

            // Assert
            Assert.AreEqual(expectedIndex, actualIndex);
        }
    }
}
