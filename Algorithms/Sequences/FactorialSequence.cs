using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences
{
    /// <summary>
    /// <para>
    ///     Sequence of factorial numbers.
    /// </para>
    /// <para>
    ///     Wikipedia: https://en.wikipedia.org/wiki/Factorial.
    /// </para>
    /// <para>
    ///     OEIS: https://oeis.org/A000142.
    /// </para>
    /// </summary>
    public class FactorialSequence : ISequence
    {
        /// <summary>
        /// Gets sequence of factorial numbers.
        /// </summary>
        public IEnumerable<BigInteger> Sequence
        {
            get
            {
                var n = 0;
                var factorial = new BigInteger(1);
                while (true)
                {
                    yield return factorial;
                    n++;
                    factorial *= n;
                }
            }
        }
    }
}
