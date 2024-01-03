using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Number of halving and tripling steps to reach 1 in the '3n+1' problem.
///     </para>
///     <para>
///         Wikipedia: https://en.wikipedia.org/wiki/Collatz_conjecture.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A006577.
///     </para>
/// </summary>
public class ThreeNPlusOneStepsSequence : ISequence
{
    /// <summary>
    /// Gets sequence of number of halving and tripling steps to reach 1 in the '3n+1' problem.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            BigInteger startingValue = 1;

            while (true)
            {
                BigInteger counter = 0;
                BigInteger currentValue = startingValue;

                while (currentValue != 1)
                {
                    if (currentValue.IsEven)
                    {
                        currentValue /= 2;
                    }
                    else
                    {
                        currentValue = 3 * currentValue + 1;
                    }

                    counter++;
                }

                yield return counter;
                startingValue++;
            }
        }
    }
}
