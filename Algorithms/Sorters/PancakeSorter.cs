using System.Collections.Generic;

namespace Algorithms.Sorters
{
    public class PancakeSorter<T> : ISorter<T>
    {
        /// <summary>
        /// Sorts array using specified comparer,
        /// internal, in-place, stable,
        /// time complexity: O(n^2),
        /// space complexity: O(1),
        /// where n - array length
        /// </summary>
        /// <param name="array">Array to sort</param>
        /// <param name="comparer">Compares elements</param>
        /// 
        public void Sort(T[] array, IComparer<T> comparer)
        {
            int n = array.Length;

            // Start from the complete array and one by one 
            // reduce current size by one 
            //
            for (var curr_size = n; curr_size > 1; --curr_size)
            {
                // Find index of the maximum element in 
                // array[0..curr_size-1] 
                //
                var mi = FindMax(array, curr_size, comparer);

                // Move the maximum element to end of current array 
                // if it's not already at  the end 
                //
                if (mi != curr_size - 1)
                {
                    // To move to the end, first move maximum 
                    // number to beginning 
                    //
                    Flip(array, mi);

                    // Now move the maximum number to end by 
                    // reversing current array 
                    //
                    Flip(array, curr_size - 1);
                }
            }
        }

        // Reverses array[0..i]
        //
        private void Flip(T[] array, int i)
        {
            T temp;
            int start = 0;
            while (start < i)
            {
                temp = array[start];
                array[start] = array[i];
                array[i] = temp;
                start++;
                i--;
            }
        }

        // Returns index of the maximum element 
        // in array[0..n-1]
        //
        private int FindMax(T[] array, int n, IComparer<T> comparer)
        {
            var mi = 0;
            for (var i = 0; i < n; i++)
            {
                if (comparer.Compare(array[i], array[mi]) == 1)
                {
                    mi = i;
                }
            }
            return mi;
        }
    }
}
