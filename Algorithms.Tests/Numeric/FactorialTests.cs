using System;
using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric
{
    public static class FactorialTests
    {
        [Test]
        [TestCase(5, 120)]
        [TestCase(1, 1)]
        [TestCase(0, 1)]
        [TestCase(4, 24)]
        [TestCase(18, 6402373705728000)]
        [TestCase(10, 3628800)]
        public static void GetsFactorial(int input, long expected)
        {
            // Arrange

            // Act
            var result = Factorial.Calculate(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public static void GetsFactorialExceptionForNonPositiveNumbers([Random(-1000, -1, 10, Distinct = true)]int input)
        {
            // Arrange

            // Act
            void Act() => Factorial.Calculate(input);

            // Assert

            _ = Assert.Throws<ArgumentException>(Act);
        }
    }
}
