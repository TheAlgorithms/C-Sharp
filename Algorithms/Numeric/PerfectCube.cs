using System;

namespace Algorithms.Numeric
{
    /// <summary>
    /// A perfect cube is an element of algebraic structure that is equal to the cube of another element.
    /// </summary>
    public static class PerfectCube
    {
        /// <summary>
        /// Checks if a number is a perfect cube or not.
        /// </summary>
        /// <param name="number">Number to check.</param>
        /// <returns>True if is a perfect cube; False otherwise.</returns>
        public static bool IsPerfectCube(int number)
        {
            double a = Math.Round(Math.Pow(number, 1.0 / 3));
            return a * a * a == number;
        }
    }
}
