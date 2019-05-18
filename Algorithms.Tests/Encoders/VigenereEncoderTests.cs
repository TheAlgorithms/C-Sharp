using Algorithms.Encoders;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Encoders
{
    public class VigenereEncoderTests
    {
        readonly Randomizer random = new Randomizer();
        readonly VigenereEncoder encoder = new VigenereEncoder();
        string RandomMessage => random.GetString();

        [Test]
        [Parallelizable]
        [Repeat(100)]
        public void DecodedStringIsSame()
        {
            var message = RandomMessage;
            var key = RandomMessage;

            var encoded = encoder.Encode(message, key);
            var decoded = encoder.Decode(encoded, key);

            Assert.AreEqual(message, decoded);
        }
    }
}
