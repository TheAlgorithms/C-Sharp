using System;
using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric
{
    public static class AliquotTest
    {
        [Test]
        [TestCase(1, 0)]
        [TestCase(3, 1)]
        [TestCase(25, 6)]
        [TestCase(99, 57)]
        public static void AliquotSumWork(int number, int expectedAS)
        {
            // Arrange

            // Act
            var result = Aliquot.AliquotSum(number);

            // Assert
            Assert.AreEqual(result, expectedAS);
        }

        [Test]
        [TestCase(-2)]
        public static void AliquotSumShouldThrowEx(int number)
        {
            // Arrange

            // Assert
            Assert.Throws<ArgumentException>(() => Aliquot.AliquotSum(number));
        }
    }
}
