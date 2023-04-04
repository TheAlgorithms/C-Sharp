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
        ///     Calculates factorial of a any integer number that implements IBinaryInteger.
        /// </summary>
        /// <param name="num">Any integer that implements IBinaryInteger.</param>
        /// <typeparam name="T">The type of input integer.</typeparam>
        /// <returns>Factorial of integer number.</returns>
        public static BigInteger Calculate<T>(T num) where T : notnull, IBinaryInteger<T>
        {
            BigInteger result = BigInteger.One;
            BigInteger numBigInt = BigInteger.Parse(num.ToString() ?? string.Empty);

            // Don't calculate factorial if input is a negative number.
            if (BigInteger.Compare(numBigInt, BigInteger.Zero) < 0)
            {
                throw new ArgumentException("Only for num >= 0");
            }

            // Factorial of 0 is 1. No need to calculate further.
            if (BigInteger.Compare(numBigInt, BigInteger.Zero) == 0)
            {
                return BigInteger.One;
            }

            // Factorial of numbers greater than 0.
            for (BigInteger i = BigInteger.One; BigInteger.Compare(i, numBigInt) <= 0; i = BigInteger.Add(i, BigInteger.One))
            {
                result = BigInteger.Multiply(result, i);
            }

            return result;
        }
    }
}
