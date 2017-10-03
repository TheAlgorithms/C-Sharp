using System;

namespace quick_sort
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

            QuickSort(data, 0, data.Length);
            Console.Write("\nSorted: ");
            for (int i = data.Length - 1; i >= 0; i--)
            {
                Console.Write(data[i] + " ");
            }
            Console.ReadKey();
        }

        private static void QuickSort(int[] array, int startPos, int endPos)
        {
            if (endPos - startPos <= 1)
                return;
            
            int piv = startPos;
            for (int i = piv + 1; i < endPos; i++)
            {
                if (array[piv] > array[i])
                {
                    SwitchValues(array, piv, piv + 1);
                    if (piv + 1 != i)
                        SwitchValues(array, piv, i);
                }
            }

            QuickSort(array, startPos, piv);
            QuickSort(array, piv + 1, endPos);
        }

        private static void SwitchValues(int[] array, int pos1, int pos2)
        {
            int temp = array[pos1];
            array[pos1] = array[pos2];
            array[pos2] = temp;
        }
    }
}