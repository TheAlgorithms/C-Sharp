using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Sorters
{
    /// <summary>
    /// Class that implements the BuckerSort Algorithm.
    /// </summary>
    public class BucketSort : ISorter<int>
    {
        /// <summary>
        /// Method that output values in array.
        /// </summary>
        /// <param name="data">Input Array.</param>
        /// <returns>Values in Array.</returns>
        public static string BucketToString(IEnumerable<int> data)
        {
            var temporary = data.Aggregate(
                string.Empty,
                (current, element) => current + element);

            return temporary;
        } // end method ToString

        /// <summary>
        /// Sorts array elements using BucketSort Algorithm.
        /// </summary>
        /// <param name="array">Input Array.</param>
        /// <param name="comparer">int comparer.</param>
        public void Sort(int[] array, IComparer<int> comparer)
        {
            // store maximum number of digits in numbers to sort
            var totalDigits = NumberOfDigits(array);

            // bucket array where numbers will be placed
            var pail = new int[10, array.Length + 1];

            // go through all digit places and sort each number
            // according to digit place value
            for (var pass = 1; pass <= totalDigits; pass++)
            {
                DistributeElements(array, pail, pass); // distribution pass
                CollectElements(array, pail); // gathering pass

                if (pass != totalDigits)
                {
                    EmptyBucket(pail); // set size of buckets to 0
                }
            }
        }

        /// <summary>
        /// Determines the number of digits in the largest number.
        /// </summary>
        /// <param name="array">Input array.</param>
        /// <returns>Number of digits.</returns>
        private static int NumberOfDigits(IEnumerable<int> array)
        {
            // loop over elements to find largest
            var largest = array.Max();

            // calculate number of digits in largest value
            return (int)Math.Floor(Math.Log10(largest) + 1);
        }

        /// <summary>
        /// To distribute elements into buckets based on specified digit.
        /// </summary>
        /// <param name="data">Input array.</param>
        /// <param name="pail">Pail.</param>
        /// <param name="digit">Digit.</param>
        private static void DistributeElements(IEnumerable<int> data, int[,] pail, int digit)
        {
            // determine the divisor used to get specific digit
            var divisor = (int)Math.Pow(10, digit);

            foreach (var element in data)
            {
                // bucketNumber example for hundreds digit:
                // ( 1234 % 1000 ) / 100 --> 2
                var bucketNumber = element % divisor / (divisor / 10); // number of bucket to place element

                // retrieve value in pail[ bucketNumber , 0 ] to
                // determine the location in row to store element
                var elementNumber = ++pail[bucketNumber, 0]; // location in bucket to place element
                pail[bucketNumber, elementNumber] = element;
            } // end foreach
        } // end method DistributeElements

        /// <summary>
        /// Return elements to original array.
        /// </summary>
        /// <param name="data">Input array.</param>
        /// <param name="pails">Pails.</param>
        private static void CollectElements(IList<int> data, int[,] pails)
        {
            var subscript = 0; // initialize location in data

            // loop over buckets
            for (var i = 0; i < 10; i++)
            {
                // loop over elements in each bucket
                for (var j = 1; j <= pails[i, 0]; j++)
                {
                    data[subscript++] = pails[i, j]; // add element to array
                }
            } // end outer for
        } // end method CollectElements

        /// <summary>
        /// Sets size of all buckets to zero.
        /// </summary>
        /// <param name="pails">Pails.</param>
        private static void EmptyBucket(int[,] pails)
        {
            for (var i = 0; i < 10; i++)
            {
                pails[i, 0] = 0; // set size of bucket to 0
            }
        } // end method EmptyBucket
    }
}
