using Algorithms.Numerics;
using NUnit.Framework;

namespace Algorithms.Test.Numerics
{
    public static class PerfectSquareTests
    {
        [Test]
        [TestCase(4)]
        [TestCase(9)]
        [TestCase(16)]
        [TestCase(81)]
        public static void PerfectSquareWork(int number)
        {
            // Arrange

            // Act
            var result = PerfectSquare.IsPerfectSquare(number);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
