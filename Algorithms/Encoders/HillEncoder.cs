﻿using Algorithms.Numeric;
using System;
using System.Text;

namespace Algorithms.Encoders
{
    /// <summary>
    /// Lester S. Hill's polygraphic substitution cipher, 
    /// without representing letters using mod26, using 
    /// corresponding "(char)value" instead.
    /// </summary>
    public class HillEncoder : IEncoder<double[,]>
    {
        #region DEFINITION

        public string Decode(string text, double[,] key) => Decipher(text, key);

        public string Encode(string text, double[,] key) => Cipher(text, key);

        #endregion DEFINITION

        #region MAIN
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

            return BuildStringFromArray(merged);
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
            var str = BuildStringFromArray(merged);

            return UnFillGaps(str);
        }
        #endregion MAIN

        /// <summary>
        /// To convert elements from the array to their corresponding char ASCII.
        /// </summary>
        /// <param name="arr">array of vectors</param>
        /// <returns>Message</returns>
        private string BuildStringFromArray(double[] arr)
        {
            var strBuilder = new StringBuilder();

            for (var i = 0; i < arr.Length; i++)
            {
                // Try to cast it to its corresponding (char)value.
                try
                {
                    strBuilder.Append((char)arr[i]);
                }
                catch (InvalidCastException)
                {
                    throw;
                }
            }

            return strBuilder.ToString();
        }

        /// <summary>
        /// Given a list of vectors, returns a single array of elements.
        /// </summary>
        /// <param name="list">List of ciphered arrays</param>
        /// <returns>unidimensional list</returns>
        private double[] MergeArrayList(double[][] list)
        {
            var merged = new double[list.Length * 3];

            for (var i = 0; i < list.Length; i++)
            {
                Array.Copy(list[i], 0, merged, i * 3, list[0].Length);
            }

            return merged;
        }

        /// <summary>
        /// To multiply the key for the given scalar.
        /// </summary>
        /// <param name="vector">list of splitted words as numbers.</param>
        /// <param name="key">Cipher selected key</param>
        /// <returns>Ciphered vector</returns>
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

        /// <summary>
        /// To split the input text message as chunks of words.
        /// </summary>
        /// <param name="chunked">chunked words list</param>
        /// <returns>spliiter char array.</returns>
        private char[] SplitToCharArray(string[] chunked)
        {
            char[] splitted = new char[chunked.Length * 3];

            for (int i = 0; i < chunked.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    splitted[(i * 3) + j] = chunked[i].ToCharArray()[j];
                }
            }

            return splitted;
        }

        /// <summary>
        /// To chunk the input text message.
        /// </summary>
        /// <param name="text">text message</param>
        /// <returns>array of words.</returns>
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

        /// <summary>
        /// Fills a text message with spaces at the end
        /// to enable a simple split by 3-length-word.
        /// </summary>
        /// <param name="text">Text Message</param>
        /// <returns>Modified text Message.</returns>
        private string FillGaps(string text)
        {
            string newText = text;
            var isChunkable = false;

            while (!isChunkable)
            {
                if (newText.Length % 3 != 0)
                {
                    newText += " ";
                }
                else
                {
                    isChunkable = true;
                }
            }

            return newText;
        }

        /// <summary>
        /// Removes the extra spaces included on the cipher phase.
        /// </summary>
        /// <param name="text">Text message</param>
        /// <returns>Deciphered Message</returns>
        private string UnFillGaps(string text)
        {
            return text.TrimEnd();
        }

        /// <summary>
        /// Finds the inverse of the given matrix using a linear equation solver.
        /// </summary>
        /// <param name="vector">Splitted words vector.</param>
        /// <param name="key">Key used for the cipher.</param>
        /// <returns></returns>
        private double[] MatrixDeCipher(double[] vector, double[,] key)
        {
            // To augment the original key with the given vector.
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

            GaussJordanElimination.Solve(augM);

            return new double[] { augM[0, 3], augM[1, 3], augM[2, 3] };
        }
    }
}