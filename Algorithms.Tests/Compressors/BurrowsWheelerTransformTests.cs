using Algorithms.DataCompression;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Compressors;

public class BurrowsWheelerTransformTests
{
    [TestCase("banana", "nnbaaa", 3)]
    [TestCase("SIX.MIXED.PIXIES.SIFT.SIXTY.PIXIE.DUST.BOXES", "TEXYDST.E.IXIXIXXSSMPPS.B..E.S.EUSFXDIIOIIIT", 29)]
    [TestCase("", "", 0)]
    public void Encode(string input, string expectedString, int expectedIndex)
    {
        var bwt = new BurrowsWheelerTransform();

        var (encoded, index) = bwt.Encode(input);

        Assert.That(encoded, Is.EqualTo(expectedString));
        Assert.That(index, Is.EqualTo(expectedIndex));
    }

    [TestCase("nnbaaa", 3, "banana")]
    [TestCase("TEXYDST.E.IXIXIXXSSMPPS.B..E.S.EUSFXDIIOIIIT", 29, "SIX.MIXED.PIXIES.SIFT.SIXTY.PIXIE.DUST.BOXES")]
    [TestCase("", 0, "")]
    public void Decode(string encoded, int index, string expected)
    {
        var bwt = new BurrowsWheelerTransform();

        var result = bwt.Decode(encoded, index);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    [Repeat(100)]
    public void RandomEncodeDecode()
    {
        var bwt = new BurrowsWheelerTransform();
        var random = new Randomizer();
        var inputString = random.GetString();

        var (encoded, index) = bwt.Encode(inputString);
        var result = bwt.Decode(encoded, index);

        Assert.That(result, Is.EqualTo(inputString));
    }
}
