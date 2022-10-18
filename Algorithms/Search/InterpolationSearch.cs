using System.Collections.Generic;

namespace Algorithms.Search;

/// <summary>
/// Interpolation search is an algorithm used for searching for a key in an array that has been ordered by numerical values.
/// It's similar to a BinarySearch in the form of it truncating the array of which is searching though.
/// There are a few things that the array needs to keep track of while running.
/// L = The left most index of the array.
/// ESTIndex = The estimated position of this value in the array (which it gets based on "The Formula").
/// R = The right most index of the array.
/// Key = The value we're trying to find.
/// It utilizes a formula which runs every iteration until it finds the key we're looking for.
/// The Formula: L + ((R-L) / (values[R] - values[L])) * (key - values[L]).
/// If it does not find the key that we're looking for, then it's going to return -1 in order to indicate that we did not find it.
/// </summary>
public class InterpolationSearch
{
    public int FindValue(IReadOnlyList<int> valuesToSearch, int keyToFind)
    {
        int l = 0;
        int r = valuesToSearch.Count - 1;
        while (l <= r && keyToFind >= valuesToSearch[l] && keyToFind <= valuesToSearch[r])
        {
            /* Perform the formula in order to get the estimate index of where the Key might be. */
            int estIndex = l + ((keyToFind - valuesToSearch[l]) * (r - l)) / (valuesToSearch[r] - valuesToSearch[l]);
            if (keyToFind > valuesToSearch[estIndex])
            {
                /* If the seek value is less than the key, Then we want to truncate the left side of the array. */
                l = estIndex + 1;
            }
            else if (keyToFind < valuesToSearch[estIndex])
            {
                /* If the seek value is greater than than the key, Then we want to truncate the right side of the array. */
                r = estIndex - 1;
            }
            else
            {
                return estIndex;
            }
        }

        /* Return -1 to indicate that we did not find the value within the array. */
        return -1;
    }
}
