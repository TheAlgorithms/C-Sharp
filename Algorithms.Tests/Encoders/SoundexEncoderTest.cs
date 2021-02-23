using System.Collections.Generic;
using System.Linq;
using Algorithms.Encoders;
using NUnit.Framework;

namespace Algorithms.Tests.Encoders
{
    public static class SoundexEncoderTest
    {
        [TestCaseSource(nameof(TestData))]
        public static void AttemptSoundex(string source, string encoded)
        {
            SoundexEncoder enc = new();
            var nysiis = enc.Encode(source);
            Assert.AreEqual(nysiis, encoded);
        }

        static IEnumerable<string[]> TestData => _names.Zip(_expected, (l, r) => new[] { l, r });

        static readonly string[] _names = {
            "Robert", "Rupert", "Rubin", "Ashcraft", "Ashcroft",
            "Tymczak", "Pfister", "Honeyman"
        };
        static readonly string[] _expected = {
            "R163", "R163", "R150", "A261", "A261", "T522", "P236",
            "H555"
        };
    }
}
