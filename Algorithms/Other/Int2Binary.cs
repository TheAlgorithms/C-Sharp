using System;

namespace Algorithms.Other
{
    /// <summary>
    /// Manually converts an integer of certain size to a string of the binary representation.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1121:Use built-in type alias", Justification = "Built-in type aliases aren't as clear as concerning the amount of bits")]
    public static class Int2Binary
    {
        /// <summary>
        /// Returns string of the binary representation of given Int.
        /// </summary>
        /// <param name="input">Number to be converted.</param>
        /// <returns>Binary representation of input.</returns>
        public static string Int2bin(UInt16 input)
        {
            UInt16 msb = UInt16.MaxValue / 2 + 1;
            String output = string.Empty;
            for (int i = 0; i < 16; i++)
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
        public static string Int2bin(UInt32 input)
        {
            UInt32 msb = UInt32.MaxValue / 2 + 1;
            String output = string.Empty;
            for (int i = 0; i < 32; i++)
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
        public static string Int2bin(UInt64 input)
        {
            UInt64 msb = UInt64.MaxValue / 2 + 1;
            String output = string.Empty;
            for (int i = 0; i < 64; i++)
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
