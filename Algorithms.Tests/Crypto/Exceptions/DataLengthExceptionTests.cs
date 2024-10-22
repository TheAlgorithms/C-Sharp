using NUnit.Framework;
using FluentAssertions;
using System;
using Algorithms.Crypto.Exceptions;

namespace Algorithms.Tests.Crypto.Exceptions
{
    [TestFixture]
    public class DataLengthExceptionTests
    {
        [Test]
        public void DataLengthException_ShouldBeCreatedWithoutMessageOrInnerException()
        {
            // Act
            var exception = new DataLengthException();

            // Assert
            exception.Should().BeOfType<DataLengthException>()
                .And.Subject.As<DataLengthException>()
                .Message.Should().NotBeNullOrEmpty();
            exception.InnerException.Should().BeNull();
        }

        [Test]
        public void DataLengthException_ShouldSetMessage()
        {
            // Arrange
            var expectedMessage = "Data length is invalid.";

            // Act
            var exception = new DataLengthException(expectedMessage);

            // Assert
            exception.Should().BeOfType<DataLengthException>()
                .And.Subject.As<DataLengthException>()
                .Message.Should().Be(expectedMessage);
            exception.InnerException.Should().BeNull();
        }

        [Test]
        public void DataLengthException_ShouldSetMessageAndInnerException()
        {
            // Arrange
            var expectedMessage = "An error occurred due to incorrect data length.";
            var innerException = new ArgumentException("Invalid argument");

            // Act
            var exception = new DataLengthException(expectedMessage, innerException);

            // Assert
            exception.Should().BeOfType<DataLengthException>()
                .And.Subject.As<DataLengthException>()
                .Message.Should().Be(expectedMessage);
            exception.InnerException.Should().Be(innerException);
        }

        [Test]
        public void DataLengthException_MessageShouldNotBeNullWhenUsingDefaultConstructor()
        {
            // Act
            var exception = new DataLengthException();

            // Assert
            exception.Message.Should().NotBeNullOrEmpty(); // Even the default Exception message is not null or empty.
        }
    }
}
