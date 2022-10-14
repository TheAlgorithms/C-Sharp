namespace Algorithms.Search;

/// <summary>
/// An interpolation search requires a sorted array in order for it to work.
/// </summary>
public static class InterpolationSearch
{
    private static int[] values = { 1, 3, 5, 8, 10, 22, 31, 35, 37, 42, 51 };

    /* L = Left most index
     * R = Right most index
     * Key = Value to find
     */

    /* Calculate an estimated index */
    /* Formula: L + ((R-L) / (values[R] - values[L])) * (key - values[L]) */
    /* Formula: 0 + ((10-0) / (51 - 1)) * (22 - 1) */

    public static int Search()
    {
        var key = 22;
        var l = 0;
        var r = values.Length - 1;

        while (key >= values[l] && key <= values[r] && l <= r)
        {
            int seek = l + (r - l) * (key - values[l]) / (values[r] - values[l]);
            if (values[seek] == key)
            {
                return seek;
            }

            /* If the seek value is less than the key,
             Then we want to truncate the left side of the array. */
            if (values[seek] < key)
            {
                l = seek + 1;
            }
            else
            {
                r = seek - 1;
            }
        }

        /* Return -1 in order to indicate that the key was not found. */
        return -1;
    }
}
