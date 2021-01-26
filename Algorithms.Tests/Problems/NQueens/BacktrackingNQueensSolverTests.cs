using System;
using System.Linq;
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
        [TestCase(12, 14200)]
        public static void SolvesCorrectly(int n, int expectedNumberOfSolutions)
        {
            // Arrange
            // Act
            var result = new BacktrackingNQueensSolver().BacktrackSolve(n).ToList();

            // Assert
            result.Should().HaveCount(expectedNumberOfSolutions);
            foreach (var solution in result)
            {
                ValidateOneQueenPerRow(solution);
                ValidateOneQueenPerColumn(solution);
                ValidateOneQueenPerTopLeftBottomRightDiagonalLine(solution);
                ValidateOneQueenPerBottomLeftTopRightDiagonalLine(solution);
            }
        }

        [Test]
        public static void NCannotBeNegative()
        {
            var n = -1;

            Action act = () => new BacktrackingNQueensSolver().BacktrackSolve(n);

            act.Should().Throw<ArgumentException>();
        }
        
        private static void ValidateOneQueenPerRow(bool[,] solution)
        {
            for (var i = 0; i < solution.GetLength(1); i++)
            {
                var foundQueen = false;
                for (var j = 0; j < solution.GetLength(0); j++)
                {
                    foundQueen = ValidateCell(foundQueen, solution[j, i]);
                }
            }
        }

        private static void ValidateOneQueenPerColumn(bool[,] solution)
        {
            for (var i = 0; i < solution.GetLength(0); i++)
            {
                var foundQueen = false;
                for (var j = 0; j < solution.GetLength(1); j++)
                {
                    foundQueen = ValidateCell(foundQueen, solution[i, j]);
                }
            }
        }

        private static void ValidateOneQueenPerTopLeftBottomRightDiagonalLine(bool[,] solution)
        {
            for (var i = 0; i < solution.GetLength(0); i++)
            {
                var foundQueen = false;
                for (var j = 0; i + j < solution.GetLength(1); j++)
                {
                    foundQueen = ValidateCell(foundQueen, solution[i + j, i]);
                }
            }
            
            for (var i = 0; i < solution.GetLength(1); i++)
            {
                var foundQueen = false;
                for (var j = 0; i + j < solution.GetLength(0); j++)
                {
                    foundQueen = ValidateCell(foundQueen, solution[j, i + j]);
                }
            }
        }

        private static void ValidateOneQueenPerBottomLeftTopRightDiagonalLine(bool[,] solution)
        {
            for (var i = 0; i < solution.GetLength(0); i++)
            {
                var foundQueen = false;
                for (var j = 0; i - j >= 0; j++)
                {
                    foundQueen = ValidateCell(foundQueen, solution[i - j, i]);
                }
            }
            
            for (var i = 0; i < solution.GetLength(1); i++)
            {
                var foundQueen = false;
                for (var j = 0; i - j >= 0 && solution.GetLength(0) - j > 0; j++)
                {
                    foundQueen = ValidateCell(foundQueen, solution[solution.GetLength(0) - j - 1, i - j]);
                }
            }
        }

        static bool ValidateCell(bool foundQueen, bool currentCell)
        {
            if (foundQueen)
            {
                currentCell.Should().BeFalse();
            }

            return foundQueen || currentCell;
        }
    }
}
