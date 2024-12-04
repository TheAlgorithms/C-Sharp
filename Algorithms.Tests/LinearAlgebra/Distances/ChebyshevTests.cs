using NUnit.Framework;
using Algorithms.LinearAlgebra.Distances;
using FluentAssertions;
using System;

namespace Algorithms.Tests.LinearAlgebra.Distances;

public class ChebyshevTests
{
    [TestCase(new[] { 1.0, 1.0 }, new[] { 2.0, 2.0 }, 1.0)]
    [TestCase(new[] { 1.0, 1.0, 9.0 }, new[] { 2.0, 2.0, -5.2 }, 14.2)]
    [TestCase(new[] { 1.0, 2.0, 3.0 }, new[] { 1.0, 2.0, 3.0 }, 0.0)]
    [TestCase(new[] { 1.0, 2.0, 3.0, 4.0 }, new[] { 1.75, 2.25, -3.0, 0.5 }, 6.0)]
    public void DistanceTest(double[] point1, double[] point2, double expectedDistance)
    {
        Chebyshev.Distance(point1, point2).Should().BeApproximately(expectedDistance, 0.01);
    }

    [TestCase(new[] { 2.0, 3.0 }, new[] { -1.0 })]
    [TestCase(new[] { 1.0 }, new[] { 1.0, 2.0, 3.0 })]
    public void DistanceThrowsArgumentExceptionOnDifferentPointDimensions(double[] point1, double[] point2)
    {
        Action action = () => Chebyshev.Distance(point1, point2);
        action.Should().Throw<ArgumentException>();
    }
}