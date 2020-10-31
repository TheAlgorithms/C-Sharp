using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Other
{
    /// <summary>
    /// Luhn algorithm is a simple
    /// checksum formula used to validate
    /// a variety of identification numbers,
    /// such as credit card numbers.
    /// More information on the link:
    /// https://en.wikipedia.org/wiki/Luhn_algorithm.
    /// </summary>
    public static class Luhn
    {
        /// <summary>
        /// Checking the validity of a sequence of numbers.
        /// </summary>
        /// <param name="number">The number that will be checked for validity.</param>
        /// <returns>True: Number is valid.
        /// False: Number isn`t valid.</returns>
        public static bool Validate(string number)
        {
            if (GetSum(number) % 10 == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The sum found by the algorithm.
        /// </summary>
        /// <param name="number">The number for which the amount will be found.</param>
        /// <returns>Sum.</returns>
        private static int GetSum(string number)
        {
            string reverseNumber = Reverse(number);
            int sum = 0;
            int temp;
            for (int i = 0; i < reverseNumber.Length; i++)
            {
                temp = reverseNumber[i] - '0';
                if (i % 2 != 0)
                {
                    temp *= 2;
                    if (temp > 9)
                    {
                        temp -= 9;
                    }

                    sum += temp;
                }
                else
                {
                    sum += temp;
                }
            }

            return sum;
        }

        /// <summary>
        /// Reverses string.
        /// </summary>
        /// <param name="text">The text to be flipped.</param>
        /// <returns>String in reverse order.</returns>
        private static string Reverse(string text)
        {
            char[] tempCharArray = text.ToCharArray();
            Array.Reverse(tempCharArray);
            return new string(tempCharArray);
        }
    }
}
