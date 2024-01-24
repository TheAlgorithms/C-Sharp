using System.Collections.Generic;
using System.Linq;
using Algorithms.Encoders;
using NUnit.Framework;

namespace Algorithms.Tests.Encoders;

public static class SoundexEncoderTest
{
    private static readonly string[] Names =
    {
        "Robert", "Rupert", "Rubin", "Ashcraft", "Ashcroft", "Tymczak", "Pfister", "Honeyman",
    };

    private static readonly string[] Expected = { "R163", "R163", "R150", "A261", "A261", "T522", "P236", "H555" };

    private static IEnumerable<string[]> TestData => Names.Zip(Expected, (l, r) => new[] { l, r });

    [TestCaseSource(nameof(TestData))]
    public static void AttemptSoundex(string source, string encoded)
    {
        SoundexEncoder enc = new();
        var nysiis = enc.Encode(source);
        Assert.That(encoded, Is.EqualTo(nysiis));
    }
}
