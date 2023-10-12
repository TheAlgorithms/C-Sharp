using System;
using System.Text;
using Algorithms.Crypto.Digests;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Crypto.Digesters;

[NonParallelizable]
public class Md2DigestTests
{
    private readonly Md2Digest digest = new Md2Digest();

    [TestCase("", "8350E5A3E24C153DF2275C9F80692773")]
    [TestCase("a", "32EC01EC4A6DAC72C0AB96FB34C0B5D1")]
    [TestCase("abc", "DA853B0D3F88D99B30283A69E6DED6BB")]
    [TestCase("message digest", "AB4F496BFB2A530B219FF33031FE06B0")]
    [TestCase("abcdefghijklmnopqrstuvwxyz", "4E8DDFF3650292AB5A4108C3AA47940B")]
    [TestCase("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", "DA33DEF2A42DF13975352846C30338CD")]
    [TestCase("12345678901234567890123456789012345678901234567890123456789012345678901234567890", "D5976F79D83D3A0DC9806C3C66F3EFD8")]
    [TestCase("123456789012345678901234567890123456789012345678901234567890123456789012345678901", "6FAD0685C4A3D03E3D352D12BBAD6BE3")]
    public void Digest_ReturnsCorrectValue(string input, string expected)
    {
        var inputBytes = Encoding.ASCII.GetBytes(input);

        var result = digest.Digest(inputBytes);

        var output = Convert.ToHexString(result);

        output.Should().Be(expected);
    }
}
