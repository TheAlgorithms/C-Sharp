using System.Collections.Generic;
using System.Linq;
using Algorithms.Encoders;
using NUnit.Framework;

namespace Algorithms.Tests.Encoders;

public class NysiisEncoderTests
{
    private static readonly string[] Names =
    {
        "Jay", "John", "Jane", "Zayne", "Guerra", "Iga", "Cowan", "Louisa", "Arnie", "Olsen", "Corban", "Nava",
        "Cynthia Malone", "Amiee MacKee", "MacGyver", "Yasmin Edge",
    };

    private static readonly string[] Expected =
    {
        "JY", "JAN", "JAN", "ZAYN", "GAR", "IG", "CAN", "LAS", "ARNY", "OLSAN", "CARBAN", "NAV", "CYNTANALAN",
        "ANANACY", "MCGYVAR", "YASNANADG",
    };

    private static IEnumerable<string[]> TestData => Names.Zip(Expected, (l, r) => new[] { l, r });

    [TestCaseSource(nameof(TestData))]
    public void AttemptNysiis(string source, string expected)
    {
        var enc = new NysiisEncoder();
        var nysiis = enc.Encode(source);
        Assert.That(nysiis, Is.EqualTo(expected));
    }
}
