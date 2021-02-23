using System;
using FluentAssertions;
using Utilities.Extensions;
using NUnit.Framework;

namespace Utilities.Tests.Extensions
{
    public class MatrixExtensionsTests
    {
        [Test]
        public void Multiply_ShouldThrowInvalidOperationException_WhenOperandsAreNotCompatible()
        {
            // Arrange
            var source = new double[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            var operand = new double[,] { { 1 }, { 1 } };

            // Act
            Action action = () => source.Multiply(operand);

            // Assert
            action.Should().Throw<InvalidOperationException>()
                .WithMessage("The width of a first operand should match the height of a second.");
        }

        [Test, TestCaseSource(nameof(MatrixMultiplyTestCases))]
        public void Multiply_ShouldCalculateDotProductMultiplicationResult(
            double[,] source, double[,] operand, double[,] result) =>
            source.Multiply(operand).Should().BeEquivalentTo(result);

        [Test]
        public void Copy_ShouldReturnImmutableCopyOfMatrix()
        {
            // Arrange
            var sutMatrix = new double[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };

            // Act
            var actualMatrix = sutMatrix.Copy();

            // Assert
            actualMatrix.Should().NotBeSameAs(sutMatrix);
            actualMatrix.Should().BeEquivalentTo(sutMatrix);
        }

        [Test, TestCaseSource(nameof(MatrixTransposeTestCases))]
        public void Transpose_ShouldReturnTransposedMatrix(
            double[,] source, double[,] target) =>
            source.Transpose().Should().BeEquivalentTo(target);

        [Test]
        public void MultiplyVector_ShouldCalculateDotProductMultiplicationResult()
        {
            // Arrange
            var source = new double[,] { { 2, 2, -1 }, { 0, -2, -1 }, { 0, 0, 5 } };
            var operand = new double[] { 2, 2, 3 };
            var result = new double[] { 5, -7, 15 };

            // Act
            var actualMatrix = source.MultiplyVector(operand);

            // Assert
            actualMatrix.Should().BeEquivalentTo(result);
        }

        [Test]
        public void Subtract_ShouldThrowArgumentException_WhenOperandsAreNotCompatible()
        {
            // Arrange
            var source = new double[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            var operand = new double[,] { { 1 }, { 1 } };

            // Act
            Action action = () => source.Subtract(operand);

            // Assert
            action.Should().Throw<ArgumentException>()
                .WithMessage("Dimensions of matrices must be the same");
        }

        [Test]
        public static void EqualMatricesShouldReturnTrue()
        {
            // Arrange
            var a = new double[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } };
            var b = new double[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } };

            // Act
            var result = a.IsEqual(b);

            // Assert
            Assert.True(result);
        }

        [Test]
        public static void NonEqualMatricesShouldReturnFalse()
        {
            // Arrange
            var a = new double[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } };
            var b = new double[,] { { 1, 2, 3 }, { 1, 2, 6 }, { 1, 2, 3 } };

            // Act
            var result = a.IsEqual(b);

            // Assert
            Assert.False(result);
        }

        [Test]
        public static void DifferentSizeMatricesShouldReturnFalse()
        {
            // Arrange
            var a = new double[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } };
            var b = new double[,] { { 1, 2, 3 }, { 1, 2, 3 } };

            // Act
            var result = a.IsEqual(b);

            // Assert
            Assert.False(result);
        }

        [Test, TestCaseSource(nameof(MatrixSubtractTestCases))]
        public void Subtract_ShouldCalculateSubtractionResult(
            double[,] source, double[,] operand, double[,] result) =>
            source.Subtract(operand).Should().BeEquivalentTo(result);

        private static readonly object[] MatrixMultiplyTestCases =
        {
            new object[]
            {
                new double[,] {{2, 2, -1}, {0, -2, -1}, {0, 0, 5}},
                new double[,] {{2}, {2}, {3}},
                new double[,] {{5}, {-7}, {15}}
            },
            new object[]
            {
                new double[,] {{5, 8, -4}, {6, 9, -5}, {4, 7, -3}},
                new double[,] {{3, 2, 5}, {4, -1, 3}, {9, 6, 5}},
                new double[,] {{11, -22, 29}, {9, -27, 32}, {13, -17, 26}}
            }
        };

        private static readonly object[] MatrixTransposeTestCases =
        {
            new object[]
            {
                new double[,] {{2, 2, 3}},
                new double[,] {{2}, {2}, {3}},
            },
            new object[]
            {
                new double[,] {{5, 8}, {6, 9}},
                new double[,] {{5, 6}, {8, 9}}
            }
        };

        private static readonly object[] MatrixSubtractTestCases =
        {
            new object[]
            {
                new double[,] {{0, 0}, {0, 0}},
                new double[,] {{1, 1}, {1, 1}},
                new double[,] {{-1, -1}, {-1, -1}}
            },
            new object[]
            {
                new double[,] {{1, 2}, {2, 3}, {3, 4}},
                new double[,] {{1, 1}, {1, 1}, {1, 1}},
                new double[,] {{0, 1}, {1, 2}, {2, 3}}
            },
            new object[]
            {
                new double[,] {{-1, -2, 0}, {2, -3, 2}, {3, 4, 1}},
                new double[,] {{2, 5, 12}, {0, 5, 1}, {1, 1, 4}},
                new double[,] {{-3, -7, -12}, {2, -8, 1}, {2, 3, -3}}
            }
        };
    }
}