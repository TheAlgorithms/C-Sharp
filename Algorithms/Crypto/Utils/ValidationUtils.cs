using System;
using System.Diagnostics;
using Algorithms.Crypto.Exceptions;

namespace Algorithms.Crypto.Utils;

/// <summary>
/// Provides utility methods for validating the lengths of input and output data in cryptographic operations.
/// </summary>
/// <remarks>
/// The <see cref="ValidationUtils"/> class contains static methods to validate the length and position of data buffers used in
/// cryptographic operations. These methods throw appropriate exceptions such as <see cref="DataLengthException"/> or
/// <see cref="OutputLengthException"/> when the validation fails. These are critical for ensuring that cryptographic computations
/// do not run into buffer overflows, underflows, or incorrect input/output buffer lengths.
/// </remarks>
public static class ValidationUtils
{
    /// <summary>
    /// Validates that the specified offset and length fit within the bounds of the given buffer.
    /// </summary>
    /// <param name="buffer">The byte array to validate.</param>
    /// <param name="offset">The offset into the byte array where validation should start.</param>
    /// <param name="length">The number of bytes to validate from the specified offset.</param>
    /// <param name="message">The message that describes the error if the exception is thrown.</param>
    /// <exception cref="DataLengthException">Thrown if the offset and length exceed the bounds of the buffer.</exception>
    /// <remarks>
    /// This method ensures that the specified offset and length fit within the bounds of the buffer. If the offset and length
    /// go out of bounds, a <see cref="DataLengthException"/> is thrown with the provided error message.
    /// </remarks>
    public static void CheckDataLength(byte[] buffer, int offset, int length, string message)
    {
        if (offset > (buffer.Length - length))
        {
            throw new DataLengthException(message);
        }
    }

    /// <summary>
    /// Throws an <see cref="OutputLengthException"/> if the specified condition is true.
    /// </summary>
    /// <param name="condition">A boolean condition indicating whether the exception should be thrown.</param>
    /// <param name="message">The message that describes the error if the exception is thrown.</param>
    /// <exception cref="OutputLengthException">Thrown if the condition is true.</exception>
    /// <remarks>
    /// This method performs a simple conditional check for output length validation. If the condition is true, an
    /// <see cref="OutputLengthException"/> is thrown with the provided message.
    /// </remarks>
    public static void CheckOutputLength(bool condition, string message)
    {
        if (condition)
        {
            throw new OutputLengthException(message);
        }
    }

    /// <summary>
    /// Validates that the specified offset and length fit within the bounds of the output buffer.
    /// </summary>
    /// <param name="buffer">The byte array to validate.</param>
    /// <param name="offset">The offset into the byte array where validation should start.</param>
    /// <param name="length">The number of bytes to validate from the specified offset.</param>
    /// <param name="message">The message that describes the error if the exception is thrown.</param>
    /// <exception cref="OutputLengthException">Thrown if the offset and length exceed the bounds of the buffer.</exception>
    /// <remarks>
    /// This method ensures that the specified offset and length do not exceed the bounds of the output buffer. If the
    /// validation fails, an <see cref="OutputLengthException"/> is thrown with the provided message.
    /// </remarks>
    public static void CheckOutputLength(byte[] buffer, int offset, int length, string message)
    {
        if (offset > (buffer.Length - length))
        {
            throw new OutputLengthException(message);
        }
    }

    /// <summary>
    /// Validates that the length of the output span does not exceed the specified length.
    /// </summary>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <param name="output">The span to validate.</param>
    /// <param name="length">The maximum allowed length for the output span.</param>
    /// <param name="message">The message that describes the error if the exception is thrown.</param>
    /// <exception cref="OutputLengthException">Thrown if the length of the output span exceeds the specified length.</exception>
    /// <remarks>
    /// This method checks that the span does not exceed the specified length. If the span length exceeds the allowed length,
    /// an <see cref="OutputLengthException"/> is thrown with the provided error message.
    /// </remarks>
    public static void CheckOutputLength<T>(Span<T> output, int length, string message)
    {
        if (output.Length > length)
        {
            throw new OutputLengthException(message);
        }
    }
}
