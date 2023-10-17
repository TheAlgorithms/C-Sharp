using System;

namespace Algorithms.Crypto.Paddings;

/// <summary>
/// <para>
/// Trailing-Bit-Complement padding is a padding scheme that is defined in the ISO/IEC 9797-1 standard.
/// </para>
/// <para>
/// It is used for adding data to the end of a message that needs to be encrypted or decrypted by a block cipher.
/// </para>
/// <para>
/// The padding bytes are either 0x00 or 0xFF, depending on the last bit of the original data. For example, if the last
/// bit of the original data is 0, then the padding bytes are 0xFF; if the last bit is 1, then the padding bytes are 0x00.
/// The padding bytes are added at the end of the data block until the desired length is reached.
/// </para>
/// </summary>
public class TbcPadding : IBlockCipherPadding
{
    /// <summary>
    /// Adds padding to the input array according to the TBC standard.
    /// </summary>
    /// <param name="input">The input array to be padded.</param>
    /// <param name="inputOffset">The offset in the input array where the padding starts.</param>
    /// <returns>The number of bytes that were added.</returns>
    /// <exception cref="ArgumentException">Thrown when the input array does not have enough space for padding.</exception>
    public int AddPadding(byte[] input, int inputOffset)
    {
        // Calculate the number of bytes to be padded.
        var count = input.Length - inputOffset;
        byte code;

        // Check if the input array has enough space for padding.
        if (count < 0)
        {
            throw new ArgumentException("Not enough space in input array for padding");
        }

        if (inputOffset > 0)
        {
            // Get the last bit of the previous byte.
            var lastBit = input[inputOffset - 1] & 0x01;

            // Set the padding code to 0xFF if the last bit is 0, or 0x00 if the last bit is 1.
            code = (byte)(lastBit == 0 ? 0xff : 0x00);
        }
        else
        {
            // Get the last bit of the last byte in the input array.
            var lastBit = input[^1] & 0x01;

            // Set the padding code to 0xff if the last bit is 0, or 0x00 if the last bit is 1.
            code = (byte)(lastBit == 0 ? 0xff : 0x00);
        }

        while (inputOffset < input.Length)
        {
            // Set each byte to the padding code.
            input[inputOffset] = code;
            inputOffset++;
        }

        // Return the number of bytes that were padded.
        return count;
    }

    /// <summary>
    /// Removes the padding from a byte array according to the Trailing-Bit-Complement padding algorithm.
    /// </summary>
    /// <param name="input">The byte array to remove the padding from.</param>
    /// <returns>A new byte array without the padding.</returns>
    /// <remarks>
    /// This method assumes that the input array has padded with either 0x00 or 0xFF bytes, depending on the last bit of
    /// the original data. The method works by finding the last byte that does not match the padding code and copying all
    /// the bytes up to that point into a new array. If the input array is not padded or has an invalid padding, the
    /// method may return incorrect results.
    /// </remarks>
    public byte[] RemovePadding(byte[] input)
    {
        if (input.Length == 0)
        {
            return Array.Empty<byte>();
        }

        // Get the last byte of the input array.
        var lastByte = input[^1];

        // Determine the byte code
        var code = (byte)((lastByte & 0x01) == 0 ? 0x00 : 0xff);

        // Start from the end of the array and move towards the front.
        int i;
        for (i = input.Length - 1; i >= 0; i--)
        {
            // If the current byte does not match the padding code, stop.
            if (input[i] != code)
            {
                break;
            }
        }

        // Create a new array of the appropriate length.
        var unpadded = new byte[i + 1];

        // Copy the unpadded data into the new array.
        Array.Copy(input, unpadded, i + 1);

        // Return the new array.
        return unpadded;
    }

    /// <summary>
    /// Returns the number of padding bytes in a byte array according to the Trailing-Bit-Complement padding algorithm.
    /// </summary>
    /// <param name="input">The byte array to check for padding.</param>
    /// <returns>The number of padding bytes in the input array.</returns>
    /// <remarks>
    /// This method assumes that the input array has been padded with either 0x00 or 0xFF bytes, depending on the last
    /// bit of the original data. The method works by iterating backwards from the end of the array and counting the
    /// number of bytes that match the padding code. The method uses bitwise operations to optimize the performance and
    /// avoid branching. If the input array is not padded or has an invalid padding, the method may return incorrect
    /// results.
    /// </remarks>
    public int GetPaddingCount(byte[] input)
    {
        var length = input.Length;

        if (length == 0)
        {
            throw new ArgumentException("No padding found.");
        }

        // Get the value of the last byte as the padding value
        var paddingValue = input[--length] & 0xFF;
        var paddingCount = 1; // Start count at 1 for the last byte
        var countingMask = -1; // Initialize counting mask

        // Check if there is no padding
        if (paddingValue != 0 && paddingValue != 0xFF)
        {
            throw new ArgumentException("No padding found");
        }

        // Loop backwards through the array
        for (var i = length - 1; i >= 0; i--)
        {
            var currentByte = input[i] & 0xFF;

            // Calculate matchMask. If currentByte equals paddingValue, matchMask will be 0, otherwise -1
            var matchMask = ((currentByte ^ paddingValue) - 1) >> 31;

            // Update countingMask. Once a non-matching byte is found, countingMask will remain -1
            countingMask &= matchMask;

            // Increment count only if countingMask is 0 (i.e., currentByte matches paddingValue)
            paddingCount -= countingMask;
        }

        return paddingCount;
    }
}
