using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearSearch
{
    class linear_search
    {
        static int searchInt; // search
        static int position; // location of search key in array
        static int[] data; // array of values

        static void Main(string[] args)
        {
            Random generator = new Random();

            Console.WriteLine("How many values do you want to create?");
            int size = Convert.ToInt32(Console.ReadLine());

            data = new int[size]; // create space for array

            // fill array with random ints in range 10-99
            for (int i = 0; i < size; i++)
            {
                data[i] = generator.Next(10, 100);
            }

            //display the array
            foreach(var item in data)
            {
                Console.Write(" {0} ", item);
            }

            // get value we want to find
            Console.Write("\nEnter the value to search for: ");
            searchInt = Convert.ToInt32(Console.ReadLine());

            // search array linearly
            position = LinearSearch(searchInt);

            // return value of -1 indicates integer was not found
            if (position == -1)
            {
                Console.WriteLine("The integer {0} was not found.\n", searchInt);
            }
            else
            {
                Console.WriteLine("The integer {0} was found in position {1}.\n", searchInt, position);
            }

            
        }

        // perform a linear search on the data
        public static int LinearSearch(int search)
        {
            return RecursiveLinearSearch(search, 0);
        } // end method LinearSearch

        public static int RecursiveLinearSearch(int search, int start)
        {
            int location; // variable to store return value

            if (start >= data.Length) // if at end of array
                location = -1; // value not found
            else
            {
                // if item is equal to search key
                if (data[start] == search)
                    location = start; // return current location
                else
                    // recursively search rest of array
                    location = RecursiveLinearSearch(search, start + 1);
            } // end else

            return location; // return location of search key
        } // end method RecursiveLinearSearch

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
