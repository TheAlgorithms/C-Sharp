using System;

namespace shell_sort
{
    class Program
    {
        static void Main()
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

            var result = ShellSort(data);
            Console.Write("\nSorted: ");
            for (var i = result.Length - 1; i >= 0; i--)
            {
                Console.Write(result[i] + " ");
            }
            Console.ReadKey();
        }
        public static int[] ShellSort(int[] array)
        {
            var gap = array.Length / 2;
            while (gap > 0)
            {
                for (var i = 0; i < array.Length - gap; i++)
                {
                    var j = i + gap;
                    var tmp = array[j];
                    while (j >= gap && tmp > array[j - gap])
                    {
                        array[j] = array[j - gap];
                        j -= gap;
                    }
                    array[j] = tmp;
                }
                if (gap == 2)
                {
                    gap = 1;
                }
                else
                {
                    gap = (int)(gap / 2.2);
                }
            }
            return array;
        }
    }
}
