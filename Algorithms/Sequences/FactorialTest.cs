using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sequences
{
    internal class FactorialTest
    {
        public static void Main(string[] args)
        {
            // Utilize n to be your input value.
            var n = 0;
            Console.WriteLine(Factorial(n));
        }

        /// <summary>
        /// Factorial Method finds the nth factorial for the desired input number.
        /// </summary>
        public static string Factorial(int number)
        {
            var factorialNum = 1;
            if (number > 0)
            {
                for (var i = 1; i <= number; i++)
                {
                    factorialNum *= i;
                }

                Console.Write($"Factorial of {number} is = ");
            }
            else if (number == 0)
            {
                return "Factorial of 0 = 1";
            }
            else
            {
                return "Factorial is not defined for a negative number.";
            }

            return factorialNum.ToString();
        }
    }
}
