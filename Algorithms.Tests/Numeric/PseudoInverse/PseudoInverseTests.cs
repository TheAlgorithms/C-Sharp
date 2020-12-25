using Algorithms.Numeric;
using NUnit.Framework;
using Utilities.Extensions;

namespace Algorithms.Tests.Numeric
{
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
            var result = PseudoInverse.PInv(inMat);
            var aainva = inMatCopy.Multiply(result).Multiply(inMatCopy);

            var rounded = aainva.RoundToNextInt();
            var isequal = rounded.IsEqual(inMatCopy);
            // Assert
            Assert.IsTrue(isequal);
        }

        [Test]
        public static void NonSquaredMatrixPseudoInverseMatrixWorks()
        {
            // Arrange
            var inMat = new double[,] { { 1, 2, 3, 4 }, { 0, 1, 4, 7 }, { 5, 6, 0, 1 } };
            var inMatCopy = new double[,] { { 1, 2, 3, 4 }, { 0, 1, 4, 7 }, { 5, 6, 0, 1 } };

            // Act
            // using (A+)+ = A
            var result = PseudoInverse.PInv(inMat);
            var result2 = PseudoInverse.PInv(result);

            var rounded = result2.RoundToNextInt();

            var isequal = rounded.IsEqual(inMatCopy);
            // Assert
            Assert.IsTrue(isequal);
        }
    }
}
