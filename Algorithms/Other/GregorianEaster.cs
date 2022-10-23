using System;

namespace Algorithms.Other
{
    /// <summary>
    ///     Date of Easter calculated with the Gregorian algorithm.
    ///     The algorithm is described <a href="https://en.wikipedia.org/wiki/Date_of_Easter#Anonymous_Gregorian_algorithm">here</a>.
    /// </summary>
    public static class GregorianEaster
    {
        /// <summary>
        ///     Calculates the date of Easter.
        /// </summary>
        /// <param name="year">Year to calculate the date of Easter [from 1583 onwards]</param>
        /// <returns>Date of Easter as a DateTime</returns>
        public static DateTime Calculate(int year)
        {
            if (year < 1583)        //checks if the year is in the Gregorian calendar
                throw new ArgumentException("Invalid year");

            //implementation of the algorithm
            int a = year % 19;
            int b = year / 100;
            int c = year % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int month = (h + l - 7 * m + 114) / 31;
            int day = (h + l - 7 * m + 114) % 31 + 1;

            return new DateTime(year, month, day);
        }
    }
}
