using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Algorithms.Crypto.Utils;

/// <summary>
/// Provides utility methods for converting between byte arrays and 64-bit unsigned integers using big-endian byte order.
/// </summary>
/// <remarks>
/// The <see cref="ByteEncodingUtils"/> class contains static methods that assist in reading and writing 64-bit unsigned integers
/// from and to byte arrays or spans in big-endian format. These methods are optimized for cryptographic operations where byte
/// encoding is critical for consistency and security.
/// </remarks>
public static class ByteEncodingUtils
{
    /// <summary>
    /// Converts an 8-byte segment from a byte array (starting at the specified offset) into a 64-bit unsigned integer using big-endian format.
    /// </summary>
    /// <param name="byteStream">The byte array containing the input data.</param>
    /// <param name="offset">The offset within the byte array to start reading from.</param>
    /// <returns>A 64-bit unsigned integer representing the big-endian interpretation of the byte array segment.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified offset is out of range of the byte array.</exception>
    /// <remarks>
    /// This method reads 8 bytes from the specified offset within the byte array and converts them to a 64-bit unsigned integer
    /// in big-endian format. Big-endian format stores the most significant byte first, followed by the less significant bytes.
    /// </remarks>
    public static ulong BigEndianToUint64(byte[] byteStream, int offset)
    {
        return BinaryPrimitives.ReadUInt64BigEndian(byteStream.AsSpan(offset));
    }

    /// <summary>
    /// Converts a read-only span of bytes into a 64-bit unsigned integer using big-endian format.
    /// </summary>
    /// <param name="byteStream">A read-only span containing the input data.</param>
    /// <returns>A 64-bit unsigned integer representing the big-endian interpretation of the span of bytes.</returns>
    /// <remarks>
    /// This method is optimized for performance using the <see cref="MethodImplOptions.AggressiveInlining"/> attribute to encourage
    /// inlining by the compiler. It reads exactly 8 bytes from the input span and converts them into a 64-bit unsigned integer.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong BigEndianToUint64(ReadOnlySpan<byte> byteStream)
    {
        return BinaryPrimitives.ReadUInt64BigEndian(byteStream);
    }

    /// <summary>
    /// Writes a 64-bit unsigned integer to a span of bytes using big-endian format.
    /// </summary>
    /// <param name="value">The 64-bit unsigned integer to write.</param>
    /// <param name="byteStream">The span of bytes where the value will be written.</param>
    /// <remarks>
    /// This method writes the 64-bit unsigned integer into the span in big-endian format, where the most significant byte is written first.
    /// The method is optimized using the <see cref="MethodImplOptions.AggressiveInlining"/> attribute to improve performance in scenarios
    /// where frequent byte-to-integer conversions are required, such as cryptographic algorithms.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void UInt64ToBigEndian(ulong value, Span<byte> byteStream)
    {
        BinaryPrimitives.WriteUInt64BigEndian(byteStream, value);
    }
}
