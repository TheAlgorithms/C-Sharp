using Algorithms.Stack;
using NUnit.Framework;
using System;

namespace Algorithms.Tests.Stack
{
    [TestFixture]
    public class InfixToPostfixTests
    {
        private static string Convert(string infixExpression) =>
            InfixToPostfix.InfixToPostfixConversion(infixExpression);

        private static int Evaluate(string postfixExpression) =>
            InfixToPostfix.PostfixExpressionEvaluation(postfixExpression);

        // ---------- Infix to Postfix Tests ---------- //

        [Test]
        public void InfixToPostfix_EmptyString_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => Convert(""));
            Assert.That(ex!.Message, Is.EqualTo("Infix cannot be null or empty."));
        }

        [Test]
        public void InfixToPostfix_SimpleExpression_ReturnsCorrectPostfix()
        {
            Assert.That(Convert("A+B"), Is.EqualTo("AB+"));
        }

        [Test]
        public void InfixToPostfix_WithParentheses_ReturnsCorrectPostfix()
        {
            Assert.That(Convert("(A+B)*C"), Is.EqualTo("AB+C*"));
        }

        [Test]
        public void InfixToPostfix_ComplexExpression_ReturnsCorrectPostfix()
        {
            Assert.That(Convert("A+(B*C-(D/E^F)*G)*H"), Is.EqualTo("ABC*DEF^/G*-H*+"));
        }

        [Test]
        public void InfixToPostfix_InvalidCharacter_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => Convert("A+B#C"));
            Assert.That(ex!.Message, Does.Contain("Invalid character"));
        }

        [Test]
        public void InfixToPostfix_MismatchedParentheses_ThrowsInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => Convert("(A+B"));
        }

        // ---------- Postfix Evaluation Tests ---------- //

        [Test]
        public void PostfixEvaluation_ComplexExpression_ReturnsCorrectValue()
        {
            Assert.That(Evaluate("23*54*+"), Is.EqualTo(26)); // (2*3)+(5*4)
        }

        [Test]
        public void PostfixEvaluation_EmptyString_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => Evaluate(""));
            Assert.That(ex!.Message, Is.EqualTo("Postfix cannot be null or empty."));
        }

        [Test]
        public void PostfixEvaluation_DivideByZero_ThrowsException()
        {
            Assert.Throws<DivideByZeroException>(() => Evaluate("50/"));
        }

        [Test]
        public void PostfixEvaluation_InsufficientOperands_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => Evaluate("2+"));
        }

        [Test]
        public void PostfixEvaluation_InvalidCharacter_ThrowsException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => Evaluate("23*X"));
            Assert.That(ex!.Message, Does.Contain("Invalid character"));
        }

        [Test]
        public void PostfixEvaluation_LeftoverOperands_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => Evaluate("23"));
        }
    }
}
