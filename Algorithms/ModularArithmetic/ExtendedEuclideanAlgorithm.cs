using System.Numerics;

namespace Algorithms.ModularArithmetic
{
    /// <summary>
    ///     Extended Euclidean algorithm: https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm.
    /// </summary>
    public static class ExtendedEuclideanAlgorithm
    {
        /// <summary>
        ///     Computes the greatest common divisor (gcd) of integers a and b, also the coefficients of Bézout's identity,
        ///     which are integers x and y such that a*bezoutCoefficientOfA + b*bezoutCoefficientOfB = gcd(a, b).
        /// </summary>
        /// <param name="a">Input number.</param>
        /// <param name="b">Second input number.</param>
        /// <param name="bezoutOfA">The Bézout coefficient of a.</param>
        /// <param name="bezoutOfB">The Bézout coefficient of b.</param>
        /// <returns>The greatest common divisor of a and b.</returns>
        public static long Compute(long a, long b, out long bezoutOfA, out long bezoutOfB)
        {
            long quotient;
            long tmp;
            var s = 0L;
            bezoutOfA = 1L;
            var r = b;
            var gcd = a;
            bezoutOfB = 0L;

            while (r != 0)
            {
                quotient = gcd / r; // integer division

                tmp = gcd;
                gcd = r;
                r = tmp - quotient * r;

                tmp = bezoutOfA;
                bezoutOfA = s;
                s = tmp - quotient * s;
            }

            if (b != 0)
            {
                bezoutOfB = (gcd - bezoutOfA * a) / b; // integer division
            }

            return gcd;
        }

        /// <summary>
        ///     Computes the greatest common divisor (gcd) of integers a and b, also the coefficients of Bézout's identity,
        ///     which are integers x and y such that a*bezoutCoefficientOfA + b*bezoutCoefficientOfB = gcd(a, b).
        /// </summary>
        /// <param name="a">Input number.</param>
        /// <param name="b">Second input number.</param>
        /// <param name="bezoutOfA">The Bézout coefficient of a.</param>
        /// <param name="bezoutOfB">The Bézout coefficient of b.</param>
        /// <returns>The greatest common divisor of a and b.</returns>
        public static BigInteger Compute(BigInteger a, BigInteger b, out BigInteger bezoutOfA, out BigInteger bezoutOfB)
        {
            BigInteger quotient;
            BigInteger tmp;
            var s = BigInteger.Zero;
            bezoutOfA = BigInteger.One;
            var r = b;
            var gcd = a;
            bezoutOfB = BigInteger.Zero;

            while (r != 0)
            {
                quotient = gcd / r; // integer division

                tmp = gcd;
                gcd = r;
                r = tmp - quotient * r;

                tmp = bezoutOfA;
                bezoutOfA = s;
                s = tmp - quotient * s;
            }

            if (b != 0)
            {
                bezoutOfB = (gcd - bezoutOfA * a) / b; // integer division
            }

            return gcd;
        }
    }
}
