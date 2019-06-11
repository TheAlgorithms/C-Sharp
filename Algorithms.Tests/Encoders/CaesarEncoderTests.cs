using Algorithms.Encoders;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;

namespace Algorithms.Tests.Encoders
{
    public class CaesarEncoderTests
    {
        private readonly Randomizer random = new Randomizer();
        private readonly CaesarEncoder encoder = new CaesarEncoder();

        private string RandomMessage => random.GetString();

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
