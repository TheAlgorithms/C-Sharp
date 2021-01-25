using Algorithms.Problems.NQueens;

using FluentAssertions;

using NUnit.Framework;

namespace Algorithms.Tests.Problems.NQueens
{
    public static class BacktrackingNQueensSolverTests
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 0)]
        [TestCase(3, 0)]
        [TestCase(4, 2)]
        [TestCase(5, 10)]
        [TestCase(6, 4)]
        [TestCase(7, 40)]
        [TestCase(8, 92)]
        [TestCase(8, 92)]
        [TestCase(9, 352)]
        [TestCase(10, 724)]
        [TestCase(11, 2680)]
        public static void SolvesCorrectly(int n, int expectedNumberOfSolutions)
        {
            // Arrange
            // Act
            var result = new BacktrackingNQueensSolver().BacktrackSolve(n);

            // Assert
            result.Should().HaveCount(expectedNumberOfSolutions);
        }
    }
}
