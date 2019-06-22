using Algorithms.DataCompression;
using NUnit.Framework;

namespace Algorithms.Tests.DataCompression
{
    public static class HuffmanTests
    {
        [Test]
        [Parallelizable]
        public static void CompressingPhrase()
        {
            //Arrange
            const string phrase = "This is a string";
            var huffman = new HuffmanAlgorithm();

            //Act
            var phraseResult = huffman.Compress(phrase);

            //Assert
            Assert.IsNotEmpty(phraseResult);
            Assert.AreEqual(
                "000001000010000000000000010000000100000000000000100000001001000000010000001000001100000000010001",
                phraseResult);
        }

        [Test]
        [Parallelizable]
        public static void CompressingSingleWord()
        {
            //Arrange
            const string word = "Hello";
            var huffman = new HuffmanAlgorithm();

            //Act
            var wordResult = huffman.Compress(word);

            //Assert
            Assert.IsNotEmpty(wordResult);
            Assert.AreEqual("011000000001", wordResult);
        }
    }
}
