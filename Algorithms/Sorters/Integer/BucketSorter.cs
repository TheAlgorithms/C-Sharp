using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Sorters.Integer
{
    /// <summary>
    /// Class that implements bucket sort algorithm.
    /// </summary>
    public class BucketSorter : IIntegerSorter
    {
        private const int NumOfDigitsInBase10 = 10;

        /// <summary>
        /// Sorts array elements using BucketSort Algorithm.
        /// </summary>
        /// <param name="array">Array to sort.</param>
        public void Sort(int[] array)
        {
            if (array.Length <= 1)
            {
                return;
            }

            // store maximum number of digits in numbers to sort
            var totalDigits = NumberOfDigits(array);

            // bucket array where numbers will be placed
            var buckets = new int[NumOfDigitsInBase10, array.Length + 1];

            // go through all digit places and sort each number
            // according to digit place value
            for (var pass = 1; pass <= totalDigits; pass++)
            {
                DistributeElements(array, buckets, pass); // distribution pass
                CollectElements(array, buckets); // gathering pass

                if (pass != totalDigits)
                {
                    EmptyBucket(buckets); // set size of buckets to 0
                }
            }
        }

        /// <summary>
        /// Determines the number of digits in the largest number.
        /// </summary>
        /// <param name="array">Input array.</param>
        /// <returns>Number of digits.</returns>
        private static int NumberOfDigits(IEnumerable<int> array) => (int)Math.Floor(Math.Log10(array.Max()) + 1);

        /// <summary>
        /// To distribute elements into buckets based on specified digit.
        /// </summary>
        /// <param name="data">Input array.</param>
        /// <param name="buckets">Array of buckets.</param>
        /// <param name="digit">Digit.</param>
        private static void DistributeElements(IEnumerable<int> data, int[,] buckets, int digit)
        {
            // determine the divisor used to get specific digit
            var divisor = (int)Math.Pow(10, digit);

            foreach (var element in data)
            {
                // bucketNumber example for hundreds digit:
                // ( 1234 % 1000 ) / 100 --> 2
                var bucketNumber = NumOfDigitsInBase10 * (element % divisor) / divisor; // number of bucket to place element

                // retrieve value in pail[ bucketNumber , 0 ] to
                // determine the location in row to store element
                var elementNumber = ++buckets[bucketNumber, 0]; // location in bucket to place element
                buckets[bucketNumber, elementNumber] = element;
            }
        }

        /// <summary>
        /// Return elements to original array.
        /// </summary>
        /// <param name="data">Input array.</param>
        /// <param name="buckets">Array of buckets.</param>
        private static void CollectElements(IList<int> data, int[,] buckets)
        {
            var subscript = 0; // initialize location in data

            // loop over buckets
            for (var i = 0; i < NumOfDigitsInBase10; i++)
            {
                // loop over elements in each bucket
                for (var j = 1; j <= buckets[i, 0]; j++)
                {
                    data[subscript++] = buckets[i, j]; // add element to array
                }
            }
        }

        /// <summary>
        /// Sets size of all buckets to zero.
        /// </summary>
        /// <param name="buckets">Array of buckets.</param>
        private static void EmptyBucket(int[,] buckets)
        {
            for (var i = 0; i < NumOfDigitsInBase10; i++)
            {
                buckets[i, 0] = 0; // set size of bucket to 0
            }
        }
    }
}
