using System;

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
        public static bool Validate(string number) => GetSum(number) % 10 == 0;

        /// <summary>
        /// This algorithm only finds one number.
        /// In place of the unknown digit, put "x".
        /// </summary>
        /// <param name="number">The number in which to find the missing digit.</param>
        /// <returns>Missing digit.</returns>
        public static int GetLostNum(string number)
        {
            var lostIndex = number.Length - 1 - number.LastIndexOf("x", StringComparison.CurrentCultureIgnoreCase);
            var lostNum = GetSum(number.Replace("x", "0", StringComparison.CurrentCultureIgnoreCase)) * 9 % 10;

            // Case 1: If the index of the lost digit is even.
            if (lostIndex % 2 == 0)
            {
                return lostNum;
            }

            var tempLostNum = lostNum / 2;

            // Case 2: if the index of the lost digit isn`t even and that number <= 4.
            // Case 3: if the index of the lost digit isn`t even and that number > 4.
            return Validate(number.Replace("x", tempLostNum.ToString())) ? tempLostNum : (lostNum + 9) / 2;
        }

        /// <summary>
        /// Computes the sum found by the algorithm.
        /// </summary>
        /// <param name="number">The number for which the sum will be found.</param>
        /// <returns>Sum.</returns>
        private static int GetSum(string number)
        {
            var sum = 0;
            for (var i = 0; i < number.Length; i++)
            {
                var d = number[i] - '0';
                d = (i + number.Length) % 2 == 0
                    ? 2 * d
                    : d;
                if (d > 9)
                {
                    d -= 9;
                }

                sum += d;
            }

            return sum;
        }
    }
}
