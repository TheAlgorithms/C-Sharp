using Algorithms.Encoders;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Encoders;

// Tests ported from the Java Algorithms repository

public class BlowfishEncoderTests
{
    private BlowfishEncoder _encoder = new();
    const string key = "aabb09182736ccdd";

    [SetUp]
    public void Setup()
    {
        _encoder = new BlowfishEncoder();
        _encoder.GenerateKey(key);
    }

    [Test]
    public void BlowfishEncoder_Encryption_ShouldWorkCorrectly()
    {
        const string plainText = "123456abcd132536";

        const string cipherText = "d748ec383d3405f7";

        var result = _encoder.Encrypt(plainText);

        result.Should().Be(cipherText);
    }

    [Test]
    public void BlowfishEncoder_Decryption_ShouldWorkCorrectly()
    {
        const string cipherText = "d748ec383d3405f7";

        const string plainText = "123456abcd132536";

        var result = _encoder.Decrypt(cipherText);

        result.Should().Be(plainText);
    }
}
