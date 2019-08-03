﻿using Algorithms.Encoders;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Encoders
{
    public class VigenereEncoderTests
    {
        [Test]
        [Repeat(100)]
        public void DecodedStringIsTheSame()
        {
            // Arrange
            var random = new Randomizer();
            var encoder = new VigenereEncoder();
            var message = random.GetString();
            var key = random.GetString(random.Next(0,1000));

            // Act
            var encoded = encoder.Encode(message, key);
            var decoded = encoder.Decode(encoded, key);

            // Assert
            Assert.AreEqual(message, decoded);
        }

        //[Test]
        //public void DecodedStringIsTheSameManualKey()
        //{
        //    // Arrange
        //    var random = new Randomizer();
        //    var encoder = new VigenereEncoder();
        //    var message = random.GetString();
        //    const string key = "shiftIt";

        //    // Act
        //    var encoded = encoder.Encode(message, key);
        //    var decoded = encoder.Decode(encoded, key);

        //    // Assert
        //    Assert.AreEqual(message, decoded);
        //}
    }
}
