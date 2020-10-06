using Algorithms.Strings;
using NUnit.Framework;
using Utilities.Exceptions;

namespace Algorithms.Tests.Strings
{
    public static class KnuthMorrisPrattSearcherTests
    {
        [Test]
        public static void FindIndexes_ItemsPresent_PassExpected()
        {
            // Arrange
            var searcher = new KnuthMorrisPrattSearcher();
            var str = "ABABAcdeABA";
            var pat = "ABA";

            // Act
            var expectedItem = new[]{0,2,8};
            var actualItem = searcher.FindIndexes(str, pat);

            // Assert
            CollectionAssert.AreEqual(expectedItem, actualItem);
        }

        [Test]
        public static void FindIndexes_ItemsMissing_ItemNotFoundExceptionThrown()
        {
            // Arrange
            var searcher = new KnuthMorrisPrattSearcher();
            var str = "ABABA";
            var pat = "ABB";

            // Act & Assert
            _ = Assert.Throws<ItemNotFoundException>(() => searcher.FindIndexes(str, pat));
        }

        [Test]
        public static void LongestPrefixSuffixArray_PrefixSuffixOfLength1_PassExpected()
        {
            // Arrange
            var searcher = new KnuthMorrisPrattSearcher();
            var s = "ABA";

            // Act
            var expectedItem = new[] { 0,0,1 };
            var actualItem = searcher.LongestPrefixSuffixValues(s);

            // Assert
            CollectionAssert.AreEqual(expectedItem, actualItem);
        }

        [Test]
        public static void LongestPrefixSuffixArray_PrefixSuffixOfLength5_PassExpected()
        {
            // Arrange
            var searcher = new KnuthMorrisPrattSearcher();
            var s = "AABAACAABAA";

            // Act
            var expectedItem = new[] { 0, 1, 0, 1, 2, 0, 1, 2, 3, 4, 5 };
            var actualItem = searcher.LongestPrefixSuffixValues(s);

            // Assert
            CollectionAssert.AreEqual(expectedItem, actualItem);
        }

        [Test]
        public static void LongestPrefixSuffixArray_PrefixSuffixOfLength0_PassExpected()
        {
            // Arrange
            var searcher = new KnuthMorrisPrattSearcher();
            var s = "AB";

            // Act
            var expectedItem = new[] { 0, 0 };
            var actualItem = searcher.LongestPrefixSuffixValues(s);

            // Assert
            CollectionAssert.AreEqual(expectedItem, actualItem);
        }

    }
}
