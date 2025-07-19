using System;

namespace Algorithms.Shufflers
{
    /// <summary>
    ///     Recursive Shuffler is a recursive version of
    ///     Fisher-Yates shuffle algorithm. This can only be used
    ///     for educational purposes due to stack depth limits.
    /// </summary>
    /// <typeparam name="T">Type array input.</typeparam>
    public class RecursiveShuffler<T>
    {
        /// <summary>
        /// First, it will check the length of the array on the base case.
        /// Next, if there's still element left, it will shuffle the sub-array.
        /// Lastly, it will randomly select index from 0 to length of array then
        /// swap the elements array[arrayLength] and array[index].
        /// </summary>
        /// <param name="array">Array to shuffle.</param>
        /// <param name="arrayLength">The length of the array. Used for terminator.</param>
        /// <param name="seed">Random generator seed. Used to repeat the shuffle.</param>
        public void Shuffle(T[] array, int arrayLength, int? seed = null)
        {
            if(arrayLength <= 0)
            {
                return;
            }

            Shuffle(array, arrayLength - 1, seed);
            var random = seed is null ? new Random() : new Random(seed.Value);
            int index = random.Next(arrayLength + 1);
            (array[arrayLength], array[index]) = (array[index], array[arrayLength]);
        }
    }
}
