using System.Collections.Generic;

namespace Algorithms.Sorters
{
    public class BubbleSorter<T> : ISorter<T>
    {       
        public void Sort(T[] array, IComparer<T> comparer)
        {
            for (var i = 0; i < array.Length - 1; i++)
            {
                var wasChanged = false;
                for (var j = 0; j < array.Length - i - 1; j++)
                {
                    if (comparer.Compare(array[j], array[j + 1]) == 1)
                    {
                        var temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        wasChanged = true;
                    }
                }
                if (!wasChanged)
                {
                    break;
                }
            }
        }
    }
}
