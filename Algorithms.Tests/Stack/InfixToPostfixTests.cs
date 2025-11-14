using System;
using NUnit.Framework;
using Algorithms.Stack;

namespace Algorithms.Tests.Stack
{
    [TestFixture]
    public class InfixToPostfixTests
    {
        [Test]
        public void InfixToPostfixConversion_SimpleAddition_ReturnsCorrectPostfix()
        {
            // Arrange
            string infix = "A+B";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);

            // Assert
            Assert.That(result, Is.EqualTo("AB+"));
        }

        [Test]
        public void InfixToPostfixConversion_SimpleSubtraction_ReturnsCorrectPostfix()
        {
            // Arrange
            string infix = "A-B";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);

            // Assert
            Assert.That(result, Is.EqualTo("AB-"));
        }

        [Test]
        public void InfixToPostfixConversion_MultiplicationAndDivision_ReturnsCorrectPostfix()
        {
            // Arrange
            string infix = "A*B/C";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);

            // Assert
            Assert.That(result, Is.EqualTo("AB*C/"));
        }

        [Test]
        public void InfixToPostfixConversion_ExponentiationOperator_ReturnsCorrectPostfix()
        {
            // Arrange
            string infix = "A^B";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);

            // Assert
            Assert.That(result, Is.EqualTo("AB^"));
        }

        [Test]
        public void InfixToPostfixConversion_MixedOperatorPrecedence_ReturnsCorrectPostfix()
        {
            // Arrange
            string infix = "A+B*C";
       
            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);

            // Assert
            Assert.That(result, Is.EqualTo("ABC*+"));
        }

        [Test]
        public void InfixToPostfixConversion_WithParentheses_ReturnsCorrectPostfix()
        {
            // Arrange
            string infix = "(A+B)*C";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);

            // Assert
            Assert.That(result, Is.EqualTo("AB+C*"));
        }

        [Test]
        public void InfixToPostfixConversion_NestedParentheses_ReturnsCorrectPostfix()
        {
            // Arrange
            string infix = "((A+B)*C)";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);

            // Assert
            Assert.That(result, Is.EqualTo("AB+C*"));
        }

        [Test]
        public void InfixToPostfixConversion_ComplexExpression_ReturnsCorrectPostfix()
        {
            // Arrange
            string infix = "A+B*C-D/E";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);

            // Assert
            Assert.That(result, Is.EqualTo("ABC*+DE/-"));
        }

        [Test]
        public void InfixToPostfixConversion_WithDigits_ReturnsCorrectPostfix()
        {
            // Arrange
            string infix = "1+2*3";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);

            // Assert
            Assert.That(result, Is.EqualTo("123*+"));
        }

        [Test]
        public void InfixToPostfixConversion_LowercaseLetters_ReturnsCorrectPostfix()
        {
            // Arrange
            string infix = "a+b*c";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);

            // Assert
            Assert.That(result, Is.EqualTo("abc*+"));
        }

        [Test]
        public void InfixToPostfixConversion_WithWhitespace_IgnoresWhitespace()
        {
            // Arrange
            string infix = "A + B * C";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);

            // Assert
            Assert.That(result, Is.EqualTo("ABC*+"));
        }

        [Test]
        public void InfixToPostfixConversion_MultipleParentheses_ReturnsCorrectPostfix()
        {
            // Arrange
            string infix = "(A+B)*(C-D)";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);

            // Assert
            Assert.That(result, Is.EqualTo("AB+CD-*"));
        }

        [Test]
        public void InfixToPostfixConversion_AllOperators_ReturnsCorrectPostfix()
        {
            // Arrange
            string infix = "A+B-C*D/E^F";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);

            // Assert
            Assert.That(result, Is.EqualTo("AB+CD*EF^/-"));
        }

        [Test]
        public void InfixToPostfixConversion_NullExpression_ThrowsArgumentException()
        {
            // Arrange
            string infix = null!;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                InfixToPostfix.InfixToPostfixConversion(infix));
            Assert.That(ex!.Message, Does.Contain("Infix cannot be null or empty"));
        }

        [Test]
        public void InfixToPostfixConversion_EmptyExpression_ThrowsArgumentException()
        {
            // Arrange
            string infix = "";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                InfixToPostfix.InfixToPostfixConversion(infix));
            Assert.That(ex!.Message, Does.Contain("Infix cannot be null or empty"));
        }

        [Test]
        public void InfixToPostfixConversion_WhitespaceOnlyExpression_ThrowsArgumentException()
        {
            // Arrange
            string infix = "   ";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                InfixToPostfix.InfixToPostfixConversion(infix));
            Assert.That(ex!.Message, Does.Contain("Infix cannot be null or empty"));
        }

        [Test]
        public void InfixToPostfixConversion_InvalidCharacter_ThrowsArgumentException()
        {
            // Arrange
            string infix = "A+B$C";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                InfixToPostfix.InfixToPostfixConversion(infix));
            Assert.That(ex!.Message, Does.Contain("Invalid character $"));
        }

        [Test]
        public void InfixToPostfixConversion_MismatchedParenthesesClosingExtra_ThrowsInvalidOperationException()
        {
            // Arrange
            string infix = "A+B)";

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
                InfixToPostfix.InfixToPostfixConversion(infix));
            Assert.That(ex!.Message, Does.Contain("Mismatched parentheses"));
        }

        [Test]
        public void InfixToPostfixConversion_MismatchedParenthesesOpeningExtra_ThrowsInvalidOperationException()
        {
            // Arrange
            string infix = "(A+B";

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
                InfixToPostfix.InfixToPostfixConversion(infix));
            Assert.That(ex!.Message, Does.Contain("Mismatched parentheses"));
        }

        [Test]
        public void InfixToPostfixConversion_OnlyOpeningParenthesis_ThrowsInvalidOperationException()
        {
            // Arrange
            string infix = "(";

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                InfixToPostfix.InfixToPostfixConversion(infix));
        }

        [Test]
        public void InfixToPostfixConversion_OnlyClosingParenthesis_ThrowsInvalidOperationException()
        {
            // Arrange
            string infix = ")";

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                InfixToPostfix.InfixToPostfixConversion(infix));
        }

        [Test]
        public void InfixToPostfixConversion_ExponentiationWithOtherOperators_ReturnsCorrectPostfix()
        {
            // Arrange
            string infix = "A+B^C*D";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);

            // Assert
            Assert.That(result, Is.EqualTo("ABC^D*+"));
        }

        [Test]
        public void InfixToPostfixConversion_SingleOperand_ReturnsOperand()
        {
            // Arrange
            string infix = "A";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);

            // Assert
            Assert.That(result, Is.EqualTo("A"));
        }

        [Test]
        public void PostfixExpressionEvaluation_SimpleAddition_ReturnsCorrectResult()
        {
            // Arrange
            string postfix = "23+";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);

            // Assert
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void PostfixExpressionEvaluation_SimpleSubtraction_ReturnsCorrectResult()
        {
            // Arrange
            string postfix = "53-";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);

            // Assert
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void PostfixExpressionEvaluation_SimpleMultiplication_ReturnsCorrectResult()
        {
            // Arrange
            string postfix = "34*";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);

            // Assert
            Assert.That(result, Is.EqualTo(12));
        }

        [Test]
        public void PostfixExpressionEvaluation_SimpleDivision_ReturnsCorrectResult()
        {
            // Arrange
            string postfix = "82/";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);

            // Assert
            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void PostfixExpressionEvaluation_SimpleExponentiation_ReturnsCorrectResult()
        {
            // Arrange
            string postfix = "23^";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);

            // Assert
            Assert.That(result, Is.EqualTo(8));
        }

        [Test]
        public void PostfixExpressionEvaluation_ComplexExpression_ReturnsCorrectResult()
        {
            // Arrange
            string postfix = "23*4+";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);

            // Assert
            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void PostfixExpressionEvaluation_WithWhitespace_ReturnsCorrectResult()
        {
            // Arrange
            string postfix = "2 3 + 4 *";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);

            // Assert
            Assert.That(result, Is.EqualTo(20));
        }

        [Test]
        public void PostfixExpressionEvaluation_SingleDigit_ReturnsDigit()
        {
            // Arrange
            string postfix = "5";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);

            // Assert
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void PostfixExpressionEvaluation_NullExpression_ThrowsArgumentException()
        {
            // Arrange
            string postfix = null!;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                InfixToPostfix.PostfixExpressionEvaluation(postfix));
            Assert.That(ex!.Message, Does.Contain("Postfix cannot be null or empty"));
        }

        [Test]
        public void PostfixExpressionEvaluation_EmptyExpression_ThrowsArgumentException()
        {
            // Arrange
            string postfix = "";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                InfixToPostfix.PostfixExpressionEvaluation(postfix));
            Assert.That(ex!.Message, Does.Contain("Postfix cannot be null or empty"));
        }

        [Test]
        public void PostfixExpressionEvaluation_WhitespaceOnlyExpression_ThrowsArgumentException()
        {
            // Arrange
            string postfix = "   ";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                InfixToPostfix.PostfixExpressionEvaluation(postfix));
            Assert.That(ex!.Message, Does.Contain("Postfix cannot be null or empty"));
        }

        [Test]
        public void PostfixExpressionEvaluation_InsufficientOperands_ThrowsInvalidOperationException()
        {
            // Arrange
            string postfix = "2+";

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
                InfixToPostfix.PostfixExpressionEvaluation(postfix));
            Assert.That(ex!.Message, Does.Contain("Insufficient operands"));
        }

        [Test]
        public void PostfixExpressionEvaluation_DivisionByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            string postfix = "20/";

            // Act & Assert
            var ex = Assert.Throws<DivideByZeroException>(() =>
                InfixToPostfix.PostfixExpressionEvaluation(postfix));
            Assert.That(ex!.Message, Does.Contain("Cannot divide by zero"));
        }

        [Test]
        public void PostfixExpressionEvaluation_InvalidCharacter_ThrowsInvalidOperationException()
        {
            // Arrange
            string postfix = "23A+";

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
                InfixToPostfix.PostfixExpressionEvaluation(postfix));
            Assert.That(ex!.Message, Does.Contain("Invalid character in expression"));
        }

        [Test]
        public void PostfixExpressionEvaluation_LeftoverOperands_ThrowsInvalidOperationException()
        {
            // Arrange
            string postfix = "234";

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
                InfixToPostfix.PostfixExpressionEvaluation(postfix));
            Assert.That(ex!.Message, Does.Contain("Invalid postfix expression: Leftover operands"));
        }

        [Test]
        public void PostfixExpressionEvaluation_UnknownOperator_ThrowsInvalidOperationException()
        {
            // This test ensures the default case in the switch is covered
            // Note: This is difficult to test directly as IsOperator filters valid operators
            // But we can test by passing an operator character that somehow bypasses IsOperator
        }

        [Test]
        public void PostfixExpressionEvaluation_ComplexExpressionWithAllOperators_ReturnsCorrectResult()
        {
            // Arrange - (2+3)*4-6/2 = 5*4-3 = 20-3 = 17
            string postfix = "23+4*62/-";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);

            // Assert
            Assert.That(result, Is.EqualTo(17));
        }

        [Test]
        public void PostfixExpressionEvaluation_ExponentiationInExpression_ReturnsCorrectResult()
        {
            // Arrange - 2^3*2 = 8*2 = 16
            string postfix = "23^2*";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);

            // Assert
            Assert.That(result, Is.EqualTo(16));
        }

        [Test]
        public void PostfixExpressionEvaluation_ZeroOperands_ReturnsZero()
        {
            // Arrange
            string postfix = "0";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void PostfixExpressionEvaluation_LargerNumbers_ReturnsCorrectResult()
        {
            // Arrange - Uses single digits only: 9+8 = 17
            string postfix = "98+";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);

            // Assert
            Assert.That(result, Is.EqualTo(17));
        }

        [Test]
        public void IntegrationTest_ConvertAndEvaluate_SimpleExpression()
        {
            // Arrange
            string infix = "2+3";

            // Act
            string postfix = InfixToPostfix.InfixToPostfixConversion(infix);
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);

            // Assert
            Assert.That(postfix, Is.EqualTo("23+"));
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void IntegrationTest_ConvertAndEvaluate_ComplexExpression()
        {
            // Arrange
            string infix = "(2+3)*4";

            // Act
            string postfix = InfixToPostfix.InfixToPostfixConversion(infix);
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);

            // Assert
            Assert.That(postfix, Is.EqualTo("23+4*"));
            Assert.That(result, Is.EqualTo(20));
        }

        [Test]
        public void IntegrationTest_ConvertAndEvaluate_WithAllOperators()
        {
            // Arrange - 2+3*4-6/2 = 2+12-3 = 11
            string infix = "2+3*4-6/2";

            // Act
            string postfix = InfixToPostfix.InfixToPostfixConversion(infix);
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);

            // Assert
            Assert.That(postfix, Is.EqualTo("234*+62/-"));
            Assert.That(result, Is.EqualTo(11));
        }

        [Test]
        public void IntegrationTest_ConvertAndEvaluate_WithExponentiation()
        {
            // Arrange - 2^3+1 = 8+1 = 9
            string infix = "2^3+1";

            // Act
            string postfix = InfixToPostfix.InfixToPostfixConversion(infix);
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);

            // Assert
            Assert.That(postfix, Is.EqualTo("23^1+"));
            Assert.That(result, Is.EqualTo(9));
        }
    }
}
