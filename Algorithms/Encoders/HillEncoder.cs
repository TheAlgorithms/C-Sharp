using Algorithms.Numeric;
using System;

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
            throw new NotImplementedException();
        }

        private string Decipher(string text, double[,] key)
        {
            throw new NotImplementedException();
        }
    }
}