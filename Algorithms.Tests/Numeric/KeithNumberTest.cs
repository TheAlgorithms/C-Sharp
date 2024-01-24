using Algorithms.Numeric;
using NUnit.Framework;
using System;

namespace Algorithms.Tests.Numeric;

public static class KeithNumberTest
{
    [TestCase(14)]
    [TestCase(47)]
    [TestCase(197)]
    [TestCase(7909)]
    public static void KeithNumberWork(int number)
    {
        // Act
        var result = KeithNumberChecker.IsKeithNumber(number);

        // Assert
        Assert.That(result, Is.True);
    }

    [TestCase(-2)]
    public static void KeithNumberShouldThrowEx(int number)
    {
        // Arrange

        // Assert
        Assert.Throws<ArgumentException>(() => KeithNumberChecker.IsKeithNumber(number));
    }
}
