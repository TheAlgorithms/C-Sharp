using System.Numerics;

namespace Algorithms.Crypto.Utils;

/// <summary>
/// Provides utility methods for performing bitwise rotation operations (left and right) on 64-bit integers.
/// </summary>
/// <remarks>
/// The <see cref="LongUtils"/> class contains methods to rotate 64-bit signed and unsigned integers to the left or right.
/// These rotations are crucial in various cryptographic algorithms, where circular shifts are used to mix data and
/// introduce non-linearity. The methods use the underlying <see cref="System.Numerics.BitOperations"/> for efficient,
/// hardware-supported bitwise rotations.
/// </remarks>
public static class LongUtils
{
    /// <summary>
    /// Rotates the bits of a 64-bit signed integer to the left by a specified number of bits.
    /// </summary>
    /// <param name="i">The 64-bit signed integer to rotate.</param>
    /// <param name="distance">The number of bits to rotate the integer to the left.</param>
    /// <returns>The result of rotating the integer to the left by the specified distance.</returns>
    /// <remarks>
    /// This method uses the underlying <see cref="BitOperations.RotateLeft(ulong, int)"/> method, converting the signed integer to an unsigned integer
    /// for the rotation, then casting it back to a signed integer. The rotation is performed in a circular manner, where bits shifted
    /// out of the most significant bit are reintroduced into the least significant bit.
    /// </remarks>
    public static long RotateLeft(long i, int distance)
    {
        return (long)BitOperations.RotateLeft((ulong)i, distance);
    }

    /// <summary>
    /// Rotates the bits of a 64-bit unsigned integer to the left by a specified number of bits.
    /// </summary>
    /// <param name="i">The 64-bit unsigned integer to rotate.</param>
    /// <param name="distance">The number of bits to rotate the integer to the left.</param>
    /// <returns>The result of rotating the integer to the left by the specified distance.</returns>
    /// <remarks>
    /// The rotation is performed circularly, meaning bits shifted out of the most significant bit are reintroduced into
    /// the least significant bit. This method is optimized for performance using hardware-supported operations through
    /// <see cref="BitOperations.RotateLeft(ulong, int)"/>.
    /// </remarks>
    public static ulong RotateLeft(ulong i, int distance)
    {
        return BitOperations.RotateLeft(i, distance);
    }

    /// <summary>
    /// Rotates the bits of a 64-bit signed integer to the right by a specified number of bits.
    /// </summary>
    /// <param name="i">The 64-bit signed integer to rotate.</param>
    /// <param name="distance">The number of bits to rotate the integer to the right.</param>
    /// <returns>The result of rotating the integer to the right by the specified distance.</returns>
    /// <remarks>
    /// Similar to the left rotation, this method uses <see cref="BitOperations.RotateRight(ulong, int)"/> to perform the rotation.
    /// The signed integer is cast to an unsigned integer for the operation and cast back to a signed integer afterward.
    /// The rotation wraps bits shifted out of the least significant bit into the most significant bit.
    /// </remarks>
    public static long RotateRight(long i, int distance)
    {
        return (long)BitOperations.RotateRight((ulong)i, distance);
    }

    /// <summary>
    /// Rotates the bits of a 64-bit unsigned integer to the right by a specified number of bits.
    /// </summary>
    /// <param name="i">The 64-bit unsigned integer to rotate.</param>
    /// <param name="distance">The number of bits to rotate the integer to the right.</param>
    /// <returns>The result of rotating the integer to the right by the specified distance.</returns>
    /// <remarks>
    /// This method performs the rotation circularly, where bits shifted out of the least significant bit are reintroduced
    /// into the most significant bit. The operation uses hardware-supported instructions via <see cref="BitOperations.RotateRight(ulong, int)"/>.
    /// </remarks>
    public static ulong RotateRight(ulong i, int distance)
    {
        return BitOperations.RotateRight(i, distance);
    }
}
