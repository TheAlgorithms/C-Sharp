using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Other
{
    public class GeoLocation
    {
        private static readonly double EarthRadius = 6371.01d;

        /// <summary>
        /// Calculate distance from 2 point of latitude, longitude from https://www.movable-type.co.uk/scripts/latlong.html.
        /// </summary>
        /// <param name="lat1">Latitude of point A.</param>
        /// <param name="lng1">Longitude of point A.</param>
        /// <param name="lat2">Latitude of point B.</param>
        /// <param name="lng2">Longitude of point B.</param>
        /// <returns>Distance between point A to point B.</returns>
        public static decimal CalculateDistanceFromLatLng(double lat1, double lng1, double lat2, double lng2)
        {
            var result = 0m;

            try
            {
                if (lat1 == lat2 && lng1 == lng2)
                {
                    return result;
                }

                var pi180 = Math.PI / 180d;
                var newLat1 = lat1 * pi180;
                var newLng1 = lng1 * pi180;
                var newLat2 = lat2 * pi180;
                var newLng2 = lng2 * pi180;

                var diffLat = newLat2 - newLat1;
                var diffLng = newLng2 - newLng1;

                var haversine = Math.Sin(diffLat / 2) * Math.Sin(diffLat / 2) + Math.Cos(newLat1) * Math.Cos(newLat2) * Math.Sin(diffLng / 2) * Math.Sin(diffLng / 2);
                var distance = EarthRadius * (2d * Math.Atan2(Math.Sqrt(haversine), Math.Sqrt(1 - haversine)));

                return Convert.ToDecimal(distance * 1000); // Convert from km -> m
            }
            catch (Exception)
            {
                return 0m;
            }
        }
    }
}
