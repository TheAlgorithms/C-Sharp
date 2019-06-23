using Algorithms.DataCompression;
using NUnit.Framework;

namespace Algorithms.Tests.DataCompression
{
    public static class ShannonFanoTests
    {
        [Test]
        [Parallelizable]
        public static void ThatAlgorithWorks()
        {
            // Arrange
            var shannonFano = new ShannonFano();

            const string phrase = "This is a string";

            // Act
            var result = shannonFano.Compress(phrase);

            // Assert
            Assert.IsNotEmpty(result);
            Assert.AreEqual("1001111000110100001101011100100111001101001100101", result);
        }

        [Test]
        [Parallelizable]
        public static void ThatAlgorithWorksWord()
        {
            // Arrange
            var shannonFano = new ShannonFano();

            const string word = "Hello";

            // Act
            var result = shannonFano.Compress(word);

            // Assert
            Assert.IsNotEmpty(result);
            Assert.AreEqual("1111100010", result);
        }
    }
}
