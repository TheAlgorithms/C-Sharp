using System;
using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric
{
    public static class BinomialCoefficientTests
    {
        [TestCase(4, 2, 6)]
        [TestCase(7, 3, 35)]
        public static void CalculateFromPairs(int n, int k, long expected)
        {
            // Arrange

            // Act
            var result = BinomialCoefficient.Calculate(n, k);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(3, 7)]
        public static void TeoremCalculateThrowsException(int n, int k)
        {
            // Arrange

            // Act

            // Assert
            _ = Assert.Throws<ArgumentException>(() => BinomialCoefficient.Calculate(n, k));
        }
    }
}
