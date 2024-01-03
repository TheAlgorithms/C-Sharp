using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of Kummer numbers (also called Euclid numbers of the second kind):
///         -1 + product of first n consecutive primes.
///     </para>
///     <para>
///         Wikipedia: https://wikipedia.org/wiki/Euclid_number.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A057588.
///     </para>
/// </summary>
public class KummerNumbersSequence : ISequence
{
    /// <summary>
    /// Gets sequence of Kummer numbers.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var primorialNumbers = new PrimorialNumbersSequence().Sequence.Skip(1);

            foreach (var n in primorialNumbers)
            {
                yield return n - 1;
            }
        }
    }
}
