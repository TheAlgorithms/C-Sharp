using Algorithms.DataCompression;
using Algorithms.Knapsack;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Compressors
{
    public static class ShannonFanoCompressorTests
    {
        [Test]
        [TestCase("dddddddddd", "1111111111")]
        [TestCase("a", "1")]
        [TestCase("", "")]
        public static void CompressingPhrase(string uncompressedText, string expectedCompressedText)
        {
            //Arrange
            var solver = new NaiveKnapsackSolver<(char, double)>();
            var translator = new Translator();
            var shannonFanoCompressor = new ShannonFanoCompressor(solver, translator);

            //Act
            var (compressedText, decompressionKeys) = shannonFanoCompressor.Compress(uncompressedText);
            var decompressedText = translator.Translate(compressedText, decompressionKeys);

            //Assert
            Assert.AreEqual(expectedCompressedText, compressedText);
            Assert.AreEqual(uncompressedText, decompressedText);
        }

        [Test]
        public static void DecompressedTextTheSameAsOriginal([Random(0, 1000, 100)]int length)
        {
            //Arrange
            var solver = new NaiveKnapsackSolver<(char, double)>();
            var translator = new Translator();
            var shannonFanoCompressor = new ShannonFanoCompressor(solver, translator);
            var text = Randomizer.CreateRandomizer().GetString(length);

            //Act
            var (compressedText, decompressionKeys) = shannonFanoCompressor.Compress(text);
            var decompressedText = translator.Translate(compressedText, decompressionKeys);

            //Assert
            Assert.AreEqual(text, decompressedText);
        }
    }
}
