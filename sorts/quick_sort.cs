using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class QuickSortProgram
    {
        static void Main(string[] args)
        {
            var unsorted = new List<int> { 9, 8, 7, 6 };
            Console.WriteLine($"Unsorted: {string.Join(" ", unsorted.ToArray())}");
            var result = new List<int>(QuickSort(unsorted));
            Console.WriteLine($"Sorted: {string.Join(" ", result.ToArray())}");

            Console.ReadLine();
        }

        public static List<int> QuickSort(List<int> a)
        {
            var r = new Random();
            var less = new List<int>();
            var greater = new List<int>();
            if (a.Count <= 1)
                return a;
            var pos = r.Next(a.Count);

            var pivot = a[pos];
            a.RemoveAt(pos);
            foreach (var x in a)
            {
                if (x <= pivot)
                {
                    less.Add(x);
                }
                else
                {
                    greater.Add(x);
                }
            }
            return Concat(QuickSort(less), pivot, QuickSort(greater));
        }

        public static List<int> Concat(List<int> less, int pivot, List<int> greater)
        {
            var sorted = new List<int>(less) {pivot};
            sorted.AddRange(greater);
            return sorted;
        }
    }
}
