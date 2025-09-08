using Algorithms.Encoders;

namespace Algorithms.Tests.Encoders
{
    public static class AutokeyEncoderTests
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
