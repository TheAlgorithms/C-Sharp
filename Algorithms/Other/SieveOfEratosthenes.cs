using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Other
{
    public class SieveOfEratosthenes
    {
        public static List<int> GetPrimeNumbers(int count)
        {
            var output = new List<int>();
            for (int n = 2, i = 0; n < int.MaxValue && i < count; n++)
            {
                if (output.Any(x => n % x == 0))
                {
                    continue;
                }

                output.Add(n);
                i++;
            }

            return output;
        }
    }
}
