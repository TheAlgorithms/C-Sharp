using System;
using System.Numerics;
using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric;

public static class CeilTests
{
    [TestCase(0.0, 0)]
    [TestCase(1.1, 2)]
    [TestCase(1.9, 2)]
    [TestCase(1.0, 1)]
    [TestCase(-1.1, -1)]
    [TestCase(-1.9, -1)]
    [TestCase(-1.0, -1)]
    [TestCase(1000000000.1, 1000000001)]
    [TestCase(1, 1)]
    public static void GetsCeilVal<T>(T inputNum, T expected) where T : INumber<T>
    {
        // Act
        var result = Ceil.CeilVal(inputNum);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}