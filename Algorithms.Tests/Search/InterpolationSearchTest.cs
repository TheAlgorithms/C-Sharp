using System.Linq;
using Algorithms.Search;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Search;

public class InterpolationSearchTest
{
    [Test]
    public void Interpolation_Find_Index_Test([Random(1, 1000, 100)] int n)
    {
        // Arrange
        var search = new InterpolationSearch();
        var random = Randomizer.CreateRandomizer();
        var arrayToSearch = Enumerable.Range(0, n).Select(_ => random.Next(0, 1000)).OrderBy(x => x).ToArray();

        var index = random.Next(0, arrayToSearch.Length - 1);
        var selectedValue = arrayToSearch[index];

        //Act
        int indexFound = search.FindValue(arrayToSearch, selectedValue);

        //Assert
        indexFound.Should().BeGreaterOrEqualTo(0);
    }

    [Test]
    public void Interpolation_Find_Index_Not_Found_Test([Random(1, 1000, 100)] int n)
    {
        // Arrange
        var search = new InterpolationSearch();
        var random = Randomizer.CreateRandomizer();
        var arrayToSearch = Enumerable.Range(0, n).Select(_ => random.Next(0, 1000)).OrderBy(x => x).ToArray();
        var index = random.Next(0, arrayToSearch.Length - 1);
        var selectedValue = 1001;

        //Act
        int res = search.FindValue(arrayToSearch, selectedValue);

        //Assert
        res.Should().BeLessThan(0);
    }
}
