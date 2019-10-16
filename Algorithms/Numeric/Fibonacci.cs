using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Numeric
{
    /// <summary>
    /// The class Fibonacci handles the generation and printing of the fibonacci sequence up to its nth element.
    /// For additional information, see https://en.wikipedia.org/wiki/Fibonacci_number.
    /// </summary>
    public static class Fibonacci
    {
        /// <summary>
        /// Yields the Fibonacci sequence.
        /// </summary>
        /// <returns>An iterator that contains the Fibonacci sequence.</returns>
        public static IEnumerable<ulong> GetSequence()
        {
            yield return 0;
            yield return 1;
            ulong a = 0;
            ulong b = 1;
            while (true)
            {
                var temp = a + b;
                yield return temp;
                a = b;
                b = temp;
            }
        }
    }
}
