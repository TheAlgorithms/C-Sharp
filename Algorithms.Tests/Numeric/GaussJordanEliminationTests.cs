using System;
using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric;

/// <summary>
///     Class for testing Gauss-Jordan Elimination Algorithm.
/// </summary>
public static class GaussJordanEliminationTests
{
    [Test]
    public static void NonSquaredMatrixThrowsException()
    {
        // Arrange
        var solver = new GaussJordanElimination();
        var input = new double[,] { { 2, -1, 5 }, { 0, 2, 1 }, { 3, 17, 7 } };

        // Act
        void Act() => solver.Solve(input);

        // Assert
        _ = Assert.Throws<ArgumentException>(Act);
    }

    [Test]
    public static void UnableToSolveSingularMatrix()
    {
        // Arrange
        var solver = new GaussJordanElimination();
        var input = new double[,] { { 0, 0, 0 }, { 0, 0, 0 } };

        // Act
        var result = solver.Solve(input);

        // Assert
        Assert.That(result, Is.False);
    }
}
