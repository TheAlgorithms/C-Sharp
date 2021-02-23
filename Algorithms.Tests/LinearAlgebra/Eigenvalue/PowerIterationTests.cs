using System;
using Algorithms.LinearAlgebra.Eigenvalue;
using FluentAssertions;
using Utilities.Extensions;
using NUnit.Framework;

namespace Algorithms.Tests.LinearAlgebra.Eigenvalue
{
    public class PowerIterationTests
    {
        private readonly double epsilon = Math.Pow(10, -5);

        [Test]
        public void Dominant_ShouldThrowArgumentException_WhenSourceMatrixIsNotSquareShaped()
        {
            // Arrange
            var source = new double[,] {{1, 0, 0}, {0, 1, 0}, {0, 0, 1}, {0, 0, 0}};
            
            // Act
            Action action = () => PowerIteration.Dominant(source, StartVector(source.GetLength(0)), epsilon);
            
            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("The source matrix is not square-shaped.");
        }
        
        [Test]
        public void Dominant_ShouldThrowArgumentException_WhenStartVectorIsNotSameSizeAsMatrix()
        {
            // Arrange
            var source = new double[,] {{1, 0, 0}, {0, 1, 0}, {0, 0, 1}};
            var startVector = new double[] {1, 0, 0, 0};
            
            // Act
            Action action = () => PowerIteration.Dominant(source, startVector, epsilon);
            
            // Assert
            action.Should().Throw<ArgumentException>()
                .WithMessage("The length of the start vector doesn't equal the size of the source matrix.");
        }

        [Test, TestCaseSource(nameof(DominantVectorTestCases))]
        public void Dominant_ShouldCalculateDominantEigenvalueAndEigenvector(
            double eigenvalue, double[] eigenvector, double[,] source)
        {
            // Act
            var (actualEigVal, actualEigVec) = PowerIteration.Dominant(source, StartVector(source.GetLength(0)), epsilon);
            
            // Assert
            actualEigVal.Should().BeApproximately(eigenvalue, epsilon);
            actualEigVec.Magnitude().Should().BeApproximately(eigenvector.Magnitude(), epsilon);
        }

        static readonly object[] DominantVectorTestCases =
        {
            new object[]
            {
                3.0,
                new[] { 0.7071039, 0.70710966 },
                new[,] { { 2.0, 1.0 }, { 1.0, 2.0 } }
            },
            new object[]
            {
                4.235889,
                new[] { 0.91287093, 0.40824829 },
                new[,] { { 2.0, 5.0 }, { 1.0, 2.0 } }
            }
        };

        private double[] StartVector(int length) => new Random(Seed: 111111).NextVector(length);
    }
}