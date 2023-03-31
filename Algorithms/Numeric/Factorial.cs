using System;
using System.Numerics;

namespace Algorithms.Numeric
{
    /// <summary>
    ///     The factorial of a positive integer n, denoted by n!,
    ///     is the product of all positive integers less than or equal to n.
    /// </summary>
    public static class Factorial
    {
        /// <summary>
        ///     Calculates factorial of a number.
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

        /// <summary>
        ///     Calculates factorial of a big number.
        /// </summary>
        /// <param name="num">Big Input number.</param>
        /// <returns>Factorial of Big input number.</returns>
        public static BigInteger Calculate(BigInteger num)
        {
            // Don't calculate factorial if input is a negative number.
            if (BigInteger.Compare(num, BigInteger.Zero) < 0)
            {
                throw new ArgumentException("Only for num >= 0");
            }

            // Factorial of 0 is 1. No need to calculate further.
            if(BigInteger.Compare(num, BigInteger.Zero) == 0)
            {
                return BigInteger.One;
            }

            // Factorial of numbers greater than 0.
            BigInteger result = BigInteger.One;

            for (BigInteger i = BigInteger.One; BigInteger.Compare(i, num) <= 0; i = BigInteger.Add(i, BigInteger.One))
            {
                result = BigInteger.Multiply(result, i);
            }

            return result;
        }
    }
}
