using System;

namespace Algorithms.Crypto.Paddings;

/// <summary>
/// <para>
/// ISO 7816-4 padding is a padding scheme that is defined in the ISO/IEC 7816-4 documentation.
/// </para>
/// <para>
/// It is used for adding data to the end of a message that needs to be encrypted or decrypted by a block cipher.
/// </para>
/// ISO 7816-4 padding works as follows:
/// <para>
/// The first byte of the padding is 0x80, which is the hexadecimal representation of the binary value 10000000. This
/// byte indicates the start of the padding.
/// </para>
/// <para>
/// All other bytes of the padding are 0x00, which is the hexadecimal representation of the binary value 00000000. These
/// bytes fill up the remaining space in the last block.
/// </para>
/// <para>
/// The padding can be of any size, from 1 byte to the block size. For example, if the block size is 8 bytes and the
/// message has 5 bytes, then 3 bytes of padding are needed. The padding would be <c>0x80 0x00 0x00</c>.
/// </para>
/// <para>
/// ISO 7816-4 padding is also known as bit padding,because it simply places a single 1 bit after the plaintext, followed
/// by 0 valued bits up to the block size. It works for both byte-oriented and bit-oriented protocols, as it does not
/// depend on any specific character encoding or representation.
/// </para>
/// </summary>
public class Iso7816D4Padding : IBlockCipherPadding
{
    /// <summary>
    /// Adds padding to the input data according to the ISO 7816-4 standard.
    /// </summary>
    /// <param name="inputData">The input data array that needs padding.</param>
    /// <param name="inputOffset">The offset in the input data array where the padding should start.</param>
    /// <returns>The number of bytes added as padding.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when there is not enough space in the input array for padding or when the input offset is invalid.
    /// </exception>
    public int AddPadding(byte[] inputData, int inputOffset)
    {
        // Calculate the number of padding bytes based on the input data length and offset.
        var code = (byte)(inputData.Length - inputOffset);

        // Check if the padding bytes are valid and fit in the input array.
        if (code == 0 || inputOffset + code > inputData.Length)
        {
            throw new ArgumentException("Not enough space in input array for padding");
        }

        // Set the first padding byte to 80. This marks the start of padding in the ISO 7816-4 standard.
        inputData[inputOffset] = 80;
        inputOffset++;

        // Set the remaining padding bytes to 0.
        while (inputOffset < inputData.Length)
        {
            inputData[inputOffset] = 0;
            inputOffset++;
        }

        // Return the number of padding bytes.
        return code;
    }

    /// <summary>
    /// Removes the padding from the input data array and returns the original data.
    /// </summary>
    /// <param name="inputData">
    /// The input data with ISO 7816-4 padding. Must not be null and must have a valid length and padding.
    /// </param>
    /// <returns>The input data without the padding as a new byte array.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the input data has invalid padding.
    /// </exception>
    public byte[] RemovePadding(byte[] inputData)
    {
        // Find the index of the first padding byte by scanning from the end of the input.
        var paddingIndex = inputData.Length - 1;

        // Skip all the padding bytes that are 0.
        while (paddingIndex >= 0 && inputData[paddingIndex] == 0)
        {
            paddingIndex--;
        }

        // Check if the first padding byte is 0x80.
        if (paddingIndex < 0 || inputData[paddingIndex] != 0x80)
        {
            throw new ArgumentException("Invalid padding");
        }

        // Create a new array to store the unpadded data.
        var unpaddedData = new byte[paddingIndex];

        // Copy the unpadded data from the input data to the new array.
        Array.Copy(inputData, 0, unpaddedData, 0, paddingIndex);

        // Return the unpadded data array.
        return unpaddedData;
    }

    /// <summary>
    /// Gets the number of padding bytes in the input data according to the ISO 7816-4 standard.
    /// </summary>
    /// <param name="input">The input data array that has padding.</param>
    /// <returns>The number of padding bytes in the input data.</returns>
    /// <exception cref="ArgumentException"> Thrown when the input data has invalid padding.</exception>
    public int GetPaddingCount(byte[] input)
    {
        // Initialize the index of the first padding byte to -1.
        var paddingStartIndex = -1;

        // Initialize a mask to indicate if the current byte is still part of the padding.
        var stillPaddingMask = -1;

        // Initialize the current index to the end of the input data.
        var currentIndex = input.Length;

        // Loop backwards through the input data.
        while (--currentIndex >= 0)
        {
            // Get the current byte as an unsigned integer.
            var currentByte = input[currentIndex] & 0xFF;

            // Compute a mask to indicate if the current byte is 0x00.
            var isZeroMask = (currentByte - 1) >> 31;

            // Compute a mask to indicate if the current byte is 0x80.
            var isPaddingStartMask = ((currentByte ^ 0x80) - 1) >> 31;

            // Update the index of the first padding byte using bitwise operations.
            // If the current byte is 0x80 and still part of the padding, set the index to the current index.
            // Otherwise, keep the previous index.
            paddingStartIndex ^= (currentIndex ^ paddingStartIndex) & (stillPaddingMask & isPaddingStartMask);

            // Update the mask to indicate if the current byte is still part of the padding using bitwise operations.
            // If the current byte is 0x00, keep the previous mask.
            // Otherwise, set the mask to 0.
            stillPaddingMask &= isZeroMask;
        }

        // Check if the index of the first padding byte is valid.
        if (paddingStartIndex < 0)
        {
            throw new ArgumentException("Pad block corrupted");
        }

        // Return the number of padding bytes.
        return input.Length - paddingStartIndex;
    }
}
