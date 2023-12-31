using System;
using System.Numerics;

namespace Algorithms.Other;

/// <summary>
///     Fermat's prime tester https://en.wikipedia.org/wiki/Fermat_primality_test.
/// </summary>
public static class FermatPrimeChecker
{
    /// <summary>
    ///     Checks if input number is a probable prime.
    /// </summary>
    /// <param name="numberToTest">Input number.</param>
    /// <param name="timesToCheck">Number of times to check.</param>
    /// <returns>True if is a prime; False otherwise.</returns>
    public static bool IsPrime(int numberToTest, int timesToCheck)
    {
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

        return prime;
    }
}
