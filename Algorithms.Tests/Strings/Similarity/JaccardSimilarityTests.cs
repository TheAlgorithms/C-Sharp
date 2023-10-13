using System;
using Algorithms.Strings.Similarity;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Strings.Similarity;

public class JaccardSimilarityTests
{
    private readonly JaccardSimilarity jaccard = new JaccardSimilarity();
    private readonly double precision = 0.0001;

    [TestCase("left", null)]
    [TestCase(null, "right")]
    [TestCase(null, null)]
    public void Calculate_WhenStringsAreNull_ThrowsArgumentNullException(string left, string right)
    {
        Action action = () => jaccard.Calculate(left, right);
        action.Should().Throw<ArgumentNullException>();
    }

    [TestCase("", "", 1.0d)]
    [TestCase("left", "", 0.0d)]
    [TestCase("", "right", 0.0d)]
    [TestCase("frog", "fog", 0.75d)]
    [TestCase("fly", "ant", 0.0d)]
    [TestCase("elephant", "hippo", 0.22222d)]
    [TestCase("ABC Corporation", "ABC Corp", 0.636363d)]
    public void Calculate_WhenProvidedWithStrings_CalculatesTheCorrectDistance(string left, string right, double expected)
    {
        var similarity = jaccard.Calculate(left, right);

        similarity.Should().BeApproximately(expected, precision);
    }
}
