using Algorithms.Numeric;
using NUnit.Framework;
using System;

namespace Algorithms.Tests.Numeric;

[TestFixture]
public static class ReluTests
{
    // Tolerance for floating-point comparisons
    private const double Tolerance = 1e-9;

    // --- SCALAR TESTS (Relu.Compute(double)) ---

    [TestCase(0.0, 0.0)]
    [TestCase(1.0, 1.0)]
    [TestCase(-1.0, 0.0)]
    [TestCase(5.0, 5.0)]
    [TestCase(-5.0, 0.0)]
    public static void ReluFunction_Scalar_ReturnsCorrectValue(double input, double expected)
    {
        var result = Relu.Compute(input);
        Assert.That(result, Is.EqualTo(expected).Within(Tolerance));
    }

    [Test]
    public static void ReluFunction_Scalar_HandlesLimitsAndNaN()
    {
        // Positive infinity stays +Infinity, negative infinity becomes 0, NaN propagates
        Assert.That(RelUComputePositiveInfinity(), Is.EqualTo(double.PositiveInfinity));
        Assert.That(RelUComputeNegativeInfinity(), Is.EqualTo(0.0).Within(Tolerance));
        Assert.That(RelUComputeNaN(), Is.NaN);

        static double RelUComputePositiveInfinity() => Relu.Compute(double.PositiveInfinity);
        static double RelUComputeNegativeInfinity() => Relu.Compute(double.NegativeInfinity);
        static double RelUComputeNaN() => Relu.Compute(double.NaN);
    }

    [TestCase(100.0)]
    [TestCase(0.0001)]
    [TestCase(-100.0)]
    public static void ReluFunction_Scalar_ResultIsNonNegative(double input)
    {
        var result = Relu.Compute(input);
        Assert.That(result, Is.GreaterThanOrEqualTo(0.0));
    }

    // --- VECTOR TESTS (Relu.Compute(double[])) ---

    [Test]
    public static void ReluFunction_Vector_ReturnsCorrectValues()
    {
        var input = new[] { 0.0, 1.0, -2.0 };
        var expected = new[] { 0.0, 1.0, 0.0 };

        var result = Relu.Compute(input);

        Assert.That(result, Is.EqualTo(expected).Within(Tolerance));
    }

    [Test]
    public static void ReluFunction_Vector_HandlesLimitsAndNaN()
    {
        var input = new[] { double.PositiveInfinity, 0.0, double.NaN };
        var result = Relu.Compute(input);

        Assert.That(result.Length, Is.EqualTo(input.Length));
        Assert.That(result[0], Is.EqualTo(double.PositiveInfinity));
        Assert.That(result[1], Is.EqualTo(0.0).Within(Tolerance));
        Assert.That(result[2], Is.NaN);
    }

    // --- EXCEPTION TESTS ---

    [Test]
    public static void ReluFunction_Vector_ThrowsOnNullInput()
    {
        double[]? input = null;
        Assert.Throws<ArgumentNullException>(() => Relu.Compute(input!));
    }

    [Test]
    public static void ReluFunction_Vector_ThrowsOnEmptyInput()
    {
        var input = Array.Empty<double>();
        Assert.Throws<ArgumentException>(() => Relu.Compute(input));
    }
}
