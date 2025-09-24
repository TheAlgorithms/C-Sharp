using Algorithms.Stack;

namespace Algorithms.Tests.Stack
{
    [TestFixture]
    public class BalancedParenthesesCheckerTests
    {
        public static bool IsBalanced(string expression)
        {
            var checker = new BalancedParenthesesChecker();
            return checker.IsBalanced(expression);
        }

        [Test]
        public void IsBalanced_EmptyString_ThrowsArgumentException()
        {
            // Arrange
            var expression = string.Empty;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => IsBalanced(expression));

            if (ex != null)
            {
                Assert.That(ex.Message, Is.EqualTo("The input expression cannot be null or empty."));
            }

        }

        [Test]
        public void IsBalanced_ValidBalancedExpression_ReturnsTrue()
        {
            // Arrange
            var expression = "{[()]}";

            // Act
            var result = IsBalanced(expression);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsBalanced_ValidUnbalancedExpression_ReturnsFalse()
        {
            // Arrange
            var expression = "{[(])}";

            // Act
            var result = IsBalanced(expression);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsBalanced_UnbalancedWithExtraClosingBracket_ReturnsFalse()
        {
            // Arrange
            var expression = "{[()]}]";

            // Act
            var result = IsBalanced(expression);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsBalanced_ExpressionWithInvalidCharacters_ThrowsArgumentException()
        {
            // Arrange
            var expression = "{[a]}";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => IsBalanced(expression));
            if (ex != null)
            {
                Assert.That(ex.Message, Is.EqualTo("Invalid character 'a' found in the expression."));
            }
        }

        [Test]
        public void IsBalanced_SingleOpeningBracket_ReturnsFalse()
        {
            // Arrange
            var expression = "(";

            // Act
            var result = IsBalanced(expression);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsBalanced_SingleClosingBracket_ReturnsFalse()
        {
            // Arrange
            var expression = ")";

            // Act
            var result = IsBalanced(expression);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsBalanced_ExpressionWithMultipleBalancedBrackets_ReturnsTrue()
        {
            // Arrange
            var expression = "[{()}]()";

            // Act
            var result = IsBalanced(expression);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }
    }
}
