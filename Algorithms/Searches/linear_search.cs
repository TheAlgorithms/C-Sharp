using System;

namespace linear_search
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Please enter some integers, sorted & separated by spaces:");
            var input = Console.ReadLine();
            var integers = input.Split(' ');
            var data = new int[integers.Length];
            for (var i = 0; i < data.Length; i++)
            {
                data[i] = int.Parse(integers[i]);
            }

            while (true)
            {
                Console.WriteLine("\nWhich to find? (blank line to end):");
                input = Console.ReadLine();
                if (input.Length == 0)
                {
                    break;
                }

                var searchItem = int.Parse(input);
                var foundPos = LinearSearch(data, searchItem);
                if (foundPos < 0)
                {
                    Console.WriteLine("Item {0} not found", searchItem);
                }
                else
                {
                    Console.WriteLine("Item {0} found at position {1}", searchItem, foundPos);
                }
            }
        }

        private static int LinearSearch(int[] data, int searchItem)
        {
            for (var i = 0; i < data.Length; i++)
            {
                if (data[i] == searchItem)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
