using System;
using System.Numerics;

namespace Algorithms.ModularArithmetic;

/// <summary>
/// Modular multiplicative inverse: https://en.wikipedia.org/wiki/Modular_multiplicative_inverse.
/// </summary>
public static class ModularMultiplicativeInverse
{
    /// <summary>
    ///     Computes the modular multiplicative inverse of a in Z/nZ, if there is any (i.e. if a and n are coprime).
    /// </summary>
    /// <param name="a">The number a, of which to compute the multiplicative inverse.</param>
    /// <param name="n">The modulus n.</param>
    /// <returns>The multiplicative inverse of a in Z/nZ, a value in the interval [0, n).</returns>
    /// <exception cref="ArithmeticException">If there exists no multiplicative inverse of a in Z/nZ.</exception>
    public static long Compute(long a, long n)
    {
        var eeaResult = ExtendedEuclideanAlgorithm.Compute(a, n);

        // Check if there is an inverse:
        if (eeaResult.gcd != 1)
        {
            throw new ArithmeticException($"{a} is not invertible in Z/{n}Z.");
        }

        // Make sure, inverseOfA (i.e. the bezout coefficient of a) is in the interval [0, n).
        var inverseOfA = eeaResult.bezoutA;
        if (inverseOfA < 0)
        {
            inverseOfA += n;
        }

        return inverseOfA;
    }

    /// <summary>
    ///     Computes the modular multiplicative inverse of a in Z/nZ, if there is any (i.e. if a and n are coprime).
    /// </summary>
    /// <param name="a">The number a, of which to compute the multiplicative inverse.</param>
    /// <param name="n">The modulus n.</param>
    /// <returns>The multiplicative inverse of a in Z/nZ, a value in the interval [0, n).</returns>
    /// <exception cref="ArithmeticException">If there exists no multiplicative inverse of a in Z/nZ.</exception>
    public static BigInteger Compute(BigInteger a, BigInteger n)
    {
        var eeaResult = ExtendedEuclideanAlgorithm.Compute(a, n);

        // Check if there is an inverse:
        if (eeaResult.gcd != 1)
        {
            throw new ArithmeticException($"{a} is not invertible in Z/{n}Z.");
        }

        // Make sure, inverseOfA (i.e. the bezout coefficient of a) is in the interval [0, n).
        var inverseOfA = eeaResult.bezoutA;
        if (inverseOfA < 0)
        {
            inverseOfA += n;
        }

        return inverseOfA;
    }
}
