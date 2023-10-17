using System;
using Algorithms.Strings.Similarity;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Strings.Similarity;

public class JaccardDistanceTests
{
    private readonly JaccardDistance jaccard = new JaccardDistance();
    private readonly double precision = 0.0001;

    [TestCase("left", null)]
    [TestCase(null, "right")]
    [TestCase(null, null)]
    public void Calculate_WhenStringsAreNull_ThrowsArgumentNullException(string left, string right)
    {
        Action action = () => jaccard.Calculate(left, right);
        action.Should().Throw<ArgumentNullException>();
    }


    [TestCase("", "", 0.0d)]
    [TestCase("left", "", 1.0d)]
    [TestCase("", "right", 1.0d)]
    [TestCase("frog", "fog", 0.25d)]
    [TestCase("fly", "ant", 1.0d)]
    [TestCase("elephant", "hippo", 0.777777d)]
    [TestCase("ABC Corporation", "ABC Corp", 0.36363d)]
    public void Calculate_WhenProvidedWithStrings_CalculatesCorrectDistance(string left, string right, double expected)
    {
        var distance = jaccard.Calculate(left, right);

        distance.Should().BeApproximately(expected, precision);
    }
}
