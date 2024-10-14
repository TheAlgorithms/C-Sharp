using NUnit.Framework;
using FluentAssertions;
using System;
using Algorithms.Crypto.Utils;
using Algorithms.Crypto.Exceptions;

namespace Algorithms.Tests.Crypto.Utils
{
    [TestFixture]
    public class ValidationUtilsTests
    {
        [Test]
        public void CheckDataLength_WithBufferOutOfBounds_ShouldThrowDataLengthException()
        {
            // Arrange
            var buffer = new byte[5];  // A byte array of length 5
            var offset = 3;               // Starting at index 3
            var length = 4;               // Expecting to read 4 bytes (which will exceed the buffer size)
            var errorMessage = "Buffer is too short";

            // Act
            var act = () => ValidationUtils.CheckDataLength(buffer, offset, length, errorMessage);

            // Assert
            act.Should().Throw<DataLengthException>()
                .WithMessage(errorMessage);
        }

        [Test]
        public void CheckOutputLength_WithCondition_ShouldThrowOutputLengthException()
        {
            // Arrange
            var condition = true;
            var errorMessage = "Output length is invalid";

            // Act
            var act = () => ValidationUtils.CheckOutputLength(condition, errorMessage);

            // Assert
            act.Should().Throw<OutputLengthException>()
               .WithMessage(errorMessage);
        }

        [Test]
        public void CheckOutputLength_WithCondition_ShouldNotThrowOutputLengthException()
        {
            // Arrange
            var condition = false;
            var errorMessage = "Output length is invalid";

            // Act
            var act = () => ValidationUtils.CheckOutputLength(condition, errorMessage);

            // Assert
            act.Should().NotThrow<OutputLengthException>();
        }

        [Test]
        public void CheckOutputLength_WithBufferOutOfBounds_ShouldThrowOutputLengthException()
        {
            // Arrange
            var buffer = new byte[5];
            var offset = 3;
            var length = 4;
            var errorMessage = "Output buffer is too short";

            // Act
            var act = () => ValidationUtils.CheckOutputLength(buffer, offset, length, errorMessage);

            // Assert
            act.Should().Throw<OutputLengthException>()
               .WithMessage(errorMessage);
        }

        [Test]
        public void CheckOutputLength_WithBProperBufferSize_ShouldThrowOutputLengthException()
        {
            // Arrange
            var buffer = new byte[5];
            var offset = 0;
            var length = 4;
            var errorMessage = "Output buffer is too short";

            // Act
            var act = () => ValidationUtils.CheckOutputLength(buffer, offset, length, errorMessage);

            // Assert
            act.Should().NotThrow<OutputLengthException>();
        }

        [Test]
        public void CheckOutputLength_SpanExceedsLimit_ShouldThrowOutputLengthException()
        {
            // Arrange
            Span<byte> output = new byte[10];
            var outputLength = output.Length;
            var maxLength = 5;
            var errorMessage = "Output exceeds maximum length";

            // Act
            var act = () => ValidationUtils.CheckOutputLength(outputLength > maxLength, errorMessage); // Capture the length

            // Assert
            act.Should().Throw<OutputLengthException>()
                .WithMessage(errorMessage);
        }

        [Test]
        public void CheckOutputLength_SpanDoesNotExceedLimit_ShouldThrowOutputLengthException()
        {
            // Arrange
            Span<byte> output = new byte[10];
            var outputLength = output.Length;
            var maxLength = 15;
            var errorMessage = "Output exceeds maximum length";

            // Act
            var act = () => ValidationUtils.CheckOutputLength(outputLength > maxLength, errorMessage); // Capture the length

            // Assert
            act.Should().NotThrow<OutputLengthException>();
        }
    }
}
