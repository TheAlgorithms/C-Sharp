using System.Linq;
using Algorithms.Strings;
using Algorithms.Strings.PatternMatching;
using NUnit.Framework;

namespace Algorithms.Tests.Strings;

public static class NaiveStringSearchTests
{
    [Test]
    public static void ThreeMatchesFound_PassExpected()
    {
        // Arrange
        var pattern = "ABB";
        var content = "ABBBAAABBAABBBBAB";

        // Act
        var expectedOccurrences = new[] { 0, 6, 10 };
        var actualOccurrences = NaiveStringSearch.NaiveSearch(content, pattern);
        var sequencesAreEqual = expectedOccurrences.SequenceEqual(actualOccurrences);

        // Assert
        Assert.That(sequencesAreEqual, Is.True);
    }

    [Test]
    public static void OneMatchFound_PassExpected()
    {
        // Arrange
        var pattern = "BAAB";
        var content = "ABBBAAABBAABBBBAB";

        // Act
        var expectedOccurrences = new[] { 8 };
        var actualOccurrences = NaiveStringSearch.NaiveSearch(content, pattern);
        var sequencesAreEqual = expectedOccurrences.SequenceEqual(actualOccurrences);

        // Assert
        Assert.That(sequencesAreEqual, Is.True);
    }

    [Test]
    public static void NoMatchFound_PassExpected()
    {
        // Arrange
        var pattern = "XYZ";
        var content = "ABBBAAABBAABBBBAB";

        // Act
        var expectedOccurrences = new int[0];
        var actualOccurrences = NaiveStringSearch.NaiveSearch(content, pattern);
        var sequencesAreEqual = expectedOccurrences.SequenceEqual(actualOccurrences);

        // Assert
        Assert.That(sequencesAreEqual, Is.True);
    }
}
