using System;
using System.Numerics;
using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric;

public static class FactorialTests
{
    [TestCase(0, "1")]
    [TestCase(1, "1")]
    [TestCase(4, "24")]
    [TestCase(10, "3628800")]
    [TestCase(18, "6402373705728000")]
    public static void GetsFactorial(int input, string expected)
    {
        // Arrange
        BigInteger expectedBigInt = BigInteger.Parse(expected);

        // Act
        var result = Factorial.Calculate(input);

        // Assert
        Assert.That(result, Is.EqualTo(expectedBigInt));
    }

    [TestCase(-5)]
    [TestCase(-10)]
    public static void GetsFactorialExceptionForNegativeNumbers(int num)
    {
        // Arrange

        // Act
        void Act() => Factorial.Calculate(num);

        // Assert
        _ = Assert.Throws<ArgumentException>(Act);
    }
}
