using System;
using System.Numerics;

namespace Algorithms;

public static class NewtonSquareRoot
{
    public static BigInteger Calculate(BigInteger number)
    {
        if (number < 0)
        {
            throw new ArgumentException("Cannot calculate the square root of a negative number.");
        }

        if (number == 0)
        {
            return BigInteger.Zero;
        }

        var bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(number, 2)));
        BigInteger root = BigInteger.One << (bitLength / 2);

        while (!IsSquareRoot(number, root))
        {
            root += number / root;
            root /= 2;
        }

        return root;
    }

    private static bool IsSquareRoot(BigInteger number, BigInteger root)
    {
        var lowerBound = root * root;
        return number >= lowerBound && number <= lowerBound + root + root;
    }
}
