using System;
using Algorithms.Strings;
using NUnit.Framework;

namespace Algorithms.Tests.Strings
{
    public static class GeneralStringAlgorithmsTests
    {
        [Test]
        [TestCase("Griffith", 'f', 2)]
        [TestCase("Randomwoooord", 'o', 4)]
        [TestCase("Control", 'C', 1)]
        public static void MaxCountCharIsObtained(string text, char expectedSymbol, int expectedCount)
        {
            // Arrange
            // Act
            var (symbol, count) = GeneralStringAlgorithms.FindLongestConsecutiveCharacters(text);

            // Assert
            Assert.AreEqual(expectedSymbol, symbol);
            Assert.AreEqual(expectedCount, count);
        }

        [Test]
        [TestCase("bluh", 4, new []{ "b", "l", "u", "h" })]
        [TestCase("fox",2,new []{"fo","x" })]
        public static void SplitTextIsObtained(string text, int parts, string[] expectedSplit)
        {
            string[] str = GeneralStringAlgorithms.SplitText(text, parts);
            CollectionAssert.AreEqual(expectedSplit, str);
        }
    }
}
