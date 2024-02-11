using Algorithms.Encoders;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;

namespace Algorithms.Tests.Encoders;

public static class FeistelCipherTests
{
    [Test]
    public static void DecodedStringIsTheSame([Random(100)] uint key)
    {
        // Arrange
        var encoder = new FeistelCipher();
        var random = new Randomizer();

        int lenOfString = random.Next(1000);

        string message = random.GetString(lenOfString);

        // Act
        var encoded = encoder.Encode(message, key);
        var decoded = encoder.Decode(encoded, key);

        // Assert
        Assert.That(decoded, Is.EqualTo(message));
    }

    [TestCase("00001111",                           (uint)0x12345678)]
    [TestCase("00001111222233334444555566667",      (uint)0x12345678)]
    [TestCase("000011112222333344445555666677",     (uint)0x12345678)]
    [TestCase("0000111122223333444455556666777",    (uint)0x12345678)]
    // The plain text will be padded to fill the size of block (16 bytes), so the encoded message should be aligned with the rule
    // (text.Length % 16 == 0)
    public static void TestEncodedMessageSize(string testCase, uint key)
    {
        // Arrange
        var encoder = new FeistelCipher();

        // Assert
        Assert.Throws<ArgumentException>(() => encoder.Decode(testCase, key));
    }
}
