using Algorithms.Other;
using NUnit.Framework;

namespace Algorithms.Tests.Other;

public static class Int2BinaryTests
{
    [TestCase((ushort)0, "0000000000000000")]
    [TestCase((ushort)0b1, "0000000000000001")]
    [TestCase((ushort)0b0001010100111000, "0001010100111000")]
    [TestCase((ushort)0b1110111100110010, "1110111100110010")]
    [TestCase((ushort)(ushort.MaxValue - 1), "1111111111111110")]
    [TestCase(ushort.MaxValue, "1111111111111111")]
    public static void GetsBinary(ushort input, string expected)
    {
        // Arrange

        // Act
        var result = Int2Binary.Int2Bin(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }


    [TestCase((uint)0, "00000000000000000000000000000000")]
    [TestCase((uint)0b1, "00000000000000000000000000000001")]
    [TestCase((uint)0b0001010100111000, "00000000000000000001010100111000")]
    [TestCase((uint)0b1110111100110010, "00000000000000001110111100110010")]
    [TestCase(0b10101100001110101110111100110010, "10101100001110101110111100110010")]
    [TestCase(uint.MaxValue - 1, "11111111111111111111111111111110")]
    [TestCase(uint.MaxValue, "11111111111111111111111111111111")]
    public static void GetsBinary(uint input, string expected)
    {
        // Arrange

        // Act
        var result = Int2Binary.Int2Bin(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase((ulong)0, "0000000000000000000000000000000000000000000000000000000000000000")]
    [TestCase((ulong)0b1, "0000000000000000000000000000000000000000000000000000000000000001")]
    [TestCase((ulong)0b0001010100111000, "0000000000000000000000000000000000000000000000000001010100111000")]
    [TestCase((ulong)0b1110111100110010, "0000000000000000000000000000000000000000000000001110111100110010")]
    [TestCase((ulong)0b10101100001110101110111100110010,
        "0000000000000000000000000000000010101100001110101110111100110010")]
    [TestCase(0b1000101110100101000011010101110101010101110101001010000011111000,
        "1000101110100101000011010101110101010101110101001010000011111000")]
    [TestCase(ulong.MaxValue - 1, "1111111111111111111111111111111111111111111111111111111111111110")]
    [TestCase(ulong.MaxValue, "1111111111111111111111111111111111111111111111111111111111111111")]
    public static void GetsBinary(ulong input, string expected)
    {
        // Arrange

        // Act
        var result = Int2Binary.Int2Bin(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}
