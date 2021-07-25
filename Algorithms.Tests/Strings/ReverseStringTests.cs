using Algorithms.Strings;
using NUnit.Framework;

namespace Algorithms.Tests.Strings
{
    public static class ReverseStringTests
    {
        [Test]
        [TestCase("abcd", "dcba")]
        [TestCase("1234", "4321")]
        [TestCase("!@#$", "$#@!")]
        public static void TextReversedByArrayReverseMethod_PassExpected(string text, string expectedResult)
        {
            // Act
            string reversedText = ReverseString.ReverseByArrayReverseMethod(text);

            // Assert
            Assert.AreEqual(reversedText, expectedResult);
        }

        [Test]
        [TestCase("abcd", "dcba")]
        [TestCase("1234", "4321")]
        [TestCase("!@#$", "$#@!")]
        public static void TextReversedByForLoop_PassExpected(string text, string expectedResult)
        {
            // Act
            string reversedText = ReverseString.ReverseByForLoop(text);

            // Assert
            Assert.AreEqual(reversedText, expectedResult);
        }

        [Test]
        [TestCase("abcd", "dcba")]
        [TestCase("1234", "4321")]
        [TestCase("!@#$", "$#@!")]
        public static void TextReversedByRecursion_PassExpected(string text, string expectedResult)
        {
            // Act
            string reversedText = ReverseString.ReverseByRecursion(text, 0);

            // Assert
            Assert.AreEqual(reversedText, expectedResult);
        }
    }
}
