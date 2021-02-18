using System;

namespace Algorithms.Numeric
{
    /// <summary>
    /// A Narcissistic number is equal to the sum of the cubes of its digits. For example, 370 is a
    /// Narcissistic number because 3*3*3 + 7*7*7 + 0*0*0 = 370.
    /// </summary>
    public static class NarcissisticNumber
    {
        /// <summary>
        /// Checks if a number is a Narcissistic number or not.
        /// </summary>
        /// <param name="number">Number to check.</param>
        /// <returns>True if is a Narcissistic number; False otherwise.</returns>
        public static bool IsNarcissistic(int number)
        {
            int sum = 0;
            int temp = number;
            int numberOfDigits = 0;
            while (temp != 0)
            {
                numberOfDigits++;
                temp /= 10;
            }

            temp = number;
            while (number > 0)
            {
                int remainder = number % 10;
                int power = (int)Math.Pow(remainder, numberOfDigits);

                sum += power;
                number /= 10;
            }

            return sum == temp;
        }
    }
}
