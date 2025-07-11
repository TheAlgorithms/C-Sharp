using System;
using NUnit.Framework;
using Algorithms.Strings;

namespace Algorithms.Tests.Strings
{
    public static class ValidParenthesesTests
    {
        [TestCase("([{}])")]
        public static void IsValidParentheses_TrueExpected(string parentheses)
        {
            // Arrange
            // Act
            var isValidParentheses = ValidParentheses.IsValidParentheses(parentheses);

            // Assert
            Assert.That(isValidParentheses, Is.True);
        }

        [TestCase("([)[}")]
        public static void IsValidParentheses_FalseExpected(string parentheses)
        {
            // Arrange
            // Act
            var isValidParentheses = ValidParentheses.IsValidParentheses(parentheses);

            // Assert
            Assert.That(isValidParentheses, Is.False);
        }
    }
}
