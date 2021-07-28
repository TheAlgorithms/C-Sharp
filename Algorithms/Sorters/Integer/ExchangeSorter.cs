using System;
using System.Linq;

namespace Algorithms.Sorters.Integer
{
    /// <summary>
    /// Exchange sort is an algorithm for sorting the integer.
    /// Exchange sort has some similiarity to the bubble sort but it has the differences.
    /// Exchange sort will compare the first element with each of the following element of array.
    /// The neccessasry swaps will be done when the first element greater than second elements.
    /// Example:
    /// 1. An array of [1,3,2,4,5] and compare each of the elements in array.
    /// 2. Since 3 is greater than 2
    /// 3. Swap both position
    /// 4. Returns [1,2,3,4,5]
    /// </summary>
    public class ExchangeSorter : IIntegerSorter
    {
        /// <summary>
        /// Sorts the array in assending order.
        /// </summary>
        /// <param name="array">Array to sort.</param>
        public void Sort(int[] array)
        {
            if(array.Length <= 1)
            {
                return;
            }
            for(int i = 0; i < array.Length; i++)
            {
                for(int j = i+1; j < array.Length; j++)
                {
                    if(array[i]>array[j])
                    {
                        var temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }             
        }
    }
}