using Algorithms.Strings;
using Algorithms.Strings.PatternMatching;
using NUnit.Framework;

namespace Algorithms.Tests.Strings;

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
        var expectedItem = new[] { 0, 2, 8 };
        var actualItem = searcher.FindIndexes(str, pat);

        // Assert
        Assert.That(actualItem, Is.EqualTo(expectedItem));
    }

    [Test]
    public static void FindIndexes_ItemsMissing_NoIndexesReturned()
    {
        // Arrange
        var searcher = new KnuthMorrisPrattSearcher();
        var str = "ABABA";
        var pat = "ABB";

        // Act & Assert
        var indexes = searcher.FindIndexes(str, pat);

        // Assert
        Assert.That(indexes, Is.Empty);
    }

    [Test]
    public static void LongestPrefixSuffixArray_PrefixSuffixOfLength1_PassExpected()
    {
        // Arrange
        var searcher = new KnuthMorrisPrattSearcher();
        var s = "ABA";

        // Act
        var expectedItem = new[] { 0, 0, 1 };
        var actualItem = searcher.FindLongestPrefixSuffixValues(s);

        // Assert
        Assert.That(actualItem, Is.EqualTo(expectedItem));
    }

    [Test]
    public static void LongestPrefixSuffixArray_PrefixSuffixOfLength5_PassExpected()
    {
        // Arrange
        var searcher = new KnuthMorrisPrattSearcher();
        var s = "AABAACAABAA";

        // Act
        var expectedItem = new[] { 0, 1, 0, 1, 2, 0, 1, 2, 3, 4, 5 };
        var actualItem = searcher.FindLongestPrefixSuffixValues(s);

        // Assert
        Assert.That(actualItem, Is.EqualTo(expectedItem));
    }

    [Test]
    public static void LongestPrefixSuffixArray_PrefixSuffixOfLength0_PassExpected()
    {
        // Arrange
        var searcher = new KnuthMorrisPrattSearcher();
        var s = "AB";

        // Act
        var expectedItem = new[] { 0, 0 };
        var actualItem = searcher.FindLongestPrefixSuffixValues(s);

        // Assert
        Assert.That(actualItem, Is.EqualTo(expectedItem));
    }
}
