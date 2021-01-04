using System;

using Algorithms.Problems;

using NUnit.Framework;

namespace Algorithms.Tests.Problems
{
    public static class NQueenProblemTests
    {
        [Test]
        public static void SolvesCorrectly()
        {
            // Arrange
            const int n = 8;
            const int startCol = 0;

            var board = new int[n, n];

            // Act
            var result = NQueenProblem.BacktrackSolve(board, n, startCol);

            // Assert
            Assert.True(result);
        }

        [Test]
        [TestCase(8, 15)]
        [TestCase(5, 7)]
        [TestCase(4, -1)]
        public static void OutOfChessboardThrowsException(int n, int startCol)
        {
            // Arrange
            var board = new int[n, n];

            // Act
            void Act() => NQueenProblem.BacktrackSolve(board, n, startCol);

            // Assert
            _ = Assert.Throws<ArgumentException>(Act);
        }

        [Test]
        [TestCase(2)]
        [TestCase(3)]
        public static void UnsolvableSolutionThrowsError(int dim)
        {
            // Arrange

            // Act
            void Act() => NQueenProblem.BacktrackSolve(new int[dim, dim], dim, 0);

            // Assert
            _ = Assert.Throws<ArgumentException>(Act);
        }
    }
}
