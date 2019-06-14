using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Other
{
    public static class SieveOfEratosthenes
    {
        public static List<int> GetPrimeNumbers(int count)
        {
            var output = new List<int>();
            var primesFound = 0;
            for (var n = 2; n < int.MaxValue && primesFound < count; n++)
            {
                if (output.Any(x => n % x == 0))
                {
                    continue;
                }

                output.Add(n);
                primesFound++;
            }

            return output;
        }
    }
}
