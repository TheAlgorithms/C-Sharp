using System;
using System.Linq;
using Algorithms.Numeric;

namespace Algorithms.Encoders
{
    /// <summary>
    /// Lester S. Hill's polygraphic substitution cipher,
    /// without representing letters using mod26, using
    /// corresponding "(char)value" instead.
    /// </summary>
    public class HillEncoder : IEncoder<double[,]>
    {
        private readonly GaussJordanElimination linearEquationSolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="HillEncoder"/> class.
        /// TODO.
        /// </summary>
        public HillEncoder() => linearEquationSolver = new GaussJordanElimination(); // TODO: add DI

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="text">TODO. 2.</param>
        /// <param name="key">TODO. 3.</param>
        /// <returns>TODO. 4.</returns>
        public string Encode(string text, double[,] key)
        {
            var preparedText = FillGaps(text);
            var chunked = ChunkTextToArray(preparedText);
            var splitted = SplitToCharArray(chunked);

            var ciphered = new double[chunked.Length][];

            for (var i = 0; i < chunked.Length; i++)
            {
                var vector = new double[3];
                Array.Copy(splitted, i * 3, vector, 0, 3);
                var product = MatrixCipher(vector, key);
                ciphered[i] = product;
            }

            var merged = MergeArrayList(ciphered);

            return BuildStringFromArray(merged);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="text">TODO. 1.</param>
        /// <param name="key">TODO. 2.</param>
        /// <returns>TODO. 3.</returns>
        public string Decode(string text, double[,] key)
        {
            var chunked = ChunkTextToArray(text);
            var splitted = SplitToCharArray(chunked);

            var deciphered = new double[chunked.Length][];

            for (var i = 0; i < chunked.Length; i++)
            {
                var vector = new double[3];
                Array.Copy(splitted, i * 3, vector, 0, 3);
                var product = MatrixDeCipher(vector, key);
                deciphered[i] = product;
            }

            var merged = MergeArrayList(deciphered);
            var str = BuildStringFromArray(merged);

            return UnFillGaps(str);
        }

        /// <summary>
        /// Converts elements from the array to their corresponding Unicode characters.
        /// </summary>
        /// <param name="arr">array of vectors.</param>
        /// <returns>Message.</returns>
        private static string BuildStringFromArray(double[] arr) => new string(arr.Select(c => (char)c).ToArray());

        /// <summary>
        /// Multiplies the key for the given scalar.
        /// </summary>
        /// <param name="vector">list of splitted words as numbers.</param>
        /// <param name="key">Cipher selected key.</param>
        /// <returns>Ciphered vector.</returns>
        private static double[] MatrixCipher(double[] vector, double[,] key)
        {
            var multiplied = new double[vector.Length];

            for (var i = 0; i < key.GetLength(1); i++)
            {
                for (var j = 0; j < key.GetLength(0); j++)
                {
                    multiplied[i] += key[i, j] * vector[j];
                }
            }

            return multiplied;
        }

        /// <summary>
        /// Given a list of vectors, returns a single array of elements.
        /// </summary>
        /// <param name="list">List of ciphered arrays.</param>
        /// <returns>unidimensional list.</returns>
        private static double[] MergeArrayList(double[][] list)
        {
            var merged = new double[list.Length * 3];

            for (var i = 0; i < list.Length; i++)
            {
                Array.Copy(list[i], 0, merged, i * 3, list[0].Length);
            }

            return merged;
        }

        /// <summary>
        /// Splits the input text message as chunks of words.
        /// </summary>
        /// <param name="chunked">chunked words list.</param>
        /// <returns>spliiter char array.</returns>
        private static char[] SplitToCharArray(string[] chunked)
        {
            var splitted = new char[chunked.Length * 3];

            for (var i = 0; i < chunked.Length; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    splitted[(i * 3) + j] = chunked[i].ToCharArray()[j];
                }
            }

            return splitted;
        }

        /// <summary>
        /// Chunks the input text message.
        /// </summary>
        /// <param name="text">text message.</param>
        /// <returns>array of words.</returns>
        private static string[] ChunkTextToArray(string text)
        {
            // To split the message into chunks
            var div = text.Length / 3;
            var chunks = new string[div];

            for (var i = 0; i < div; i++)
            {
                chunks.SetValue(text.Substring(i * 3, 3), i);
            }

            return chunks;
        }

        /// <summary>
        /// Fills a text message with spaces at the end
        /// to enable a simple split by 3-length-word.
        /// </summary>
        /// <param name="text">Text Message.</param>
        /// <returns>Modified text Message.</returns>
        private static string FillGaps(string text)
        {
            var remainder = text.Length % 3;
            return remainder == 0 ? text : text + new string(' ', 3 - remainder);
        }

        /// <summary>
        /// Removes the extra spaces included on the cipher phase.
        /// </summary>
        /// <param name="text">Text message.</param>
        /// <returns>Deciphered Message.</returns>
        private static string UnFillGaps(string text) => text.TrimEnd();

        /// <summary>
        /// Finds the inverse of the given matrix using a linear equation solver.
        /// </summary>
        /// <param name="vector">Splitted words vector.</param>
        /// <param name="key">Key used for the cipher.</param>
        /// <returns>TODO.</returns>
        private double[] MatrixDeCipher(double[] vector, double[,] key)
        {
            // To augment the original key with the given vector.
            var augM = new double[3, 4];

            for (var i = 0; i < key.GetLength(0); i++)
            {
                for (var j = 0; j < key.GetLength(1); j++)
                {
                    augM[i, j] = key[i, j];
                }
            }

            for (var k = 0; k < vector.Length; k++)
            {
                augM[k, 3] = vector[k];
            }

            _ = linearEquationSolver.Solve(augM);

            return new[] { augM[0, 3], augM[1, 3], augM[2, 3] };
        }
    }
}
