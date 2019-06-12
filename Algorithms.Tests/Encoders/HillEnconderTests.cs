using Algorithms.Encoders;
using NUnit.Framework;

namespace Algorithms.Tests.Encoders
{
    public class HillEnconderTests
    {
        private readonly HillEncoder encoder = new HillEncoder();

        // TODO: Add more random tests
        [Test]
        [Parallelizable]
        public void DecodedStringIsTheSame()
        {
            // Arrange
            var key = new double[,] { { 2, 4, 5 }, { 9, 2, 1 }, { 3, 17, 7 } };
            var inputText = "Attack at dawn";

            // Act
            var encodedText = encoder.Encode(inputText, key);
            var decodeText = encoder.Decode(encodedText, key);

            // Assert
            Assert.AreEqual(inputText, decodeText);
        }
    }
}
