using Algorithms.Numeric.Factorization;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric.Factorization;

public static class TrialDivisionFactorizerTests
{
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(29)]
    [TestCase(31)]
    public static void PrimeNumberFactorizationFails(int p)
    {
        // Arrange
        var factorizer = new TrialDivisionFactorizer();

        // Act
        var success = factorizer.TryFactor(p, out _);

        // Assert
        Assert.That(success, Is.False);
    }

    [TestCase(4, 2)]
    [TestCase(6, 2)]
    [TestCase(8, 2)]
    [TestCase(9, 3)]
    [TestCase(15, 3)]
    [TestCase(35, 5)]
    [TestCase(49, 7)]
    [TestCase(77, 7)]
    public static void PrimeNumberFactorizationSucceeds(int n, int expected)
    {
        // Arrange
        var factorizer = new TrialDivisionFactorizer();

        // Act
        var success = factorizer.TryFactor(n, out var factor);

        // Assert
        Assert.That(success, Is.True);
        Assert.That(factor, Is.EqualTo(expected));
    }
}
