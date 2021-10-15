using Algorithms.Encoders;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Encoders
{
    public static class FeistelCipherTests
    {
        [Test]
        public static void DecodedStringIsTheSame([Random(100)] int key)
        {
            // Arrange
            var encoder = new FeistelCipher();
            var random = new Randomizer();

            int len_of_string = random.Next(1000);
            var message = random.GetString(len_of_string);

            // Act
            var encoded = encoder.Encode(message, 0x12345678);
            var decoded = encoder.Decode(encoded, 0x12345678);

            // Assert
            Assert.AreEqual(message, decoded);
        }
    }
}
