using Algorithms.Strings.PatternMatching;
using NUnit.Framework;

namespace Algorithms.Tests.Strings.PatternMatching;

public static class WildCardMatcherTests
{
    [TestCase("aab", "c*a*b", true)]
    [TestCase("aaa", "aa", false)]
    [TestCase("aaa", "a.a", true)]
    [TestCase("aaab", "aa*", false)]
    [TestCase("aaab", ".*", true)]
    [TestCase("a", "bbbb", false)]
    [TestCase("", "bbbb", false)]
    [TestCase("a", "", false)]
    [TestCase("", "", true)]
    public static void MatchPattern(string inputString, string pattern, bool expected)
    {
        // Act
        var result = WildCardMatcher.MatchPattern(inputString, pattern);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public static void MatchPatternThrowsArgumentException()
    {
        // Arrange
        var inputString = "abc";
        var pattern = "*abc";

        // Assert
        Assert.Throws<System.ArgumentException>(() => WildCardMatcher.MatchPattern(inputString, pattern));
    }
}
