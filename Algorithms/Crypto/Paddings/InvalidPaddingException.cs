using System;
using System.Runtime.Serialization;

namespace Algorithms.Crypto.Paddings;

public class InvalidPaddingException : Exception, ISerializable
{
    public InvalidPaddingException()
    {
    }

    public InvalidPaddingException(string? message)
        : base(message)
    {
    }

    public InvalidPaddingException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    protected InvalidPaddingException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
