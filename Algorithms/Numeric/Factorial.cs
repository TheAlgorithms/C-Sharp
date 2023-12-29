using System;
using System.Numerics;

namespace Algorithms.Numeric;

/// <summary>
///     The factorial of a positive integer n, denoted by n!,
///     is the product of all positive integers less than or equal to n.
/// </summary>
public static class Factorial
{
    /// <summary>
    ///     Calculates factorial of a integer number.
    /// </summary>
    /// <param name="inputNum">Integer Input number.</param>
    /// <returns>Factorial of integer input number.</returns>
    public static BigInteger Calculate(int inputNum)
    {
        // Convert integer input to BigInteger
        BigInteger num = new BigInteger(inputNum);

        // Don't calculate factorial if input is a negative number.
        if (BigInteger.Compare(num, BigInteger.Zero) < 0)
        {
            throw new ArgumentException("Only for num >= 0");
        }

        // Factorial of numbers greater than 0.
        BigInteger result = BigInteger.One;

        for (BigInteger i = BigInteger.One; BigInteger.Compare(i, num) <= 0; i = BigInteger.Add(i, BigInteger.One))
        {
            result = BigInteger.Multiply(result, i);
        }

        return result;
    }
}
