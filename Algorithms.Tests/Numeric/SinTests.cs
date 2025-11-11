using System;
using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric;

/// <summary>
/// Tests for the Sin class, which implements sine calculation via Maclaurin series.
/// </summary>
public static class SinTests
{
    // A tolerance level for comparing the series approximation with the built-in Math.Sin.
    private const double Tolerance = 1e-6;

    /// <summary>
    /// Tests the sine calculation for various common trigonometric angles 
    /// using the default number of terms (now 20).
    /// </summary>
    /// <param name="x">The angle in radians.</param>
    [TestCase(0.0)]
    [TestCase(Math.PI / 6)] // sin(30 deg) = 0.5
    [TestCase(Math.PI / 2)] // sin(90 deg) = 1.0
    [TestCase(Math.PI)]     // sin(180 deg) = 0.0
    [TestCase(3 * Math.PI / 2)] // sin(270 deg) = -1.0
    [TestCase(2 * Math.PI)] // sin(360 deg) = 0.0
    [TestCase(1.5)]         // Arbitrary value
    public static void GetsSineValueWithDefaultTerms(double x)
    {
        // Arrange
        var expected = Math.Sin(x);

        // Act
        var result = Sin.Calculate(x);

        // Assert
        // Check if the result is close to the expected value within the defined tolerance.
        Assert.That(result, Is.EqualTo(expected).Within(Tolerance));
    }

    /// <summary>
    /// Tests if the angle reduction logic works correctly by passing a large angle.
    /// The angle (20*PI + PI/2) should be equivalent to PI/2, where sin(x) = 1.
    /// </summary>
    [Test]
    public static void GetsSineValueWithLargeAngleReduction()
    {
        // Arrange: Angle significantly larger than 2*PI
        double largeAngle = 20 * Math.PI + (Math.PI / 2); // Should reduce to PI/2

        // Act
        var result = Sin.Calculate(largeAngle);

        // Assert
        // Expected value is 1.0 (sin(PI/2))
        Assert.That(result, Is.EqualTo(1.0).Within(Tolerance));
    }

    /// <summary>
    /// Tests the sine calculation for negative angles.
    /// </summary>
    /// <param name="x">The negative angle in radians.</param>
    [TestCase(-Math.PI / 2)] // sin(-90 deg) = -1.0
    [TestCase(-Math.PI / 4)] // sin(-45 deg) = -0.707...
    public static void GetsSineValueForNegativeAngle(double x)
    {
        // Arrange
        var expected = Math.Sin(x);

        // Act
        var result = Sin.Calculate(x);

        // Assert
        Assert.That(result, Is.EqualTo(expected).Within(Tolerance));
    }

    /// <summary>
    /// Tests the precision when only a small number of terms is used (e.g., 1 term).
    /// sin(x) approx x for small x.
    /// </summary>
    [Test]
    public static void GetsSineValueWithMinimalTerms()
    {
        // Arrange
        double x = 0.1; // A small angle where the first term dominates
        int terms = 1;  // Only the first term (x) is calculated

        // Act
        var result = Sin.Calculate(x, terms);

        // Assert
        // Result should be approximately Math.Sin(0.1). 
        // The error of the one-term approximation (x) is roughly x^3/3! (approx 1.66e-4 for x=0.1), 
        // so we use a looser tolerance (2e-4) to ensure the test passes reliably.
        Assert.That(result, Is.EqualTo(Math.Sin(x)).Within(2e-4)); // Tolerance increased to 2e-4
    }

    /// <summary>
    /// Tests that calculating the sine with a non-positive number of terms throws an ArgumentException.
    /// </summary>
    /// <param name="numTerms">The invalid number of terms.</param>
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-10)]
    public static void ThrowsExceptionForInvalidTerms(int numTerms)
    {
        // Arrange
        double x = 1.0;

        // Act
        void Act() => Sin.Calculate(x, numTerms);

        // Assert
        _ = Assert.Throws<ArgumentException>(Act);
    }
}