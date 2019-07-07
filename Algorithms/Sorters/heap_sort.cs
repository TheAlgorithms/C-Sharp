using System;

namespace heap_sort
{
    internal class Program
    {
        private static void Sort(int[] data)
        {
            var heapSize = data.Length;
            for (var p = (heapSize - 1) / 2; p >= 0; p--)
            {
                MakeHeap(data, heapSize, p);
            }

            for (var i = data.Length - 1; i > 0; i--)
            {
                var temp = data[i];
                data[i] = data[0];
                data[0] = temp;

                heapSize--;
                MakeHeap(data, heapSize, 0);
            }
        }

        private static void MakeHeap(int[] input, int heapSize, int index)
        {
            var left = (index + 1) * 2 - 1;
            var right = (index + 1) * 2;
            var largest = left < heapSize && input[left] > input[index] ? left : index;

            // finds the index of the largest
            if (right < heapSize && input[right] > input[largest])
            {
                largest = right;
            }

            if (largest != index)
            {
                // process of reheaping / swapping
                var temp = input[index];
                input[index] = input[largest];
                input[largest] = temp;

                MakeHeap(input, heapSize, largest);
            }
        }

        public static void Main()
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

            Sort(data);
            Console.Write("\nSorted: ");
            for (var i = 0; i < data.Length; i++)
            {
                Console.Write(data[i] + " ");
            }

            _ = Console.ReadLine();
        }
    }
}
