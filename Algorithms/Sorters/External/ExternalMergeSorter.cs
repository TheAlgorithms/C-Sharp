using System;
using System.Collections.Generic;

namespace Algorithms.Sorters.External
{
    public class ExternalMergeSorter<T> : IExternalSorter<T>
    {
        public void Sort(
            ISequentialStorage<T> mainMemory,
            ISequentialStorage<T> temporaryMemory,
            IComparer<T> comparer)
        {
            var originalSource = mainMemory;
            var source = mainMemory;
            var temp = temporaryMemory;
            var totalLength = mainMemory.Length;
            for (var stripLength = 1L; stripLength < totalLength; stripLength *= 2)
            {
                using var left = source.GetReader();
                using var right = source.GetReader();
                using var output = temp.GetWriter();

                for (var i = 0L; i < stripLength; i++)
                {
                    right.Read();
                }

                Merge(left, right, output, stripLength, Math.Min(stripLength, totalLength - stripLength), comparer);
                var step = 2 * stripLength;
                long rightStripStart;
                for (rightStripStart = stripLength + step; rightStripStart < mainMemory.Length; rightStripStart += step)
                {
                    for (var i = 0L; i < stripLength; i++)
                    {
                        left.Read();
                        right.Read();
                    }

                    Merge(left, right, output, stripLength, Math.Min(stripLength, totalLength - rightStripStart), comparer);
                }

                for (var i = 0L; i < totalLength + stripLength - rightStripStart; i++)
                {
                    output.Write(right.Read());
                }

                (source, temp) = (temp, source);
            }

            if (source == originalSource)
            {
                return;
            }

            using var sorted = source.GetReader();
            using var dest = originalSource.GetWriter();
            for (var i = 0; i < totalLength; i++)
            {
                dest.Write(sorted.Read());
            }
        }

        private static void Merge(
            ISequentialStorageReader<T> left,
            ISequentialStorageReader<T> right,
            ISequentialStorageWriter<T> output,
            long leftLength,
            long rightLength,
            IComparer<T> comparer)
        {
            var leftIndex = 0L;
            var rightIndex = 0L;

            var l = left.Read();
            var r = right.Read();
            while (true)
            {
                if (comparer.Compare(l, r) < 0)
                {
                    output.Write(l);
                    leftIndex++;
                    if (leftIndex == leftLength)
                    {
                        break;
                    }

                    l = left.Read();
                }
                else
                {
                    output.Write(r);
                    rightIndex++;
                    if (rightIndex == rightLength)
                    {
                        break;
                    }

                    r = right.Read();
                }
            }

            if (leftIndex < leftLength)
            {
                output.Write(l);
                Copy(left, output, leftLength - leftIndex - 1);
            }

            if (rightIndex < rightLength)
            {
                output.Write(r);
                Copy(right, output, rightLength - rightIndex - 1);
            }
        }

        private static void Copy(ISequentialStorageReader<T> from, ISequentialStorageWriter<T> to, long count)
        {
            for (var i = 0; i < count; i++)
            {
                to.Write(from.Read());
            }
        }
    }
}
