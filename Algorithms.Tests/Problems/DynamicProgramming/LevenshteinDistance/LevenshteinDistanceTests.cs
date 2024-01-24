using NUnit.Framework;
using Algorithms.Problems.DynamicProgramming;

namespace Algorithms.Tests.DynamicProgramming;

public class LevenshteinDistanceTests
{
    [TestCase("kitten", "sitting", 3)]
    [TestCase("bob", "bond", 2)]
    [TestCase("algorithm", "logarithm", 3)]
    [TestCase("star", "", 4)]
    [TestCase("", "star", 4)]
    [TestCase("abcde", "12345", 5)]
    public void Calculate_ReturnsCorrectLevenshteinDistance(string source, string destination, int expectedDistance)
    {
        var result = LevenshteinDistance.Calculate(source, destination);
        Assert.That(result, Is.EqualTo(expectedDistance));
    }
}
