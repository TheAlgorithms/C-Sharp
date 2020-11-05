using System.Collections.Generic;
using Algorithms.Encoders;
using NUnit.Framework;
using System.Linq;

namespace Algorithms.Tests.Encoders
{
    public class NysiisEncoderTests
    {
        [TestCaseSource(nameof(TestData))]
        public void AttemptNysiis(string source, string expected)
        {
            var enc = new NysiisEncoder();
            var nysiis = enc.Encode(source);
            Assert.AreEqual(expected, nysiis);
        }
        
        static IEnumerable<string[]> TestData => names.Zip(expected, (l, r) => new[] { l, r });

        static string[] names = {
            "Jay", "John", "Jane", "Zayne", "Guerra",
            "Iga", "Cowan", "Louisa", "Arnie", "Olsen",
            "Corban", "Nava", "Cynthia Malone", "Amiee MacKee",
            "MacGyver", "Yasmin Edge"
        };
        static string[] expected = {
            "JY", "JAN", "JAN", "ZAYN", "GAR",
            "IG", "CAN", "LAS", "ARNY", "OLSAN",
            "CARBAN", "NAV", "CYNTANALAN", "ANANACY",
            "MCGYVAR", "YASNANADG"
        };
    }
}
