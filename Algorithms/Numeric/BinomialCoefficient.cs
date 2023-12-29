using System;
using System.Numerics;

namespace Algorithms.Numeric;

/// <summary>
///     The binomial coefficients are the positive integers
///     that occur as coefficients in the binomial theorem.
/// </summary>
public static class BinomialCoefficient
{
    /// <summary>
    ///     Calculates Binomial coefficients for given input.
    /// </summary>
    /// <param name="num">First number.</param>
    /// <param name="k">Second number.</param>
    /// <returns>Binimial Coefficients.</returns>
    public static BigInteger Calculate(BigInteger num, BigInteger k)
    {
        if (num < k || k < 0)
        {
            throw new ArgumentException("num ≥ k ≥ 0");
        }

        // Tricks to gain performance:
        // 1. Because (num over k) equals (num over (num-k)), we can save multiplications and divisions
        // by replacing k with the minimum of k and (num - k).
        k = BigInteger.Min(k, num - k);

        // 2. We can simplify the computation of (num! / (k! * (num - k)!)) to ((num * (num - 1) * ... * (num - k + 1) / (k!))
        // and thus save some multiplications and divisions.
        var numerator = BigInteger.One;
        for (var val = num - k + 1; val <= num; val++)
        {
            numerator *= val;
        }

        // 3. Typically multiplication is a lot faster than division, therefore compute the value of k! first (i.e. k - 1 multiplications)
        // and then divide the numerator by the denominator (i.e. 1 division); instead of performing k - 1 divisions (1 for each factor in k!).
        var denominator = BigInteger.One;
        for (var val = k; val > BigInteger.One; val--)
        {
            denominator *= val;
        }

        return numerator / denominator;
    }
}
