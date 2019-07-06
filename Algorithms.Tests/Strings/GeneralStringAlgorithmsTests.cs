using Algorithms.Strings;
using NUnit.Framework;
using System;

namespace Algorithms.Tests.Strings
{
    public class GeneralStringAlgorithmsTests
    {
        [Test]
        [TestCase("Griffith", 'f', 2)]
        [TestCase("Randomwoooord", 'o', 4)]
        [TestCase("Control", 'C', 1)]
        public void MaxCountCharIsObtained(string text, char expectedSymbol, int expectedCount)
        {
            // Arrange
            // Act
            var (symbol, count) = GeneralStringAlgorithms.FindLongestConsecutiveCharacters(text);

            // Assert
            Assert.AreEqual(expectedSymbol, symbol);
            Assert.AreEqual(expectedCount, count);
        }
    }
}
