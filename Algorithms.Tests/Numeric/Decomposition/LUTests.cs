using System;
using Algorithms.Numeric.Decomposition;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric.Decomposition
{
    public class LUTests
    {
        [Test]
        public void DecomposeIdentityMatrix()
        {
            // Arrange
            var identityMatrix = new double[,] {{1, 0, 0}, {0, 1, 0}, {0, 0, 1}};
            var expectedLower = new double[,] {{1, 0, 0}, {0, 1, 0}, {0, 0, 1}};
            var expectedUpper = new double[,] {{1, 0, 0}, {0, 1, 0}, {0, 0, 1}};
            
            // Act
            (double[,] lower, double[,] upper) = LU.Decompose(identityMatrix);
            
            // Assert
            Assert.AreEqual(expectedLower, lower);
            Assert.AreEqual(expectedUpper, upper);
        }
        
        [Test]
        public void DecomposeMatrix_Case3X3()
        {
            // Arrange
            var source = new double[,] {{2, 1, 4}, {7, 1, 1}, {4, 2, 9}};
            var expectedLower = new double[,] {{1, 0, 0}, {3.5, 1, 0}, {2, 0, 1}};
            var expectedUpper = new double[,] {{2, 1, 4}, {0, -2.5, -13}, {0, 0, 1}};

            // Act
            (double[,] lower, double[,] upper) = LU.Decompose(source);
            
            // Assert
            Assert.AreEqual(expectedLower, lower);
            Assert.AreEqual(expectedUpper, upper);
        }
        
        [Test]
        public void DecomposeMatrix_Case4X4()
        {
            // Arrange
            var source = new double[,] {{1, 2, 4.5, 7}, {3, 8, 0.5, 2}, {2, 6, 4, 1.5}, {4, 14, 2, 10.5}};
            var expectedLower = new double[,] {{1, 0, 0, 0}, {3, 1, 0, 0}, {2, 1, 1, 0}, {4, 3, 2.875, 1}};
            var expectedUpper = new double[,] {{1, 2, 4.5, 7}, {0, 2, -13, -19}, {0, 0, 8, 6.5}, {0, 0, 0, 20.8125}};

            // Act
            (double[,] lower, double[,] upper) = LU.Decompose(source);
            
            // Assert
            Assert.AreEqual(expectedLower, lower);
            Assert.AreEqual(expectedUpper, upper);
        }

        [Test]
        public void FailOnDecomposeNonSquareMatrix()
        {
            // Arrange
            var nonSquareMatrix = new double[,] {{1, 0, 0}, {0, 1, 0}, {0, 0, 1}, {0, 0, 0}};

            // Act
            void Act() => LU.Decompose(nonSquareMatrix ?? new double[,] {});

            // Assert
            Assert.Throws<ArgumentException>(Act);
        }
        
        [Test]
        public void EliminateIdentityEquation()
        {
            // Arrange
            var identityMatrix = new double[,] {{1, 0, 0}, {0, 1, 0}, {0, 0, 1}};
            var coefficients = new double[,] {{1}, {2}, {3}};
            var expectedSolution = new double[,] {{1}, {2}, {3}};

            // Act
            var solution = LU.Eliminate(identityMatrix, coefficients);
            
            // Assert
            Assert.AreEqual(expectedSolution, solution);
        }
        
        [Test]
        public void EliminateEquation_Case3X3()
        {
            // Arrange
            var identityMatrix = new double[,] {{2, 1, -1}, {-3, -1, 2}, {-2, 1, 2}};
            var coefficients = new double[,] {{8}, {-11}, {-3}};
            var expectedSolution = new double[,] {{2}, {3}, {-1}};

            // Act
            var solution = LU.Eliminate(identityMatrix, coefficients);
            
            // Assert
            Assert.AreEqual(expectedSolution, solution);
        }
        
        [Test]
        public void EliminateEquation_Case4X4()
        {
            // Arrange
            var identityMatrix = new double[,] {{1.0, 2.0, -3.0, -1.0}, {0.0, -3.0, 2.0, 6.0}, {0.0, 5.0, -6.0, -2.0}, {0.0, -1.0, 8.0, 1.0}};
            var coefficients = new double[,] {{0.0}, {-8.0}, {0.0}, {-8.0}};
            var expectedSolution = new double[,] {{-1.0000000000000004}, {-2.0000000000000018}, {-1.0000000000000011}, {-2.0000000000000004}};

            // Act
            var solution = LU.Eliminate(identityMatrix, coefficients);
            
            // Assert
            Assert.AreEqual(expectedSolution, solution);
        }
        
        [Test]
        public void FailOnEliminateEquationWithNonSquareMatrix()
        {
            // Arrange
            var nonSquareMatrix = new double[,] {{1, 0, 0}, {0, 1, 0}, {0, 0, 1}, {0, 0, 0}};
            var coefficients = new double[,] {{1}, {2}, {3}, {4}};

            // Act
            void Act() => LU.Eliminate(nonSquareMatrix ?? new double[,] {}, coefficients ?? new double[,] {});

            // Assert
            Assert.Throws<ArgumentException>(Act);
        }
    }
}