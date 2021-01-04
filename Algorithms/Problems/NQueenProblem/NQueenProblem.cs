using System;
using System.Linq;

namespace Algorithms.Problems
{
    public static class NQueenProblem
    {
        /// <summary>
        /// Solves N-Queen Problem given a n dimension chessboard and using backtracking with recursion algorithm.
        /// If we find a dead-end within or current solution we go back and try another position for queen.
        /// </summary>
        /// <param name="board">initial board.</param>
        /// <param name="n">chessboard dimensions.</param>
        /// <param name="col">starting column.</param>
        /// <returns>Matrix solved. </returns>
        public static bool BacktrackSolve(int[,] board, int n, int col)
        {
            if (n == 2 || n == 3)
            {
                // To return an error on known solution exceptions.
                throw new ArgumentException("These dimensions do not have a solution.");
            }

            if (col < 0 || col > n + 1)
            {
                throw new ArgumentException("Out-of-the-board exception.");
            }

            // To check whether we have placed any queen on the board.
            if (col >= n)
            {
                return board.Cast<int>().ToList().Contains(1);
            }

            // To start placing queens on possible spaces within the board.
            for (var i = 0; i < n; i++)
            {
                if (CanPlace(board, i, col))
                {
                    board[i, col] = 1;

                    if (BacktrackSolve(board, n, col + 1))
                    {
                        return true;
                    }

                    // if hitted a dead-end do backtracking.
                    board[i, col] = 0;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks whether current queen can be placed in current position,
        /// outside attacking range of another queen.
        /// </summary>
        /// <param name="board">Source board.</param>
        /// <param name="row">Row coordinate.</param>
        /// <param name="col">Col coordinate.</param>
        /// <returns>true if queen can be placed in given chessboard coordinates; false otherwise.</returns>
        private static bool CanPlace(int[,] board, int row, int col)
        {
            int i;
            int j;

            // To check whether there are any queens on current row.
            for (i = 0; i < col; i++)
            {
                if (board[row, i] == 1)
                {
                    return false;
                }
            }

            // To check diagonal attack top-left range.
            for (i = row, j = col; i >= 0 && j >= 0; i--, j--)
            {
                if (board[i, j] == 1)
                {
                    return false;
                }
            }

            // To check diagonal attack bottom-left range.
            for (i = row, j = col; j >= 0 && i < board.GetLength(0); i++, j--)
            {
                if (board[i, j] == 1)
                {
                    return false;
                }
            }

            // Return true if it can use position.
            return true;
        }
    }
}
