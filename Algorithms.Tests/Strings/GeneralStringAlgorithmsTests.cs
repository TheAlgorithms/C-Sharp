using Algorithms.Strings;
using NUnit.Framework;
using System;

namespace Algorithms.Tests.Strings
{
    public class GeneralStringAlgorithmsTests
    {
        [Test]
        [Parallelizable]
        public void MaxCountCharIsObtained()
        {
            // Arrange
            const string input = "Griffith";
            const string input2 = "Randomwoooord";
            const string input3 = "Control";

            // Act
            var (rChar, rMax) = GeneralStringAlgorithms.FindLongestConsecutiveCharacters(input);
            var (rChar2, rMax2) = GeneralStringAlgorithms.FindLongestConsecutiveCharacters(input2);
            var (rChar3, rMax3) = GeneralStringAlgorithms.FindLongestConsecutiveCharacters(input3);

            // Assert
            Assert.NotNull(rChar);
            Assert.NotNull(rChar2);
            Assert.NotNull(rChar3);

            Assert.AreEqual('f', rChar);
            Assert.AreEqual(2, rMax);

            Assert.AreEqual('o', rChar2);
            Assert.AreEqual(4, rMax2);

            Assert.AreEqual('C', rChar3);
            Assert.AreEqual(1, rMax3);
        }
    }
}
