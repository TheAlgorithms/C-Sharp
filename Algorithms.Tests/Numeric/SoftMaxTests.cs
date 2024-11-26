using System;
using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric;

public static class SoftMaxTests
{
    [TestCase(new[] {5.0, 5.0}, new[] {0.5, 0.5})]
    [TestCase(new[] {1.0, 2.0, 3.0}, new[] {0.09003057317038046, 0.24472847105479767, 0.6652409557748219})]
    [TestCase(new[] {0.0}, new[] {1.0})]
    public static void SoftMaxFunction(double[] input, double[] expected)
    {
        // Act
        var result = SoftMax.Compute(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [Test]
    public static void SoftMaxFunctionThrowsArgumentException()
    {
        // Arrange
        var input = Array.Empty<double>();

        // Assert
        Assert.Throws<ArgumentException>(() => SoftMax.Compute(input));
    }

    [TestCase(new[] {1.0, 2.0, 3.0, 4.0, 5.0})]
    [TestCase(new[] {0.0, 0.0, 0.0, 0.0, 0.0})]
    [TestCase(new[] {5.0})]
    public static void SoftMaxFunctionSumsToOne(double[] input)
    {
        // Act
        var result = SoftMax.Compute(input);

        var sum = 0.0;
        foreach (var value in result)
        {
            sum += value;
        }

        // Assert
        Assert.That(sum, Is.EqualTo(1.0).Within(1e-9));
    }
}