using NUnit.Framework;
using Utilities.Extensions;

namespace Algorithms.Tests.Numeric.PseudoInverse;

public static class PseudoInverseTests
{
    [Test]
    public static void SquaredMatrixInverseWorks()
    {
        // Arrange
        var inMat = new double[,] { { 2, 4, 6 }, { 2, 0, 2 }, { 6, 8, 14 } };
        var inMatCopy = new double[,] { { 2, 4, 6 }, { 2, 0, 2 }, { 6, 8, 14 } };

        // Act
        // using AA+A = A
        var result = Algorithms.Numeric.Pseudoinverse.PseudoInverse.PInv(inMat);
        var aainva = inMatCopy.Multiply(result).Multiply(inMatCopy);

        var rounded = aainva.RoundToNextInt();
        var isequal = rounded.IsEqual(inMatCopy);
        // Assert
        Assert.That(isequal, Is.True);
    }

    [Test]
    public static void NonSquaredMatrixPseudoInverseMatrixWorks()
    {
        // Arrange
        var inMat = new double[,] { { 1, 2, 3, 4 }, { 0, 1, 4, 7 }, { 5, 6, 0, 1 } };
        var inMatCopy = new double[,] { { 1, 2, 3, 4 }, { 0, 1, 4, 7 }, { 5, 6, 0, 1 } };

        // Act
        // using (A+)+ = A
        var result = Algorithms.Numeric.Pseudoinverse.PseudoInverse.PInv(inMat);
        var result2 = Algorithms.Numeric.Pseudoinverse.PseudoInverse.PInv(result);

        var rounded = result2.RoundToNextInt();

        var isequal = rounded.IsEqual(inMatCopy);
        // Assert
        Assert.That(isequal, Is.True);
    }
}
