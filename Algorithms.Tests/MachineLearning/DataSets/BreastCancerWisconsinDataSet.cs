using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Shufflers;

namespace Algorithms.Tests.MachineLearning.DataSets
{
    /// <summary>
    /// https://archive.ics.uci.edu/ml/machine-learning-databases/breast-cancer-wisconsin/
    /// </summary>
    internal class BreastCancerWisconsinDataSet
    {
        /// <summary> Download URL. </summary>
        private const string DownloadUrl = "https://archive.ics.uci.edu/ml/machine-learning-databases/breast-cancer-wisconsin/wdbc.data";

        /// <summary> Number of samples in the dataset.</summary>
        private const int NumSamples = 569;

        /// <summary> Number of features in the dataset.</summary>
        private const int NumFeatures = 30;

        /// <summary>
        /// Gets or sets the list of samles and its features.
        /// To see what these features are read:
        /// https://archive.ics.uci.edu/ml/machine-learning-databases/breast-cancer-wisconsin/wdbc.names
        /// </summary>        
        public double[][] Data { get; set; }

        /// <summary>
        /// Gets or Sets the list of outcomes for each sample:
        /// 0: B = benign
        /// 1: M = malignant
        /// </summary>
        /// <remarks>
        /// In the dataset, this outcome comes as a classification of M or B. As
        /// this dataset will be used for regression, the value is converted to 0 or 1, so we will get
        /// a probability [0-1] of the sample being malingnant instead of a classification [M,B].
        /// </remarks>
        public double[] Class { get; set; }

        public BreastCancerWisconsinDataSet(string url = DownloadUrl)
        {
            try
            {
                Data = new double[NumSamples][];
                Class = new double[NumSamples];

                // Download the data from URL
                string rawText = string.Empty;
                using (var client = new WebClient())
                {
                    // Download the Web resource and save it into a data buffer.
                    byte[] myDataBuffer = client.DownloadData(url);
                    rawText = Encoding.ASCII.GetString(myDataBuffer);
                }

                // Load data
                string[] rawData = rawText.Split("\n");
                int numSample = 0;
                foreach (var line in rawData)
                {
                    if (line != string.Empty)
                    {
                        FillFeaturesFromRow(line, numSample);
                        numSample++;
                    }
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Dataset could not be loaded from the given URL.");
            }
        }

        private void FillFeaturesFromRow(string line, int numSample)
        {
            Data[numSample] = new double[30];
            var values = line.Split(",");

            if (values.Length != 32)
            {
                throw new ArgumentException("Line has different data length than expected.");
            }

            for (int i = 2; i < 32; i++)
            {
                Data[numSample][i - 2] = double.Parse(values[i], CultureInfo.InvariantCulture);
            }

            Class[numSample] = values[1] == "M" ? 1 : 0;
        }

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
