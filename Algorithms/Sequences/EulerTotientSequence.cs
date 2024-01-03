using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of Euler totient function phi(n).
///     </para>
///     <para>
///         Wikipedia: https://en.wikipedia.org/wiki/Euler%27s_totient_function.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A000010.
///     </para>
/// </summary>
public class EulerTotientSequence : ISequence
{
    /// <summary>
    ///     <para>
    ///         Gets sequence of Euler totient function phi(n).
    ///     </para>
    ///     <para>
    ///         'n' is copied from value of the loop of i that's being enumerated over.
    ///         1) Initialize result as n
    ///         2) Consider every number 'factor' (where 'factor' is a prime divisor of n).
    ///            If factor divides n, then do following
    ///            a) Subtract all multiples of factor from 1 to n [all multiples of factor
    ///               will have gcd more than 1 (at least factor) with n]
    ///            b) Update n by repeatedly dividing it by factor.
    ///         3) If the reduced n is more than 1, then remove all multiples
    ///            of n from result.
    ///     </para>
    ///     <para>
    ///         Base code was from https://www.geeksforgeeks.org/eulers-totient-function/.
    ///      </para>
    ///     <para>
    ///         Implementation avoiding floating point operations was used for base
    ///         and replacement of loop going from 1 to sqrt(n) was replaced with
    ///         List of prime factors.
    ///     </para>
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            yield return BigInteger.One;

            for (BigInteger i = 2; ; i++)
            {
                var n = i;
                var result = n;

                var factors = PrimeFactors(i);
                foreach (var factor in factors)
                {
                    while (n % factor == 0)
                    {
                        n /= factor;
                    }

                    result -= result / factor;
                }

                if (n > 1)
                {
                    result -= result / n;
                }

                yield return result;
            }
        }
    }

    /// <summary>
    ///     <para>
    ///         Uses the prime sequence to find all prime factors of the
    ///         number we're looking at.
    ///     </para>
    ///     <para>
    ///         The prime sequence is examined until its value squared is
    ///         less than or equal to target, and checked to make sure it
    ///         evenly divides the target.  If it evenly divides, it's added
    ///         to the result which is returned as a List.
    ///     </para>
    /// </summary>
    /// <param name="target">Number that is being factored.</param>
    /// <returns>List of prime factors of target.</returns>
    private static IEnumerable<BigInteger> PrimeFactors(BigInteger target)
    {
        return new PrimesSequence()
              .Sequence.TakeWhile(prime => prime * prime <= target)
              .Where(prime => target % prime == 0)
              .ToList();
    }
}
