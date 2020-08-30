using System;
using Algorithms.Encoders;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Encoders
{
    public static class VigenereEncoderTests
    {
        [Test]
        [Repeat(100)]
        public static void DecodedStringIsTheSame()
        {
            // Arrange
            var random = new Randomizer();
            var encoder = new VigenereEncoder();
            var message = random.GetString();
            var key = random.GetString(random.Next(1, 1000));

            // Act
            var encoded = encoder.Encode(message, key);
            var decoded = encoder.Decode(encoded, key);

            // Assert
            Assert.AreEqual(message, decoded);
        }

        [Test]
        public static void Encode_KeyIsTooShort_KeyIsAppended()
        {
            // Arrange
            var encoder = new VigenereEncoder();
            var message = new string('a', 2);
            var key = new string('a', 1);

            // Act
            var encoded = encoder.Encode(message, key);
            var decoded = encoder.Decode(encoded, key);

            // Assert
            Assert.AreEqual(message, decoded);
        }

        [Test]
        public static void EmptyKeyThrowsException()
        {
            var random = new Randomizer();
            var encoder = new VigenereEncoder();
            var message = random.GetString();
            var key = string.Empty;

            _ = Assert.Throws<ArgumentOutOfRangeException>(() => encoder.Encode(message, key));
            _ = Assert.Throws<ArgumentOutOfRangeException>(() => encoder.Decode(message, key));
        }
    }
}
