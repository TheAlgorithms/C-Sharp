using Algorithms.Crypto.Exceptions;

namespace Algorithms.Tests.Crypto.Exceptions
{
    [TestFixture]
    public class OutputLengthExceptionTests
    {
        [Test]
        public void OutputLengthException_ShouldBeCreatedWithoutMessageOrInnerException()
        {
            // Act
            var exception = new OutputLengthException();

            // Assert
            exception.Should().BeOfType<OutputLengthException>()
                .And.Subject.As<OutputLengthException>()
                .Message.Should().NotBeNullOrEmpty();
            exception.InnerException.Should().BeNull();
        }

        [Test]
        public void OutputLengthException_ShouldSetMessage()
        {
            // Arrange
            var expectedMessage = "Output buffer is too short.";

            // Act
            var exception = new OutputLengthException(expectedMessage);

            // Assert
            exception.Should().BeOfType<OutputLengthException>()
                .And.Subject.As<OutputLengthException>()
                .Message.Should().Be(expectedMessage);
            exception.InnerException.Should().BeNull();
        }

        [Test]
        public void OutputLengthException_ShouldSetMessageAndInnerException()
        {
            // Arrange
            var expectedMessage = "Output length error.";
            var innerException = new ArgumentException("Invalid argument");

            // Act
            var exception = new OutputLengthException(expectedMessage, innerException);

            // Assert
            exception.Should().BeOfType<OutputLengthException>()
                .And.Subject.As<OutputLengthException>()
                .Message.Should().Be(expectedMessage);
            exception.InnerException.Should().Be(innerException);
        }

        [Test]
        public void OutputLengthException_MessageShouldNotBeNullWhenUsingDefaultConstructor()
        {
            // Act
            var exception = new OutputLengthException();

            // Assert
            exception.Message.Should().NotBeNullOrEmpty(); // Even the default Exception message is not null or empty.
        }
    }
}
