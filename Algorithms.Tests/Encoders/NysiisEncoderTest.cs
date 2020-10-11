using System;
using Algorithms.Encoders;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Encoders
{
    public static class NysiisEncoderTest
    {
        [Test]
        public static void AttemptNysiis()
        {
            string[] names = {
                "Jay", "John", "Jane", "Zayne", "Guerra",
                "Iga", "Cowan", "Louisa", "Arnie", "Olsen",
                "Corban", "Nava", "Cynthia Malone", "Amiee MacKee",
                "MacGyver", "Yasmin Edge"
            };
            string[] expected = {
                "JY", "JAN", "JAN", "ZAYN", "GAR",
                "IG", "CAN", "LAS", "ARNY", "OLSAN",
                "CARBAN", "NAV", "CYNTANALAN", "ANANACY",
                "MCGYVAR", "YASNANADG"
            };

            NysiisEncoder enc = new NysiisEncoder();
            names.Enumerate((name, ind) => {
                string nysiis = enc.Encode(name);
                Assert.AreEqual(nysiis, expected[ind]);
            });
        }
    }
}
