using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketSort
{
    class BucketSort
    {
        private int[] data; // array of values
        private static Random generator = new Random();

        static void Main(string[] args)
        {
            // create object to perform bucket sort
            BucketSort sortArray = new BucketSort(10);

            Console.WriteLine("Before:");
            Console.WriteLine(sortArray); // display unsorted array

            sortArray.Sort(); // sort array

            Console.WriteLine("After:");
            Console.WriteLine(sortArray); // display sorted array
        }


        // create array of given size and fill with random integers
        public BucketSort(int size)
        {
            data = new int[size]; // create space for array

            // fill array with random ints in range 10-99
            for (int i = 0; i < size; i++)
                data[i] = generator.Next(10, 100);
        } // end BucketSort constructor

        // perform bucket sort algorithm on array
        public void Sort()
        {
            // store maximum number of digits in numbers to sort
            int totalDigits = NumberOfDigits();

            // bucket array where numbers will be placed
            int[,] pail = new int[10, data.Length + 1];

            // go through all digit places and sort each number
            // according to digit place value
            for (int pass = 1; pass <= totalDigits; pass++)
            {
                DistributeElements(pail, pass); // distribution pass
                CollectElements(pail); // gathering pass

                if (pass != totalDigits)
                    EmptyBucket(pail); // set size of buckets to 0
            } // end for
        } // end method Sort

        // determine number of digits in the largest number
        public int NumberOfDigits()
        {
            int largest = data[0]; // set largest to first element

            // loop over elements to find largest
            foreach (var element in data)
                if (element > largest)
                    largest = element; // set largest to current element

            // calculate number of digits in largest value
            int digits = (int)(Math.Floor(Math.Log10(largest)) + 1);

            return digits;
        } // end method NumberOfDigits

        // distribute elements into buckets based on specified digit
        public void DistributeElements(int[,] pail, int digit)
        {
            int bucketNumber; // number of bucket to place element
            int elementNumber; // location in bucket to place element

            // determine the divisor used to get specific digit
            int divisor = (int)(Math.Pow(10, digit));

            foreach (var element in data)
            {
                // bucketNumber example for hundreds digit:
                // ( 1234 % 1000 ) / 100 --> 2
                bucketNumber = (element % divisor) / (divisor / 10);

                // retrieve value in pail[ bucketNumber , 0 ] to
                // determine the location in row to store element
                elementNumber = ++pail[bucketNumber, 0];
                pail[bucketNumber, elementNumber] = element;
            } // end foreach
        } // end method DistributeElements

        // return elements to original array
        public void CollectElements(int[,] pails)
        {
            int subscript = 0; // initialize location in data

            for (int i = 0; i < 10; i++) // loop over buckets
            {
                // loop over elements in each bucket
                for (int j = 1; j <= pails[i, 0]; j++)
                    data[subscript++] = pails[i, j]; // add element to array
            } // end outer for
        } // end method CollectElements

        // set size of all buckets to zero
        public void EmptyBucket(int[,] pails)
        {
            for (int i = 0; i < 10; i++)
                pails[i, 0] = 0; // set size of bucket to 0
        } // end method EmptyBucket

        // method to output values in array
        public override string ToString()
        {
            string temporary = string.Empty;

            // iterate through array
            foreach (var element in data)
                temporary += element + " ";

            temporary += "\n"; // add newline character
            return temporary;
        } // end method ToString
    }
}
