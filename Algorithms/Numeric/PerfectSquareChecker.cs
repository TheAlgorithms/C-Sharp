using System;

namespace Algorithms.Numeric
{
    /// <summary>
    ///     A perfect square is an element of algebraic structure that is equal to the square of another element.
    /// </summary>
    public static class PerfectSquareChecker
    {
        /// <summary>
        ///     Checks if a number is a perfect square or not.
        /// </summary>
        /// <param name="number">Number too check.</param>
        /// <returns>True if is a perfect square; False otherwise.</returns>
        public static bool IsPerfectSquare(int number)
        {
            if (number < 0)
            {
                return false;
            }

            var sqrt = (int)Math.Sqrt(number);
            return sqrt * sqrt == number;
        }

        /// <summary>Checks if a number is square or not. This method only uses integer operations.</summary>
        /// <param name="number">The number to check.</param>
        /// <param name="root">The squareroot, if the number is indeed a perfect square.</param>
        /// <returns>True if the number is a perfect square; False otherwise.</returns>
        public static bool IsPerfectSquare(int number, out int root)
        {
            if (number < 0)
            {// A square number must be positive
                root = 0;
                return false;
            }
            else if (number < 2)
            {// For positive numbers that are less than 2, i.e {0, 1}, they are squareroots of themselves
                root = number;
                return true;
            }
            else
            {// Implementing a binary search for the number
                root = 0;
                int lb = 2, ub = number >> 1, mid, sq;
                while (lb <= ub)
                {
                    mid = lb / 2 + ub / 2;
                    sq = mid * mid;
                    if (sq == number)
                    {
                        root = mid;
                        return true;
                    }
                    else if (sq < number)
                    {
                        lb = mid + 1;
                    }
                    else
                    {
                        ub = mid - 1;
                    }
                }

                return false;
            }
        }
    }
}
