namespace Algorithms.Crypto.Exceptions;

/// <summary>
/// Represents errors that occur when the length of data in a cryptographic operation is invalid or incorrect.
/// </summary>
public class DataLengthException : CryptoException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataLengthException"/> class.
    /// </summary>
    public DataLengthException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataLengthException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public DataLengthException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataLengthException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    public DataLengthException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
