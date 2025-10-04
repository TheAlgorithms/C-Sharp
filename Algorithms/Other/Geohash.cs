namespace Algorithms.Other;

public static class Geohash
{
    private const string Base32Characters = "0123456789bcdefghjkmnpqrstuvwxyz"; // Convert latitude and longitude coordinates into a concise string
    private const int GeohashLength = 12; // ± 1.86 cm

    /// <summary>
    /// Encodes the provided latitude and longitude coordinates into a Geohash string.
    /// Geohashing is a method to encode geographic coordinates (latitude, longitude).
    /// into a short string of letters and digits. Each character in the resulting Geohash .
    /// string adds more precision to the location. The longer the Geohash, the smaller the area.
    /// </summary>
    /// <param name="latitude">The latitude of the location to encode. It must be a value between -90 and 90.</param>
    /// <param name="longitude">The longitude of the location to encode. It must be a value between -180 and 180.</param>
    /// <returns>
    /// A Geohash string of length 12 representing the location with high precision.
    /// A longer Geohash provides higher precision in terms of geographic area.
    /// and a 12-character Geohash can be accurate down to around 1.86 cm.
    /// </returns>
    public static string Encode(double latitude, double longitude)
    {
        double[] latitudeRange = [-90.0, 90.0];
        double[] longitudeRange = [-180.0, 180.0];
        bool isEncodingLongitude = true;
        int currentBit = 0;
        int base32Index = 0;
        StringBuilder geohashResult = new StringBuilder();

        while (geohashResult.Length < GeohashLength)
        {
            double midpoint;

            if (isEncodingLongitude)
            {
                midpoint = (longitudeRange[0] + longitudeRange[1]) / 2;
                if (longitude > midpoint)
                {
                    base32Index |= 1 << (4 - currentBit);
                    longitudeRange[0] = midpoint;
                }
                else
                {
                    longitudeRange[1] = midpoint;
                }
            }
            else
            {
                midpoint = (latitudeRange[0] + latitudeRange[1]) / 2;
                if (latitude > midpoint)
                {
                    base32Index |= 1 << (4 - currentBit);
                    latitudeRange[0] = midpoint;
                }
                else
                {
                    latitudeRange[1] = midpoint;
                }
            }

            isEncodingLongitude = !isEncodingLongitude;

            if (currentBit < 4)
            {
                currentBit++;
            }
            else
            {
                geohashResult.Append(Base32Characters[base32Index]);
                currentBit = 0;
                base32Index = 0;
            }
        }

        return geohashResult.ToString();
    }
}
