using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric
{
    public static class PerfectSquareTests
    {
        [Test]
        [TestCase(-4, ExpectedResult = false)]
        [TestCase(4, ExpectedResult = true)]
        [TestCase(9, ExpectedResult = true)]
        [TestCase(10, ExpectedResult = false)]
        [TestCase(16, ExpectedResult = true)]
        [TestCase(70, ExpectedResult = false)]
        [TestCase(81, ExpectedResult = true)]
        public static bool PerfectSquareWork(int number)
        {
            // Arrange

            // Act
            var result = PerfectSquare.IsPerfectSquare(number);

            // Assert
            return result;
        }
    }
}
