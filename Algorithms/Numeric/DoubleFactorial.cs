using System.Numerics;

namespace Algorithms.Numeric;

/// <summary>
///     The double factorial of a positive integer n, denoted by n!!,
///     is the product of all integers from 1 up to n that have the same parity (odd or even) as n.
///     E.g., 5!! = 5 * 3 * 1 = 15, and 6!! = 6 * 4 * 2 = 48.
/// </summary>
public static class DoubleFactorial
{
    /// <summary>
    ///     Calculates the double factorial of a non-negative integer number.
    /// </summary>
    /// <param name="inputNum">Non-negative integer input number.</param>
    /// <returns>Double factorial of the integer input number.</returns>
    public static BigInteger Calculate(int inputNum)
    {
        // Don't calculate double factorial if input is a negative number.
        if (inputNum < 0)
        {
            throw new ArgumentException("Double factorial is only defined for non-negative integers (num >= 0).");
        }

        // Base cases: 0!! = 1 and 1!! = 1
        if (inputNum <= 1)
        {
            return BigInteger.One;
        }

        // Initialize result.
        BigInteger result = BigInteger.One;

        // Start the iteration from the input number and step down by 2.
        // This handles both odd (n, n-2, ..., 3, 1) and even (n, n-2, ..., 4, 2) cases naturally.
        BigInteger current = inputNum;

        while (current > BigInteger.Zero)
        {
            result *= current;

            // Decrease the current number by 2 for the next factor.
            current -= 2;
        }

        return result;
    }
}
