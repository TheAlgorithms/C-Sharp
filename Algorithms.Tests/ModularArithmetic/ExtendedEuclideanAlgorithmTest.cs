using Algorithms.ModularArithmetic;
using NUnit.Framework;
using System.Numerics;

namespace Algorithms.Tests.ModularArithmetic
{
    public static class ExtendedEuclideanAlgorithmTest
    {
        [Test]
        [TestCase(240, 46, 2, -9, 47)]
        [TestCase(46, 240, 2, 47, -9)]
        [TestCase(2, 3, 1, -1, 1)]
        [TestCase(1, 1, 1, 0, 1)]
        [TestCase(13, 17, 1, 4, -3)]
        [TestCase(0, 17, 17, 0, 1)]
        [TestCase(17, 0, 17, 1, 0)]
        [TestCase(17, 17, 17, 0, 1)]
        [TestCase(2 * 17, 17, 17, 0, 1)]
        [TestCase(0, 0, 0, 1, 0)]
        [TestCase(2 * 13 * 17, 4 * 9 * 13, 2 * 13, -1, 1)]
        public static void TestCompute(long a, long b, long expectedGCD, long expectedBezoutOfA, long expectedBezoutOfB)
        {
            // Act
            var gcd = ExtendedEuclideanAlgorithm.Compute(a, b, out var bezoutOfA, out var bezoutOfB);

            // Assert
            Assert.AreEqual(expectedGCD, gcd);
            Assert.AreEqual(expectedBezoutOfA, bezoutOfA);
            Assert.AreEqual(expectedBezoutOfB, bezoutOfB);
        }

        [Test]
        [TestCase(240, 46, 2, -9, 47)]
        [TestCase(46, 240, 2, 47, -9)]
        [TestCase(2, 3, 1, -1, 1)]
        [TestCase(1, 1, 1, 0, 1)]
        [TestCase(13, 17, 1, 4, -3)]
        [TestCase(0, 17, 17, 0, 1)]
        [TestCase(17, 0, 17, 1, 0)]
        [TestCase(17, 17, 17, 0, 1)]
        [TestCase(2 * 17, 17, 17, 0, 1)]
        [TestCase(0, 0, 0, 1, 0)]
        [TestCase(2 * 13 * 17, 4 * 9 * 13, 2 * 13, -1, 1)]
        public static void TestCompute_BigInteger(long a, long b, long expectedGCD, long expectedBezoutOfA, long expectedBezoutOfB)
        {
            // Act
            var gcd = ExtendedEuclideanAlgorithm.Compute(new BigInteger(a), new BigInteger(b), out var bezoutOfA, out var bezoutOfB);

            // Assert
            Assert.AreEqual(new BigInteger(expectedGCD), gcd);
            Assert.AreEqual(new BigInteger(expectedBezoutOfA), bezoutOfA);
            Assert.AreEqual(new BigInteger(expectedBezoutOfB), bezoutOfB);
        }
    }
}
