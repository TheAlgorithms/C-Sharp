using Algorithms.Encoders;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Encoders
{
    public class HillEnconderTests
    {
        // TODO: Add more random tests
        [Test]
        [Repeat(1000)]
        public void DecodedStringIsTheSame()
        {
            // Arrange
            var encoder = new HillEncoder();
            var random = new Randomizer();
            var message = random.GetString();
            var key = new double[,] { { 2, 4, 5 }, { 9, 2, 1 }, { 3, 17, 7 } };

            // Act
            var encodedText = encoder.Encode(message, key);
            var decodeText = encoder.Decode(encodedText, key);

            // Assert
            Assert.AreEqual(message, decodeText);
        }
    }
}
