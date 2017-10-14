using System;

namespace cycle_sort {
    internal class Program {
        private static void Main(string[] args) {
            Console.WriteLine("Please enter some integers, separated by spaces:");
            var input = Console.ReadLine();
            var integers = input.Split(' ');
            var data = new int[integers.Length];
            for (var i = 0; i < data.Length; i++) {
                data[i] = int.Parse(integers[i]);
            }

            Console.Write("\nUnsorted: ");
            for (var i = 0; i < data.Length; i++) {
                Console.Write(data[i] + " ");
            }

            CycleSort(data);
            Console.Write("\nSorted: ");
            for (var i = 0; i < data.Length; i++) {
                Console.Write(data[i] + " ");
            }
            Console.ReadKey();
        }

        public static void CycleSort(int[] data) {
            for (var cycleStart = 0; cycleStart <= data.Length - 2; cycleStart++) {
                var item = data[cycleStart];
                var pos = cycleStart;
                for (var i = cycleStart + 1; i <= data.Length - 1; i++) {
                    if (data[i] < item) {
                        pos++;
                    }
                }
                if (pos == cycleStart) {
                    continue;
                }
                while (data[pos] == item) {
                    pos++;
                }
                var temp = data[pos];
                data[pos] = item;
                item = temp;
                while (pos != cycleStart) {
                    pos = cycleStart;
                    for (var i = cycleStart + 1; i <= data.Length - 1; i++) {
                        if (data[i] < item) {
                            pos++;
                        }
                    }
                    while (data[pos] == item) {
                        pos++;
                    }
                    temp = data[pos];
                    data[pos] = item;
                    item = temp;
                }
            }
        }
    }
}