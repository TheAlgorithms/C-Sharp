using System;
using Algorithms.Encoders;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Encoders
{
    public class AutokeyEncoderTests
    {
        [Test]
        public static void DecodedStringIsTheSame()
        {
            // Arrange
            var plainText = "PLAINTEXT";
            var keyword = "KEYWORD";
            var encoder = new AutokeyEncorder();

            // Act
            var encoded = encoder.Encode(plainText, keyword);
            var decoded = encoder.Decode(encoded, keyword);

            // Assert
            Assert.That(decoded, Is.EqualTo(plainText));
        }
    }
}
