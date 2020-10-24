using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

using Algorithms.Problems.Fibonacci;

using NUnit.Framework;

namespace Algorithms.Tests.Problems.FibonacciTests
{
    public static class FibonacciMemoTests
    {
        [TestCase(-1)]
        public static void NegativeNumberTest(int n)
        {
            Assert.That(() => FibonacciMemo.GetFibonacciNumberMemo(n), Throws.TypeOf<ArgumentException>());
        }


        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        [TestCase(5, 5)]
        [TestCase(6, 8)]
        [TestCase(15, 610)]
        public static void NumberTests(int n, int expectedResult)
        {
            Assert.AreEqual((BigInteger)expectedResult, FibonacciMemo.GetFibonacciNumberMemo(n));
        }
    }
}
