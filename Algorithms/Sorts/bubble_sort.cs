using System;

namespace bubble_sort
{
    class Program
    {
        static void Main(string[] args)
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

            var result = BubbleSort(data);
            Console.Write("\nSorted: ");
            for (var i = 0; i < result.Length; i++)
            {
                Console.Write(result[i] + " ");
            }
            Console.ReadKey();
        }
        public static int[] BubbleSort(int[] intArray)
        {
            for (var i = intArray.Length - 1; i > 0; i--)
            {
                for (var j = 0; j <= i - 1; j++)
                {
                    if (intArray[j] > intArray[j + 1])
                    {
                        var highValue = intArray[j];

                        intArray[j] = intArray[j + 1];
                        intArray[j + 1] = highValue;
                    }
                }
            }
            return intArray;
        }
    }
}
