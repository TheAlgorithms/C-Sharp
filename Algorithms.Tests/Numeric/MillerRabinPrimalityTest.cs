using System;
using System.Numerics;
using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric
{
    class MillerRabinPrimalityTest
    {
        [Test]
        [TestCase("7", ExpectedResult = true)]  // true
        [TestCase("47", ExpectedResult = true)] // true
        [TestCase("247894109041876714378152933343208766493", ExpectedResult = true)] // true
        [TestCase("315757551269487563269454472438030700351", ExpectedResult = true)] // true

        [TestCase("2476099", ExpectedResult = false)]       // false 19^5
        // false 247894109041876714378152933343208766493*315757551269487563269454472438030700351
        [TestCase("78274436845194327170519855212507883195883737501141260366253362532531612139043", ExpectedResult = false)]
        public static bool MillerRabinPrimalityWork(String testcase)
        {
            // Arrange
            BigInteger number = BigInteger.Parse(testcase);

            // Recommended number of checks' rounds = Log2(number) as Biginter has no Log2 function we need to convert Log10
            BigInteger rounds = (BigInteger)(BigInteger.Log10(number) / BigInteger.Log10(2));

            // Act
            var result = MillerRabinPrimalityChecker.IsProbablyPrimeNumber(number,rounds);

            // Assert
            //Assert.IsTrue(result);
            return result;
        }

        [Test]
        [TestCase("-2")]
        [TestCase("0")]
        [TestCase("3")]
        public static void MillerRabinPrimalityShouldThrowEx(String testcase)
        {
            // Arrange
            BigInteger number = BigInteger.Parse(testcase);
            BigInteger rounds = 1;
            // Assert
            Assert.Throws<ArgumentException>(() => MillerRabinPrimalityChecker.IsProbablyPrimeNumber(number, rounds));
        }
    }
}
