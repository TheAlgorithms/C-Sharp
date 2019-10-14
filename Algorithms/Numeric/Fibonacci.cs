using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Numeric
{
    /// <summary>
    /// The class Fibonacci handles the generation and printing of the fibonacci sequence up to its nth element.
    /// For additional information, see https://en.wikipedia.org/wiki/Fibonacci_number.
    /// </summary>
    public abstract class Fibonacci
    {
        /// <summary>
        /// Generates and returns the first n elements of the Fibonacci sequence.
        /// </summary>
        /// <param name="size">The number of elements in the sequence to generate.</param>
        /// <returns>An IEnumerable of ulong variables containing the first elements of the Fibonacci sequence.</returns>
        /// <exception cref="ArgumentOutOfRangeException">This exception is thrown if a negative size is specified.</exception>
        public static IEnumerable<ulong> GetSequence(int size)
        {
            List<ulong> sequence = new List<ulong>(size);
            if (size >= 2)
            {
                sequence.Add(0);
                sequence.Add(1);
                ulong a = 0;
                ulong b = 1;
                for (int i = 2; i < size; ++i)
                {
                    sequence.Add(a + b);

                    ulong temp = a + b;
                    a = b;
                    b = temp;
                }
            }

            return sequence;
        }
    }
}
