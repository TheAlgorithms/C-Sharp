using NUnit.Framework;

namespace Algorithms.Tests.Search;

public class InterpolationSearchTest
{
    [Test]
    public void Interpolation_Search_Test()
    {
        int[] values = { 1, 3, 5, 8, 10, 22, 31, 35, 37, 42, 51 };
        var key = 22;
        var l = 0;
        var r = values.Length - 1;

        while (key >= values[l] && key <= values[r] && l <= r)
        {
            int seek = l + (r - l) * (key - values[l]) / (values[r] - values[l]);
            if (values[seek] == key)
            {
                Assert.AreEqual(values[seek], key);
            }

            if (values[seek] < key)
            {
                l = seek + 1;
            }
            else
            {
                r = seek - 1;
            }
        }
    }

    [Test]
    public void Interpolation_Search_Not_Found_Test()
    {
        int[] values = { 1, 3, 5, 8, 10, 22, 31, 35, 37, 42, 51 };
        var key = 100;
        var l = 0;
        var r = values.Length - 1;

        int value = -1;

        while (key >= values[l] && key <= values[r] && l <= r)
        {
            int seek = l + (r - l) * (key - values[l]) / (values[r] - values[l]);
            value = values[seek];
            if (value == key)
            {
                Assert.AreEqual(values[seek], key);
            }

            if (value < key)
            {
                l = seek + 1;
            }
            else
            {
                r = seek - 1;
            }
        }

        Assert.AreNotEqual(key, value);
    }
}
