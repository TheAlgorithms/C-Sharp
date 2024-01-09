using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests;

public class InvertedIndexTests
{
    [Test]
    public void Or_GetSourcesWithAtLeastOneFromList_ReturnAllSources()
    {
        var index = new InvertedIndex();
        var source1 = "one star is sparkling bright";
        var source2 = "two stars are sparkling even brighter";
        index.AddToIndex(nameof(source1), source1);
        index.AddToIndex(nameof(source2), source2);

        var or = index.Or(new List<string> { "star", "sparkling" });

        or.Should().BeEquivalentTo(nameof(source1), nameof(source2));
    }

    [Test]
    public void And_GetSourcesWithAllInsideList_ReturnFirstSource()
    {
        var index = new InvertedIndex();
        var source1 = "one star is sparkling bright";
        var source2 = "two stars are sparkling even brighter";
        index.AddToIndex(nameof(source1), source1);
        index.AddToIndex(nameof(source2), source2);

        var and = index.And(new List<string> { "star", "sparkling" });

        and.Should().BeEquivalentTo(nameof(source1));
    }
}
