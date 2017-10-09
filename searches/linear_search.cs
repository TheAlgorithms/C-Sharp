using System;

namespace linear_search
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter some integers, sorted & separated by spaces:");
            string input = Console.ReadLine();
            string[] integers = input.Split(' ');
            int[] data = new int[integers.Length];
            for (int i = 0; i < data.Length; i++)
                data[i] = int.Parse(integers[i]);

            while (true)
            {
                Console.WriteLine("\nWhich to find? (blank line to end):");
                input = Console.ReadLine();
                if (input.Length == 0)
                    break;
                int searchItem = int.Parse(input);
                int foundPos = LinearSearch(data, searchItem);
                if (foundPos < 0)
                    Console.WriteLine("Item {0} not found", searchItem);
                else
                    Console.WriteLine("Item {0} found at position {1}", searchItem, foundPos);
            }
        }

        static int LinearSearch(int[] data, int searchItem)
        {
            for(int i=0; i<data.Length; i++)
            {
                if (data[i] == searchItem)
                    return i;
            }
            return -1;
        }
    }
}
