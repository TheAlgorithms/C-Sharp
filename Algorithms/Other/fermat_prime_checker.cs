using System;
using System.Numerics;

namespace ConsoleTest
{
    internal class Program
    {
        // Fermat's prime tester https://en.wikipedia.org/wiki/Fermat_primality_test
        private static void Main()
        {
            Console.WriteLine("Welcome to Fermat's prime tester");
            Console.WriteLine("--------------------------------");
            Console.Write("Introduce a number to check if it is prime: ");
            var numberToTestEntry = Console.ReadLine();
            Console.Write("How many times this test is going to be run?: ");
            var timesToCheckEntry = Console.ReadLine();

            var numberToTest = int.Parse(numberToTestEntry);
            var timesToCheck = int.Parse(timesToCheckEntry);

            // You have to use BigInteger for two reasons:
            //   1. The pow operation between two int numbers usually overflows an int
            //   2. The pow and modular operation is very optimized
            var numberToTestBigInteger = new BigInteger(numberToTest);
            var exponentBigInteger = new BigInteger(numberToTest - 1);

            // Create a random number generator using the current time as seed
            var r = new Random(default(DateTime).Millisecond);

            var iterator = 1;
            var prime = true;

            while (iterator < timesToCheck && prime)
            {
                var randomNumber = r.Next(1, numberToTest);
                var randomNumberBigInteger = new BigInteger(randomNumber);
                if (BigInteger.ModPow(randomNumberBigInteger, exponentBigInteger, numberToTestBigInteger) != 1)
                {
                    prime = false;
                }

                iterator++;
            }

            if (prime)
            {
                Console.WriteLine($"The number {0} seems prime", numberToTestEntry);
                return;
            }

            Console.WriteLine($"The number {0} isn't prime", numberToTestEntry);
        }
    }
}
