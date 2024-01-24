using Algorithms.Other;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Other;

public static class FermatPrimeCheckerTests
{
    [TestCase(5, true)]
    [TestCase(2633, true)]
    [TestCase(9439, true)]
    [TestCase(1, false)]
    [TestCase(8, false)]
    public static void IsProbablePrime(int inputNum, bool expected)
    {
        // Arrange
        var random = new Randomizer();
        var times = random.Next(1, 1000);

        // Act
        var result = FermatPrimeChecker.IsPrime(inputNum, times);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}
