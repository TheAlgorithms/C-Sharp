using System;
using Algorithms.Numeric;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric
{
    public static class AliquotSumCalculatorTests
    {
        [Test]
        [TestCase(1, 0)]
        [TestCase(3, 1)]
        [TestCase(25, 6)]
        [TestCase(99, 57)]
        public static void CalculateSum_SumIsCorrect(int number, int expectedSum)
        {
            // Arrange

            // Act
            var result = AliquotSumCalculator.CalculateAliquotSum(number);

            // Assert
            result.Should().Be(expectedSum);
        }

        [Test]
        [TestCase(-2)]
        public static void CalculateSum_NegativeInput_ExceptionIsThrown(int number)
        {
            // Arrange
            Action act = () => AliquotSumCalculator.CalculateAliquotSum(number);

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}
