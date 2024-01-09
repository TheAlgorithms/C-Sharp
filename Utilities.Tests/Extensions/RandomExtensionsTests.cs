using System;
using FluentAssertions;
using NUnit.Framework;
using Utilities.Extensions;

namespace Utilities.Tests.Extensions;

public class RandomExtensionsTests
{
    [Test]
    public void NextVector_ShouldReturnNormalizedVector()
    {
        var random = new Random(0);

        var result = random.NextVector(10);

        result.Length.Should().Be(10);
        result.Magnitude().Should().BeApproximately(1.0, 1e-6);
    }
}
