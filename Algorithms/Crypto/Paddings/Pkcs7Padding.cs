using System;

namespace Algorithms.Crypto.Paddings;

/// <summary>
/// <para>
/// This class implements the PKCS7 padding scheme, which is a standard way of padding data to fit a certain block size.
/// </para>
/// <para>
/// PKCS7 padding adds N bytes of value N to the end of the data, where N is the number of bytes needed to reach the block size.
/// For example, if the block size is 16 bytes, and the data is 11 bytes long, then 5 bytes of value 5 will be added to the
/// end of the data. This way, the padded data will be 16 bytes long and can be encrypted or decrypted by a block cipher algorithm.
/// </para>
/// <para>
/// The padding can be easily removed after decryption by looking at the last byte and subtracting that many bytes from the
/// end of the data.
/// </para>
/// <para>
/// This class supports any block size from 1 to 255 bytes, and can be used with any encryption algorithm that requires
/// padding, such as AES.
/// </para>
/// </summary>
public class Pkcs7Padding : IBlockCipherPadding
{
    private readonly int blockSize;

    public Pkcs7Padding(int blockSize)
    {
        if (blockSize is < 1 or > 255)
        {
            throw new ArgumentOutOfRangeException(nameof(blockSize), $"Invalid block size: {blockSize}");
        }

        this.blockSize = blockSize;
    }

    /// <summary>
    /// Adds padding to the end of a byte array according to the PKCS#7 standard.
    /// </summary>
    /// <param name="input">The byte array to be padded.</param>
    /// <param name="inputOffset">The offset from which to start padding.</param>
    /// <returns>The padding value that was added to each byte.</returns>
    /// <exception cref="ArgumentException">
    /// If the input array does not have enough space to add <c>blockSize</c> bytes as padding.
    /// </exception>
    /// <remarks>
    /// The padding value is equal to the number of of bytes that are added to the array.
    /// For example, if the input array has a length of 16 and the input offset is 10,
    /// then 6 bytes with the value 6 will be added to the end of the array.
    /// </remarks>
    public int AddPadding(byte[] input, int inputOffset)
    {
        // Calculate how many bytes need to be added to reach the next multiple of block size.
        var code = (byte)((blockSize - (input.Length % blockSize)) % blockSize);

        // If no padding is needed, add a full block of padding.
        if (code == 0)
        {
            code = (byte)blockSize;
        }

        if (inputOffset + code > input.Length)
        {
            throw new ArgumentException("Not enough space in input array for padding");
        }

        // Add the padding
        for (var i = 0; i < code; i++)
        {
            input[inputOffset + i] = code;
        }

        return code;
    }

    /// <summary>
    /// Removes the PKCS7 padding from the given input data.
    /// </summary>
    /// <param name="input">The input data with PKCS7 padding. Must not be null and must have a vaild length and padding.</param>
    /// <returns>The input data without the padding as a new byte array.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the input data is null, has an invalid length, or has an invalid padding.
    /// </exception>
    public byte[] RemovePadding(byte[] input)
    {
        // Check if input length is a multiple of blockSize
        if (input.Length % blockSize != 0)
        {
            throw new ArgumentException("Input length must be a multiple of block size");
        }

        // Get the padding length from the last byte of input
        var paddingLength = input[^1];

        // Check if padding length is valid
        if (paddingLength < 1 || paddingLength > blockSize)
        {
            throw new ArgumentException("Invalid padding length");
        }

        // Check if all padding bytes have the correct value
        for (var i = 0; i < paddingLength; i++)
        {
            if (input[input.Length - 1 - i] != paddingLength)
            {
                throw new ArgumentException("Invalid padding");
            }
        }

        // Create a new array with the size of input minus the padding length
        var output = new byte[input.Length - paddingLength];

        // Copy the data without the padding into the output array
        Array.Copy(input, output, output.Length);

        return output;
    }

    /// <summary>
    /// Gets the number of padding bytes in the given input data according to the PKCS7 padding scheme.
    /// </summary>
    /// <param name="input">The input data with PKCS7 padding. Must not be null and must have a valid padding.</param>
    /// <returns>The number of padding bytes in the input data.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the input data is null or has an invalid padding.
    /// </exception>
    /// <remarks>
    /// This method uses bitwise operations to avoid branching.
    /// </remarks>
    public int GetPaddingCount(byte[] input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input), "Input cannot be null");
        }

        // Get the last byte of the input data as the padding value.
        var lastByte = input[^1];
        var paddingCount = lastByte & 0xFF;

        // Calculate the index where the padding starts
        var paddingStartIndex = input.Length - paddingCount;
        var paddingCheckFailed = 0;

        // Check if the padding start index is negative or greater than the input length.
        // This is done by using bitwise operations to avoid branching.
        // If the padding start index is negative, then its most significant bit will be 1.
        // If the padding count is greater than the block size, then its most significant bit will be 1.
        // By ORing these two cases, we can get a non-zero value rif either of them is true.
        // By shifting this value right by 31 bits, we can get either 0 or -1 as the result.
        paddingCheckFailed = (paddingStartIndex | (paddingCount - 1)) >> 31;

        for (var i = 0; i < input.Length; i++)
        {
            // Check if each byte matches the padding value.
            // This is done by using bitwise operations to avoid branching.
            // If a byte does not match the padding value, then XORing them will give a non-zero value.
            // If a byte is before the padding start index, then we want to ignore it.
            // This is done by using bitwise operations to create a mask that is either all zeros or all ones.
            // If i is less than the padding start index, then subtracting them will give a negative value.
            // By shifting this value right by 31 bits, we can get either -1 or 0 as the mask.
            // By negating this mask, we can get either 0 or -1 as the mask.
            // By ANDing this mask with the XOR result, we can get either 0 or the XOR result as the final result.
            // By ORing this final result with the previous padding check result, we can accumulate any non-zero values.
            paddingCheckFailed |= (input[i] ^ lastByte) & ~((i - paddingStartIndex) >> 31);
        }

        // Check if the padding check failed.
        if (paddingCheckFailed != 0)
        {
            throw new ArgumentException("Padding block is corrupted");
        }

        // Return the number of padding bytes.
        return paddingCount;
    }
}
