using Algorithms.Crypto.Utils;

namespace Algorithms.Tests.Crypto.Utils
{
    [TestFixture]
    public class LongUtilsTests
    {
        [Test]
        public void RotateLeft_Long_ShouldRotateCorrectly()
        {
            // Arrange
            var input = 0x0123456789ABCDEF;
            var distance = 8;
            var expected = 0x23456789ABCDEF01L;  // The expected result is a signed long value.

            // Act
            var result = LongUtils.RotateLeft(input, distance);

            // Assert
            result.Should().Be(expected);
        }

        [Test]
        public void RotateLeft_Ulong_ShouldRotateCorrectly()
        {
            // Arrange
            var input = 0x0123456789ABCDEFUL;
            var distance = 8;
            var expected = 0x23456789ABCDEF01UL;  // The expected result is an unsigned ulong value.

            // Act
            var result = LongUtils.RotateLeft(input, distance);

            // Assert
            result.Should().Be(expected);
        }

        [Test]
        public void RotateRight_Long_ShouldRotateCorrectly()
        {
            // Arrange
            var input = 0x0123456789ABCDEF;
            var distance = 8;
            var expected = unchecked((long)0xEF0123456789ABCD);  // Using unchecked to correctly represent signed long.

            // Act
            var result = LongUtils.RotateRight(input, distance);

            // Assert
            result.Should().Be(expected);
        }

        [Test]
        public void RotateRight_Ulong_ShouldRotateCorrectly()
        {
            // Arrange
            var input = 0x0123456789ABCDEFUL;
            var distance = 8;
            var expected = 0xEF0123456789ABCDUL;  // The expected result is an unsigned ulong value.

            // Act
            var result = LongUtils.RotateRight(input, distance);

            // Assert
            result.Should().Be(expected);
        }

        [Test]
        public void RotateLeft_Long_ShouldHandleZeroRotation()
        {
            // Arrange
            var input = 0x0123456789ABCDEF;
            var distance = 0;

            // Act
            var result = LongUtils.RotateLeft(input, distance);

            // Assert
            result.Should().Be(input);  // No rotation, result should be the same as input.
        }

        [Test]
        public void RotateRight_Ulong_ShouldHandleFullRotation()
        {
            // Arrange
            var input = 0x0123456789ABCDEFUL;
            var distance = 64;

            // Act
            var result = LongUtils.RotateRight(input, distance);

            // Assert
            result.Should().Be(input);  // Full 64-bit rotation should result in the same value.
        }
    }
}
