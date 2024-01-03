using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Number of ways of making change for n cents using coins of 1, 2, 5, 10 cents.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A000008.
///     </para>
/// </summary>
public class MakeChangeSequence : ISequence
{
    /// <summary>
    ///     <para>
    ///         Gets sequence of number of ways of making change for n cents
    ///         using coins of 1, 2, 5, 10 cents.
    ///     </para>
    ///     <para>
    ///         Uses formula from OEIS page by Michael Somos
    ///         along with first 17 values to prevent index issues.
    ///     </para>
    ///     <para>
    ///         Formula:
    ///         a(n) = a(n-2) +a(n-5) - a(n-7) + a(n-10) - a(n-12) - a(n-15) + a(n-17) + 1.
    ///     </para>
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var seed = new List<BigInteger>
                       {
                           1, 1, 2, 2, 3, 4, 5, 6, 7, 8,
                           11, 12, 15, 16, 19, 22, 25,
                       };
            foreach (var value in seed)
            {
                yield return value;
            }

            for(var index = 17; ; index++)
            {
                BigInteger newValue = seed[index - 2] + seed[index - 5] - seed[index - 7]
                                    + seed[index - 10] - seed[index - 12] - seed[index - 15]
                                    + seed[index - 17] + 1;

                seed.Add(newValue);
                yield return newValue;
            }
        }
    }
}
