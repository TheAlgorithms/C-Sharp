using Algorithms.Compressors;
using Algorithms.Sorters;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Compressors
{
    public class HuffmanCompressorTests
    {
        [Test]
        [TestCase("This is a string", "101010110111011101110111100011111010010010010011000")]
        [TestCase("Hello", "1101110010")]
        [TestCase("dddddddddd", "1111111111")]
        [TestCase("a", "1")]
        [TestCase("", "")]
        public void CompressingPhrase(string uncompressedText, string expectedCompressedText)
        {
            //Arrange
            var sorter = new BubbleSorter<HuffmanCompressor.ListNode>();
            var translator = new Translator();
            var huffman = new HuffmanCompressor(sorter, translator);

            //Act
            var (compressedText, decompressionKeys) = huffman.Compress(uncompressedText);
            var decompressedText = translator.Translate(compressedText, decompressionKeys);

            //Assert
            Assert.AreEqual(expectedCompressedText, compressedText);
            Assert.AreEqual(uncompressedText, decompressedText);
        }

        [Test]
        public void DecompressedTextTheSameAsOriginal([Random(0, 1000, 1000, Distinct = true)]int length)
        {
            //Arrange
            var sorter = new BubbleSorter<HuffmanCompressor.ListNode>();
            var translator = new Translator();
            var huffman = new HuffmanCompressor(sorter, translator);
            var text = Randomizer.CreateRandomizer().GetString(length);

            //Act
            var (compressedText, decompressionKeys) = huffman.Compress(text);
            var decompressedText = translator.Translate(compressedText, decompressionKeys);

            //Assert
            Assert.AreEqual(text, decompressedText);
        }
    }
}
