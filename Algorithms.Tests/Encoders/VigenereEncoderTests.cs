using Algorithms.Encoders;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Encoders
{
    public class VigenereEncoderTests
    {
        [Test]
        [Repeat(1000)]
        public void DecodedStringIsTheSame()
        {
            // Arrange
            var random = new Randomizer();
            var encoder = new VigenereEncoder();
            var message = random.GetString();
            var key = random.GetString();

            // Act
            var encoded = encoder.Encode(message, key);
            var decoded = encoder.Decode(encoded, key);

            // Assert
            Assert.AreEqual(message, decoded);
        }
    }
}
