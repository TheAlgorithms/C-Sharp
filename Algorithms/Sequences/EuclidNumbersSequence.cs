using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of Euclid numbers: 1 + product of the first n primes.
///     </para>
///     <para>
///         Wikipedia: https://wikipedia.org/wiki/Euclid_number.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A006862.
///     </para>
/// </summary>
public class EuclidNumbersSequence : ISequence
{
    /// <summary>
    /// Gets sequence of Euclid numbers.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var primorialNumbers = new PrimorialNumbersSequence().Sequence;

            foreach (var n in primorialNumbers)
            {
                yield return n + 1;
            }
        }
    }
}
