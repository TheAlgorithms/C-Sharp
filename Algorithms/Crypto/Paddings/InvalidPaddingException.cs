using System;

namespace Algorithms.Crypto.Paddings;

public class InvalidPaddingException : Exception
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
}
