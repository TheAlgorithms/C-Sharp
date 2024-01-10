using System;
using System.Numerics;
using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric;

public static class MillerRabinPrimalityTest
{
    [TestCase("7", ExpectedResult = true)]  // true
    [TestCase("47", ExpectedResult = true)] // true
    [TestCase("247894109041876714378152933343208766493", ExpectedResult = true)] // true
    [TestCase("247894109041876714378152933343208766493", 1, ExpectedResult = true)] // true
    [TestCase("315757551269487563269454472438030700351", ExpectedResult = true)] // true
    [TestCase("2476099", 12445, ExpectedResult = false)] // false 19^5
    // false 247894109041876714378152933343208766493*315757551269487563269454472438030700351
    [TestCase("78274436845194327170519855212507883195883737501141260366253362532531612139043", ExpectedResult = false)]
    [Retry(3)]
    public static bool MillerRabinPrimalityWork(string testcase, int? seed = null)
    {
        // Arrange
        BigInteger number = BigInteger.Parse(testcase);

        // Recommended number of checks' rounds = Log2(number) as BigInteger has no Log2 function we need to convert Log10
        BigInteger rounds = (BigInteger)(BigInteger.Log10(number) / BigInteger.Log10(2));

        // Act
        var result = MillerRabinPrimalityChecker.IsProbablyPrimeNumber(number, rounds, seed);

        // Assert
        return result;
    }

    [TestCase("-2")]
    [TestCase("0")]
    [TestCase("3")]
    // By the algorithm definition the number which is checked should be more than 3
    public static void MillerRabinPrimalityShouldThrowEx(string testcase)
    {
        // Arrange
        BigInteger number = BigInteger.Parse(testcase);
        BigInteger rounds = 1;
        // Assert
        Assert.Throws<ArgumentException>(() => MillerRabinPrimalityChecker.IsProbablyPrimeNumber(number, rounds));
    }
}
