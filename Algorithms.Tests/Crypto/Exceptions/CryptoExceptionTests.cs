using Algorithms.Crypto.Exceptions;

namespace Algorithms.Tests.Crypto.Exceptions
{
    [TestFixture]
    public class CryptoExceptionTests
    {
        [Test]
        public void CryptoException_ShouldBeCreatedWithoutMessageOrInnerException()
        {
            // Act
            var exception = new CryptoException();

            // Assert
            exception.Should().BeOfType<CryptoException>()
                .And.Subject.As<CryptoException>()
                .Message.Should().NotBeNullOrEmpty();
            exception.InnerException.Should().BeNull();
        }

        [Test]
        public void CryptoException_ShouldSetMessage()
        {
            // Arrange
            var expectedMessage = "This is a custom cryptographic error.";

            // Act
            var exception = new CryptoException(expectedMessage);

            // Assert
            exception.Should().BeOfType<CryptoException>()
                .And.Subject.As<CryptoException>()
                .Message.Should().Be(expectedMessage);
            exception.InnerException.Should().BeNull();
        }

        [Test]
        public void CryptoException_ShouldSetMessageAndInnerException()
        {
            // Arrange
            var expectedMessage = "An error occurred during encryption.";
            var innerException = new InvalidOperationException("Invalid operation");

            // Act
            var exception = new CryptoException(expectedMessage, innerException);

            // Assert
            exception.Should().BeOfType<CryptoException>()
                .And.Subject.As<CryptoException>()
                .Message.Should().Be(expectedMessage);
            exception.InnerException.Should().Be(innerException);
        }

        [Test]
        public void CryptoException_MessageShouldNotBeNullWhenUsingDefaultConstructor()
        {
            // Act
            var exception = new CryptoException();

            // Assert
            exception.Message.Should().NotBeNullOrEmpty(); // Even the default Exception message is not null or empty.
        }
    }
}

