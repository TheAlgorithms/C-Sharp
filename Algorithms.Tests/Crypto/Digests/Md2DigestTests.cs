using System;
using System.Text;
using Algorithms.Crypto.Digests;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Crypto.Digesters;

public class Md2DigestTests
{
    private readonly Md2Digest digest = new Md2Digest();

    private readonly string[] messages =
    {
        "",
        "a",
        "abc",
        "message digest",
        "abcdefghijklmnopqrstuvwxyz",
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789",
        "12345678901234567890123456789012345678901234567890123456789012345678901234567890",
    };

    private readonly string[] expectedDigests =
    {
        "8350E5A3E24C153DF2275C9F80692773",
        "32EC01EC4A6DAC72C0AB96FB34C0B5D1",
        "DA853B0D3F88D99B30283A69E6DED6BB",
        "AB4F496BFB2A530B219FF33031FE06B0",
        "4E8DDFF3650292AB5A4108C3AA47940B",
        "DA33DEF2A42DF13975352846C30338CD",
        "D5976F79D83D3A0DC9806C3C66F3EFD8",
    };

    [Test]
    public void Digest_ReturnsCorrectValue()
    {
        for (var i = 0; i < messages.Length; i++)
        {
            var input = Encoding.ASCII.GetBytes(messages[i]);

            var result = digest.Digest(input);

            var output = Convert.ToHexString(result);

            output.Should().Be(expectedDigests[i]);
        }
    }
}
