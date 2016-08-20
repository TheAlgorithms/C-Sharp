using System;

namespace shell_sort
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

            int[] result = ShellSort(data);
            Console.Write("\nSorted: ");
            for (int i = result.Length - 1; i >= 0; i--)
            {
                Console.Write(result[i] + " ");
            }
            Console.ReadKey();
        }
        public static int[] ShellSort(int[] array)
        {
            int gap = array.Length / 2;
            while (gap > 0)
            {
                for (int i = 0; i < array.Length - gap; i++)
                {
                    int j = i + gap;
                    int tmp = array[j];
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
