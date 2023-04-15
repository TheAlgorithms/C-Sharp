using System;

namespace Algorithms.Numeric;

public static class JosephusProblem
{
    /// <summary>
    /// Calculates the winner in the Josephus problem.
    /// </summary>
    /// <param name="n">The number of people in the initial circle.</param>
    /// <param name="k">The count of each step. k-1 people are skipped and the k-th is executed.</param>
    /// <returns>The 1-indexed position where the player must choose in order to win the game.</returns>
    public static long FindWinner(long n, long k)
    {
        if (k <= 0)
        {
            throw new ArgumentException("The step cannot be smaller than 1");
        }

        if (k > n)
        {
            throw new ArgumentException("The step cannot be greater than the size of the group");
        }

        long winner = 0;
        for (long stepIndex = 1; stepIndex <= n; ++stepIndex)
        {
            winner = (winner + k) % stepIndex;
        }

        return winner + 1;
    }
}
