using System;
using Algorithms.Other;
using NUnit.Framework;

namespace Algorithms.Tests.Other
{
    public static class Int2BinaryTests
    {
        [Test]
        [TestCase((UInt16) 0, "0000000000000000")]
        [TestCase((UInt16) 0b1, "0000000000000001")]
        [TestCase((UInt16) 0b0001010100111000, "0001010100111000")]
        [TestCase((UInt16) 0b1110111100110010, "1110111100110010")]
        [TestCase((UInt16)(UInt16.MaxValue - 1), "1111111111111110")]
        [TestCase(UInt16.MaxValue , "1111111111111111")]
        public static void GetsBinary(UInt16 input, string expected)
        {
            // Arrange

            // Act
            var result = Int2Binary.Int2bin(input);

            // Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        [TestCase((UInt32)0, "00000000000000000000000000000000")]
        [TestCase((UInt32)0b1, "00000000000000000000000000000001")]
        [TestCase((UInt32)0b0001010100111000, "00000000000000000001010100111000")]
        [TestCase((UInt32)0b1110111100110010, "00000000000000001110111100110010")]
        [TestCase((UInt32)0b10101100001110101110111100110010, "10101100001110101110111100110010")]
        [TestCase((UInt32)(UInt32.MaxValue - 1), "11111111111111111111111111111110")]
        [TestCase(UInt32.MaxValue, "11111111111111111111111111111111")]
        public static void GetsBinary(UInt32 input, string expected)
        {
            // Arrange

            // Act
            var result = Int2Binary.Int2bin(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase((UInt64)0, "0000000000000000000000000000000000000000000000000000000000000000")]
        [TestCase((UInt64)0b1, "0000000000000000000000000000000000000000000000000000000000000001")]
        [TestCase((UInt64)0b0001010100111000, "0000000000000000000000000000000000000000000000000001010100111000")]
        [TestCase((UInt64)0b1110111100110010, "0000000000000000000000000000000000000000000000001110111100110010")]
        [TestCase((UInt64)0b10101100001110101110111100110010, "0000000000000000000000000000000010101100001110101110111100110010")]
        [TestCase((UInt64)0b1000101110100101000011010101110101010101110101001010000011111000, "1000101110100101000011010101110101010101110101001010000011111000")]
        [TestCase((UInt64)(UInt64.MaxValue - 1), "1111111111111111111111111111111111111111111111111111111111111110")]
        [TestCase(UInt64.MaxValue, "1111111111111111111111111111111111111111111111111111111111111111")]
        public static void GetsBinary(UInt64 input, string expected)
        {
            // Arrange

            // Act
            var result = Int2Binary.Int2bin(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

    }
}
