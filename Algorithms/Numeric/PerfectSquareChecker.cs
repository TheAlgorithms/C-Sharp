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
        /// <returns>Tuple (True, squareroot) if the number is a perfect square; (False, 0) otherwise.</returns>
        public static (bool IsPerfectSquare, int Root) IsPerfectSquareIntOp(int number)
        {
            if (number < 0)
            {// A square number must be positive
                return (false, 0);
            }
            else if (number < 2)
            {// For positive numbers that are less than 2, i.e {0, 1}, they are squareroots of themselves
                return (true, number);
            }
            else
            {// Implementing a binary search for the number
                var lb = 2;
                var ub = number >> 1;
                while (lb <= ub)
                {
                    var mid = lb / 2 + ub / 2;
                    var sq = mid * mid;
                    if (sq == number)
                    {
                        return (true, mid);
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

                return (false, 0);
            }
        }
    }
}
