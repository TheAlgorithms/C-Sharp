using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Numeric
{
    /// <summary>
    /// The class Fibonacci handles the generation and printing of the fibonacci sequence up to its nth element.
    /// </summary>
    public class Fibonacci
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Fibonacci"/> class.
        /// </summary>
        /// <param name="size">The number of elements in the Fibonacci sequence to generate.</param>
        public Fibonacci(int size)
        {
            if (size < 1)
            {
                size = 1;
            }

            Size = size;
            GenerateFibonacci();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fibonacci"/> class with a default value of 10 as the value of field <see cref="Size"/>.
        /// </summary>
        public Fibonacci()
            : this(10)
        {
        }

        /// <summary>
        /// Gets or sets the number of elements to generate in the Fibonacci sequence.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Gets the Fibonacci sequence generated via a call to method <see cref="GenerateFibonacci" />.
        /// </summary>
        public ulong[] Sequence { get; private set; }

        /// <summary>
        /// Prints the first n elements in the Fibonacci sequence, where n is equal to the value of <see cref="Size" />.
        /// </summary>
        public void PrintFibonacci()
        {
            if (Sequence.Length != Size)
            {
                GenerateFibonacci();
            }

            foreach (ulong element in Sequence)
            {
                Console.WriteLine(element);
            }
        }

        /// <summary>
        /// Generates the first n elements in the Fibonacci sequence, where n is equal to the value of <see cref="Size" />.
        /// </summary>
        private void GenerateFibonacci()
        {
            Sequence = new ulong[Size];
            Sequence[0] = 0;
            if (Size >= 2)
            {
                Sequence[1] = 1;
            }

            for (int i = 2; i < Size; i++)
            {
                Sequence[i] = Sequence[i - 1] + Sequence[i - 2];
            }
        }
    }
}
