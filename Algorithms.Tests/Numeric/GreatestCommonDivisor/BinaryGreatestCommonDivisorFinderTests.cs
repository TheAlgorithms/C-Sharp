using Algorithms.Numeric.GreatestCommonDivisor;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric.GreatestCommonDivisor
{
    public static class BinaryGreatestCommonDivisorFinderTests
    {
        [Test]
        [TestCase(2, 3, 1)]
        [TestCase(1, 1, 1)]
        [TestCase(13, 17, 1)]
        [TestCase(0, 17, 17)]
        [TestCase(17, 0, 17)]
        [TestCase(17, 17, 17)]
        [TestCase(2 * 17, 17, 17)]
        [TestCase(0, 0, int.MaxValue)]
        [TestCase(2 * 13 * 17, 4 * 9 * 13, 2 * 13)]
        public static void GreatestCommonDivisorCorrect(int a, int b, int expectedGcd)
        {
            // Arrange
            var gcdFinder = new BinaryGreatestCommonDivisorFinder();

            // Act
            var actualGcd = gcdFinder.Find(a, b);

            // Assert
            Assert.AreEqual(expectedGcd, actualGcd);
        }
    }
}
