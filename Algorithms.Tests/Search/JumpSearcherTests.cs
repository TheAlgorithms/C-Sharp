using Algorithms.Search;
using NUnit.Framework;
using System;
using System.Linq;

namespace Algorithms.Tests.Search
{
    public class JumpSearcherTests
    {
        [Test]
        public void FindIndex_ItemPresent_ItemCorrect([Random(1, 1000, 100)] int n)
        {
            // Arrange
            var searcher = new JumpSearcher<int>();
            var sortedArray = Enumerable.Range(0, n).Select(x => TestContext.CurrentContext.Random.Next(1_000_000)).OrderBy(x => x).ToArray();
            var expectedIndex = TestContext.CurrentContext.Random.Next(sortedArray.Length);

            // Act
            var actualIndex = searcher.FindIndex(sortedArray, sortedArray[expectedIndex]);

            // Assert
            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [Test]
        public void FindIndex_ItemMissing_MinusOneReturned([Random(1, 1000, 10)] int n, [Random(-100, 1100, 10)] int missingItem)
        {
            // Arrange
            var searcher = new JumpSearcher<int>();
            var sortedArray = Enumerable.Range(0, n).Select(x => TestContext.CurrentContext.Random.Next(1_000_000)).Where(x => x != missingItem).OrderBy(x => x).ToArray();
            var expectedIndex = -1;

            // Act
            var actualIndex = searcher.FindIndex(sortedArray, missingItem);

            // Assert
            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [TestCase(new String[] { }, "abc")]
        [TestCase(null, "abc")]
        [TestCase(new String[] { "abc", "def", "ghi" }, null)]
        public void FindIndex_ArrayEmpty_ArrayNull_ItemNull_NullReferenceExceptionThrown(String[] sortedArray, String searchItem)
        {
            // Arrange
            var searcher = new JumpSearcher<String>();

            // Act, Assert
            _ = Assert.Throws<NullReferenceException>(() => searcher.FindIndex(sortedArray, searchItem));
        }
    }
}
