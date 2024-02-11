using Algorithms.Strings;
using NUnit.Framework;

namespace Algorithms.Tests.Strings;

public static class PalindromeTests
{
    [TestCase("Anna")]
    [TestCase("A Santa at Nasa")]
    public static void TextIsPalindrome_TrueExpected(string text)
    {
        // Arrange
        // Act
        var isPalindrome = Palindrome.IsStringPalindrome(text);

        // Assert
        Assert.That(isPalindrome, Is.True);
    }

    [TestCase("hallo")]
    [TestCase("Once upon a time")]
    public static void TextNotPalindrome_FalseExpected(string text)
    {
        // Arrange
        // Act
        var isPalindrome = Palindrome.IsStringPalindrome(text);

        // Assert
        Assert.That(isPalindrome, Is.False);
    }
}
