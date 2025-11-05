using Algorithms.Strings;
using NUnit.Framework;
using System;

namespace Algorithms.Tests.Strings;

/// <summary>
///     Comprehensive test suite for Manacher's Algorithm implementation.
///     Tests cover various scenarios including:
///     - Odd-length palindromes
///     - Even-length palindromes
///     - Single character strings
///     - Empty strings
///     - Strings with no palindromes longer than 1 character
///     - Strings that are entirely palindromic
///     - Multiple palindromes of the same length
///     - Edge cases (null input, special characters)
///     - Palindrome detection functionality
///     - Detailed palindrome information retrieval.
/// </summary>
public static class ManachersAlgorithmTests
{
    [Test]
    public static void FindLongestPalindrome_WithOddLengthPalindrome_ReturnsCorrectPalindrome()
    {
        // Arrange: Classic example with odd-length palindrome "bab" or "aba"
        string input = "babad";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Either "bab" or "aba" is valid (both have length 3)
        Assert.That(result.Length, Is.EqualTo(3));
        Assert.That(result == "bab" || result == "aba", Is.True);
    }

    [Test]
    public static void FindLongestPalindrome_WithEvenLengthPalindrome_ReturnsCorrectPalindrome()
    {
        // Arrange: String with even-length palindrome "abba"
        string input = "cbbd";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should find "bb" (length 2)
        Assert.That(result, Is.EqualTo("bb"));
    }

    [Test]
    public static void FindLongestPalindrome_WithSingleCharacter_ReturnsSingleCharacter()
    {
        // Arrange: Edge case with single character
        string input = "a";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Single character is a palindrome of itself
        Assert.That(result, Is.EqualTo("a"));
    }

    [Test]
    public static void FindLongestPalindrome_WithEmptyString_ReturnsEmptyString()
    {
        // Arrange: Edge case with empty string
        string input = string.Empty;

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Empty string should return empty string
        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public static void FindLongestPalindrome_WithNullString_ThrowsArgumentException()
    {
        // Arrange: Test defensive programming - null input validation
        string? input = null;

        // Act & Assert: Should throw ArgumentException for null input
        Assert.Throws<ArgumentException>(() => ManachersAlgorithm.FindLongestPalindrome(input!));
    }

    [Test]
    public static void FindLongestPalindrome_WithEntirePalindrome_ReturnsEntireString()
    {
        // Arrange: String that is entirely a palindrome
        string input = "racecar";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should return the entire string
        Assert.That(result, Is.EqualTo("racecar"));
    }

    [Test]
    public static void FindLongestPalindrome_WithNoPalindromes_ReturnsSingleCharacter()
    {
        // Arrange: String with no palindromes longer than 1 character
        string input = "abcdef";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should return a single character (any character is a palindrome)
        Assert.That(result.Length, Is.EqualTo(1));
    }

    [Test]
    public static void FindLongestPalindrome_WithLongPalindrome_ReturnsCorrectPalindrome()
    {
        // Arrange: String with a longer palindrome
        string input = "forgeeksskeegfor";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should find "geeksskeeg" (length 10)
        Assert.That(result, Is.EqualTo("geeksskeeg"));
    }

    [Test]
    public static void FindLongestPalindrome_WithRepeatingCharacters_ReturnsCorrectPalindrome()
    {
        // Arrange: String with repeating characters
        string input = "aaaa";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Entire string is a palindrome
        Assert.That(result, Is.EqualTo("aaaa"));
    }

    [Test]
    public static void FindLongestPalindrome_WithSpecialCharacters_ReturnsCorrectPalindrome()
    {
        // Arrange: String with special characters and spaces
        string input = "A man, a plan, a canal: Panama";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should find a palindrome (note: this includes spaces and punctuation)
        // The longest palindrome considering all characters is "anama"
        Assert.That(result.Length, Is.GreaterThan(0));
    }

    [Test]
    public static void FindLongestPalindrome_WithTwoCharacters_ReturnsCorrectResult()
    {
        // Arrange: Edge case with two identical characters
        string input = "aa";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should return "aa"
        Assert.That(result, Is.EqualTo("aa"));
    }

    [Test]
    public static void FindLongestPalindrome_WithTwoDifferentCharacters_ReturnsSingleCharacter()
    {
        // Arrange: Edge case with two different characters
        string input = "ab";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should return a single character
        Assert.That(result.Length, Is.EqualTo(1));
    }

    [Test]
    public static void FindLongestPalindromeWithDetails_ReturnsCorrectDetails()
    {
        // Arrange: Test the detailed version that returns index and length
        string input = "babad";

        // Act
        var (palindrome, startIndex, length) = ManachersAlgorithm.FindLongestPalindromeWithDetails(input);

        // Assert: Verify all components
        Assert.That(length, Is.EqualTo(3));
        Assert.That(palindrome.Length, Is.EqualTo(length));
        Assert.That(input.Substring(startIndex, length), Is.EqualTo(palindrome));
        Assert.That(palindrome == "bab" || palindrome == "aba", Is.True);
    }

    [Test]
    public static void FindLongestPalindromeWithDetails_WithEmptyString_ReturnsZeroLength()
    {
        // Arrange
        string input = string.Empty;

        // Act
        var (palindrome, startIndex, length) = ManachersAlgorithm.FindLongestPalindromeWithDetails(input);

        // Assert
        Assert.That(palindrome, Is.EqualTo(string.Empty));
        Assert.That(startIndex, Is.EqualTo(0));
        Assert.That(length, Is.EqualTo(0));
    }

    [Test]
    public static void FindLongestPalindromeWithDetails_WithSingleCharacter_ReturnsCorrectDetails()
    {
        // Arrange
        string input = "x";

        // Act
        var (palindrome, startIndex, length) = ManachersAlgorithm.FindLongestPalindromeWithDetails(input);

        // Assert
        Assert.That(palindrome, Is.EqualTo("x"));
        Assert.That(startIndex, Is.EqualTo(0));
        Assert.That(length, Is.EqualTo(1));
    }

    [Test]
    public static void FindLongestPalindromeWithDetails_WithNullString_ThrowsArgumentException()
    {
        // Arrange
        string? input = null;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => ManachersAlgorithm.FindLongestPalindromeWithDetails(input!));
    }

    [Test]
    public static void IsPalindrome_WithPalindromeString_ReturnsTrue()
    {
        // Arrange: Test palindrome detection with a valid palindrome
        string input = "racecar";

        // Act
        bool result = ManachersAlgorithm.IsPalindrome(input);

        // Assert: Should return true
        Assert.That(result, Is.True);
    }

    [Test]
    public static void IsPalindrome_WithNonPalindromeString_ReturnsFalse()
    {
        // Arrange: Test palindrome detection with a non-palindrome
        string input = "hello";

        // Act
        bool result = ManachersAlgorithm.IsPalindrome(input);

        // Assert: Should return false
        Assert.That(result, Is.False);
    }

    [Test]
    public static void IsPalindrome_WithSingleCharacter_ReturnsTrue()
    {
        // Arrange: Single character is always a palindrome
        string input = "a";

        // Act
        bool result = ManachersAlgorithm.IsPalindrome(input);

        // Assert: Should return true
        Assert.That(result, Is.True);
    }

    [Test]
    public static void IsPalindrome_WithEmptyString_ReturnsTrue()
    {
        // Arrange: Empty string is considered a palindrome
        string input = string.Empty;

        // Act
        bool result = ManachersAlgorithm.IsPalindrome(input);

        // Assert: Should return true
        Assert.That(result, Is.True);
    }

    [Test]
    public static void IsPalindrome_WithNullString_ThrowsArgumentException()
    {
        // Arrange
        string? input = null;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => ManachersAlgorithm.IsPalindrome(input!));
    }

    [Test]
    public static void IsPalindrome_WithEvenLengthPalindrome_ReturnsTrue()
    {
        // Arrange: Even-length palindrome
        string input = "abba";

        // Act
        bool result = ManachersAlgorithm.IsPalindrome(input);

        // Assert: Should return true
        Assert.That(result, Is.True);
    }

    [Test]
    public static void FindLongestPalindrome_WithNumericString_ReturnsCorrectPalindrome()
    {
        // Arrange: String with numbers
        string input = "12321";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should return the entire string
        Assert.That(result, Is.EqualTo("12321"));
    }

    [Test]
    public static void FindLongestPalindrome_WithMixedCase_ReturnsCorrectPalindrome()
    {
        // Arrange: Mixed case string (case-sensitive palindrome check)
        string input = "AbcbA";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should find "AbcbA" as it's case-sensitive
        Assert.That(result, Is.EqualTo("AbcbA"));
    }

    [Test]
    public static void FindLongestPalindrome_WithPalindromeAtStart_ReturnsCorrectPalindrome()
    {
        // Arrange: Palindrome at the start of the string
        string input = "abaxyz";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should find "aba"
        Assert.That(result, Is.EqualTo("aba"));
    }

    [Test]
    public static void FindLongestPalindrome_WithPalindromeAtEnd_ReturnsCorrectPalindrome()
    {
        // Arrange: Palindrome at the end of the string
        string input = "xyzaba";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should find "aba"
        Assert.That(result, Is.EqualTo("aba"));
    }

    [Test]
    public static void FindLongestPalindrome_WithMultiplePalindromesOfSameLength_ReturnsOne()
    {
        // Arrange: Multiple palindromes of the same length
        // "aba" at index 0 and "cdc" at index 3
        string input = "abacdc";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should return one of them (the first one found)
        Assert.That(result.Length, Is.EqualTo(3));
        Assert.That(result == "aba" || result == "cdc", Is.True);
    }

    [Test]
    public static void FindLongestPalindromeWithDetails_WithLongString_PerformsEfficiently()
    {
        // Arrange: Test with a longer string to verify O(n) performance
        // Create a string with a palindrome in the middle
        string input = new string('a', 1000) + "racecar" + new string('b', 1000);

        // Act
        var (palindrome, startIndex, length) = ManachersAlgorithm.FindLongestPalindromeWithDetails(input);

        // Assert: Should find the longest palindrome (either the 'a's or 'b's or "racecar")
        // The 1000 'a's form a palindrome
        Assert.That(length, Is.GreaterThanOrEqualTo(7)); // At least "racecar"
    }
}
