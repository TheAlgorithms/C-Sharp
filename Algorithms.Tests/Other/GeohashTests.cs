using Algorithms.Other;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Tests.Other
{
    [TestFixture]
    public class GeohashTests
    {
        [Test]
        public void Encode_ShouldReturnCorrectGeohash_ForHoChiMinhCity()
        {
            double latitude = 10.8231;
            double longitude = 106.6297;
            string result = Geohash.Encode(latitude, longitude);
            Assert.That(result, Is.EqualTo("w3gvd6m3hh54"));
        }

        [Test]
        public void Encode_ShouldReturnCorrectGeohash_ForHanoi()
        {
            double latitude = 21.0285;
            double longitude = 105.8542;
            string result = Geohash.Encode(latitude, longitude);
            Assert.That(result, Is.EqualTo("w7er8u0evss2"));
        }

        [Test]
        public void Encode_ShouldReturnCorrectGeohash_ForDaNang()
        {
            double latitude = 16.0544;
            double longitude = 108.2022;
            string result = Geohash.Encode(latitude, longitude);
            Assert.That(result, Is.EqualTo("w6ugq4w7wj04"));
        }

        [Test]
        public void Encode_ShouldReturnCorrectGeohash_ForNhaTrang()
        {
            double latitude = 12.2388;
            double longitude = 109.1967;
            string result = Geohash.Encode(latitude, longitude);
            Assert.That(result, Is.EqualTo("w6jtsu485t8v"));
        }

        [Test]
        public void Encode_ShouldReturnCorrectGeohash_ForVungTau()
        {
            double latitude = 10.3460;
            double longitude = 107.0843;
            string result = Geohash.Encode(latitude, longitude);
            Assert.That(result, Is.EqualTo("w3u4ug2mv41m"));
        }
    }
}
