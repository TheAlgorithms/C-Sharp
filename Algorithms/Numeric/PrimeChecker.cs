namespace Algorithms.Numeric;

/// <summary>
///     A prime number (or a prime) is a natural number greater than 1 that is not a product of two smaller natural numbers.
/// </summary>
public static class PrimeChecker
{
    /// <summary>
    ///     Checks if a number is a prime number or not using optimized trial division.
    ///     This method avoids checking multiples of 2 and 3, optimizing the loop by checking
    ///     divisors of the form 6k ± 1 up to the square root of the number.
    /// </summary>
    /// <param name="number">The integer number to check.</param>
    /// <returns>True if the number is prime; False otherwise.</returns>
    public static bool IsPrime(int number)
    {
        // Numbers less than or equal to 1 are not prime.
        if (number <= 1)
        {
            return false;
        }

        // 2 and 3 are the first two prime numbers.
        if (number <= 3)
        {
            return true;
        }

        // Check for divisibility by 2 and 3.
        if (number % 2 == 0 || number % 3 == 0)
        {
            return false;
        }

        // Check for divisibility by numbers of the form 6k ± 1 up to sqrt(number).
        // The loop increments by 6 to skip known non-prime divisors.
        for (int i = 5; i <= number / i; i = i + 6)
        {
            // Check 6k - 1
            if (number % i == 0)
            {
                return false;
            }

            // Check 6k + 1
            if (number % (i + 2) == 0)
            {
                return false;
            }
        }

        // If no divisors are found, the number is prime.
        return true;
    }
}
