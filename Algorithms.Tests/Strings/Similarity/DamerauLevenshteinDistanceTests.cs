using Algorithms.Strings.Similarity;
using NUnit.Framework;

namespace Algorithms.Tests.Strings.Similarity;

[TestFixture]
public class DamerauLevenshteinDistanceTests
{
    [Test]
    public void Calculate_IdenticalStrings_ReturnsZero()
    {
        var str1 = "test";
        var str2 = "test";
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(0), "Identical strings should have a Damerau-Levenshtein distance of 0.");
    }

    [Test]
    public void Calculate_CompletelyDifferentStrings_ReturnsLengthOfLongestString()
    {
        var str1 = "abc";
        var str2 = "xyz";
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(3),"Completely different strings should have a Damerau-Levenshtein distance equal to the length of the longest string.");
    }

    [Test]
    public void Calculate_OneEmptyString_ReturnsLengthOfOtherString()
    {
        var str1 = "test";
        var str2 = "";
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(4),"One empty string should have a Damerau-Levenshtein distance equal to the length of the other string.");
    }

    [Test]
    public void Calculate_BothEmptyStrings_ReturnsZero()
    {
        var str1 = "";
        var str2 = "";
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(0), "Both empty strings should have a Damerau-Levenshtein distance of 0.");
    }

    [Test]
    public void Calculate_DifferentLengths_ReturnsCorrectValue()
    {
        var str1 = "short";
        var str2 = "longer";
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(6), "Strings of different lengths should return the correct Damerau-Levenshtein distance.");
    }

    [Test]
    public void Calculate_SpecialCharacters_ReturnsCorrectValue()
    {
        var str1 = "hello!";
        var str2 = "hello?";
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(1), "Strings with special characters should return the correct Damerau-Levenshtein distance.");
    }

    [Test]
    public void Calculate_DifferentCases_ReturnsCorrectValue()
    {
        var str1 = "Hello";
        var str2 = "hello";
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(1), "Strings with different cases should return the correct Damerau-Levenshtein distance.");
    }

    [Test]
    public void Calculate_CommonPrefixes_ReturnsCorrectValue()
    {
        var str1 = "prefix";
        var str2 = "pre";
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(3), "Strings with common prefixes should return the correct Damerau-Levenshtein distance.");
    }

    [Test]
    public void Calculate_CommonSuffixes_ReturnsCorrectValue()
    {
        var str1 = "suffix";
        var str2 = "fix";
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(3), "Strings with common suffixes should return the correct Damerau-Levenshtein distance.");
    }

    [Test]
    public void Calculate_Transpositions_ReturnsCorrectValue()
    {
        var str1 = "abcd";
        var str2 = "acbd";
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(1), "Strings with transpositions should return the correct Damerau-Levenshtein distance.");
    }

    [Test]
    public void Calculate_RepeatedCharacters_ReturnsCorrectValue()
    {
        var str1 = "aaa";
        var str2 = "aaaaa";
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(2), "Strings with repeated characters should return the correct Damerau-Levenshtein distance.");
    }

    [Test]
    public void Calculate_UnicodeCharacters_ReturnsCorrectValue()
    {
        var str1 = "こんにちは";
        var str2 = "こんばんは";
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(2), "Strings with Unicode characters should return the correct Damerau-Levenshtein distance.");
    }
}
