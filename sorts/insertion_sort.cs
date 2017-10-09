using System;
using System.Collections.Generic;

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

            int[] result = InsertionSort(data);
            Console.Write("\nSorted: ");
            for (int i = 0; i < result.Length; i++)
            {
                Console.Write(result[i] + " ");
            }
            Console.ReadKey();
        }
        public static int[] InsertionSort(int[] inputarray)
        {
            var equalityComparer = Comparer<int>.Default;
            for (var counter = 0; counter < inputarray.Length - 1; counter++)
            {
                var index = counter + 1;
                while (index > 0)
                {
                    if(equalityComparer.Compare(inputarray[index - 1],inputarray[index])>0)
                    {
                        var temp = inputarray[index - 1];
                        inputarray[index - 1] = inputarray[index];
                        inputarray[index] = temp;
                    }
                    index--;
                }
            }
            return inputarray;
        }
    }
}
