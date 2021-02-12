using System;

namespace Algorithms.Numerics
{
    /// <summary>
    /// A perfect square is an element of algebraic structure that is equal to the square of another element.
    /// </summary>
    public static class PerfectSquare
    {
        /// <summary>
        /// Checks if a number is a perfect square or not.
        /// </summary>
        /// <param name="number">Number too check.</param>
        /// <returns>True if is a perfect square; False otherwise.</returns>
        /// <exception cref="ArgumentException">Error number is not on interval (0.0; int.MaxValue).</exception>
        public static bool IsPerfectSquare(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException(string.Format("Error number is not on interval (0.0; {0}).", int.MaxValue));
            }

            int sqrt = (int)Math.Sqrt(number);
            return sqrt * sqrt == number;
        }
    }
}
