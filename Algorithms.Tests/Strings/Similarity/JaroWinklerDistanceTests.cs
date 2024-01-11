using Algorithms.Strings;
using Algorithms.Strings.Similarity;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Strings;

public class JaroWinklerDistanceTests
{
    [TestCase("equal", "equal", 0)]
    [TestCase("abc", "123", 1)]
    [TestCase("Winkler", "Welfare", 0.33)]
    [TestCase("faremviel", "farmville", 0.08)]
    [TestCase("", "", 0)]
    public void Calculate_ReturnsCorrectJaroWinklerDistance(string s1, string s2, double expected)
    {
        JaroWinklerDistance.Calculate(s1, s2).Should().BeApproximately(expected, 0.01);
    }
}
