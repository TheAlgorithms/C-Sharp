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
    /// <typeparam name="T">Type array input.</typeparam>
    public class CycleSorter<T> : ISorter<T>
    {
        /// <summary>
        /// Sorts input array using Cycle sort.
        /// </summary>
        /// <param name="array">Input array.</param>
        /// <param name="comparer">Integer comparer.</param>
        public void Sort(T[] array, IComparer<T> comparer) => CycleSort(array, comparer);

        private static void CycleSort(IList<T> data, IComparer<T> comparer)
        {
            for (var cycleStart = 0; cycleStart <= data.Count - 2; cycleStart++)
            {
                var item = data[cycleStart];
                var pos = cycleStart;

                for (var i = cycleStart + 1; i <= data.Count - 1; i++)
                {
                    if (comparer.Compare(data[i], item) == -1)
                    {
                        pos++;
                    }
                }

                if (pos == cycleStart)
                {
                    continue;
                }

                while (comparer.Compare(data[pos], item) == 0)
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
                        if (comparer.Compare(data[i], item) == -1)
                        {
                            pos++;
                        }
                    }

                    while (comparer.Compare(data[pos], item) == 0)
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
