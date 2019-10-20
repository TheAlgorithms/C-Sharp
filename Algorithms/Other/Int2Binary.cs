namespace Algorithms.Other
{
    /// <summary>
    /// Manually converts an integer of certain size to a string of the binary representation.
    /// </summary>
    public static class Int2Binary
    {
        /// <summary>
        /// Returns string of the binary representation of given Int.
        /// </summary>
        /// <param name="input">Number to be converted.</param>
        /// <returns>Binary representation of input.</returns>
        public static string Int2Bin(ushort input)
        {
            ushort msb = ushort.MaxValue / 2 + 1;
            var output = string.Empty;
            for (var i = 0; i < 16; i++)
            {
                if (input >= msb)
                {
                    output += "1";
                    input -= msb;
                    msb /= 2;
                }
                else
                {
                    output += "0";
                    msb /= 2;
                }
            }

            return output;
        }

        /// <summary>
        /// Returns string of the binary representation of given Int.
        /// </summary>
        /// <param name="input">Number to be converted.</param>
        /// <returns>Binary representation of input.</returns>
        public static string Int2Bin(uint input)
        {
            var msb = uint.MaxValue / 2 + 1;
            var output = string.Empty;
            for (var i = 0; i < 32; i++)
            {
                if (input >= msb)
                {
                    output += "1";
                    input -= msb;
                    msb /= 2;
                }
                else
                {
                    output += "0";
                    msb /= 2;
                }
            }

            return output;
        }

        /// <summary>
        /// Returns string of the binary representation of given Int.
        /// </summary>
        /// <param name="input">Number to be converted.</param>
        /// <returns>Binary representation of input.</returns>
        public static string Int2Bin(ulong input)
        {
            var msb = ulong.MaxValue / 2 + 1;
            var output = string.Empty;
            for (var i = 0; i < 64; i++)
            {
                if (input >= msb)
                {
                    output += "1";
                    input -= msb;
                    msb /= 2;
                }
                else
                {
                    output += "0";
                    msb /= 2;
                }
            }

            return output;
        }
    }
}
