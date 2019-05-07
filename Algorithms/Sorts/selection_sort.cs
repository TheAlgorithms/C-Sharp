using System;

namespace selection_sort
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

            var result = SelectionSort(data);
            Console.Write("\nSorted: ");
            for (var i = 0; i < result.Length; i++)
            {
                Console.Write(result[i] + " ");
            }
            Console.ReadKey();
        }
        public static int[] SelectionSort(int[] intArray)
        {
            var newArray = new int[intArray.Length];
            var accessed = new bool[intArray.Length];
            for (var i = 0; i < intArray.Length; i++)
            {
                var smallest = 0;
                var set = false;
                for (var j = 0; j < intArray.Length; j++)
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
