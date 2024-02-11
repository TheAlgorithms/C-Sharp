using Algorithms.ModularArithmetic;
using NUnit.Framework;
using System;
using System.Numerics;

namespace Algorithms.Tests.ModularArithmetic;

public static class ModularMultiplicativeInverseTest
{
    [TestCase(2, 3, 2)]
    [TestCase(1, 1, 0)]
    [TestCase(13, 17, 4)]
    public static void TestCompute(long a, long n, long expected)
    {
        // Act
        var inverse = ModularMultiplicativeInverse.Compute(a, n);

        // Assert
        Assert.That(inverse, Is.EqualTo(expected));
    }

    [TestCase(46, 240)]
    [TestCase(0, 17)]
    [TestCase(17, 0)]
    [TestCase(17, 17)]
    [TestCase(0, 0)]
    [TestCase(2 * 13 * 17, 4 * 9 * 13)]
    public static void TestCompute_Irrevertible(long a, long n)
    {
        // Act
        void Act() => ModularMultiplicativeInverse.Compute(a, n);

        // Assert
        _ = Assert.Throws<ArithmeticException>(Act);
    }

    [TestCase(2, 3, 2)]
    [TestCase(1, 1, 0)]
    [TestCase(13, 17, 4)]
    public static void TestCompute_BigInteger(long a, long n, long expected)
    {
        // Act
        var inverse = ModularMultiplicativeInverse.Compute(new BigInteger(a), new BigInteger(n));

        // Assert
        Assert.That(inverse, Is.EqualTo(new BigInteger(expected)));
    }

    [TestCase(46, 240)]
    [TestCase(0, 17)]
    [TestCase(17, 0)]
    [TestCase(17, 17)]
    [TestCase(0, 0)]
    [TestCase(2 * 13 * 17, 4 * 9 * 13)]
    public static void TestCompute_BigInteger_Irrevertible(long a, long n)
    {
        // Act
        void Act() => ModularMultiplicativeInverse.Compute(new BigInteger(a), new BigInteger(n));

        // Assert
        _ = Assert.Throws<ArithmeticException>(Act);
    }
}
