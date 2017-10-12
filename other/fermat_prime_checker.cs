using System;
using System.Numerics;

namespace ConsoleTest
{
    class Program
    {
		// Fermat's prime tester https://en.wikipedia.org/wiki/Fermat_primality_test
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Fermat's prime tester");
            Console.WriteLine("--------------------------------");
            Console.Write("Introduce a number to check if it is prime: ");
            String numberToTestEntry = Console.ReadLine();
            Console.Write("How many times this test is going to be run?: ");
            String timesToCheckEntry = Console.ReadLine();

            int numberToTest = int.Parse(numberToTestEntry);
            int timesToCheck = int.Parse(timesToCheckEntry);

            // You have to use BigInteger for two reasons:
            //   1. The pow operation between two int numbers usually overflows an int
            //   2. The pow and modular operation is very optimized
            BigInteger numberToTestBigInteger = new BigInteger(numberToTest);
            BigInteger exponentBigInteger = new BigInteger(numberToTest-1);

            //Create a random number generator using the current time as seed
            Random r = new Random(new DateTime().Millisecond);

            int iterator = 1;
            bool prime = true;

            while (iterator < timesToCheck && prime)
            {
                int randomNumber = r.Next(1, numberToTest);
                BigInteger randomNumberBigInteger = new BigInteger(randomNumber);
                if (BigInteger.ModPow(randomNumberBigInteger,exponentBigInteger,numberToTestBigInteger) != 1)
                {
                    prime = false;
                }
                iterator++;
            }

            if (prime)
            {
                Console.WriteLine($"The number {0} seems prime",numberToTestEntry);
                return;
            }
            Console.WriteLine($"The number {0} isn't prime",numberToTestEntry);     
        }
    }
}