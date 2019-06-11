using System;

namespace merge_sort
{
    internal class Program
    {
        public static void MainMerge(int[] numbers, int left, int mid, int right)
        {
            var temp = new int[25];
            int i, eol, num, pos;

            eol = mid - 1;
            pos = left;
            num = right - left + 1;

            while ((left <= eol) && (mid <= right))
            {
                if (numbers[left] <= numbers[mid])
                {
                    temp[pos++] = numbers[left++];
                }
                else
                {
                    temp[pos++] = numbers[mid++];
                }
            }

            while (left <= eol)
            {
                temp[pos++] = numbers[left++];
            }

            while (mid <= right)
            {
                temp[pos++] = numbers[mid++];
            }

            for (i = 0; i < num; i++)
            {
                numbers[right] = temp[right];
                right--;
            }
        }

        public static void SortMerge(int[] numbers, int left, int right)
        {
            int mid;

            if (right > left)
            {
                mid = (right + left) / 2;
                SortMerge(numbers, left, mid);
                SortMerge(numbers, mid + 1, right);

                MainMerge(numbers, left, mid + 1, right);
            }
        }

        private static void Main()
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

            SortMerge(data, 0, data.Length - 1);
            Console.Write("\nSorted: ");
            for (var i = 0; i < data.Length; i++)
            {
                Console.Write(data[i] + " ");
            }
            Console.ReadLine();
        }
    }
}
