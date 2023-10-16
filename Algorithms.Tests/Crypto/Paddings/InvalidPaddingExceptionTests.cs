using System;
using Algorithms.Crypto.Paddings;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Crypto.Paddings;


public class InvalidPaddingExceptionTests
{
    [Test]
    public void InvalidPaddingException_NoArgs_CreatesInstance()
    {
        var exception = new InvalidPaddingException();
        exception.Should().NotBeNull();
    }

    [Test]
    public void InvalidPaddingException_WithMessage_CreatesInstanceWithMessage()
    {
        var message = "Invalid padding detected.";
        var exception = new InvalidPaddingException(message);
        exception.Message.Should().Be(message);
    }

    [Test]
    public void InvalidPaddingException_WithInnerException_CreatesInstanceWithInnerException()
    {
        var innerException = new Exception("Inner exception message.");
        var exception = new InvalidPaddingException("Invalid padding detected.", innerException);
        exception.InnerException.Should().Be(innerException);
    }

}
