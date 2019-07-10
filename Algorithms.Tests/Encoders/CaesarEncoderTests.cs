using Algorithms.Encoders;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Encoders
{
    public class CaesarEncoderTests
    {
        [Test]
        public void DecodedStringIsTheSame([Random(1000)]int key)
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
