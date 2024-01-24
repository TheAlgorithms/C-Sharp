using System;
using System.Numerics;
using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric;

public static class BinomialCoefficientTests
{
    [TestCase(4, 2, 6)]
    [TestCase(7, 3, 35)]
    public static void CalculateFromPairs(int n, int k, int expected)
    {
        // Arrange

        // Act
        var result = BinomialCoefficient.Calculate(new BigInteger(n), new BigInteger(k));

        // Assert
        Assert.That(result, Is.EqualTo(new BigInteger(expected)));
    }

    [TestCase(3, 7)]
    public static void TeoremCalculateThrowsException(int n, int k)
    {
        // Arrange

        // Act

        // Assert
        _ = Assert.Throws<ArgumentException>(() => BinomialCoefficient.Calculate(new BigInteger(n), new BigInteger(k)));
    }
}
