using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Other
{
    public class Triangulator
    {
        public (double Latitude, double Longitude) CalculatePosition(List<(double Latitude, double Longitude)> baseLocations, List<double> distances)
        {
            if (baseLocations.Count < 3 || distances.Count < 3)
            {
                throw new ArgumentException("At least three points and corresponding distances are required.");
            }

            // Get the coordinates of the three base stations
            double lat1 = baseLocations[0].Latitude;
            double lon1 = baseLocations[0].Longitude;
            double lat2 = baseLocations[1].Latitude;
            double lon2 = baseLocations[1].Longitude;
            double lat3 = baseLocations[2].Latitude;
            double lon3 = baseLocations[2].Longitude;

            // Convert coordinates to radians
            lat1 = ToRadians(lat1);
            lon1 = ToRadians(lon1);
            lat2 = ToRadians(lat2);
            lon2 = ToRadians(lon2);
            lat3 = ToRadians(lat3);
            lon3 = ToRadians(lon3);

            // Calculate the center point
            double centerLat = (lat1 + lat2 + lat3) / 3;
            double centerLon = (lon1 + lon2 + lon3) / 3;

            // Convert back to degrees
            centerLat = ToDegrees(centerLat);
            centerLon = ToDegrees(centerLon);

            return (centerLat, centerLon);
        }

        private double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        private double ToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }
    }
}
