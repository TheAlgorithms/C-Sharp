using System;
using NUnit.Framework;
using Algorithms.Strings;

namespace Algorithms.Tests.Strings
{
    public static class ValidParenthesesTests
    {
        [TestCase("([{}])")]
        [TestCase("((({{{[[[]]]}}})))")]
        public static void IsValidParentheses_TrueExpected(string parentheses)
        {
            // Arrange
            // Act
            var isValidParentheses = ValidParentheses.IsValidParentheses(parentheses);

            // Assert
            Assert.That(isValidParentheses, Is.True);
        }

        [TestCase("([)[}{")]
        [TestCase("([}}])")]
        public static void IsValidParentheses_FalseExpected(string parentheses)
        {
            // Arrange
            // Act
            var isValidParentheses = ValidParentheses.IsValidParentheses(parentheses);

            // Assert
            Assert.That(isValidParentheses, Is.False);
        }

        [TestCase("(")]
        [TestCase("(((")]
        [TestCase("({{}")]
        public static void IsValidParentheses_OddLength(string parentheses)
        {
            // Arrange
            // Act
            var isValidParentheses = ValidParentheses.IsValidParentheses(parentheses);

            // Assert
            Assert.That(isValidParentheses, Is.False);
        }

        [TestCase("a")]
        [TestCase("[a]")]
        [TestCase("//")]
        public static void IsValidParentheses_InvalidCharFalse(string parentheses)
        {
            // Arrange
            // Act
            var isValidParentheses = ValidParentheses.IsValidParentheses(parentheses);

            // Assert
            Assert.That(isValidParentheses, Is.False);
        }

        [Test]
        public static void IsValidParentheses_EmptyStringTrue()
        {
            // Arrange
            // Act
            var isValidParentheses = ValidParentheses.IsValidParentheses(string.Empty);

            // Assert
            Assert.That(isValidParentheses, Is.True);
        }
    }
}
