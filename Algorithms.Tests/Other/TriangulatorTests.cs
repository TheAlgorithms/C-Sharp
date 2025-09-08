using Algorithms.Other;

namespace Algorithms.Tests.Other
{
    [TestFixture]
    public class TriangulatorTests
    {
        [Test]
        public void CalculatePosition_ValidCoordinatesAndDistances_ReturnsExpectedPosition()
        {
            var triangulator = new Triangulator();
            var baseLocations = new List<(double Latitude, double Longitude)>
            {
                (16.054407, 108.202167),
                (16.049807, 108.218991),
                (16.063597, 108.215553)
            };

            var distances = new List<double> { 0.5, 0.7, 0.6 };

            var expectedPosition = (Latitude: 16.054, Longitude: 108.210);
            var result = triangulator.CalculatePosition(baseLocations, distances);

            Assert.That(result.Latitude, Is.EqualTo(expectedPosition.Latitude).Within(0.01));
            Assert.That(result.Longitude, Is.EqualTo(expectedPosition.Longitude).Within(0.01));
        }

        [Test]
        public void CalculatePosition_InvalidBaseLocations_ThrowsArgumentException()
        {
            var triangulator = new Triangulator();
            var baseLocations = new List<(double Latitude, double Longitude)>
        {
            (10.762622, 106.660172)
        };
            var distances = new List<double> { 1.0 };

            Assert.That(() => triangulator.CalculatePosition(baseLocations, distances), Throws.ArgumentException);
        }

        [Test]
        public void CalculatePosition_InvalidDistances_ThrowsArgumentException()
        {
            var triangulator = new Triangulator();
            var baseLocations = new List<(double Latitude, double Longitude)>
        {
            (10.762622, 106.660172),
            (10.774981, 106.665504),
            (10.771817, 106.681179)
        };
            var distances = new List<double> { 1.0 };

            Assert.That(() => triangulator.CalculatePosition(baseLocations, distances), Throws.ArgumentException);
        }
    }
}
