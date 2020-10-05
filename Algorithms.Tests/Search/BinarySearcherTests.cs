using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Searches;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Search
{
    public static class BinarySearcherTests
    {
        [Test]
        public static void FindIndex_ItemPresent_IndexCorrect([Random(1, 1000, 100)] int n)
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
        public static void FindIndex_ItemMissing_MinusOneReturned([Random(0, 1000, 10)] int n, [Random(-100, 1100, 10)] int missingItem)
        {
            // Arrange
            var searcher = new BinarySearcher<int>();
            var random = Randomizer.CreateRandomizer();
            var arrayToSearch = Enumerable.Range(0, n).Select(x => random.Next(0, 1000)).Where(x => x != missingItem).OrderBy(x => x).ToArray();

            // Act
            var actualIndex = searcher.FindIndex(arrayToSearch, missingItem);

            // Assert
            Assert.AreEqual(-1, actualIndex);
        }

        [Test]
        public static void FindIndex_ArrayEmpty_MinusOneReturned([Random(100)] int itemToSearch)
        {
            // Arrange
            var searcher = new BinarySearcher<int>();
            var arrayToSearch = new int[0];

            // Act
            var actualIndex = searcher.FindIndex(arrayToSearch, itemToSearch);

            // Assert
            Assert.AreEqual(-1, actualIndex);
        }

        [Test]
        public static void FindIndexRecursive_ItemPresent_IndexCorrect([Random(1, 1000, 100)] int n)
        {
            // Arrange
            var searcher = new BinarySearcher<int>();
            var random = Randomizer.CreateRandomizer();
            var arrayToSearch = Enumerable.Range(0, n).Select(x => random.Next(0, 1000)).OrderBy(x => x).ToArray();
            var selectedIndex = random.Next(0, n);

            // Act
            var actualIndex = searcher.FindIndexRecursive(arrayToSearch, arrayToSearch[selectedIndex]);

            // Assert
            Assert.AreEqual(arrayToSearch[selectedIndex], arrayToSearch[actualIndex]);
        }

        [Test]
        public static void FindIndexRecursive_ItemMissing_MinusOneReturned([Random(0, 1000, 10)] int n, [Random(-100, 1100, 10)] int missingItem)
        {
            // Arrange
            var searcher = new BinarySearcher<int>();
            var random = Randomizer.CreateRandomizer();
            var arrayToSearch = Enumerable.Range(0, n).Select(x => random.Next(0, 1000)).Where(x => x != missingItem).OrderBy(x => x).ToArray();

            // Act
            var actualIndex = searcher.FindIndexRecursive(arrayToSearch, missingItem);

            // Assert
            Assert.AreEqual(-1, actualIndex);
        }

        [Test]
        public static void FindIndexRecursive_ArrayEmpty_MinusOneReturned([Random(100)] int itemToSearch)
        {
            // Arrange
            var searcher = new BinarySearcher<int>();
            var arrayToSearch = new int[0];

            // Act
            var actualIndex = searcher.FindIndexRecursive(arrayToSearch, itemToSearch);

            // Assert
            Assert.AreEqual(-1, actualIndex);
        }

        [Test]
        public static void FindIndexRecursive_NullCollection_Throws()
        {
            // Arrange
            var searcher = new BinarySearcher<int>();
            var arrayToSearch = (IList<int>?)null;

            // Act
            TestDelegate actDelegate = () =>
            {
                searcher.FindIndexRecursive(arrayToSearch, 42);
            };

            // Assert
            Assert.Throws<ArgumentNullException>(actDelegate, "collection");
        }
    }
}
