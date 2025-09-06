namespace Algorithms.Other;

public class Geofence
{
    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public double RadiusInMeters { get; set; }

    public Geofence(double latitude, double longitude, double radiusInMeters)
    {
        Latitude = latitude;
        Longitude = longitude;
        RadiusInMeters = radiusInMeters;
    }

    /// <summary>
    /// Checks whether the provided user location (latitude and longitude) is within the geofence boundary.
    /// The geofence is defined by a center point (latitude, longitude) and a radius in meters.
    /// </summary>
    /// <param name="userLatitude">The latitude of the user's current location.</param>
    /// <param name="userLongitude">The longitude of the user's current location.</param>
    /// <returns>Returns true if the user is inside the geofence, otherwise returns false.</returns>
    public bool IsInside(double userLatitude, double userLongitude)
    {
        double distance = GeoLocation.CalculateDistanceFromLatLng(Latitude, Longitude, userLatitude, userLongitude);
        return distance <= RadiusInMeters;
    }
}
