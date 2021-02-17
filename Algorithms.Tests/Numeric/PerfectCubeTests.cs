using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric
{
    public static class PerfectCubeTests
    {
        [Test]
        [TestCase(8, ExpectedResult = true)]
        [TestCase(10, ExpectedResult = false)]
        [TestCase(27, ExpectedResult = true)]
        [TestCase(32, ExpectedResult = false)]
        [TestCase(64, ExpectedResult = true)]
        [TestCase(216, ExpectedResult = true)]
        public static bool PerfectCubeWork(int number)
        {
            // Arrange

            // Act
            var result = PerfectCube.IsPerfectCube(number);
            
            // Assert
            return result;
        }
    }
}
