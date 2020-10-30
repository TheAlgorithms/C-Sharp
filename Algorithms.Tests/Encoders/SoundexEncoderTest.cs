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
            SoundexEncoder enc = new SoundexEncoder();
            var nysiis = enc.Encode(source);
            Assert.AreEqual(nysiis, encoded);
        }

        static IEnumerable<string[]> TestData => names.Zip(expected, (l, r) => new[] { l, r });

        static string[] names = {
            "Robert", "Rupert", "Rubin", "Ashcraft", "Ashcroft",
            "Tymczak", "Pfister", "Honeyman"
        };
        static string[] expected = {
            "R163", "R163", "R150", "A261", "A261", "T522", "P236",
            "H555"
        };
    }
}
