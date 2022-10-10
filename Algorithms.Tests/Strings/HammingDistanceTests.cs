using Algorithms.Strings;
using NUnit.Framework;
using System;

namespace Algorithms.Tests.Strings
{
    public class HammingDistanceTests
    {
        [Test]
        [TestCase("equal", "equal", 0)]
        [TestCase("dog", "dig", 1)]
        [TestCase("12345", "abcde", 5)]
        public void Calculate_ReturnsCorrectHammingDistance(string s1, string s2, int expectedDistance)
        {
            var result = HammingDistance.Calculate(s1, s2);
            Assert.AreEqual(expectedDistance, result);
        }

        [Test]
        public void Calculate_ThrowsArgumentExceptionWhenStringLengthsDiffer()
        {
            Assert.Throws<ArgumentException>(() => HammingDistance.Calculate("123", "12345"));
        }
    }
}
