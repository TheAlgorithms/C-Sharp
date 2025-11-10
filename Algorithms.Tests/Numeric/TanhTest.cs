using Algorithms.Numeric; 
using NUnit.Framework;

namespace Algorithms.Tests.Numeric;

/// <summary>
///     Contains unit tests for the Tanh (Hyperbolic Tangent) function implementation.
/// </summary>
[TestFixture] // Marks the class as containing test methods
public static class TanhTests
{
    private const double Tolerance = 1e-9; // Precision tolerance for floating-point comparisons

    /// <summary>
    ///     Tests the Tanh function with various positive, negative, and zero inputs.
    ///     tanh(x) is expected to return a value between -1.0 and 1.0.
    /// </summary>
    /// <param name="input">The input number.</param>
    /// <param name="expected">The expected hyperbolic tangent value.</param>
    [TestCase(0.0, 0.0)] // Case 1: Tanh(0) = 0
    [TestCase(1.0, 0.7615941559557649)] // Case 2: Positive input
    [TestCase(-1.0, -0.7615941559557649)] // Case 3: Negative input (Tanh is an odd function: tanh(-x) = -tanh(x))
    [TestCase(0.5, 0.46211715726000974)] // Case 4: Another positive value
    [TestCase(-0.5, -0.46211715726000974)] // Case 5: Another negative value
    [TestCase(5.0, 0.999909204262595)] // Case 6: Larger positive value (approaching 1.0)
    [TestCase(-5.0, -0.999909204262595)] // Case 7: Larger negative value (approaching -1.0)
    public static void TanhFunction_ReturnsCorrectValue(double input, double expected)
    {
        // Act
        var result = Tanh.Compute(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected).Within(Tolerance));
    }
    
    // ---

    /// <summary>
    ///     Tests the Tanh function's behavior when input approaches extreme limits (infinity and NaN).
    /// </summary>
    [Test]
    public static void TanhFunction_ApproachesLimits()
    {
        // Case 8: Positive Infinity
        // Tanh(x) approaches 1.0 as x -> +infinity
        var resultPosInfinity = Tanh.Compute(double.MaxValue);
        Assert.That(resultPosInfinity, Is.EqualTo(1.0).Within(Tolerance));

        // Case 9: Negative Infinity
        // Tanh(x) approaches -1.0 as x -> -infinity
        var resultNegInfinity = Tanh.Compute(double.MinValue);
        Assert.That(resultNegInfinity, Is.EqualTo(-1.0).Within(Tolerance));

        // Case 10: NaN input (Expected behavior is usually to return NaN)
        var resultNaN = Tanh.Compute(double.NaN);
        Assert.That(resultNaN, Is.NaN);
    }

    // ---

    /// <summary>
    ///     Tests the result range to ensure the output is always between -1.0 and 1.0, which is a key property of tanh.
    /// </summary>
    /// <param name="input">A variety of input numbers.</param>
    [TestCase(100.0)]
    [TestCase(-100.0)]
    [TestCase(0.0001)]
    [TestCase(-0.0001)]
    public static void TanhFunction_ResultIsBounded(double input)
    {
        // Act
        var result = Tanh.Compute(input);

        // Assert
        // Result must be greater than or equal to -1.0 and less than or equal to 1.0
        Assert.That(result, Is.GreaterThanOrEqualTo(-1.0));
        Assert.That(result, Is.LessThanOrEqualTo(1.0));
    }
}