using System;

namespace selection_sort
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

            int[] result = SelectionSort(data);
            Console.Write("\nSorted: ");
            for (int i = 0; i < result.Length; i++)
            {
                Console.Write(result[i] + " ");
            }
            Console.ReadKey();
        }
        public static int[] SelectionSort(int[] intArray)
        {
            int[] newArray = new int[intArray.Length];
            bool[] accessed = new bool[intArray.Length];
            for (int i = 0; i < intArray.Length; i++)
            {
                int smallest = 0;
                bool set = false;
                for (int j = 0; j < intArray.Length; j++)
                {
                    if (!accessed[j] && (intArray[j] <= intArray[smallest] || !set))
                    {
                        set = true;

                        smallest = j;
                    }
                }
                accessed[smallest] = true;
                newArray[i] = intArray[smallest];
            }
            return newArray;
        }
    }
}
