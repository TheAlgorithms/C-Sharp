using System;
using System.Numerics;

namespace Algorithms.Numeric;

/// <summary>
/// https://en.wikipedia.org/wiki/Miller-Rabin_primality_test
/// The Miller–Rabin primality test or Rabin–Miller primality test is a probabilistic primality test:
/// an algorithm which determines whether a given number is likely to be prime,
/// similar to the Fermat primality test and the Solovay–Strassen primality test.
/// It is of historical significance in the search for a polynomial-time deterministic primality test.
/// Its probabilistic variant remains widely used in practice, as one of the simplest and fastest tests known.
/// </summary>
public static class MillerRabinPrimalityChecker
{
    /// <summary>
    ///     Run the probabilistic primality test.
    ///     </summary>
    /// <param name="n">Number to check.</param>
    /// <param name="rounds">Number of rounds, the parameter determines the accuracy of the test, recommended value is Log2(n).</param>
    /// <param name="seed">Seed for random number generator.</param>
    /// <returns>True if is a highly likely prime number; False otherwise.</returns>
    /// <exception cref="ArgumentException">Error: number should be more than 3.</exception>
    public static bool IsProbablyPrimeNumber(BigInteger n, BigInteger rounds, int? seed = null)
    {
        Random rand = seed is null
            ? new()
            : new(seed.Value);
        return IsProbablyPrimeNumber(n, rounds, rand);
    }

    private static bool IsProbablyPrimeNumber(BigInteger n, BigInteger rounds, Random rand)
    {
        if (n <= 3)
        {
            throw new ArgumentException($"{nameof(n)} should be more than 3");
        }

        // Input #1: n > 3, an odd integer to be tested for primality
        // Input #2: k, the number of rounds of testing to perform, recommended k = Log2(n)
        // Output:   false = “composite”
        //           true  = “probably prime”

        // write n as 2r·d + 1 with d odd(by factoring out powers of 2 from n − 1)
        BigInteger r = 0;
        BigInteger d = n - 1;
        while (d % 2 == 0)
        {
            r++;
            d /= 2;
        }

        // as there is no native random function for BigInteger we suppose a random int number is sufficient
        int nMaxValue = (n > int.MaxValue) ? int.MaxValue : (int)n;
        BigInteger a = rand.Next(2, nMaxValue - 2); // ; pick a random integer a in the range[2, n − 2]

        while (rounds > 0)
        {
            rounds--;
            var x = BigInteger.ModPow(a, d, n);
            if (x == 1 || x == (n - 1))
            {
                continue;
            }

            BigInteger tempr = r - 1;
            while (tempr > 0 && (x != n - 1))
            {
                tempr--;
                x = BigInteger.ModPow(x, 2, n);
            }

            if (x == n - 1)
            {
                continue;
            }

            return false;
        }

        return true;
    }
}
