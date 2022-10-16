using System.Linq;
using Algorithms.Search;
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
        var selectedIndex = arrayToSearch[random.Next(0, arrayToSearch.Length - 1)];

        //Act
        var value = search.FindIndex(arrayToSearch, selectedIndex);

        //Assert
        Assert.AreNotEqual(-1, value);
    }

    [Test]
    public void Interpolation_Find_Index_Not_Found_Test([Random(2, 1000, 100)] int n)
    {
        // Arrange
        var search = new InterpolationSearch();
        var random = Randomizer.CreateRandomizer();
        var arrayToSearch = Enumerable.Range(0, n).Select(_ => random.Next(0, 1000)).OrderBy(x => x).ToArray();

        //Act
        var value = search.FindIndex(arrayToSearch, 1001);

        //Assert
        Assert.AreEqual(-1, value);
    }
}
