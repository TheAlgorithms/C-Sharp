namespace Algorithms.Problems.KnightTour;

/// <summary>
/// Computes a (single) Knight's Tour on an <c>n × n</c> chessboard using
/// depth-first search (DFS) with backtracking.
/// </summary>
/// <remarks>
/// <para>
/// A Knight's Tour is a sequence of knight moves that visits every square exactly once.
/// This implementation returns the first tour it finds (if any), starting from whichever
/// starting cell leads to a solution first. It explores every board square as a potential
/// starting position in row-major order.
/// </para>
/// <para>
/// The algorithm is a plain backtracking search—no heuristics (e.g., Warnsdorff’s rule)
/// are applied. As a result, runtime can grow exponentially with <c>n</c> and become
/// impractical on larger boards.
/// </para>
/// <para>
/// <b>Solvability (square boards):</b>
/// A (non-closed) tour exists for <c>n = 1</c> and for all <c>n ≥ 5</c>.
/// There is no tour for <c>n ∈ {2, 3, 4}</c>. This implementation throws an
/// <see cref="ArgumentException"/> if no tour is found.
/// </para>
/// <para>
/// <b>Coordinate convention:</b> The board is indexed as <c>[row, column]</c>,
/// zero-based, with <c>(0,0)</c> in the top-left corner.
/// </para>
/// </remarks>
public sealed class OpenKnightTour
{
    /// <summary>
    /// Attempts to find a Knight's Tour on an <c>n × n</c> board.
    /// </summary>
    /// <param name="n">Board size (number of rows/columns). Must be positive.</param>
    /// <returns>
    /// A 2D array of size <c>n × n</c> where each cell contains the
    /// 1-based visit order (from <c>1</c> to <c>n*n</c>) of the knight.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="n"/> ≤ 0, or when no tour exists / is found for the given <paramref name="n"/>.
    /// </exception>
    /// <remarks>
    /// <para>
    /// This routine tries every square as a starting point. As soon as a complete tour is found,
    /// the filled board is returned. If no tour is found, an exception is thrown.
    /// </para>
    /// <para>
    /// <b>Performance:</b> Exponential in the worst case. For larger boards, consider adding
    /// Warnsdorff’s heuristic (choose next moves with the fewest onward moves) or a hybrid approach.
    /// </para>
    /// </remarks>
    public int[,] Tour(int n)
    {
        if (n <= 0)
        {
            throw new ArgumentException("Board size must be positive.", nameof(n));
        }

        var board = new int[n, n];

        // Try every square as a starting point.
        for (var r = 0; r < n; r++)
        {
            for (var c = 0; c < n; c++)
            {
                board[r, c] = 1; // first step
                if (KnightTourHelper(board, (r, c), 1))
                {
                    return board;
                }

                board[r, c] = 0; // backtrack and try next start
            }
        }

        throw new ArgumentException($"Knight Tour cannot be performed on a board of size {n}.");
    }

    /// <summary>
    /// Recursively extends the current partial tour from <paramref name="pos"/> after placing
    /// move number <paramref name="current"/> in that position.
    /// </summary>
    /// <param name="board">The board with placed move numbers; <c>0</c> means unvisited.</param>
    /// <param name="pos">Current knight position (<c>Row</c>, <c>Col</c>).</param>
    /// <param name="current">The move number just placed at <paramref name="pos"/>.</param>
    /// <returns><c>true</c> if a full tour is completed; <c>false</c> otherwise.</returns>
    /// <remarks>
    /// Tries each legal next move in a fixed order (no heuristics). If a move leads to a dead end,
    /// it backtracks by resetting the target cell to <c>0</c> and tries the next candidate.
    /// </remarks>
    private bool KnightTourHelper(int[,] board, (int Row, int Col) pos, int current)
    {
        if (IsComplete(board))
        {
            return true;
        }

        foreach (var (nr, nc) in GetValidMoves(pos, board.GetLength(0)))
        {
            if (board[nr, nc] == 0)
            {
                board[nr, nc] = current + 1;

                if (KnightTourHelper(board, (nr, nc), current + 1))
                {
                    return true;
                }

                board[nr, nc] = 0; // backtrack
            }
        }

        return false;
    }

    /// <summary>
    /// Computes all legal knight moves from <paramref name="position"/> on an <c>n × n</c> board.
    /// </summary>
    /// <param name="position">Current position (<c>R</c>, <c>C</c>).</param>
    /// <param name="n">Board dimension (rows = columns = <paramref name="n"/>).</param>
    /// <returns>
    /// An enumeration of on-board destination coordinates. Order is fixed and unoptimized:
    /// <c>(+1,+2), (-1,+2), (+1,-2), (-1,-2), (+2,+1), (+2,-1), (-2,+1), (-2,-1)</c>.
    /// </returns>
    /// <remarks>
    /// Keeping a deterministic order makes the search reproducible, but it’s not necessarily fast.
    /// To accelerate, pre-sort by onward-degree (Warnsdorff) or by a custom heuristic.
    /// </remarks>
    private IEnumerable<(int R, int C)> GetValidMoves((int R, int C) position, int n)
    {
        var r = position.R;
        var c = position.C;

        var candidates = new (int Dr, int Dc)[]
        {
            (1,  2), (-1,  2), (1, -2), (-1, -2),
            (2,  1), (2, -1), (-2,  1), (-2, -1),
        };

        foreach (var (dr, dc) in candidates)
        {
            var nr = r + dr;
            var nc = c + dc;

            if (nr >= 0 && nr < n && nc >= 0 && nc < n)
            {
                yield return (nr, nc);
            }
        }
    }

    /// <summary>
    /// Checks whether the tour is complete; i.e., every cell is non-zero.
    /// </summary>
    /// <param name="board">The board to check.</param>
    /// <returns><c>true</c> if all cells have been visited; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// A complete board means the knight has visited exactly <c>n × n</c> distinct cells.
    /// </remarks>
    private bool IsComplete(int[,] board)
    {
        var n = board.GetLength(0);
        for (var row = 0; row < n; row++)
        {
            for (var col = 0; col < n; col++)
            {
                if (board[row, col] == 0)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
