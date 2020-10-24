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
            var source = new double[,] {{1, 1, 1}, {1, 1, 1}, {1, 1, 1}};
            var operand = new double[,] {{1}, {1}};
            
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
    }
}