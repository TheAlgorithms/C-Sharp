using Algorithms.Numeric;
using System;
using System.Text;

namespace Algorithms.Encoders
{
    public class HillEncoder : IEncoder<double[,]>
    {
        #region DEFINITION

        public string Decode(string text, double[,] key) => Decipher(text, key);

        public string Encode(string text, double[,] key) => Cipher(text, key);

        #endregion DEFINITION

        private string Cipher(string text, double[,] key)
        {
            var preparedText = FillGaps(text);
            var chunked = ChunkTextToArray(preparedText);
            var splitted = SplitToCharArray(chunked);

            double[][] ciphered = new double[chunked.Length][];

            for (var i = 0; i < chunked.Length; i++)
            {
                double[] vector = new double[3];
                Array.Copy(splitted, i * 3, vector, 0, 3);
                var product = MatrixCipher(vector, key);
                ciphered[i] = product;
            }

            var merged = MergeArrayList(ciphered);

            return BuildStringFromIntArray(merged);
        }

        private string Decipher(string text, double[,] key)
        {
            var chunked = ChunkTextToArray(text);
            var splitted = SplitToCharArray(chunked);

            double[][] deciphered = new double[chunked.Length][];

            for (var i = 0; i < chunked.Length; i++)
            {
                double[] vector = new double[3];
                Array.Copy(splitted, i * 3, vector, 0, 3);
                var product = MatrixDeCipher(vector, key);
                deciphered[i] = product;
            }

            var merged = MergeArrayList(deciphered);
            var str = BuildStringFromIntArray(merged);

            return UnFillGaps(str);
        }

        private string BuildStringFromIntArray(double[] arr)
        {
            var strBuilder = new StringBuilder();

            for (var i = 0; i < arr.Length; i++)
            {
                strBuilder.Append((char)arr[i]);
            }

            return strBuilder.ToString();
        }

        private double[] MergeArrayList(double[][] list)
        {
            var merged = new double[list.Length * 3];

            for (var i = 0; i < list.Length; i++)
            {
                Array.Copy(list[i], 0, merged, i * 3, list[0].Length);
            }

            return merged;
        }

        private double[] MatrixCipher(double[] vector, double[,] key)
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

        private char[] SplitToCharArray(string[] chunked)
        {
            char[] chopped = new char[chunked.Length * 3];

            for (int i = 0; i < chunked.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    chopped[(i * 3) + j] = chunked[i].ToCharArray()[j];
                }
            }

            return chopped;
        }

        /// <summary>
        /// To chunck the incoming text message and fill the gaps to be divisible.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string[] ChunkTextToArray(string text)
        {
            //To split the message into chunks
            var div = text.Length / 3;
            var chunks = new string[div];

            for (var i = 0; i < div; i++)
            {
                chunks.SetValue(text.Substring(i * 3, 3), i);
            }

            return chunks;
        }

        private string FillGaps(string text)
        {
            string newText = text;
            var chunkable = false;

            while (!chunkable)
            {
                if (newText.Length % 3 != 0)
                {
                    newText += " ";
                }
                else
                {
                    chunkable = true;
                }
            }

            return newText;
        }

        private string UnFillGaps(string text)
        {
            return text.TrimEnd();
        }

        private double[] MatrixDeCipher(double[] vector, double[,] key)
        {
            var augM = new double[3, 4];

            for (var i = 0; i < key.GetLength(0); i++)
            {
                for (int j = 0; j < key.GetLength(1); j++)
                {
                    augM[i, j] = key[i, j];
                }
            }

            for (int k = 0; k < vector.Length; k++)
            {
                augM[k, 3] = vector[k];
            }

            GaussianElimination.Solve(augM);

            return new double[] { augM[0, 3], augM[1, 3], augM[2, 3] };
        }
    }
}