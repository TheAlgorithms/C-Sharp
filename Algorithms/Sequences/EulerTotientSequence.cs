using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algorithms.Sequences
{
    public class EulerTotientSequence : ISequence
    {
        public IEnumerable<BigInteger> Sequence
        {
            get
            {
                yield return BigInteger.One;

                for (BigInteger i = 2; ; i++)
                {
                    var primes = new List<BigInteger>();
                    var factors = PrimeFactors(i);
                    var n = i;
                    var result = i;

                    foreach (var factor in factors)
                    {
                        if (n % factor == 0)
                        {
                            while (n % factor == 0)
                            {
                                n /= factor;
                            }

                            result -= result / factor;
                        }
                    }

                    if (n > 1)
                    {
                        result -= result / n;
                    }

                    yield return result;
                }
            }
        }

        private IEnumerable<BigInteger> PrimeFactors(BigInteger target)
        {
            return (
                       from prime in new PrimesSequence()
                                    .Sequence.TakeWhile(prime => prime * prime <= target)
                       let test = target / prime
                       where test * prime == target
                       select prime).ToList();
        }
    }
}
