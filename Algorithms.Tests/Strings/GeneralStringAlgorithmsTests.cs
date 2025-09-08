using Algorithms.Strings;

namespace Algorithms.Tests.Strings;

public static class GeneralStringAlgorithmsTests
{
    [TestCase("Griffith", 'f', 2)]
    [TestCase("Randomwoooord", 'o', 4)]
    [TestCase("Control", 'C', 1)]
    public static void MaxCountCharIsObtained(string text, char expectedSymbol, int expectedCount)
    {
        // Arrange
        // Act
        var (symbol, count) = GeneralStringAlgorithms.FindLongestConsecutiveCharacters(text);

        // Assert
        Assert.That(symbol, Is.EqualTo(expectedSymbol));
        Assert.That(count, Is.EqualTo(expectedCount));
    }
}
