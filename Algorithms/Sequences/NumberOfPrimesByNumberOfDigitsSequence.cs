using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Number of primes with n digits
///         (The number of primes between 10^(n-1) and 10^n).
///     </para>
///     <para>
///         Wikipedia: https://wikipedia.org/wiki/Prime-counting_function.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A006879.
///     </para>
/// </summary>
public class NumberOfPrimesByNumberOfDigitsSequence : ISequence
{
    /// <summary>
    /// Gets sequence of number of primes.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            ISequence primes = new PrimesSequence();
            var powerOf10 = new BigInteger(1);
            var counter = new BigInteger(0);

            foreach (var p in primes.Sequence)
            {
                if (p > powerOf10)
                {
                    yield return counter;
                    counter = 0;
                    powerOf10 *= 10;
                }

                counter++;
            }
        }
    }
}
