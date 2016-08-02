using System;

namespace Binary-Search
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
                int foundPos = BSI(data, searchItem);
                if (foundPos < 0)
                    Console.WriteLine("Item {0} not found", searchItem);
                else
                    Console.WriteLine("Item {0} found at position {1}", searchItem, foundPos);
            }
        }

        public static int BSI(int[] data, int item)
        {
            int min = 0;
            int N = data.Length;
            int max = N - 1;
            do
            {
                int mid = (min + max) / 2;
                if (item > data[mid])
                    min = mid + 1;
                else
                    max = mid - 1;
                if (data[mid] == item)
                    return mid;
            } while (min <= max);
            return -1;
        }
    }
}
