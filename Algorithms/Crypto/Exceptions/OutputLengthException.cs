namespace Algorithms.Crypto.Exceptions;

/// <summary>
/// Represents an exception that is thrown when the output buffer length is insufficient for a cryptographic operation.
/// </summary>
/// <remarks>
/// The <see cref="OutputLengthException"/> is a specific subclass of <see cref="DataLengthException"/>. It is used in cryptographic
/// operations to signal that the provided output buffer does not have enough space to store the required output. This exception is
/// typically thrown when encryption, hashing, or other cryptographic operations require more space than what has been allocated in
/// the output buffer.
/// <br />
/// This exception provides constructors for creating the exception with a custom message, an inner exception, or both. By inheriting
/// from <see cref="DataLengthException"/>, it can be handled similarly in cases where both input and output length issues may arise.
/// </remarks>
public class OutputLengthException : DataLengthException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OutputLengthException"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor initializes a new instance of the <see cref="OutputLengthException"/> class without any additional message or inner exception.
    /// It is commonly used when a generic output length issue needs to be raised without specific details.
    /// </remarks>
    public OutputLengthException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OutputLengthException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <remarks>
    /// This constructor allows for a custom error message to be provided, giving more detail about the specific issue with the output length.
    /// </remarks>
    public OutputLengthException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OutputLengthException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    /// <remarks>
    /// This constructor allows for both a custom message and an inner exception, which can be useful for propagating
    /// the underlying cause of the error. For example, if the output buffer length is too short due to incorrect calculations,
    /// the root cause (e.g., an <see cref="ArgumentException"/>) can be passed in as the inner exception.
    /// </remarks>
    public OutputLengthException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
