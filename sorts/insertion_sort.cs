using System;

namespace insertion_sort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter some integers, separated by spaces:");
            string input = Console.ReadLine();
            string[] integers = input.Split(' ');
            int[] data = new int[integers.Length];
            for (int i = 0; i < data.Length; i++)
                data[i] = int.Parse(integers[i]);

            Console.Write("\nUnsorted: ");
            for (int i = 0; i < data.Length; i++)
            {
                Console.Write(data[i] + " ");
            }

            InsertionSort(data);
            Console.Write("\nSorted: ");
            for (int i = 0; i < data.Length; i++)
            {
                Console.Write(data[i] + " ");
            }
            Console.ReadKey();
        }

        public static void InsertionSort(int[] data)
        {
            for (int index = 1; index < data.Length; index++)
            {
                for (int i = index; i > 0 && data[i] < data[i - 1]; i--)
                {
                    Exchange(data, i, i - 1);
                }
            }
        }

        public static void Exchange(int[] data, int a, int b)
        {
            int temporary;

            temporary = data[a];
            data[a] = data[b];
            data[b] = temporary;
        }
    }
}
