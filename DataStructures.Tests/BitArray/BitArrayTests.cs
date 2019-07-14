using System;
using NUnit.Framework;

namespace DataStructures.Tests.BitArray
{
    /// <summary>
    /// This class contains some tests for the class BitArray.
    /// </summary>
    public static class BitArrayTests
    {
        [Test]
        [TestCase("00100", "00100")]
        [TestCase("01101", "01101")]
        [TestCase("100", "00100")]
        public static void TestCompileToString(string sequence, string expectedSequence)
        {
            // Arrange
            var testObj = new DataStructures.BitArray.BitArray(5);

            // Act
            testObj.Compile(sequence);

            // Assert
            Assert.AreEqual(expectedSequence, testObj.ToString());
        }

        [Test]
        [TestCase("klgml", "010111111")]
        public static void TestCompileToStringThorwsException(string sequence, string expectedSequence)
        {
            // Arrange
            var testObj = new DataStructures.BitArray.BitArray(5);

            // Act

            // Assert
            _ = Assert.Throws<Exception>(() => testObj.Compile(sequence));
        }

        [Test]
        [TestCase("00100", 4)]
        public static void TestConstructor(string sequence, int expected)
        {
            // Arrange
            var testObj1 = new DataStructures.BitArray.BitArray(sequence);

            // Act

            // Assert
            Assert.AreEqual(expected, testObj1.ToInt64());
        }
        
        [Test]
        [TestCase(new[] { true, false, true }, 5)]
        public static void TestConstructorBoolArray(bool[] sequence, int expected)
        {
            // Arrange
            var testObj3 = new DataStructures.BitArray.BitArray(sequence);

            // Act

            // Assert
            Assert.AreEqual(expected, testObj3.ToInt64());
        }

        [Test]
        [TestCase("000120")]
        public static void TestConstructorThrowsException(string sequence)
        {
            // Arrange

            // Act

            // Assert
            _ = Assert.Throws<Exception>(() =>
              {
                  _ = new DataStructures.BitArray.BitArray(sequence);
              });
        }

        [Test]
        [TestCase(17, "10001")]
        [TestCase(25, "11001")]
        [TestCase(4, "00100")]
        public static void TestCompileInteger(int number, string expected)
        {
            // Arrange
            var testObj = new DataStructures.BitArray.BitArray(5);

            // Act
            testObj.Compile(number);

            // Assert
            Assert.AreEqual(expected, testObj.ToString());
        }

        [Test]
        [TestCase(-8, "Compile: only positive numbers > 0", 5)]
        [TestCase(18, "Compile: not apt length!", 3)]
        public static void TestCompileIntegerThrowsException(int number, string expectedErrorMsg, int arrayLength)
        {
            // Arrange
            var testObj = new DataStructures.BitArray.BitArray(arrayLength);

            // Act

            // Assert
            var ex = Assert.Throws<Exception>(() => testObj.Compile(number));
            Assert.AreEqual(expectedErrorMsg, ex.Message);
        }

        [Test]
        [TestCase(17, 17, "10001")]
        [TestCase(25, 31, "11001")]
        public static void TestOperatorAnd(int tObj1, int tObj2, string expected)
        {
            // Arrange
            var testObj1 = new DataStructures.BitArray.BitArray(5);
            var testObj2 = new DataStructures.BitArray.BitArray(5);

            // Act
            testObj1.Compile(tObj1);
            testObj2.Compile(tObj2);

            var result = testObj1 & testObj2;

            // Assert
            Assert.AreEqual(expected, result.ToString());
        }

        [Test]
        [TestCase(25, 30, "11111")]
        public static void TestOperatorOr(int tObj1, int tObj2, string expected)
        {
            // Arrange
            var testObj1 = new DataStructures.BitArray.BitArray(5);
            var testObj2 = new DataStructures.BitArray.BitArray(5);

            // Act
            testObj1.Compile(tObj1);
            testObj2.Compile(tObj2);

            var result = testObj1 | testObj2;

            // Assert
            Assert.AreEqual(expected, result.ToString());
        }

        [Test]
        [TestCase(16, "01111")]
        public static void TestOperatorNot(int number, string expected)
        {
            // Arrange
            var testObj = new DataStructures.BitArray.BitArray(5);

            // Act
            testObj.Compile(number);
            testObj = ~testObj;

            // Assert
            Assert.AreEqual(expected, testObj.ToString());
        }

        [Test]
        [TestCase(16, "10000000")]
        public static void TestOperatorShiftLeft(int number, string expected)
        {
            // Arrange
            var testObj = new DataStructures.BitArray.BitArray(5);

            // Act
            testObj.Compile(number);
            testObj <<= 3;

            // Assert
            Assert.AreEqual(expected, testObj.ToString());
        }

        [Test]
        [TestCase(24, "110")]
        public static void TestOperatorShiftRight(int number, string expected)
        {
            // Arrange
            var testObj = new DataStructures.BitArray.BitArray(5);

            // Act
            testObj.Compile(number);
            testObj >>= 2;

            // Assert
            Assert.AreEqual(expected, testObj.ToString());
        }

        [Test]
        [TestCase(25, 30, 7)]
        public static void TestOperatorXor(int testNum, int testNum2, int expected)
        {
            // Arrange
            var testObj1 = new DataStructures.BitArray.BitArray(5);
            var testObj2 = new DataStructures.BitArray.BitArray(5);

            // Act
            testObj1.Compile(testNum);
            testObj2.Compile(testNum2);

            var result = testObj1 ^ testObj2;

            // Assert
            Assert.AreEqual(expected, result.ToInt32());
        }

        [Test]
        public static void TestIndexer()
        {
            // Arrange
            var testObj = new DataStructures.BitArray.BitArray(5);

            // Act
            testObj.Compile(24);

            // Assert
            Assert.IsTrue(testObj[0]);
            Assert.IsTrue(testObj[1]);
            Assert.IsFalse(testObj[3]);
        }

        [Test]
        [TestCase(19, 3)]
        public static void TestNumberOfOneBits(int number, int expected)
        {
            // Arrange
            var testObj = new DataStructures.BitArray.BitArray(5);

            // Act
            testObj.Compile(number);

            // Assert
            Assert.AreEqual(expected, testObj.NumberOfOneBits());
        }

        [Test]
        [TestCase(26, 2)]
        public static void TestNumberOfZeroBits(int number, int expected)
        {
            // Arrange
            var testObj = new DataStructures.BitArray.BitArray(5);

            // Act
            testObj.Compile(number);

            // Assert
            Assert.AreEqual(expected, testObj.NumberOfZeroBits());
        }

        [Test]
        public static void TestParity()
        {
            // Arrange
            var testObj = new DataStructures.BitArray.BitArray(5);

            // Act
            testObj.Compile(26);

            // Assert
            Assert.IsFalse(testObj.EvenParity());
            Assert.IsTrue(testObj.OddParity());
        }

        [Test]
        [TestCase(33, 33)]
        public static void TestToInt64(int number, int expected)
        {
            // Arrange
            var testObj = new DataStructures.BitArray.BitArray(6);

            // Act
            testObj.Compile(33);

            // Assert
            Assert.AreEqual(testObj.ToInt64(), 33);
        }

        [Test]
        public static void TestCompare()
        {
            // Arrange
            var testObj1 = new DataStructures.BitArray.BitArray("110");
            var testObj2 = new DataStructures.BitArray.BitArray("110");
            var testObj3 = new DataStructures.BitArray.BitArray("100");

            // Act

            // Assert
            Assert.IsTrue(testObj1 == testObj2);
            Assert.IsTrue(testObj1 != testObj3);
        }

        [Test]
        public static void TestCompareThrowsException()
        {
            // Arrange
            var testObj1 = new DataStructures.BitArray.BitArray("110");
            var testObj2 = new DataStructures.BitArray.BitArray("10101");

            // Act

            // Assert
            _ = Assert.Throws<Exception>(() => Assert.IsTrue(testObj1 == testObj2));
        }

        [Test]
        [TestCase("110")]
        public static void TestResetField(string sequence)
        {
            // Arrange
            var testObj = new DataStructures.BitArray.BitArray(sequence);

            // Act
            testObj.ResetField();

            // Assert
            Assert.AreEqual(0, testObj.ToInt64());
        }

        [Test]
        [TestCase("101001", 63)]
        public static void TestSetAll(string sequence, int expected)
        {
            // Arrange
            var testObj = new DataStructures.BitArray.BitArray(sequence);

            // Act
            testObj.SetAll(true);

            // Assert
            Assert.AreEqual(expected, testObj.ToInt64());
        }

        [Test]
        public static void TestCompareTo()
        {
            // Arrange
            var testObj1 = new DataStructures.BitArray.BitArray("110");
            var testObj2 = new DataStructures.BitArray.BitArray("110");
            var testObj3 = new DataStructures.BitArray.BitArray("100");

            // Act

            // Assert
            Assert.AreEqual(testObj1.CompareTo(testObj3), 1);
            Assert.AreEqual(testObj3.CompareTo(testObj1), -1);
            Assert.AreEqual(testObj1.CompareTo(testObj2), 0);
        }

        [Test]
        public static void TestCloneEquals()
        {
            // Arrange
            var testObj1 = new DataStructures.BitArray.BitArray("110");

            // Act
            var testObj2 = (DataStructures.BitArray.BitArray)testObj1.Clone();

            // Assert
            Assert.IsTrue(testObj1.Equals(testObj2));
        }

        [Test]
        public static void TestMoveNextCurrent()
        {
            var testObj1 = new DataStructures.BitArray.BitArray("1111010");

            var counterOnes = 0;
            var counterZeros = 0;

            foreach (bool bit in testObj1)
            {
                if (bit)
                {
                    counterOnes++;
                }
                else
                {
                    counterZeros++;
                }
            }

            Assert.AreEqual(counterOnes, 5);
            Assert.AreEqual(counterZeros, 2);
        }
    }
}
