using System;
using Algorithms.Strings.Similarity;
using NUnit.Framework;

namespace Algorithms.Tests.Strings.Similarity;

[TestFixture]
public class CosineSimilarityTests
{
    [Test]
    public void Calculate_IdenticalStrings_ReturnsOne()
    {
        var str1 = "test";
        var str2 = "test";
        var result = CosineSimilarity.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(1.0).Within(1e-6), "Identical strings should have a cosine similarity of 1.");
    }

    [Test]
    public void Calculate_CompletelyDifferentStrings_ReturnsZero()
    {
        var str1 = "abc";
        var str2 = "xyz";
        var result = CosineSimilarity.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(0.0).Within(1e-6), "Completely different strings should have a cosine similarity of 0.");
    }

    [Test]
    public void Calculate_EmptyStrings_ReturnsZero()
    {
        var str1 = "";
        var str2 = "";
        var result = CosineSimilarity.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(0.0).Within(1e-6), "Empty strings should have a cosine similarity of 0.");
    }

    [Test]
    public void Calculate_OneEmptyString_ReturnsZero()
    {
        var str1 = "test";
        var str2 = "";
        var result = CosineSimilarity.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(0.0).Within(1e-6), "Empty string should have a cosine similarity of 0.");
    }

    [Test]
    public void Calculate_SameCharactersDifferentCases_ReturnsOne()
    {
        var str1 = "Test";
        var str2 = "test";
        var result = CosineSimilarity.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(1.0).Within(1e-6), "The method should be case-insensitive.");
    }

    [Test]
    public void Calculate_SpecialCharacters_ReturnsCorrectValue()
    {
        var str1 = "hello!";
        var str2 = "hello!";
        var result = CosineSimilarity.Calculate(str1, str2);
        Assert.That(result, Is.EqualTo(1.0).Within(1e-6), "Strings with special characters should have a cosine similarity of 1.");
    }

    [Test]
    public void Calculate_DifferentLengthWithCommonCharacters_ReturnsCorrectValue()
    {
        var str1 = "hello";
        var str2 = "hello world";
        var result = CosineSimilarity.Calculate(str1, str2);
        var expected = 10 / (Math.Sqrt(7) * Math.Sqrt(19)); // calculated manually
        Assert.That(result, Is.EqualTo(expected).Within(1e-6), "Strings with different lengths but some common characters should have the correct cosine similarity.");
    }

    [Test]
    public void Calculate_PartiallyMatchingStrings_ReturnsCorrectValue()
    {
        var str1 = "night";
        var str2 = "nacht";
        var result = CosineSimilarity.Calculate(str1, str2);
        // Assuming the correct calculation gives an expected value
        var expected = 3.0 / 5.0;
        Assert.That(result, Is.EqualTo(expected).Within(1e-6), "Partially matching strings should have the correct cosine similarity.");
    }
}
