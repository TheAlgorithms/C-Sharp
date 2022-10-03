using Algorithms.Numeric;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Algorithms.Tests.Numeric
{
    public class CombinationsTests
    {
        [Test]
        public void CalculateCombinationsValid()
        {
            CombinationsCalculation.Calculate(100, 1).Should().Be(100);
            CombinationsCalculation.Calculate(1, 1).Should().Be(1);
            CombinationsCalculation.Calculate(20, 5).Should().Be(15504);
            CombinationsCalculation.Calculate(200, 5).Should().Be(2535650040L);
        }

        [Test]
        public void CalculateCombinationsNegativeSet()
        {
            Action act = () => CombinationsCalculation.Calculate(-1, 1);

            act.Should().Throw<ArgumentException>()
                .WithMessage("Expected n to be non-negative. Actual n value: -1");
        }

        [Test]
        public void CalculateCombinationsNegativeSelections()
        {
            Action act = () => CombinationsCalculation.Calculate(1, -1);

            act.Should().Throw<ArgumentException>()
                .WithMessage("Expected k to be non-negative. Actual k value: -1");
        }

        [Test]
        public void CalculateCombinationsMoreSelectionsThanSize()
        {
            Action act = () => CombinationsCalculation.Calculate(1, 2);

            act.Should().Throw<ArgumentException>()
                .WithMessage("Expected n to be greater than or equal to k. Actual values n:1, k:2");
        }
    }
}
