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


        [Test]
        public void InfixToPostfix_EmptyString_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => Convert(""));
            Assert.That(ex!.Message, Is.EqualTo("Infix cannot be null or empty."));
        }

        [Test]
        public void InfixToPostfix_WhitespaceOnly_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => Convert("   "));
            Assert.That(ex!.Message, Is.EqualTo("Infix cannot be null or empty."));
        }

        [Test]
        public void InfixToPostfix_SimpleExpression_ReturnsCorrectPostfix()
        {
            Assert.That(Convert("A+B"), Is.EqualTo("AB+"));
        }

        [Test]
        public void InfixToPostfix_HandlesWhitespaceCorrectly()
        {
            Assert.That(Convert("  A + B "), Is.EqualTo("AB+"));
        }

        [Test]
        public void InfixToPostfix_WithParentheses_ReturnsCorrectPostfix()
        {
            Assert.That(Convert("(A+B)*C"), Is.EqualTo("AB+C*"));
        }

        [Test]
        public void InfixToPostfix_ComplexExpression_ReturnsCorrectPostfix()
        {
            Assert.That(Convert("A+(B*C-(D/E^F)*G)*H"),
                        Is.EqualTo("ABC*DEF^/G*-H*+"));
        }

        [Test]
        public void InfixToPostfix_OperatorPrecedence_IsCorrect()
        {
            Assert.That(Convert("A+B*C^D"), Is.EqualTo("ABCD^*+"));
        }

        [Test]
        public void InfixToPostfix_MismatchedParentheses_ThrowsInvalidOperation()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => Convert("(A+B"));
            Assert.That(ex!.Message, Is.EqualTo("Mismatched parentheses."));
        }

        [Test]
        public void InfixToPostfix_InvalidCharacter_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => Convert("A+B#C"));
            Assert.That(ex!.Message, Is.EqualTo("Invalid character #."));
        }

        [Test]
        public void InfixToPostfix_LeftoverOperators_AreFlushedCorrectly()
        {
            Assert.That(Convert("A+B+C"), Is.EqualTo("AB+C+"));
        }

        // ---------- NEW FULL-COVERAGE TESTS BELOW ---------- //

        [Test]
        public void InfixToPostfix_ClosingParenthesisWithoutOpening_Throws()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => Convert("A+B)C"));
            Assert.That(ex!.Message, Is.EqualTo("Mismatched parentheses in expression."));
        }

        [Test]
        public void InfixToPostfix_LeftoverOpeningParenthesis_Throws()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => Convert("(A+B"));
            Assert.That(ex!.Message, Is.EqualTo("Mismatched parentheses."));
        }

        [Test]
        public void InfixToPostfix_TrailingOperator_ReturnsPostfixBasedOnLogic()
        {
            Assert.That(Convert("A+"), Is.EqualTo("A+"));
        }

        [Test]
        public void InfixToPostfix_DeeplyNestedParentheses_WorksCorrectly()
        {
            Assert.That(Convert("A+(B+(C+(D+E)))"), Is.EqualTo("ABCDE++++"));
        }

        [Test]
        public void InfixToPostfix_DoubleOperator_StillProcessesWithoutException()
        {
            Assert.That(Convert("A++B"), Is.EqualTo("A+B+"));
        }

        // ---------------------------------------------------------
        //                 POSTFIX EVALUATION TESTS
        // ---------------------------------------------------------

        [Test]
        public void PostfixEvaluation_EmptyString_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => Evaluate(""));
            Assert.That(ex!.Message, Is.EqualTo("Postfix cannot be null or empty."));
        }

        [Test]
        public void PostfixEvaluation_WhitespaceOnly_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Evaluate("   "));
        }

        [Test]
        public void PostfixEvaluation_SimpleExpression_Works()
        {
            Assert.That(Evaluate("23+"), Is.EqualTo(5));
        }

        [Test]
        public void PostfixEvaluation_ComplexExpression_Works()
        {
            Assert.That(Evaluate("23*54*+"), Is.EqualTo(26));
        }

        [Test]
        public void PostfixEvaluation_HandlesWhitespaceCorrectly()
        {
            Assert.That(Evaluate(" 2 3 + "), Is.EqualTo(5));
        }

        [Test]
        public void PostfixEvaluation_ExponentOperator_Works()
        {
            Assert.That(Evaluate("23^"), Is.EqualTo(8));
        }

        [Test]
        public void PostfixEvaluation_DivideByZero_ThrowsException()
        {
            var ex = Assert.Throws<DivideByZeroException>(() => Evaluate("50/"));
            Assert.That(ex!.Message, Is.EqualTo("Cannot divide by zero"));
        }

        [Test]
        public void PostfixEvaluation_InsufficientOperands_ThrowsException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => Evaluate("2+"));
            Assert.That(ex!.Message, Is.EqualTo("Insufficient operands"));
        }

        [Test]
        public void PostfixEvaluation_InvalidCharacter_ThrowsException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => Evaluate("23*X"));
            Assert.That(ex!.Message, Is.EqualTo("Invalid character in expression: X"));
        }

        [Test]
        public void PostfixEvaluation_LeftoverOperands_ThrowsException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => Evaluate("23"));
            Assert.That(ex!.Message, Is.EqualTo("Invalid postfix expression: Leftover operands."));
        }

        // ---------- NEW FULL-COVERAGE TESTS BELOW ---------- //

        [Test]
        public void PostfixEvaluation_UnknownOperator_Throws()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => Evaluate("23&"));
            Assert.That(ex!.Message, Is.EqualTo("Invalid character in expression: &"));
        }

        [Test]
        public void PostfixEvaluation_OperatorWithoutEnoughOperands_Throws()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => Evaluate("+"));
            Assert.That(ex!.Message, Is.EqualTo("Insufficient operands"));
        }

        [Test]
        public void PostfixEvaluation_InvalidCharacterAmidExpression_Throws()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => Evaluate("23X+"));
            Assert.That(ex!.Message, Is.EqualTo("Invalid character in expression: X"));
        }

        [Test]
        public void PostfixEvaluation_ParenthesisCharacter_Throws()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => Evaluate("23+)"));
            Assert.That(ex!.Message, Does.Contain("Invalid character"));
        }
  
        [Test]
        public void PostfixEvaluation_LongExpression_Works()
        {
            Assert.That(Evaluate("23*54*+62/-"), Is.EqualTo(23));
        }
    }
}
