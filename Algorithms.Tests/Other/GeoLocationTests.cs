using System;

using Algorithms.Other;

using NUnit.Framework;

namespace Algorithms.Tests.Other
{
    public static class GeoLocationTests
    {
        [Test]
        [TestCase(53.430488d, -2.96129d, 53.430488d, -2.96129d, 0d)]
        [TestCase(53.430971d, -2.959806d, 53.430242d, -2.960830d, 105d)]
        public static void CalculateDistanceFromLatLngTest(double lat1, double lng1, double lat2, double lng2, double expectedValue)
        {
            var result = GeoLocation.CalculateDistanceFromLatLng(lat1, lng1, lat2, lng2);
            var actualValue = Convert.ToDouble(result);

            // Assert
            Assert.AreEqual(expectedValue, actualValue, 1d); // Accept if distance diff is +/-1 meters.
        }
    }
}
