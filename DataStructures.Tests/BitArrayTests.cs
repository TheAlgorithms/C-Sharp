using System;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests;

/// <summary>
///     This class contains some tests for the class BitArray.
/// </summary>
public static class BitArrayTests
{
    [Test]
    public static void TestIndexer()
    {
        // Arrange
        var testObj = new BitArray(5);

        // Act
        testObj.Compile(24);

        // Assert
        Assert.That(testObj[0], Is.True);
        Assert.That(testObj[1], Is.True);
        Assert.That(testObj[3], Is.False);
    }

    [TestCase(19, 3)]
    public static void TestNumberOfOneBits(int number, int expected)
    {
        // Arrange
        var testObj = new BitArray(5);

        // Act
        testObj.Compile(number);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.NumberOfOneBits()));
    }

    [TestCase(26, 2)]
    public static void TestNumberOfZeroBits(int number, int expected)
    {
        // Arrange
        var testObj = new BitArray(5);

        // Act
        testObj.Compile(number);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.NumberOfZeroBits()));
    }

    [TestCase(33, 33)]
    public static void TestToInt64(int number, int expected)
    {
        // Arrange
        var testObj = new BitArray(6);

        // Act
        testObj.Compile(number);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToInt64()));
    }

    [Test]
    public static void TestToInt32MaxValue()
    {
        // Arrange
        var testObj = new BitArray(33);

        // Act

        // Assert
        _ = Assert.Throws<InvalidOperationException>(() => testObj.ToInt32());
    }

    [Test]
    public static void TestToInt64MaxValue()
    {
        // Arrange
        var testObj = new BitArray(65);

        // Act

        // Assert
        _ = Assert.Throws<InvalidOperationException>(() => testObj.ToInt64());
    }

    [TestCase("110")]
    public static void TestResetField(string sequence)
    {
        // Arrange
        var testObj = new BitArray(sequence);

        // Act
        testObj.ResetField();

        // Assert
        Assert.That(0, Is.EqualTo(testObj.ToInt64()));
    }

    [TestCase("101001", 63)]
    public static void TestSetAll(string sequence, int expected)
    {
        // Arrange
        var testObj = new BitArray(sequence);

        // Act
        testObj.SetAll(true);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToInt64()));
    }

    [Test]
    public static void TestCloneEquals()
    {
        // Arrange
        var testObj1 = new BitArray("110");

        // Act
        var testObj2 = (BitArray)testObj1.Clone();

        // Assert
        Assert.That(testObj1.Equals(testObj2), Is.True);
    }

    [Test]
    public static void TestCloneNotEquals()
    {
        // Arrange
        var testObj1 = new BitArray("101");
        var testObj2 = new BitArray(15);
        var testObj3 = new BitArray(3);

        // Act
        testObj3.Reset();

        // Assert
        testObj1.Equals(testObj2).Should().BeFalse();
        testObj1.Equals(testObj3).Should().BeFalse();
    }

    [Test]
    public static void TestHasCode()
    {
        // Arrange
        const int num = 5;
        var testObj = new BitArray(3);

        // Act
        testObj.Compile(num);
        var result = testObj.GetHashCode();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(5, Is.EqualTo(result));
    }

    [Test]
    public static void TestMoveNextCurrent()
    {
        var testObj1 = new BitArray("1111010");

        var counterOnes = 0;
        var counterZeros = 0;

        foreach (var bit in testObj1)
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

        Assert.That(counterOnes, Is.EqualTo(5));
        Assert.That(counterZeros, Is.EqualTo(2));
    }

    [Test]
    public static void IEnumerable_IterationWorks()
    {
        var arr = new BitArray("010101010101010101");
        var current = 0;
        foreach (var b in arr)
        {
            b.Should().Be(arr[current]);
            current++;
        }
    }

    [Test]
    public static void Equals_NullIsNotEqualToNotNull()
    {
        var arr1 = new BitArray("010101010101010101");
        BitArray? arr2 = null;
        arr1.Equals(arr2).Should().BeFalse();
    }

    #region COMPILE TESTS

    [TestCase("00100", "00100")]
    [TestCase("01101", "01101")]
    [TestCase("100", "00100")]
    public static void TestCompileToString(string sequence, string expectedSequence)
    {
        // Arrange
        var testObj = new BitArray(5);

        // Act
        testObj.Compile(sequence);

        // Assert
        Assert.That(expectedSequence, Is.EqualTo(testObj.ToString()));
    }

    [TestCase("klgml", 5)]
    [TestCase("klgml", 3)]
    public static void TestCompileToStringThorwsException(string sequence, int arrLen)
    {
        // Arrange
        var testObj = new BitArray(arrLen);

        // Act
        void Act() => testObj.Compile(sequence);

        // Assert
        Assert.Throws<ArgumentException>(Act);
    }

    [TestCase(15, "01111")]
    [TestCase(17, "10001")]
    [TestCase(4, "00100")]
    public static void TestCompileLong(int number, string expected)
    {
        // Arrange
        var testObj = new BitArray(5);

        // Act
        testObj.Compile((long)number);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToString()));
    }

    [TestCase(46, 3)]
    [TestCase(-46, 5)]
    public static void TestCompileLongThrowsException(int number, int arrLen)
    {
        // Arrange
        var testObj = new BitArray(arrLen);

        // Act
        void Act() => testObj.Compile((long)number);

        // Assert
        Assert.Throws<ArgumentException>(Act);
    }

    [TestCase(17, "10001")]
    [TestCase(25, "11001")]
    [TestCase(4, "00100")]
    public static void TestCompileInteger(int number, string expected)
    {
        // Arrange
        var testObj = new BitArray(5);

        // Act
        testObj.Compile(number);

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToString()));
    }

    [TestCase(-8, 5)]
    [TestCase(18, 3)]
    public static void TestCompileIntegerThrowsException(int number, int arrayLength)
    {
        // Arrange
        var testObj = new BitArray(arrayLength);

        // Act
        void Act() => testObj.Compile(number);

        // Assert
        Assert.Throws<ArgumentException>(Act);
    }

    #endregion COMPILE TESTS

    #region CONSTRUCTOR TESTS

    [TestCase("00100", 4)]
    public static void TestConstructor(string sequence, int expected)
    {
        // Arrange
        var testObj1 = new BitArray(sequence);

        // Act

        // Assert
        Assert.That(expected, Is.EqualTo(testObj1.ToInt64()));
    }

    [TestCase(new[] { true, false, true }, 5)]
    public static void TestConstructorBoolArray(bool[] sequence, int expected)
    {
        // Arrange
        var testObj3 = new BitArray(sequence);

        // Act

        // Assert
        Assert.That(expected, Is.EqualTo(testObj3.ToInt64()));
    }

    [TestCase("000120")]
    [TestCase("")]
    public static void TestConstructorThrowsException(string sequence)
    {
        // Arrange

        // Act
        Action act = () => new BitArray(sequence);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    #endregion CONSTRUCTOR TESTS

    #region OPERATOR TESTS

    [TestCase(17, 17, "10001")]
    [TestCase(25, 31, "11001")]
    public static void TestOperatorAnd(int tObj1, int tObj2, string expected)
    {
        // Arrange
        var testObj1 = new BitArray(5);
        var testObj2 = new BitArray(5);

        // Act
        testObj1.Compile(tObj1);
        testObj2.Compile(tObj2);

        var result = testObj1 & testObj2;

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToString()));
    }

    [TestCase(1, 1, 1, 1, "0")]
    [TestCase(5, 3, 8, 4, "1101")]
    [TestCase(9, 4, 4, 3, "1101")]
    public static void TestOperatorXorAndDiffSizes(int t1, int s1, int t2, int s2, string expected)
    {
        // Arrange
        var testObj1 = new BitArray(s1);
        var testObj2 = new BitArray(s2);

        // Act
        testObj1.Compile(t1);
        testObj2.Compile(t2);
        var result = testObj1 ^ testObj2;

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToString()));
    }

    [TestCase(9, 4, 4, 3, "1101")]
    [TestCase(1, 1, 1, 1, "1")]
    [TestCase(5, 3, 8, 4, "1101")]
    public static void TestOperatorOrAndDiffSizes(int t1, int s1, int t2, int s2, string expected)
    {
        // Arrange
        var testObj1 = new BitArray(s1);
        var testObj2 = new BitArray(s2);

        // Act
        testObj1.Compile(t1);
        testObj2.Compile(t2);
        var result = testObj1 | testObj2;

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToString()));
    }

    [TestCase(1, 1, 1, 1, "1")]
    [TestCase(5, 3, 8, 4, "0000")]
    [TestCase(9, 4, 4, 3, "0000")]
    public static void TestOperatorAndAndDiffSizes(int t1, int s1, int t2, int s2, string expected)
    {
        // Arrange
        var testObj1 = new BitArray(s1);
        var testObj2 = new BitArray(s2);

        // Act
        testObj1.Compile(t1);
        testObj2.Compile(t2);
        var result = testObj1 & testObj2;

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToString()));
    }

    [TestCase(25, 30, "11111")]
    public static void TestOperatorOr(int tObj1, int tObj2, string expected)
    {
        // Arrange
        var testObj1 = new BitArray(5);
        var testObj2 = new BitArray(5);

        // Act
        testObj1.Compile(tObj1);
        testObj2.Compile(tObj2);

        var result = testObj1 | testObj2;

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToString()));
    }

    [TestCase(16, "01111")]
    public static void TestOperatorNot(int number, string expected)
    {
        // Arrange
        var testObj = new BitArray(5);

        // Act
        testObj.Compile(number);
        testObj = ~testObj;

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToString()));
    }

    [TestCase(25, 30, 7)]
    public static void TestOperatorXor(int testNum, int testNum2, int expected)
    {
        // Arrange
        var testObj1 = new BitArray(5);
        var testObj2 = new BitArray(5);

        // Act
        testObj1.Compile(testNum);
        testObj2.Compile(testNum2);

        var result = testObj1 ^ testObj2;

        // Assert
        Assert.That(expected, Is.EqualTo(result.ToInt32()));
    }

    [TestCase(16, "10000000")]
    public static void TestOperatorShiftLeft(int number, string expected)
    {
        // Arrange
        var testObj = new BitArray(5);

        // Act
        testObj.Compile(number);
        testObj <<= 3;

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToString()));
    }

    [TestCase(24, "110")]
    public static void TestOperatorShiftRight(int number, string expected)
    {
        // Arrange
        var testObj = new BitArray(5);

        // Act
        testObj.Compile(number);
        testObj >>= 2;

        // Assert
        Assert.That(expected, Is.EqualTo(testObj.ToString()));
    }

    #endregion OPERATOR TESTS

    #region COMPARE TESTS

    [Test]
    public static void TestParity()
    {
        // Arrange
        var testObj = new BitArray(5);

        // Act
        testObj.Compile(26);

        // Assert
        Assert.That(testObj.EvenParity(), Is.False);
        Assert.That(testObj.OddParity(), Is.True);
    }

    [Test]
    public static void TestCompare()
    {
        // Arrange
        var testObj1 = new BitArray("110");
        var testObj2 = new BitArray("110");
        var testObj3 = new BitArray("100");

        // Act

        // Assert
        Assert.That(testObj1 == testObj2, Is.True);
        Assert.That(testObj1 != testObj3, Is.True);
    }

    [Test]
    public static void ArraysOfDifferentLengthsAreNotEqual()
    {
        // Arrange
        var testObj1 = new BitArray("110");
        var testObj2 = new BitArray("10101");

        // Act

        // Assert
        Assert.That(testObj1 == testObj2, Is.False);
    }

    #endregion COMPARE TESTS
}
