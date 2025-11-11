using Algorithms.Numeric;
using NUnit.Framework;
using System;

namespace Algorithms.Tests.Numeric;

/// <summary>
/// Tests for the SumOfDigits class.
/// </summary>
public static class SumOfDigitsTests
{
    /// <summary>
    /// Tests the calculation of the sum of digits for various non-negative integers.
    /// </summary>
    /// <param name="input">The input number.</param>
    /// <param name="expectedSum">The expected sum of its digits.</param>
    [TestCase(0, 0)]
    [TestCase(7, 7)]
    [TestCase(10, 1)]
    [TestCase(42, 6)]
    [TestCase(12345, 15)]
    [TestCase(9999, 36)]
    [TestCase(8675309, 38)]
    [TestCase(2147483647, 46)] // Max value for int
    public static void GetsCorrectSumOfDigits(int input, int expectedSum)
    {
        // Act
        var result = SumOfDigits.Calculate(input);

        // Assert
        Assert.That(result, Is.EqualTo(expectedSum));
    }

    /// <summary>
    /// Tests that the method throws an ArgumentException when a negative number is provided.
    /// </summary>
    /// <param name="num">The negative input number.</param>
    [TestCase(-1)]
    [TestCase(-100)]
    [TestCase(-54321)]
    public static void ThrowsExceptionForNegativeNumbers(int num)
    {
        // Act
        void Act() => SumOfDigits.Calculate(num);

        // Assert
        _ = Assert.Throws<ArgumentException>(Act);
    }
}
