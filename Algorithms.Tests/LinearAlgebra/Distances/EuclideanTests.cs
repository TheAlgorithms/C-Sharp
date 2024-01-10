using NUnit.Framework;
using Algorithms.LinearAlgebra.Distances;
using FluentAssertions;
using System;

namespace Algorithms.Tests.LinearAlgebra.Distances;

public static class EuclideanTests
{
    /// <summary>
    /// Test the result given by Euclidean distance function.
    /// </summary>
    /// <param name="point1">Origin point.</param>
    /// <param name="point2">Target point.</param>
    /// <param name="expectedResult">Expected result.</param>
    [TestCase(new[] { 1.5 }, new[] { -1.0 }, 2.5)]
    [TestCase(new[] { 7.0, 4.0, 3.0 }, new[] { 17.0, 6.0, 2.0 }, 10.247)]
    public static void DistanceTest(double[] point1, double[] point2, double expectedResult)
    {
        Euclidean.Distance(point1, point2).Should().BeApproximately(expectedResult, 0.01);
    }

    /// <summary>
    /// Throws ArgumentException if two different dimension arrays are given.
    /// </summary>
    /// <param name="point1">First point of N dimensions.</param>
    /// <param name="point2">Second point of M dimensions, M != N.</param>
    [TestCase(new[] { 7.0, 4.5 }, new[] { -3.0 })]
    [TestCase(new[] { 12.0 }, new[] { 1.5, 7.0, 3.2 })]
    public static void DistanceThrowsArgumentExceptionOnDifferentPointDimensions(double[] point1, double[] point2)
    {
        Action action = () => Euclidean.Distance(point1, point2);
        action.Should().Throw<ArgumentException>();
    }
}
