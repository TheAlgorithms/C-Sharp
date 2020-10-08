using System;
using Algorithms.Encoders;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Encoders
{
    public static class NYSIISTests
    {
        [Test]
        public static void AttemptNYSIIS()
        {
            string[] names = new string[] {
                "Jay", "John", "Jane", "Zayne", "Guerra",
                "Iga", "Cowan", "Louisa", "Arnie", "Olsen",
                "Corban", "Nava", "Cynthia Malone", "Amiee MacKee",
                "MacGyver", "Yasmin Edge"
            };
            string[] expected = new string[] {
                "JY", "JAN", "JAN", "ZAYN", "GAR",
                "IG", "CAN", "LAS", "ARNY", "OLSAN",
                "CARBAN", "NAV", "CYNTANALAN", "ANANACY",
                "MCGYVAR", "YASNANADG"
            };

            NYSIISEncoder enc = new NYSIISEncoder();
            names.Enumerate((name, ind) => {
                string nysiis = enc.Encode(name);
                Assert.AreEqual(nysiis, expected[ind]);
            });
        }
    }
}
