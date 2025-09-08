using Algorithms.Other;

namespace Algorithms.Tests.Other
{
    [TestFixture]
    public class GeofenceTests
    {
        private Geofence? geofence;

        [SetUp]
        public void Setup()
        {
            geofence = new Geofence(10.8231, 106.6297, 500);
        }

        [Test]
        public void IsInside_ShouldReturnTrue_WhenUserIsInsideGeofence()
        {
            double userLat = 10.8221;
            double userLon = 106.6289;

            bool? result = geofence?.IsInside(userLat, userLon);

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsInside_ShouldReturnFalse_WhenUserIsOutsideGeofence()
        {
            double userLat = 10.8300;
            double userLon = 106.6400;

            bool? result = geofence?.IsInside(userLat, userLon);

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsInside_ShouldReturnTrue_WhenUserIsExactlyOnGeofenceBoundary()
        {
            double userLat = 10.8231;
            double userLon = 106.6297;

            bool? result = geofence?.IsInside(userLat, userLon);

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsInside_ShouldReturnFalse_WhenUserIsFarFromGeofence()
        {
            double userLat = 20.0000;
            double userLon = 100.0000;

            bool? result = geofence?.IsInside(userLat, userLon);

            Assert.That(result, Is.False);
        }
    }
}
