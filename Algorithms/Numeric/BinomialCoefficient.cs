using System;

namespace Algorithms.Numeric
{
    /// <summary>
    /// The binomial coefficients are the positive integers
    /// that occur as coefficients in the binomial theorem.
    /// </summary>
    public static class BinomialCoefficient
    {
        /// <summary>
        /// Calculates Binomial coefficients for given input.
        /// </summary>
        /// <param name="num">First number.</param>
        /// <param name="k">Second number.</param>
        /// <returns>Binimial Coefficients.</returns>
        public static long Calculate(int num, int k)
        {
            if (num < k || k < 0)
            {
                throw new ArgumentException("n ≥ k ≥ 0");
            }

            return Factorial.Calculate(num) / (Factorial.Calculate(k) * Factorial.Calculate(num - k));
        }
    }
}
