using System.Collections.Generic;

namespace Algorithms.Sorters
{
    /// <summary>
    /// Cycle sort is an in-place, unstable sorting algorithm,
    /// a comparison sort that is theoretically optimal in terms of the total
    /// number of writes to the original array.
    /// It is based on the idea that the permutation to be sorted can be factored
    /// into cycles, which can individually be rotated to give a sorted result.
    /// </summary>
    public class CycleSorter : ISorter<int>
    {
        /// <summary>
        /// Sorts input array using Cycle sort.
        /// </summary>
        /// <param name="array">Input array.</param>
        /// <param name="comparer">Integer comparer.</param>
        public void Sort(int[] array, IComparer<int> comparer) => CycleSort(array);

        private static void CycleSort(IList<int> data)
        {
            for (var cycleStart = 0; cycleStart <= data.Count - 2; cycleStart++)
            {
                var item = data[cycleStart];
                var pos = cycleStart;

                for (var i = cycleStart + 1; i <= data.Count - 1; i++)
                {
                    if (data[i] < item)
                    {
                        pos++;
                    }
                }

                if (pos == cycleStart)
                {
                    continue;
                }

                while (data[pos] == item)
                {
                    pos++;
                }

                var temp = data[pos];
                data[pos] = item;
                item = temp;

                while (pos != cycleStart)
                {
                    pos = cycleStart;
                    for (var i = cycleStart + 1; i <= data.Count - 1; i++)
                    {
                        if (data[i] < item)
                        {
                            pos++;
                        }
                    }

                    while (data[pos] == item)
                    {
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
