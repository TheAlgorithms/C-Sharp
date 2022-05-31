using Algorithms.Shufflers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Tests.MachineLearning.DataSets
{
    internal static class DataSetUtils<T>
    {
        /// <summary>
        /// Shuffle the dataset. 
        /// </summary>
        /// <param name="seed"></param>
        public static void Randomize(double[][] data, T[] classification, int? seed = null)
        {
            // Get the new order for the data
            int[] order = Enumerable.Range(0, data.Count()).ToArray();
            var shuffler = new FisherYatesShuffler<int>();
            shuffler.Shuffle(order, seed);

            // Set the order
            int[] order2 = (int[])order.Clone();
            Array.Sort(order, data);
            Array.Sort(order2, classification);
        }
    }
}
