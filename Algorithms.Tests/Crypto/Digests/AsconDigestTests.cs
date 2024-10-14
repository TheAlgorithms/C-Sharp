using System;
using System.Text;
using Algorithms.Crypto.Digests;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Crypto.Digests;

[NonParallelizable]
public class AsconDigestTests
{
    private readonly AsconDigest asconHash = new AsconDigest(AsconDigest.AsconParameters.AsconHash);
    private readonly AsconDigest asconHashA = new AsconDigest(AsconDigest.AsconParameters.AsconHashA);

    [TestCase("a", "02a9d471afab12914197af7090f00d16c41b6e30be0a63bbfd00bc13064de548")]
    [TestCase("abc", "d37fe9f1d10dbcfad8408a6804dbe91124a8912693322bb23ec1701e19e3fd51")]
    [TestCase("Hello","d80f38d94ad72bd18718879f753a44870e8446925ff64bd7441db5fe020b6c0c")]
    [TestCase("message digest","e8848979c5adfd21bfcf29e54be1dd085ee523d251e8e6876f2654d6368da0ca")]
    [TestCase("abcdefghijklmnopqrstuvwxyz","c62368674e1b2301f19f46c50bb7f87a988a3e41205d68ab9d7882d2a15e917b")]
    [TestCase("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "4ff71928d740524735b5ab12bb1598463054f88089f3c5f9760b6bdcd23f897b")]
    [TestCase("12345678901234567890123456789012345678901234567890123456789012345678901234567890", "2dae8b553b93841120e88ee77b9ccb8b512a32318db6012025f3f1c482b1def8")]
    public void AsconHash_ReturnsCorrectValue(string input, string expected)
    {
        var inputBytes = Encoding.ASCII.GetBytes(input);
        var result = asconHash.Digest(inputBytes);

        result.Should().Be(expected);
    }

    [TestCase("a", "062bb0346671da00da4f460308b4d2c4d9877c3e2827d6229ff5361332d36527")]
    [TestCase("abc", "836a5ddba0142b011ce3425ea9789fd6a21628d619195a48c1540f847667a84e")]
    [TestCase("Hello","15f245df8af697dc540e86083822809ab7299575d8ad6c2e17ecc603a7ab79dd")]
    [TestCase("message digest","3f18a1f398a40a77e0e9477aa6cb50e9e1abecff651c1874f9717c02c8a165ba")]
    [TestCase("abcdefghijklmnopqrstuvwxyz","406b809260f361e12dcf0bf924bfe1ffd2f987fc18d90b94fc544ff80dc2946b")]
    [TestCase("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "5c6c69ff3ee83361391b7236c8eb6718f52df43de5a61a4f4d2819d40430dc19")]
    [TestCase("12345678901234567890123456789012345678901234567890123456789012345678901234567890", "d8e38fc50d682550cd176decda61adb7fd1c793cdafa825f17f3a002d65847be")]
    public void AsconHashA_ReturnsCorrectValue(string input, string expected)
    {
        var inputBytes = Encoding.ASCII.GetBytes(input);
        var result = asconHashA.Digest(inputBytes);

        result.Should().Be(expected);
    }

    private static string ToHexString(byte[] bytes)
    {
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }
}
