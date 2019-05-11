using System;

namespace prime_finder
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Enter the number you want to check:");
            var numStr = Console.ReadLine();

            if (!int.TryParse(numStr, out var num) | num < 2)
            {
                Console.WriteLine("Invalid input! Please enter a number higher than 1");
                return;
            }

            if (CheckPrime(num))
            {
                Console.WriteLine(num + " is a prime!");
            }
            else
            {
                Console.WriteLine(num + " is not a prime!");
            }
        }

        private static bool CheckPrime(int prime)
        {
            if (prime % 2 == 0)
            {
                return false;
            }

            for (int i = 3, n = (int)Math.Sqrt(prime); i < n; i += 2)
            {
                if (prime % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}