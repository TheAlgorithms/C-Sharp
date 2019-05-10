using System;

namespace bogosort
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Please enter some integers, separated by spaces:");

            var input = Console.ReadLine();
            var integers = input.Split(' ');
            var data = new int[integers.Length];

            for (var i = 0; i < data.Length; i++)
            {
                data[i] = int.Parse(integers[i]);
            }

            Console.Write("\nUnsorted: ");
            for (var i = 0; i < data.Length; i++)
            {
                Console.Write(data[i] + " ");
            }

            // Perform the sort
            var result = BogoSort(data);

            Console.Write("\nSorted: ");
            for (var i = 0; i < result.Length; i++)
            {
                Console.Write(result[i] + " ");
            }

            Console.ReadKey();
        }

        public static int[] BogoSort(int[] intArray)
        {
            while (!IsSorted(intArray))
            {
                var random = new Random();
                
                for(var q = intArray.Length; q > 1; q--)
                {
                    // Pick random element
                    var j = random.Next(q);

                    // Swap the elements
                    var temp = intArray[j];
                    intArray[j] = intArray[q - 1];
                    intArray[q - 1] = temp;
                }
            }

            return intArray;
        }

        public static bool IsSorted(int[] testArray)
        {
            if (testArray.Length < 2)
            {
                return true;
            }
            else
            {
                for (var i = 0; i < testArray.Length - 1; i++)
                {
                    if (testArray[i] > testArray[i + 1])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}