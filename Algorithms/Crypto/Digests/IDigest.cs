using System;

namespace Algorithms.Crypto.Digests;

/// <summary>
/// Interface for message digest algorithms, providing methods to update, finalize, and reset the digest state.
/// </summary>
public interface IDigest
{
    /// <summary>
    /// Gets the name of the digest algorithm (e.g., "SHA-256").
    /// </summary>
    string AlgorithmName { get; }

    /// <summary>
    /// Gets the size of the digest in bytes (e.g., 32 bytes for SHA-256).
    /// </summary>
    /// <returns>The size of the digest in bytes.</returns>
    int GetDigestSize();

    /// <summary>
    /// Gets the byte length of the internal buffer used by the digest.
    /// </summary>
    /// <returns>The byte length of the internal buffer.</returns>
    int GetByteLength();

    /// <summary>
    /// Updates the digest with a single byte of input data.
    /// </summary>
    /// <param name="input">The byte to add to the digest.</param>
    void Update(byte input);

    /// <summary>
    /// Updates the digest with a portion of a byte array.
    /// </summary>
    /// <param name="input">The byte array containing the input data.</param>
    /// <param name="inOff">The offset within the array to start reading from.</param>
    /// <param name="inLen">The length of data to read from the array.</param>
    void BlockUpdate(byte[] input, int inOff, int inLen);

    /// <summary>
    /// Updates the digest with a portion of input data from a <see cref="ReadOnlySpan{T}"/> of bytes.
    /// </summary>
    /// <param name="input">The <see cref="ReadOnlySpan{T}"/> containing the input data.</param>
    void BlockUpdate(ReadOnlySpan<byte> input);

    /// <summary>
    /// Completes the digest calculation and stores the result in the specified byte array.
    /// </summary>
    /// <param name="output">The byte array to store the final digest.</param>
    /// <param name="outOff">The offset within the array to start writing the digest.</param>
    /// <returns>The number of bytes written to the output array.</returns>
    int DoFinal(byte[] output, int outOff);

    /// <summary>
    /// Completes the digest calculation and stores the result in the specified <see cref="Span{T}"/> of bytes.
    /// </summary>
    /// <param name="output">The <see cref="Span{T}"/> to store the final digest.</param>
    /// <returns>The number of bytes written to the output span.</returns>
    int DoFinal(Span<byte> output);

    string Digest(byte[] input);

    string Digest(Span<byte> input);

    /// <summary>
    /// Resets the digest to its initial state, clearing all data accumulated so far.
    /// </summary>
    void Reset();
}
