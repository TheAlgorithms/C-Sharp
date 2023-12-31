using System;
using System.Collections.Generic;

namespace Algorithms.Problems.NQueens;

public class BacktrackingNQueensSolver
{
    /// <summary>
    ///     Solves N-Queen Problem given a n dimension chessboard and using backtracking with recursion algorithm.
    ///     If we find a dead-end within or current solution we go back and try another position for queen.
    /// </summary>
    /// <param name="n">Number of rows.</param>
    /// <returns>All solutions.</returns>
    public IEnumerable<bool[,]> BacktrackSolve(int n)
    {
        if (n < 0)
        {
            throw new ArgumentException(nameof(n));
        }

        return BacktrackSolve(new bool[n, n], 0);
    }

    private static IEnumerable<bool[,]> BacktrackSolve(bool[,] board, int col)
    {
        var solutions = col < board.GetLength(0) - 1
            ? HandleIntermediateColumn(board, col)
            : HandleLastColumn(board);
        return solutions;
    }

    private static IEnumerable<bool[,]> HandleIntermediateColumn(bool[,] board, int col)
    {
        // To start placing queens on possible spaces within the board.
        for (var i = 0; i < board.GetLength(0); i++)
        {
            if (CanPlace(board, i, col))
            {
                board[i, col] = true;

                foreach (var solution in BacktrackSolve(board, col + 1))
                {
                    yield return solution;
                }

                board[i, col] = false;
            }
        }
    }

    private static IEnumerable<bool[,]> HandleLastColumn(bool[,] board)
    {
        var n = board.GetLength(0);
        for (var i = 0; i < n; i++)
        {
            if (CanPlace(board, i, n - 1))
            {
                board[i, n - 1] = true;

                yield return (bool[,])board.Clone();

                board[i, n - 1] = false;
            }
        }
    }

    /// <summary>
    ///     Checks whether current queen can be placed in current position,
    ///     outside attacking range of another queen.
    /// </summary>
    /// <param name="board">Source board.</param>
    /// <param name="row">Row coordinate.</param>
    /// <param name="col">Col coordinate.</param>
    /// <returns>true if queen can be placed in given chessboard coordinates; false otherwise.</returns>
    private static bool CanPlace(bool[,] board, int row, int col)
    {
        // To check whether there are any queens on current row.
        for (var i = 0; i < col; i++)
        {
            if (board[row, i])
            {
                return false;
            }
        }

        // To check diagonal attack top-left range.
        for (int i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--)
        {
            if (board[i, j])
            {
                return false;
            }
        }

        // To check diagonal attack bottom-left range.
        for (int i = row + 1, j = col - 1; j >= 0 && i < board.GetLength(0); i++, j--)
        {
            if (board[i, j])
            {
                return false;
            }
        }

        // Return true if it can use position.
        return true;
    }
}
