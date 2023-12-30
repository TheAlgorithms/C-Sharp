using System.Numerics;

namespace Algorithms.ModularArithmetic;

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
    /// <returns>A record of ExtendedEuclideanAlgorithmResult containing the bezout coefficients of a and b as well as the gcd(a,b).</returns>
    public static ExtendedEuclideanAlgorithmResult<long> Compute(long a, long b)
    {
        long quotient;
        long tmp;
        var s = 0L;
        var bezoutOfA = 1L;
        var r = b;
        var gcd = a;
        var bezoutOfB = 0L;

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

        return new ExtendedEuclideanAlgorithmResult<long>(bezoutOfA, bezoutOfB, gcd);
    }

    /// <summary>
    ///     Computes the greatest common divisor (gcd) of integers a and b, also the coefficients of Bézout's identity,
    ///     which are integers x and y such that a*bezoutCoefficientOfA + b*bezoutCoefficientOfB = gcd(a, b).
    /// </summary>
    /// <param name="a">Input number.</param>
    /// <param name="b">Second input number.</param>
    /// <returns>A record of ExtendedEuclideanAlgorithmResult containing the bezout coefficients of a and b as well as the gcd(a,b).</returns>
    public static ExtendedEuclideanAlgorithmResult<BigInteger> Compute(BigInteger a, BigInteger b)
    {
        BigInteger quotient;
        BigInteger tmp;
        var s = BigInteger.Zero;
        var bezoutOfA = BigInteger.One;
        var r = b;
        var gcd = a;
        var bezoutOfB = BigInteger.Zero;

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

        return new ExtendedEuclideanAlgorithmResult<BigInteger>(bezoutOfA, bezoutOfB, gcd);
    }

    /// <summary>
    /// The result type for the computation of the Extended Euclidean Algorithm.
    /// </summary>
    /// <typeparam name="T">The data type of the computation (i.e. long or BigInteger).</typeparam>
    /// <param name="bezoutA">The bezout coefficient of the parameter a to the computation.</param>
    /// <param name="bezoutB">The bezout coefficient of the parameter b to the computation.</param>
    /// <param name="gcd">The greatest common divisor of the parameters a and b to the computation.</param>
    public record ExtendedEuclideanAlgorithmResult<T>(T bezoutA, T bezoutB, T gcd);
}
