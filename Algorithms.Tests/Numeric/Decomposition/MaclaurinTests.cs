using System;
using Algorithms.Numeric.Series;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric.Decomposition;

public class MaclaurinTests
{
    [TestCase(0.01, 3, 0.01)]
    [TestCase(1, 7, 0.001)]
    [TestCase(-1.2, 7, 0.001)]
    public void Exp_TermsForm_ValidCases(double point, int terms, double expectedError)
    {
        // Arrange
        var expected = Math.Exp(point);

        // Act
        var actual = Maclaurin.Exp(point, terms);

        // Assert
        Assert.That(Math.Abs(expected - actual) < expectedError, Is.True);
    }

    [Test]
    public void Exp_TermsForm_InvalidCase() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => Maclaurin.Exp(0, -1));

    [TestCase(0, 1, 0.001)]
    [TestCase(1, 7, 0.001)]
    [TestCase(1.57, 7, 0.001)]
    [TestCase(3.14, 7, 0.001)]
    public void Sin_TermsForm_ValidCases(double point, int terms, double expectedError)
    {
        // Arrange
        var expected = Math.Sin(point);

        // Act
        var actual = Maclaurin.Sin(point, terms);

        // Assert
        Assert.That(Math.Abs(expected - actual) < expectedError, Is.True);
    }

    [Test]
    public void Sin_TermsForm_InvalidCase() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => Maclaurin.Sin(0, -1));

    [TestCase(0, 1, 0.001)]
    [TestCase(1, 7, 0.001)]
    [TestCase(1.57, 7, 0.001)]
    [TestCase(3.14, 7, 0.001)]
    public void Cos_TermsForm_ValidCases(double point, int terms, double expectedError)
    {
        // Arrange
        var expected = Math.Cos(point);

        // Act
        var actual = Maclaurin.Cos(point, terms);

        // Assert
        Assert.That(Math.Abs(expected - actual) < expectedError, Is.True);
    }

    [Test]
    public void Cos_TermsForm_InvalidCase() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => Maclaurin.Cos(0, -1));

    [TestCase(0.1, 0.001)]
    [TestCase(0.1, 0.00001)]
    [TestCase(2.1, 0.001)]
    [TestCase(-1.2, 0.001)]
    public void Exp_ErrorForm_ValidCases(double point, double error)
    {
        // Arrange
        var expected = Math.Exp(point);

        // Act
        var actual = Maclaurin.Exp(point, error);

        // Assert
        Assert.That(Math.Abs(expected - actual) < error, Is.True);
    }

    [TestCase(0.0)]
    [TestCase(1.0)]
    public void Exp_ErrorForm_InvalidCases(double error) =>
        Assert.Throws<ArgumentException>(() => Maclaurin.Exp(0.0, error));

    [TestCase(0, 0.001)]
    [TestCase(1, 0.00001)]
    [TestCase(1.57, 0.0001)]
    [TestCase(3.14, 0.0001)]
    public void Sin_ErrorForm_ValidCases(double point, double error)
    {
        // Arrange
        var expected = Math.Sin(point);

        // Act
        var actual = Maclaurin.Sin(point, error);

        // Assert
        Assert.That(Math.Abs(expected - actual) < error, Is.True);
    }

    [TestCase(0.0)]
    [TestCase(1.0)]
    public void Sin_ErrorForm_InvalidCases(double error) =>
        Assert.Throws<ArgumentException>(() => Maclaurin.Sin(0.0, error));

    [TestCase(0, 0.001)]
    [TestCase(1, 0.00001)]
    [TestCase(1.57, 0.0001)]
    [TestCase(3.14, 0.0001)]
    public void Cos_ErrorForm_ValidCases(double point, double error)
    {
        // Arrange
        var expected = Math.Cos(point);

        // Act
        var actual = Maclaurin.Cos(point, error);

        // Assert
        Assert.That(Math.Abs(expected - actual) < error, Is.True);
    }

    [TestCase(0.0)]
    [TestCase(1.0)]
    public void Cos_ErrorForm_InvalidCases(double error) =>
        Assert.Throws<ArgumentException>(() => Maclaurin.Cos(0.0, error));
}
