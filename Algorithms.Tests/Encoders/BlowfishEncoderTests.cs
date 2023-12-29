using Algorithms.Encoders;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Encoders;

// Tests ported from the Java Algorithms repository

public class BlowfishEncoderTests
{
    private const string Key = "aabb09182736ccdd";

    [Test]
    public void BlowfishEncoder_Encryption_ShouldWorkCorrectly()
    {
        // Arrange
        var encoder = new BlowfishEncoder();
        encoder.GenerateKey(Key);

        const string plainText = "123456abcd132536";
        const string cipherText = "d748ec383d3405f7";

        // Act
        var result = encoder.Encrypt(plainText);

        // Assert
        result.Should().Be(cipherText);
    }

    [Test]
    public void BlowfishEncoder_Decryption_ShouldWorkCorrectly()
    {
        // Arrange
        var encoder = new BlowfishEncoder();
        encoder.GenerateKey(Key);

        const string cipherText = "d748ec383d3405f7";
        const string plainText = "123456abcd132536";

        // Act
        var result = encoder.Decrypt(cipherText);

        // Assert
        result.Should().Be(plainText);
    }
}
