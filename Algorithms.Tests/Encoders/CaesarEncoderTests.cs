using System;
using Algorithms.Encoders;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Encoders
{
    public class CaesarEncoderTests
    {
        readonly Randomizer random = new Randomizer();
        readonly CaesarEncoder encoder = new CaesarEncoder();
        string RandomMessage => random.GetString();

        [Test]
        [Parallelizable]
        public void DecodedStringIsSame([Random(100, Distinct = true)]int key)
        {
            var message = RandomMessage;

            var encoded = encoder.Encode(message, key);
            var decoded = encoder.Decode(encoded, key);

            Assert.AreEqual(message, decoded);
        }
    }
}
