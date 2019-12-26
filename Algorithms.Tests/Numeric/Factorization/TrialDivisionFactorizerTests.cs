using Algorithms.Numeric.Factorization;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric
{
    public static class TrialDivisionFactorizerTests
    {
        [Test]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(29)]
        [TestCase(31)]
        public static void PrimeNumberFactorizationFails(int p)
        {
            // Arrange
            var factorizer = new TrialDivisionFactorizer();

            // Act
            var success = factorizer.TryFactor(p, out _);

            // Assert
            Assert.IsFalse(success);
        }
    }
}
