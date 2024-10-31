using System;

namespace Algorithms.Numeric;

/// <summary>
///     A perfect cube is an element of algebraic structure that is equal to the cube of another element.
/// </summary>
public static class PerfectCubeChecker
{
    /// <summary>
    ///     Checks if a number is a perfect cube or not.
    /// </summary>
    /// <param name="number">Number to check.</param>
    /// <returns>True if is a perfect cube; False otherwise.</returns>
    public static bool IsPerfectCube(int number)
    {
        if (number < 0)
        {
            number = -number;
        }

        var cubeRoot = Math.Round(Math.Pow(number, 1.0 / 3.0));
        return Math.Abs(cubeRoot * cubeRoot * cubeRoot - number) < 1e-6;
    }

    /// <summary>
    ///     Checks if a number is a perfect cube or not using binary search.
    /// </summary>
    /// <param name="number">Number to check.</param>
    /// <returns>True if is a perfect cube; False otherwise.</returns>
    public static bool IsPerfectCubeBinarySearch(int number)
    {
        if (number < 0)
        {
            number = -number;
        }

        int left = 0;
        int right = number;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            int midCubed = mid * mid * mid;
            if (midCubed == number)
            {
                return true;
            }
            else if (midCubed < number)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return false;
    }
}
