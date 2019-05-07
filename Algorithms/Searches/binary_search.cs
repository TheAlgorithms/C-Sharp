using System;

namespace Algorithms.Searches
{
    class Program
    {
        static void Main(string[] args)
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
                var foundPos = BSI(data, searchItem);
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

        public static int BSI(int[] data, int item)
        {
            var min = 0;
            var N = data.Length;
            var max = N - 1;
            do
            {
                var mid = min + (max - min) / 2;
                if (item > data[mid])
                {
                    min = mid + 1;
                }
                else
                {
                    max = mid - 1;
                }

                if (data[mid] == item)
                {
                    return mid;
                }
            } while (min <= max);
            return -1;
        }
    }
}
