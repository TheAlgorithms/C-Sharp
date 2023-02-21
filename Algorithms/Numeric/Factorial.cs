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
        /// <param name="num">Input number. Caller responsible for num >= 0.</param>
        /// <returns>Factorial of input number.</returns>
        public static BigInteger Calculate(BigInteger num)
        {
            BigInteger result = BigInteger.One;

            for(BigInteger i = 2; i <= num; i++)
            {
                result *= i;
            }

            return result;
        }
    }
}
