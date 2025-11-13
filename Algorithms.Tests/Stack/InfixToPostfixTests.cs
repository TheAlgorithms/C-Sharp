using Algorithms.Stack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Tests.Stack
{
    [TestFixture]
    public class InfixToPostfixTests
    {
        private static string Convert(string infixExpression) => InfixToPostfix.InfixToPostfixConversion(infixExpression);
        private static int Evaluate(string postfixExpression) => InfixToPostfix.PostfixExpressionEvaluation(postfixExpression);

        [Test]
        public void InfixToPostfix_EmptyString_ThrowsArgumentException()
        {
            //Arrange
            var exp = string.Empty;

            //Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => Convert(exp));
            if (ex != null)
            {
                Assert.That(ex.Message, Is.EqualTo("The input infix expression cannot be null or empty."));
            }
        }

        [Test]
        public void InfixToPostfix_SimpleExpression_ReturnsCorrectPostfix()
        {
            // Arrange
            var expression = "A+B";

            // Act
            var result = Convert(expression);

            // Assert
            Assert.That(result, Is.EqualTo("AB+"));
        }

        [Test]
        public void PostfixEvaluation_ComplexExpression_ReturnsCorrectValue()
        {
            // Arrange
            var expression = "23*54*+";
            // (2*3) + (5*4) = 6 + 20 = 26

            // Act
            var result = Evaluate(expression);

            // Assert
            Assert.That(result, Is.EqualTo(26));
        }
    }
}
