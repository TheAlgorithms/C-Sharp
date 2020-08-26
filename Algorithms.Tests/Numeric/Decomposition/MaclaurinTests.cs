using System;
using Algorithms.Numeric.Series;
using NUnit.Framework;


namespace Algorithms.Tests.Numeric.Decomposition
{
    public class MaclaurinTests
    {
        [TestCase(0.01, 3, 0.01)]
        [TestCase(1, 7, 0.001)]
        [TestCase(-1.2, 7, 0.001)]
        public void Exp_TermsForm_ValidCases(double point, int terms, double expectedError)
        {
            // Arrange
            var expected = Math.Exp(point);
            
            // Act
            var actual = Maclaurin.Exp(point, terms);

            // Assert
            Assert.IsTrue(Math.Abs(expected - actual) < expectedError);
        }
        
        [Test]
        public void Exp_TermsForm_InvalidCase()
        {
            // Arrange
            const int invalidTermsCount = -1;

            // Act
            void Act(int terms) => Maclaurin.Exp(0, terms);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Act(invalidTermsCount));
        }
        
        [TestCase(0, 1, 0.001)]
        [TestCase(1, 7, 0.001)]
        [TestCase(1.57, 7, 0.001)]
        [TestCase(3.14, 7, 0.001)]
        public void Sin_TermsForm_ValidCases(double point, int terms, double expectedError)
        {
            // Arrange
            var expected = Math.Sin(point);
            
            // Act
            var actual = Maclaurin.Sin(point, terms);

            // Assert
            Assert.IsTrue(Math.Abs(expected - actual) < expectedError);
        }
        
        [Test]
        public void Sin_TermsForm_InvalidCase()
        {
            // Arrange
            const int invalidTermsCount = -1;

            // Act
            void Act(int terms) => Maclaurin.Sin(0, terms);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Act(invalidTermsCount));
        }
        
        [TestCase(0, 1, 0.001)]
        [TestCase(1, 7, 0.001)]
        [TestCase(1.57, 7, 0.001)]
        [TestCase(3.14, 7, 0.001)]
        public void Cos_TermsForm_ValidCases(double point, int terms, double expectedError)
        {
            // Arrange
            var expected = Math.Cos(point);
            
            // Act
            var actual = Maclaurin.Cos(point, terms);

            // Assert
            Assert.IsTrue(Math.Abs(expected - actual) < expectedError);
        }
        
        [Test]
        public void Cos_TermsForm_InvalidCase()
        {
            // Arrange
            const int invalidTermsCount = -1;

            // Act
            void Act(int terms) => Maclaurin.Cos(0, terms);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Act(invalidTermsCount));
        }
    }
}