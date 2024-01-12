using Algorithms.Strings;
using Algorithms.Strings.Similarity;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Strings;

public class JaroSimilarityTests
{
    [TestCase("equal", "equal", 1)]
    [TestCase("abc", "123", 0)]
    [TestCase("FAREMVIEL", "FARMVILLE", 0.88d)]
    [TestCase("CRATE", "TRACE", 0.73d)]
    [TestCase("CRATE11111", "CRTAE11111", 0.96d)]
    [TestCase("a", "a", 1)]
    [TestCase("", "", 1)]
    public void Calculate_ReturnsCorrectJaroSimilarity(string s1, string s2, double expected)
    {
        JaroSimilarity.Calculate(s1, s2).Should().BeApproximately(expected, 0.01);
    }
}
