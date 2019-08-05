using Algorithms.Encoders;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Encoders
{
    public static class CaesarEncoderTests
    {
        [Test]
        public static void DecodedStringIsTheSame([Random(100)]int key)
        {
            // Arrange
            var encoder = new CaesarEncoder();
            var random = new Randomizer();
            var message = random.GetString();

            // Act
            var encoded = encoder.Encode(message, key);
            var decoded = encoder.Decode(encoded, key);

            // Assert
            Assert.AreEqual(message, decoded);
        }
    }
}
