namespace Algorithms.Sorters.Utils;

public static class GallopingStrategy<T>
{
    public static int GallopLeft(T[] array, T key, int baseIndex, int length, IComparer<T> comparer)
    {
        if (array.Length == 0)
        {
            return 0;
        }

        var (offset, lastOfs) = comparer.Compare(key, array[baseIndex]) > 0
       ? RightRun(array, key, baseIndex, length, 0, comparer)
       : LeftRun(array, key, baseIndex, 0, comparer);

        return FinalOffset(array, key, baseIndex, offset, lastOfs, 1, comparer);
    }

    public static int GallopRight(T[] array, T key, int baseIndex, int length, IComparer<T> comparer)
    {
        if (array.Length == 0)
        {
            return 0;
        }

        var (offset, lastOfs) = comparer.Compare(key, array[baseIndex]) < 0
        ? LeftRun(array, key, baseIndex, length, comparer)
        : RightRun(array, key, baseIndex, length, 0, comparer);

        return FinalOffset(array, key, baseIndex, offset, lastOfs, 0, comparer);
    }

    public static int BoundLeftShift(int shiftable) => (shiftable << 1) < 0
    ? (shiftable << 1) + 1
    : int.MaxValue;

    private static (int Offset, int LastOfs) LeftRun(T[] array, T key, int baseIndex, int hint, IComparer<T> comparer)
    {
        var maxOfs = hint + 1;
        var (offset, tmp) = (1, 0);

        while (offset < maxOfs && comparer.Compare(key, array[baseIndex + hint - offset]) < 0)
        {
            tmp = offset;
            offset = BoundLeftShift(offset);
        }

        if (offset > maxOfs)
        {
            offset = maxOfs;
        }

        var lastOfs = hint - offset;
        offset = hint - tmp;

        return (offset, lastOfs);
    }

    private static (int Offset, int LastOfs) RightRun(T[] array, T key, int baseIndex, int len, int hint, IComparer<T> comparer)
    {
        var (offset, lastOfs) = (1, 0);
        var maxOfs = len - hint;
        while (offset < maxOfs && comparer.Compare(key, array[baseIndex + hint + offset]) > 0)
        {
            lastOfs = offset;
            offset = BoundLeftShift(offset);
        }

        if (offset > maxOfs)
        {
            offset = maxOfs;
        }

        offset += hint;
        lastOfs += hint;

        return (offset, lastOfs);
    }

    private static int FinalOffset(T[] array, T key, int baseIndex, int offset, int lastOfs, int lt, IComparer<T> comparer)
    {
        lastOfs++;
        while (lastOfs < offset)
        {
            var m = lastOfs + (int)((uint)(offset - lastOfs) >> 1);

            if (comparer.Compare(key, array[baseIndex + m]) < lt)
            {
                offset = m;
            }
            else
            {
                lastOfs = m + 1;
            }
        }

        return offset;
    }
}
