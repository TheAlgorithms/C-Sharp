using System;
using Algorithms.Encoders;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Encoders
{
    public static class SoundexEncoderTest
    {
        [Test]
        public static void AttemptSoundex()
        {
            string[] names = {
                "Robert", "Rupert", "Rubin", "Ashcraft", "Ashcroft",
                "Tymczak", "Pfister", "Honeyman"
            };
            string[] expected = {
                "R163", "R163", "R150", "A261", "A261", "T522", "P236",
                "H555"
            };

            SoundexEncoder enc = new SoundexEncoder();
            names.Enumerate((name, ind) => {
                string nysiis = enc.Encode(name);
                Assert.AreEqual(nysiis, expected[ind]);
            });
        }
    }
}
