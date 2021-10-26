using System;
using System.Collections.Generic;

namespace Algorithms.Sorters.Comparison
{
    /// <summary>
    ///     Timsort is a hybrid stable sorting algorithm, derived from merge sort and insertion sort, designed to perform well on many kinds of real-world data.
    ///     It was originally implemented by Tim Peters in 2002 for use in the Python programming language.
    ///
    ///     This class is ported from a Java interpretation of Tim Peter's original work.
    ///     Java class is viewable here:
    ///     http://cr.openjdk.java.net/~martin/webrevs/openjdk7/timsort/raw_files/new/src/share/classes/java/util/TimSort.java
    ///
    ///     Tim Peters's list sort for Python, is described in detail here:
    ///     http://svn.python.org/projects/python/trunk/Objects/listsort.txt
    ///
    ///     Tim's C code may be found here: http://svn.python.org/projects/python/trunk/Objects/listobject.c
    ///
    ///     The underlying techniques are described in this paper (and may have even earlier origins):
    ///     "Optimistic Sorting and Information Theoretic Complexity"
    ///     Peter McIlroy
    ///     SODA (Fourth Annual ACM-SIAM Symposium on Discrete Algorithms),
    ///     pp 467-474, Austin, Texas, 25-27 January 1993.
    /// </summary>
    /// <typeparam name="T">Type of array element.</typeparam>
    public class TimSorter<T> : IComparisonSorter<T>
    {
        private readonly int minMerge;
        private readonly int initMinGallop;
        private readonly int[] runBase;
        private readonly int[] runLengths;

        private int minGallop;
        private int stackSize;

        public TimSorter(int minMerge = 32, int minGallop = 7)
        {
            initMinGallop = minGallop;
            this.minMerge = minMerge;
            runBase = new int[85];
            runLengths = new int[85];

            stackSize = 0;
            this.minGallop = minGallop;
        }

        /// <summary>
        ///     Sorts array using specified comparer
        ///     worst case performance: O(n log(n)),
        ///     best case performance:  O(n),
        ///     See <a href="https://en.wikipedia.org/wiki/Timsort">here</a> for more info.
        /// </summary>
        /// <param name="array">Array to sort.</param>
        /// <param name="comparer">Compares elements.</param>
        public void Sort(T[] array, IComparer<T> comparer)
        {
            var start = 0;
            var remaining = array.Length;

            if (remaining < minMerge)
            {
                if (remaining < 2)
                {
                    return;
                }

                // Don't need to merge, just binary sort
                BinarySort(array, comparer, start, remaining, start);
                return;
            }

            var minRun = MinRunLength(remaining, minMerge);

            do
            {
                // Identify next run
                var runLen = CountRunAndMakeAscending(array, comparer, start);

                // If the run is too short extend to Min(MIN_RUN, remaining)
                if (runLen < minRun)
                {
                    var force = Math.Min(minRun, remaining);
                    BinarySort(array, comparer, start, start + force, start + runLen);
                    runLen = force;
                }

                runBase[stackSize] = start;
                runLengths[stackSize] = runLen;
                stackSize++;

                MergeCollapse(array, comparer);

                start += runLen;
                remaining -= runLen;
            }
            while (remaining != 0);

            MergeForceCollapse(array, comparer);
        }

        /// <summary>
        /// Sorts the specified portion of the specified array using a binary
        /// insertion sort. It requires O(n log n) compares, but O(n^2) data movement.
        /// </summary>
        /// <param name="array">Array to sort.</param>
        /// <param name="comparer">Compares elements.</param>
        /// <param name="start">The index of the first element in the range to be sorted.</param>
        /// <param name="end">The index after the last element in the range to be sorted.</param>
        /// <param name="first">The index of the first element in the range that is not already known to be sorted, must be between start and end.</param>
        private static void BinarySort(T[] array, IComparer<T> comparer, int start, int end, int first)
        {
            if (first >= end || first <= start)
            {
                first = start + 1;
            }

            for (; first < end; first++)
            {
                var target = array[first];
                var targetInsertLocation = BinarySearch(array, start, first - 1, target, comparer);
                Array.Copy(array, targetInsertLocation, array, targetInsertLocation + 1, first - targetInsertLocation);

                array[targetInsertLocation] = target;
            }
        }

        private static int BinarySearch(T[] array, int left, int right, T target, IComparer<T> comparer)
        {
            while (left < right)
            {
                var mid = (left + right) >> 1;
                if (comparer.Compare(target, array[mid]) < 0)
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }

            return comparer.Compare(target, array[left]) < 0 ? left : left + 1;
        }

        /// <summary>
        /// Returns the minimum acceptable run length for an array of the specified
        /// length.Natural runs shorter than this will be extended.
        ///
        /// Computation is:
        ///   If total less than minRun, return n (it's too small to bother with fancy stuff).
        ///   Else if total is an exact power of 2, return minRun/2.
        ///   Else return an int k, where <![CDATA[minRun/2 <= k <= minRun]]>, such that total/k
        ///     is close to, but strictly less than, an exact power of 2.
        /// </summary>
        /// <param name="total">Total length remaining to sort.</param>
        /// <returns>Minimum run length to be merged.</returns>
        private static int MinRunLength(int total, int minRun)
        {
            var r = 0;
            while (total >= minRun)
            {
                r |= total & 1;
                total >>= 1;
            }

            return total + r;
        }

        /// <summary>
        /// Returns the length of the run beginning at the specified position in
        /// the specified array and reverses the run if it is descending (ensuring
        /// that the run will always be ascending when the method returns).
        ///
        /// A run is the longest ascending sequence with:
        ///
        ///    <![CDATA[a[lo] <= a[lo + 1] <= a[lo + 2] <= ...]]>
        ///
        /// or the longest descending sequence with:
        ///
        ///    <![CDATA[a[lo] >  a[lo + 1] >  a[lo + 2] >  ...]]>
        ///
        /// For its intended use in a stable mergesort, the strictness of the
        /// definition of "descending" is needed so that the call can safely
        /// reverse a descending sequence without violating stability.
        /// </summary>
        /// <param name="array">the array in which a run is to be counted and possibly reversed.</param>
        /// <param name="comparer">the comparator to used for the sort.</param>
        /// <param name="start">index of the first element in the run.</param>
        /// <returns>the length of the run beginning at the specified position in the specified array.</returns>
        private static int CountRunAndMakeAscending(T[] array, IComparer<T> comparer, int start)
        {
            var runHi = start + 1;
            if (runHi == array.Length)
            {
                return 1;
            }

            // Find end of run, and reverse range if descending
            if (comparer.Compare(array[runHi++], array[start]) < 0)
            { // Descending
                while (runHi < array.Length && comparer.Compare(array[runHi], array[runHi - 1]) < 0)
                {
                    runHi++;
                }

                ReverseRange(array, start, runHi);
            }
            else
            { // Ascending
                while (runHi < array.Length && comparer.Compare(array[runHi], array[runHi - 1]) >= 0)
                {
                    runHi++;
                }
            }

            return runHi - start;
        }

        /// <summary>
        /// Reverse the specified range of the specified array.
        /// </summary>
        /// <param name="array">the array in which a range is to be reversed.</param>
        /// <param name="start">the index of the first element in the range to be reversed.</param>
        /// <param name="end">the index after the last element in the range to be reversed.</param>
        private static void ReverseRange(T[] array, int start, int end)
        {
            end--;
            while (start < end)
            {
                var t = array[start];
                array[start++] = array[end];
                array[end--] = t;
            }
        }

        private static int GallopLeft(T[] array, IComparer<T> comparer, T key, int i, int len, int hint)
        {
            var offset = 1;
            var lastOfs = 0;

            if (comparer.Compare(key, array[i + hint]) > 0)
            {
                (offset, lastOfs) = RightRun(array, comparer, key, i, len, hint, offset, lastOfs, 0);
            }
            else
            {
                (offset, lastOfs) = LeftRun(array, comparer, key, i, hint, offset, lastOfs, 1);
            }

            return FinalOffset(array, comparer, key, i, offset, lastOfs, 1);
        }

        private static int GallopRight(T[] array, IComparer<T> comparer, T key, int i, int len, int hint)
        {
            var offset = 1;
            var lastOfs = 0;

            if (comparer.Compare(key, array[i + hint]) < 0)
            {
                (offset, lastOfs) = LeftRun(array, comparer, key, i, hint, offset, lastOfs, 0);
            }
            else
            {
                (offset, lastOfs) = RightRun(array, comparer, key, i, len, hint, offset, lastOfs, -1);
            }

            return FinalOffset(array, comparer, key, i, offset, lastOfs, 0);
        }

        private static (int offset, int lastOfs) LeftRun(T[] array, IComparer<T> comparer, T key, int i, int hint, int offset, int lastOfs, int lt)
        {
            var maxOfs = hint + 1;
            while (offset < maxOfs && comparer.Compare(key, array[i + hint - offset]) < lt)
            {
                lastOfs = offset;
                offset = LeftShiftOffset(offset);
                if (offset <= 0)
                {
                    offset = maxOfs;
                }
            }

            if (offset > maxOfs)
            {
                offset = maxOfs;
            }

            var tmp = lastOfs;
            lastOfs = hint - offset;
            offset = hint - tmp;

            return (offset, lastOfs);
        }

        private static (int offset, int lastOfs) RightRun(T[] array, IComparer<T> comparer, T key, int i, int len, int hint, int offset, int lastOfs, int gt)
        {
            var maxOfs = len - hint;
            while (offset < maxOfs && comparer.Compare(key, array[i + hint + offset]) > gt)
            {
                lastOfs = offset;
                offset = LeftShiftOffset(offset);
                if (offset <= 0)
                {
                    offset = maxOfs;
                }
            }

            if (offset > maxOfs)
            {
                offset = maxOfs;
            }

            offset += hint;
            lastOfs += hint;

            return (offset, lastOfs);
        }

        private static int LeftShiftOffset(int offset)
        {
            return (int)((uint)offset << 1) + 1;
        }

        private static int FinalOffset(T[] array, IComparer<T> comparer, T key, int i, int offset, int lastOfs, int lt)
        {
            lastOfs++;
            while (lastOfs < offset)
            {
                var m = lastOfs + (int)((uint)(offset - lastOfs) >> 1);

                if (comparer.Compare(key, array[i + m]) < lt)
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

        private static T[] EnsureCapacity(T[] array, int min)
        {
            // Compute smallest power of 2 > minCapacity
            var newSize = min;
            newSize |= newSize >> 1;
            newSize |= newSize >> 2;
            newSize |= newSize >> 4;
            newSize |= newSize >> 8;
            newSize |= newSize >> 16;
            newSize++;

            if (newSize < 0)
            {
                newSize = min;
            }
            else
            {
                newSize = Math.Min(newSize, array.Length >> 1);
            }

            return new T[newSize];
        }

        private void MergeCollapse(T[] array, IComparer<T> comparer)
        {
            while (stackSize > 1)
            {
                var n = stackSize - 2;
                if (n > 0 && runLengths[n - 1] <= runLengths[n] + runLengths[n + 1])
                {
                    if (runLengths[n - 1] < runLengths[n + 1])
                    {
                        n--;
                    }

                    MergeAt(array, comparer, n);
                }
                else if (runLengths[n] <= runLengths[n + 1])
                {
                    MergeAt(array, comparer, n);
                }
                else
                {
                    break;
                }
            }
        }

        private void MergeForceCollapse(T[] array, IComparer<T> comparer)
        {
            while (stackSize > 1)
            {
                var n = stackSize - 2;
                if (n > 0 && runLengths[n - 1] < runLengths[n + 1])
                {
                    n--;
                }

                MergeAt(array, comparer, n);
            }
        }

        private void MergeAt(T[] array, IComparer<T> comparer, int index)
        {
            var baseA = runBase[index];
            var lenA = runLengths[index];
            var baseB = runBase[index + 1];
            var lenB = runLengths[index + 1];

            runLengths[index] = lenA + lenB;

            if (index == stackSize - 3)
            {
                runBase[index + 1] = runBase[index + 2];
                runLengths[index + 1] = runLengths[index + 2];
            }

            stackSize--;

            var k = TimSorter<T>.GallopRight(array, comparer, array[baseB], baseA, lenA, 0);

            baseA += k;
            lenA -= k;

            if (lenA <= 0)
            {
                return;
            }

            lenB = TimSorter<T>.GallopLeft(array, comparer, array[baseA + lenA - 1], baseB, lenB, lenB - 1);

            if (lenB <= 0)
            {
                return;
            }

            if (lenA <= lenB)
            {
                MergeLo(array, comparer, baseA, lenA, baseB, lenB);
            }
            else
            {
                MergeHi(array, comparer, baseA, lenA, baseB, lenB);
            }
        }

        private void MergeLo(T[] array, IComparer<T> comparer, int baseA, int lenA, int baseB, int lenB)
        {
            // Copy first run into temp array
            var tmp = TimSorter<T>.EnsureCapacity(array, lenA);
            Array.Copy(array, baseA, tmp, 0, lenA);

            var cursorT = 0;     // Indexes into tmp array.
            var cursorA = baseB; // Indexes int array.
            var dest = baseA;    // Indexes int array.

            // Move first element of second run and deal with degenerate cases.
            array[dest++] = array[cursorA++];
            if (--lenB == 0)
            {
                Array.Copy(tmp, cursorT, array, dest, lenA);
                return;
            }

            if (lenA == 1)
            {
                Array.Copy(array, cursorA, array, dest, lenB);
                array[dest + lenB] = tmp[cursorT];
                return;
            }

            var minGallop = this.minGallop;

            while (true)
            {
                var count1 = 0; // Number of times in a row that first run wins.
                var count2 = 0; // Number of times in a row that second run wins.

                // Do the straightforward thing until (if ever) one run starts winning consistently.
                do
                {
                    if (comparer.Compare(array[cursorA], tmp[cursorT]) < 0)
                    {
                        array[dest++] = array[cursorA++];
                        count2++;
                        count1 = 0;
                        if (--lenB == 0)
                        {
                            goto end_of_loop;
                        }
                    }
                    else
                    {
                        array[dest++] = tmp[cursorT++];
                        count1++;
                        count2 = 0;
                        if (--lenA == 1)
                        {
                            goto end_of_loop;
                        }
                    }
                }
                while ((count1 | count2) < minGallop);

                // One run is winning so consistently that galloping may be a huge win.
                // So try that, and continue galloping until (if ever) neither run appears to be winning consistently anymore.
                do
                {
                    count1 = GallopRight(tmp, comparer, array[cursorA], cursorT, lenA, 0);
                    if (count1 != 0)
                    {
                        Array.Copy(tmp, cursorT, array, dest, count1);
                        dest += count1;
                        cursorT += count1;
                        lenA -= count1;
                        if (lenA <= 1)
                        {
                            goto end_of_loop;
                        }
                    }

                    array[dest++] = array[cursorA++];
                    if (--lenB == 0)
                    {
                        goto end_of_loop;
                    }

                    count2 = GallopLeft(array, comparer, tmp[cursorT], cursorA, lenB, 0);
                    if (count2 != 0)
                    {
                        Array.Copy(array, cursorA, array, dest, count2);
                        dest += count2;
                        cursorA += count2;
                        lenB -= count2;
                        if (lenB == 0)
                        {
                            goto end_of_loop;
                        }
                    }

                    array[dest++] = tmp[cursorT++];
                    if (--lenA == 1)
                    {
                        goto end_of_loop;
                    }

                    minGallop--;
                }
                while (count1 >= initMinGallop | count2 >= initMinGallop);

                if (minGallop < 0)
                {
                    minGallop = 0;
                }

                // Penalize for leaving gallop mode.
                minGallop += 2;
            }

        end_of_loop:

            this.minGallop = minGallop >= 1
                ? minGallop
                : 1;

            if (lenA == 1)
            {
                Array.Copy(array, cursorA, array, dest, lenB);
                array[dest + lenB] = tmp[cursorT];
            }
            else if (lenA == 0)
            {
                throw new ArgumentException("Comparison method violates its general contract!");
            }
            else
            {
                Array.Copy(tmp, cursorT, array, dest, lenA);
            }
        }

        private void MergeHi(T[] array, IComparer<T> comparer, int baseA, int lenA, int baseB, int lenB)
        {
            // Copy second run into temp array
            var tmp = TimSorter<T>.EnsureCapacity(array, lenB);
            Array.Copy(array, baseB, tmp, 0, lenB);

            var cursorA = baseA + lenA - 1; // Indexes into array.
            var cursorT = lenB - 1;         // Indexes into tmp array.
            var dest = baseB + lenB - 1;    // Indexes into array.

            // Move last element of first run and deal with degenerate cases.
            array[dest--] = array[cursorA--];
            if (--lenA == 0)
            {
                Array.Copy(tmp, 0, array, dest - (lenB - 1), lenB);
                return;
            }

            if (lenB == 1)
            {
                dest -= lenA;
                cursorA -= lenA;
                Array.Copy(array, cursorA + 1, array, dest + 1, lenA);
                array[dest] = tmp[cursorT];
                return;
            }

            var minGallop = this.minGallop;

            while (true)
            {
                var count1 = 0; // Number of times in a row that first run wins.
                var count2 = 0; // Number of times in a row that second run wins.

                // Do the straightforward thing until (if ever) one run appears to win consistently.
                do
                {
                    if (comparer.Compare(tmp[cursorT], array[cursorA]) < 0)
                    {
                        array[dest--] = array[cursorA--];
                        count1++;
                        count2 = 0;
                        if (--lenA == 0)
                        {
                            goto end_of_loop;
                        }
                    }
                    else
                    {
                        array[dest--] = tmp[cursorT--];
                        count2++;
                        count1 = 0;
                        if (--lenB == 1)
                        {
                            goto end_of_loop;
                        }
                    }
                }
                while ((count1 | count2) < minGallop);

                // One run is winning so consistently that galloping may be a huge win.
                // So try that, and continue galloping until (if ever) neither run appears to be winning consistently anymore.
                do
                {
                    count1 = lenA - GallopRight(array, comparer, tmp[cursorT], baseA, lenA, lenA - 1);
                    if (count1 != 0)
                    {
                        dest -= count1;
                        cursorA -= count1;
                        lenA -= count1;
                        Array.Copy(array, cursorA + 1, array, dest + 1, count1);
                        if (lenA == 0)
                        {
                            goto end_of_loop;
                        }
                    }

                    array[dest--] = tmp[cursorT--];
                    if (--lenB == 1)
                    {
                        goto end_of_loop;
                    }

                    count2 = lenB - GallopLeft(tmp, comparer, array[cursorA], 0, lenB, lenB - 1);
                    if (count2 != 0)
                    {
                        dest -= count2;
                        cursorT -= count2;
                        lenB -= count2;
                        Array.Copy(tmp, cursorT + 1, array, dest + 1, count2);
                        if (lenB <= 1)
                        {
                            goto end_of_loop;
                        }
                    }

                    array[dest--] = array[cursorA--];
                    if (--lenA == 0)
                    {
                        goto end_of_loop;
                    }

                    minGallop--;
                }
                while (count1 >= initMinGallop | count2 >= initMinGallop);

                if (minGallop < 0)
                {
                    minGallop = 0;
                }

                // Penalize for leaving gallop mode
                minGallop += 2;
            }

        // End of loop
        end_of_loop:

            this.minGallop = minGallop < 1 ? 1 : minGallop;  // Write back to field

            if (lenB == 1)
            {
                dest -= lenA;
                cursorA -= lenA;
                Array.Copy(array, cursorA + 1, array, dest + 1, lenA);
                array[dest] = tmp[cursorT];  // Move first elt of run2 to front of merge
            }
            else if (lenB == 0)
            {
                throw new ArgumentException(
                    "Comparison method violates its general contract!");
            }
            else
            {
                Array.Copy(tmp, 0, array, dest - (lenB - 1), lenB);
            }
        }
    }
}
