using System;

namespace Algorithms.Crypto.Paddings;

/// <summary>
/// A common interface that all block cipher padding schemes should follow.
/// </summary>
public interface IBlockCipherPadding
{
    /// <summary>
    /// Adds padding bytes to the end of the given block of the data and returns the number of bytes that were added.
    /// </summary>
    /// <param name="inputData">The input data array that needs padding.</param>
    /// <param name="inputOffset">The offset in the input array where the padding should start.</param>
    /// <returns>The number of bytes added.</returns>
    /// <remarks>
    /// This method expects that the input parameter <paramref name="inputData"/> contains the last block of plain text
    /// that needs to be padded. This means that the value of <paramref name="inputData"/> has to have the same value as
    /// the last block of plain text. The reason for this is that some modes such as the <see cref="TbcPadding"/> base the
    /// padding value on the last byte of the plain text.
    /// </remarks>
    public int AddPadding(byte[] inputData, int inputOffset);

    /// <summary>
    /// Removes the padding bytes from the given block of data and returns the original data as a new array.
    /// </summary>
    /// <param name="inputData">The input data array containing the padding.</param>
    /// <returns>The input data without the padding as a new byte array.</returns>
    /// <exception cref="ArgumentException">Thrown when the input data has invalid padding.</exception>
    public byte[] RemovePadding(byte[] inputData);

    /// <summary>
    /// Gets the number of padding bytes in the input data.
    /// </summary>
    /// <param name="input">The input data array that has padding.</param>
    /// <returns>The number of padding bytes in the input data.</returns>
    /// <exception cref="ArgumentException">Thrown when the input data has invalid padding.</exception>
    public int GetPaddingCount(byte[] input);
}
