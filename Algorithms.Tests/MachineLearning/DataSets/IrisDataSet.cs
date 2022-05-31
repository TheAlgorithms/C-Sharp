using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using Algorithms.Shufflers;

namespace Algorithms.Tests.MachineLearning.DataSets
{
    /// <summary>
    /// Iris dataset for classification.
    /// See https://archive.ics.uci.edu/ml/datasets/iris/.
    /// </summary>
    public class IrisDataSet
    {
        /// <summary> Text for class Iris-setosa. </summary>
        public static readonly string ClassIrisSetosa = "Iris-setosa";

        /// <summary> Text for class Iris-versicolor. </summary>
        public static readonly string ClassIrisVersicolor = "Iris-versicolor";

        /// <summary> Text for class Iris-virginica. </summary>
        public static readonly string ClassIrisVirginica = "Iris-virginica";

        /// <summary> Number of samples in the dataset.</summary>
        public static readonly int IrisNumSamples = 150;

        /// <summary> Download URL. </summary>
        private const string IrisUrl = "https://archive.ics.uci.edu/ml/machine-learning-databases/iris/iris.data";

        /// <summary>
        /// Gets or sets the list of samles.
        ///     0. sepal length in cm.
        ///     1. sepal width in cm.
        ///     2. petal length in cm.
        ///     3. petal width in cm.
        /// </summary>
        public double[][] Data { get; set; }

        /// <summary>
        /// Gets or Sets the list that assign a class to each sample:
        ///     - Iris Setosa.
        ///     - Iris Versicolour.
        ///     - Iris Virginica.
        /// </summary>
        /// <remarks>
        /// This classes are defined with constants for easier comparison:
        ///     <see cref="ClassIrisSetosa"/>.
        ///     <see cref="ClassIrisVersicolor"/>.
        ///     <see cref="ClassIrisVirginica"/>.
        /// </remarks>
        public string[] Class { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IrisDataSet"/> class.
        /// Downloads the data and fill <see cref="Data"/> and <see cref="Class"/> arrays.
        /// </summary>
        /// <exception cref="Exception">Might throw an exception if data could not be downloaded or parsed. </exception>
        public IrisDataSet(string url = IrisUrl)
        {
            try
            {
                Data = new double[IrisNumSamples][];
                Class = new string[IrisNumSamples];

                // Download the data from URL
                string rawText = string.Empty;
                using (var client = new WebClient())
                {
                    // Download the Web resource and save it into a data buffer.
                    byte[] myDataBuffer = client.DownloadData(url);
                    rawText = Encoding.ASCII.GetString(myDataBuffer);
                }

                // Load data as string[]
                string[] rawData = rawText.Split("\n");

                // Load the values
                for (int i = 0; i < IrisNumSamples; i++)
                {
                    var values = rawData[i].Split(",");
                    Data[i] = new[]
                    {
                        double.Parse(values[0], CultureInfo.InvariantCulture),
                        double.Parse(values[1], CultureInfo.InvariantCulture),
                        double.Parse(values[2], CultureInfo.InvariantCulture),
                        double.Parse(values[3], CultureInfo.InvariantCulture),
                    };
                    Class[i] = values[4];
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Dataset could not be loaded from the given URL.");
            }
        }

        /// <summary>
        /// Shuffle the dataset. 
        /// </summary>
        /// <param name="seed"></param>
        public void Randomize(int? seed = null)
        {
            // Get the new order for the data
            int[] order = Enumerable.Range(0, Data.Count()).ToArray();
            var shuffler = new FisherYatesShuffler<int>();
            shuffler.Shuffle(order, seed);

            // Set the order
            int[] order2 = (int[])order.Clone();
            Array.Sort(order, Data);
            Array.Sort(order2, Class);
        }
    }
}
