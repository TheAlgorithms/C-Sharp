using Algorithms.Numerics;
using NUnit.Framework;

namespace Algorithms.Test.Numerics
{
    public static class PerfectCubeTests
    {
        [Test]
        [TestCase(8)]
        [TestCase(27)]
        [TestCase(64)]
        [TestCase(216)]
        public static void PerfectCubeWork(int number)
        {
            // Arrange

            // Act
            var result = PerfectCube.IsPerfectCube(number);
            
            // Assert
            Assert.IsTrue(result);
        }
    }
}
