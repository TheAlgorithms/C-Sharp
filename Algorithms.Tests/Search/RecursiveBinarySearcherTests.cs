using System;
using System.Collections.Generic;
using System.Linq;

using Algorithms.Search;

using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Search
{
    public static class RecursiveBinarySearcherTests
    {
        [Test]
        public static void FindIndex_ItemPresent_IndexCorrect([Random(1, 1000, 100)] int n)
        {
            // Arrange
            var subject = new RecursiveBinarySearcher<int>();
            var randomizer = Randomizer.CreateRandomizer();
            var selectedIndex = randomizer.Next(0, n);
            var collection = Enumerable.Range(0, n).Select(x => randomizer.Next(0, 1000)).OrderBy(x => x).ToList();

            // Act
            var actualIndex = subject.FindIndex(collection, collection[selectedIndex]);

            // Assert
            Assert.AreEqual(collection[selectedIndex], collection[actualIndex]);
        }

        [Test]
        public static void FindIndex_ItemMissing_MinusOneReturned([Random(0, 1000, 10)] int n, [Random(-100, 1100, 10)] int missingItem)
        {
            // Arrange
            var subject = new RecursiveBinarySearcher<int>();
            var random = Randomizer.CreateRandomizer();
            var collection = Enumerable.Range(0, n).Select(x => random.Next(0, 1000)).Where(x => x != missingItem).OrderBy(x => x).ToList();

            // Act
            var actualIndex = subject.FindIndex(collection, missingItem);

            // Assert
            Assert.AreEqual(-1, actualIndex);
        }

        [Test]
        public static void FindIndex_ArrayEmpty_MinusOneReturned([Random(100)] int itemToSearch)
        {
            // Arrange
            var subject = new RecursiveBinarySearcher<int>();
            var collection = new int[0];

            // Act
            var actualIndex = subject.FindIndex(collection, itemToSearch);

            // Assert
            Assert.AreEqual(-1, actualIndex);
        }

        [Test]
        public static void FindIndex_NullCollection_Throws()
        {
            // Arrange
            var subject = new RecursiveBinarySearcher<int>();
            var collection = (IList<int>?)null;

            // Act
            TestDelegate actDelegate = () =>
            {
                subject.FindIndex(collection, 42);
            };

            // Assert
            Assert.Throws<ArgumentNullException>(actDelegate, "collection");
        }
    }
}
