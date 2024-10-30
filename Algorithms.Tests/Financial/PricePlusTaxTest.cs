using Algorithms.Financial;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Financial
{
    [TestFixture]

    public class PricePlusTaxTest
    {
        [Test]
        public void Price_Plus_Tax_Test()
        {
            PricePlusTax.Calculate(125.50m, 0.05m).Should().Be(131.775m);
            PricePlusTax.Calculate(125.50f, 0.05f).Should().Be(131.775f);

            PricePlusTax.Calculate(100.0m, 0.25m).Should().Be(125.0m);
            PricePlusTax.Calculate(100.0f, 0.25f).Should().Be(125.0f);

            PricePlusTax.Calculate(0, 0.25).Should().Be(0);
            PricePlusTax.Calculate(0.0f, 0.25f).Should().Be(0.0f);
            PricePlusTax.Calculate(0.0m, 0.25m).Should().Be(0.0m);
        }
    }
}
