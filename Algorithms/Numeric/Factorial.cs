using System;

namespace Algorithms.Numeric
{
    /// <summary>
    /// The factorial of a positive integer n, denoted by n!,
    /// is the product of all positive integers less than or equal to n.
    /// </summary>
    public static class Factorial
    {
        /// <summary>
        /// Calculates factorial of a number.
        /// </summary>
        /// <param name="num">Input number.</param>
        /// <returns>Factorial of input number.</returns>
        public static long Calculate(int num)
        {
            if (num < 0)
            {
                throw new ArgumentException("Only for num >= 0");
            }

            return num == 0 ? 1 : num * Calculate(num - 1);
        }
    }
}
