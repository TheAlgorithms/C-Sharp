using System;
using System.Collections.Generic;

namespace Algorithms.Search;

/// <summary>
/// Interpolation search is an algorithm used for searching for a key in an array that has been ordered by numerical values.
/// It's similar to a BinarySearch in the form of it truncating the array of which is searching though.
/// There are a few things that the array needs to keep track of while running.
/// L = The left most index of the array.
/// R = The right most index of the array.
/// Key = The value we're trying to find.
/// It utilizes a formula which runs every iteration until it finds the key we're looking for.
/// Formula: L + ((R-L) / (values[R] - values[L])) * (key - values[L]).
/// </summary>
public class InterpolationSearch
{
    /* Index of values to search from */
    private readonly int[] indicies = { 1, 3, 5, 8, 10, 22, 31, 35, 37, 42, 51 };

    /* The key (value) we want to find in the index of values */
    private readonly int key = 22;

    private int Search(IReadOnlyList<int> valuesToSearch, int keyToFind)
    {
        var l = 0;
        var r = valuesToSearch.Count - 1;

        while (keyToFind >= valuesToSearch[l] && keyToFind <= valuesToSearch[r] && l <= r)
        {
            /* Perform the formula in order to get the estimate index of where the Key might be. */
            int seek = l + (r - l) * (keyToFind - valuesToSearch[l]) / (valuesToSearch[r] - valuesToSearch[l]);
            /* If the value was found, we'll return the value. */
            if (valuesToSearch[seek] == keyToFind)
            {
                return seek;
            }

            /* If we did not find the value within a certain iteration then we need to truncate the array.*/
            if (valuesToSearch[seek] < keyToFind)
            {
                /* If the seek value is less than the key, Then we want to truncate the left side of the array. */
                l = seek + 1;
            }
            else
            {
                /* If the seek value is greater than than the key, Then we want to truncate the right side of the array. */
                r = seek - 1;
            }
        }

        /* Return -1 in order to indicate that the key was not found. */
        return -1;
    }

    protected InterpolationSearch()
    {
        var value = Search(indicies, key);
        Console.WriteLine(value != -1 ? $"Value [{value}]: was found!" : $"[Key {key}] was not found.");
    }
}
