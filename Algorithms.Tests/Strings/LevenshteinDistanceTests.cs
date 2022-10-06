using Algorithms.Strings;
using NUnit.Framework;

namespace Algorithms.Tests.Strings
{
    public class LevenshteinDistanceTests
    {
        [Test]
        [TestCase("kitten", "sitting", 3)]
        [TestCase("bob", "bond", 2)]
        [TestCase("algorithm", "logarithm", 3)]
        [TestCase("star", "", 4)]
        [TestCase("", "star", 4)]
        [TestCase("abcde", "12345", 5)]
        public void Calculate_ReturnsCorrectLevenshteinDistance(string source, string destination, int expectedDistance)
        {
            var result = LevenshteinDistance.Calculate(source, destination);
            Assert.AreEqual(expectedDistance, result);
        }
    }
}
