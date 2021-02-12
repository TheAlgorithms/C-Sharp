using Algorithms.Numerics;
using NUnit.Framework;

namespace Algorithms.Test.Numerics
{
    public static class NarcissisticNumberTest
    {
        [Test]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(153)]
        [TestCase(371)]
        public static void NarcissisticNumberWork(int number)
        {
            // Arrange

            // Act
            var result = NarcissisticNumber.IsNarcissistic(number);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
