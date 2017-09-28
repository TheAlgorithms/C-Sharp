using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace heap_sort
{
    class Program
    {
        static void Sort(int[] data)
        {
            int heapSize = data.Length;
            for (int p = (heapSize - 1) / 2; p >= 0; p--)
            {
                MakeHeap(data, heapSize, p);
            }

            for (int i = data.Length - 1; i > 0; i--)
            {
                int temp = data[i];
                data[i] = data[0];
                data[0] = temp;

                heapSize--;
                MakeHeap(data, heapSize, 0);
            }
        }
        static void MakeHeap(int[] input, int heapSize, int index)
        {
            int left = (index + 1) * 2 - 1;
            int right = (index + 1) * 2;
            int largest = 0;

            // finds the index of the largest
            if (left < heapSize && input[left] > input[index])
            {
                largest = left;
            }
            else
            {
                largest = index;
            }
            if (right < heapSize && input[right] > input[largest])
            {
                largest = right;
            }
            if (largest != index)
            {
                // process of reheaping / swapping
                int temp = input[index];
                input[index] = input[largest];
                input[largest] = temp;

                MakeHeap(input, heapSize, largest);
            }
        }
        public static void Main(string[] args)
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

            Sort(data);
            Console.Write("\nSorted: ");
            for (int i = 0; i < data.Length; i++)
            {
                Console.Write(data[i] + " ");
            }
            Console.ReadLine();
        }
    }
}
