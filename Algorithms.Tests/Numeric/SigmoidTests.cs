using Algorithms.Numeric;
using NUnit.Framework;
using System;

namespace Algorithms.Tests.Numeric;

/// <summary>
/// Tests for the Sigmoid class, which implements the sigmoid activation function.
/// </summary>
public static class SigmoidTests
{
    // Standard tolerance for floating-point comparisons.
    private const double Tolerance = 1e-15;

    /// <summary>
    /// Tests that the sigmoid function correctly calculates the center point (x=0).
    /// Sigmoid(0) should equal 0.5.
    /// </summary>
    [Test]
    public static void GetsCenterValue()
    {
        // Arrange
        double x = 0.0;
        double expected = 0.5;

        // Act
        var result = Sigmoid.Calculate(x);

        // Assert
        Assert.That(result, Is.EqualTo(expected).Within(Tolerance));
    }

    /// <summary>
    /// Tests that the sigmoid function approaches 1 for large positive inputs (asymptotic behavior).
    /// </summary>
    [Test]
    public static void GetsAsymptoticValueForLargePositiveX()
    {
        // Arrange
        double x = 100.0;
        double expected = 1.0; 

        // Act
        var result = Sigmoid.Calculate(x);

        // Assert
        // The result should be extremely close to 1.0. 
        Assert.That(result, Is.EqualTo(expected).Within(1e-10));
    }
    
    /// <summary>
    /// Tests that the sigmoid function approaches 0 for large negative inputs (asymptotic behavior).
    /// </summary>
    [Test]
    public static void GetsAsymptoticValueForLargeNegativeX()
    {
        // Arrange
        double x = -100.0;
        double expected = 0.0;

        // Act
        var result = Sigmoid.Calculate(x);

        // Assert
        // The result should be extremely close to 0.0.
        Assert.That(result, Is.EqualTo(expected).Within(1e-10));
    }

    /// <summary>
    /// Tests the sigmoid calculation for various general positive and negative values.
    /// Values are confirmed against a reference calculation (or manually verified).
    /// </summary>
    /// <param name="input">The input value.</param>
    /// <param name="expected">The expected sigmoid output.</param>
    [TestCase(1.0, 0.7310585786300049)]
    [TestCase(5.0, 0.9933071490757153)]
    [TestCase(-1.0, 0.2689414213699951)]
    [TestCase(-5.0, 0.006692850924284855)]
    [TestCase(0.5, 0.6224593312018546)]
    [TestCase(-0.5, 0.3775406687981454)]
    public static void GetsStandardSigmoidValues(double input, double expected)
    {
        // Act
        var result = Sigmoid.Calculate(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected).Within(Tolerance));
    }

    /// <summary>
    /// Tests that the calculation correctly handles floating-point values and large numbers.
    /// </summary>
    [Test]
    public static void HandlesFractionalAndLargeInput()
    {
        // Arrange
        double x1 = 3.14159; // PI approximation
        // Corrected expected value: 1 / (1 + e^-3.14159)
        double expected1 = 0.9585760624650355; 

        double x2 = -20.0; 
        // Expected = 1 / (1 + e^20) - Should be very close to 0
        double expected2 = 2.0611536224385583E-9;

        // Act & Assert 1
        var result1 = Sigmoid.Calculate(x1);
        Assert.That(result1, Is.EqualTo(expected1).Within(Tolerance));
        
        // Act & Assert 2
        var result2 = Sigmoid.Calculate(x2);
        Assert.That(result2, Is.EqualTo(expected2).Within(Tolerance));
    }
}
