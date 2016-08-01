using System;

namespace bubble_sort
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

            int[] result = BubbleSort(data);
            Console.Write("\nSorted: ");
            for (int i = 0; i < result.Length; i++)
            {
                Console.Write(result[i] + " ");
            }
            Console.ReadKey();
        }
        public static int[] BubbleSort(int[] intArray)
        {
            for (int i = intArray.Length - 1; i > 0; i--)
            {
                for (int j = 0; j <= i - 1; j++)
                {
                    if (intArray[j] > intArray[j + 1])
                    {
                        int highValue = intArray[j];

                        intArray[j] = intArray[j + 1];
                        intArray[j + 1] = highValue;
                    }
                }
            }
            return intArray;
        }
    }
}
