using System;
using System.Security.Cryptography;

namespace Algorithms.Crypto.Paddings;

/// <summary>
/// <para>
/// This class implements the ISO10126d2 padding scheme, which is a standard way of padding data to fit a certain block
/// size.
/// </para>
/// <para>
/// ISO10126d2 padding adds N-1 random bytes and one byte of value N to the end of the data, where N is the number of
/// bytes needed to reach the block size. For example, if the block size is 16 bytes, and the data is 10 bytes long, then
/// 5 random bytes and a byte with value 6 will be added to the end of data. This way the padded data will be 16 bytes
/// long and can be encrypted or decrypted by a block cipher algorithm.
/// </para>
/// <para>
/// The padding can easily be removed after decryption by looking at the last byte and discarding that many bytes from
/// the end of the data.
/// </para>
/// </summary>
public class Iso10126D2Padding : IBlockCipherPadding
{
    /// <summary>
    /// Adds random padding to the input data array to make it a multiple of the block size according to the
    /// ISO10126d2 standard.
    /// </summary>
    /// <param name="inputData">The input data array that needs to be padded.</param>
    /// <param name="inputOffset">The offset in the input data array where the padding should start.</param>
    /// <returns>The number of bytes added as padding.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when there is not enough space in the input array for padding.
    /// </exception>
    public int AddPadding(byte[] inputData, int inputOffset)
    {
        // Calculate how many bytes need to be added to reach the next multiple of block size.
        var code = (byte)(inputData.Length - inputOffset);

        if (code == 0 || inputOffset + code > inputData.Length)
        {
            throw new ArgumentException("Not enough space in input array for padding");
        }

        // Add the padding.
        while (inputOffset < (inputData.Length - 1))
        {
            inputData[inputOffset] = (byte)RandomNumberGenerator.GetInt32(255);
            inputOffset++;
        }

        // Set the last byte of the array to the size of the padding added.
        inputData[inputOffset] = code;

        return code;
    }

    /// <summary>
    /// Removes the padding from the input data array and returns the original data.
    /// </summary>
    /// <param name="inputData">
    /// The input data with ISO10126d2 padding. Must not be null and must have a valid length and padding.
    /// </param>
    /// <returns>
    /// The input data without the padding as a new byte array.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the padding length is invalid.
    /// </exception>
    public byte[] RemovePadding(byte[] inputData)
    {
        // Get the size of the padding from the last byte of the input data.
        var paddingLength = inputData[^1];

        // Check if the padding size is valid.
        if (paddingLength < 1 || paddingLength > inputData.Length)
        {
            throw new ArgumentException("Invalid padding length");
        }

        // Create a new array to hold the original data.
        var output = new byte[inputData.Length - paddingLength];

        // Copy the original data into the new array.
        Array.Copy(inputData, 0, output, 0, output.Length);

        return output;
    }

    /// <summary>
    /// Gets the number of padding bytes from the input data array.
    /// </summary>
    /// <param name="input">The input data array that has been padded.</param>
    /// <returns>The number of padding bytes.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the padding block is corrupted.</exception>
    public int GetPaddingCount(byte[] input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input), "Input cannot be null");
        }

        // Get the last byte of the input data as the padding value.
        var lastByte = input[^1];
        var paddingCount = lastByte & 0xFF;

        // Calculate the index where the padding starts.
        var paddingStartIndex = input.Length - paddingCount;
        var paddingCheckFailed = 0;

        // The paddingCheckFailed will be non-zero under the following circumstances:
        // 1. When paddingStartIndex is negative: This happens when paddingCount (the last byte of the input array) is
        // greater than the length of the input array. In other words, the padding count is claiming that there are more
        // padding bytes than there are bytes in the array, which is not a valid scenario.
        // 2. When paddingCount - 1 is negative: This happens when paddingCount is zero or less. Since paddingCount
        // represents the number of padding bytes and is derived from the last byte of the input array, it should always
        // be a positive number. If it's zero or less, it means that either there's no padding, or an invalid negative
        // padding count has shomehow encoded into the last byte of the input array.
        paddingCheckFailed = (paddingStartIndex | (paddingCount - 1)) >> 31;
        if (paddingCheckFailed != 0)
        {
            throw new ArgumentException("Padding block is corrupted");
        }

        return paddingCount;
    }
}
