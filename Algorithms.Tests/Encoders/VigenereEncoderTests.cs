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
            var key = random.GetString(random.Next(0, 1000));

            // Act
            var encoded = encoder.Encode(message, key);
            var decoded = encoder.Decode(encoded, key);

            // Assert
            Assert.AreEqual(message, decoded);
        }
    }
}
