using NUnit.Framework;
using System;
using V = Utilities.Extensions.VectorExtensions;

namespace Utilities.Tests
{
    public class VectorTests
    {
        [Test]
        public void VectorMagnitude()
        {
            Assert.AreEqual(Math.Sqrt(3), V.Magnitude(new double[] { 1, -1, 0, 1 }));
        }
    }
}