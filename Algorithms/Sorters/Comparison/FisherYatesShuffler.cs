using System;

namespace Algorithms.Sorters.Comparison
{
    /// <summary>
    ///     Fisher-Yates shuffle is a simple shuffling algorithm,
    ///     which is usually used to shuffle a deck of cards.
    /// </summary>
    /// <typeparam name="T">Type array input.</typeparam>
    public class FisherYatesShuffler<T> : IShuffler<T>
    {
        /// <summary>
        ///     Shuffles input array using Fisher-Yates algorithm.
        ///     The algorithm starts shuffling from the last element
        ///     and swap elements one by one. We use random index to
        ///     choose element we use in swap operation.
        /// </summary>
        /// <param name="array">Array to shuffle.</param>
        public void Shuffle(T[] array)
        {
            var random = new Random();

            for (var i = array.Length - 1; i > 0; i--)
            {
                var j = random.Next(0, i + 1);

                (array[i], array[j]) = (array[j], array[i]);
            }
        }
    }
}
