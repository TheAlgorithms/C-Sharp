using Algorithms.Searches;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Linq;

namespace Algorithms.Tests.Search
{
    public class BinarySearcherTests
    {       
        [Test]
        public void FindIndex_ItemPresent_IndexCorrect([Random(1, 1000, 1000)]int n)
        {
            // Arrange
            var searcher = new BinarySearcher<int>();
            var random = Randomizer.CreateRandomizer();
            var arrayToSearch = Enumerable.Range(0, n).Select(x => random.Next(0, 1000)).OrderBy(x => x).ToArray();
            var selectedIndex = random.Next(0, n);

            // Act
            var actualIndex = searcher.FindIndex(arrayToSearch, arrayToSearch[selectedIndex]);

            // Assert
            Assert.AreEqual(arrayToSearch[selectedIndex], arrayToSearch[actualIndex]);
        }

        [Test]
        public void FindIndex_ItemMissing_MinusOneReturned([Random(0, 1000, 100)]int n, [Random(-100, 1100, 100)]int missingItem)
        {
            // Arrange
            var searcher = new BinarySearcher<int>();
            var random = Randomizer.CreateRandomizer();
            var arrayToSearch = Enumerable.Range(0, n).Select(x => random.Next(0, 1000))
                .Where(x => x != missingItem).OrderBy(x => x).ToArray();
            var expectedIndex = -1;

            // Act
            var actualIndex = searcher.FindIndex(arrayToSearch, missingItem);

            // Assert
            Assert.AreEqual(expectedIndex, actualIndex);
        }
    }
}
