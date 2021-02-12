using Algorithms.Numerics;
using NUnit.Framework;

namespace Algorithms.Test.Numerics
{
    public static class PerfectNumberTests
    {
        [Test]
        [TestCase(6)]
        [TestCase(28)]
        [TestCase(496)]
        [TestCase(8128)]
        public static void PerfectNumberWork(int number)
        {
            // Arrange

            // Act
            var result = PerfectNumber.IsPerfectNumber(number);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
