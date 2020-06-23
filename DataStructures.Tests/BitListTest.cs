using NUnit.Framework;
using System.Drawing;
using System.IO;

namespace DataStructures.Tests
{
    public static class BitListTests
    {
        [Test]
        [TestCase(null)]
        public static void ReadTest(string filePath)
        {
            if (filePath == null)
                Assert.Ignore();
            else if (!File.Exists(filePath))
                Assert.Ignore("File not found.");
            else
            {
                var result = BitList.Read(filePath);
                Assert.IsTrue(result.ToByteArray() == File.ReadAllBytes(filePath));
            }
        }

        [Test]
        [TestCase(null)]
        public static void WriteTest(string filePath)
        {
            if (filePath == null)
                Assert.Ignore();
            else
            {
                new BitList("Hello world!").Write(filePath);
                Assert.IsTrue(File.ReadAllText(filePath) == "Hello world!");
            }
        }

        [Test]
        public static void IsEmptyTest()
        {
            Assert.IsTrue(new BitList().IsEmpty);
        }

        [Test]
        public static void CountTest()
        {
            Assert.IsTrue(new BitList(true).Count == 1);
        }

        [Test]
        public static void IndexerGetTest() //bool this[int index]
        {
            Assert.IsTrue(new BitList(true)[0]);
        }

        [Test]
        public static void IndexerSetTest() //bool this[int index]
        {
            Assert.IsTrue(new BitList(false) { [0] = true }[0]);
        }

        [Test]
        public static void IndexerInt32GetTest() //BitList this[int index, int count]
        {
            Assert.IsTrue(new BitList(new[] { true, true })[0, 2].And());
        }

        [Test]
        public static void IndexerInt32SetTest() //BitList this[int index, int count]
        {
            var bitList = new BitList(new[] { true, true, true });
            bitList[0, 2] = new BitList(new[] { false, false });
            Assert.IsTrue(bitList == new BitList(new[] { false, false, true }));
        }

        [Test]
        public static void IndexOfBoolean() //int IndexOf(bool item)
        {
            Assert.IsTrue(new BitList(true).IndexOf(true) == 0);
        }

        [Test]
        public static void IndexOfBitList() //int IndexOf(BitList items)
        {
            Assert.IsTrue(new BitList(new[] { true, true }).IndexOf(new BitList(new[] { true, true })) == 0);
        }

        [Test]
        public static void EqualsTest()
        {
            Assert.IsTrue(new BitList(true).Equals(new BitList(true)));
        }

        [Test]
        public static void ContainsBooleanTest() //bool Contains(bool item)
        {
            Assert.IsTrue(new BitList(true).Contains(true));
        }

        [Test]
        public static void ContainsBitListTest() //bool Contains(BitList items)
        {
            Assert.IsTrue(new BitList(new[] { true, true }).Contains(new BitList(new[] { true, true })));
        }

        [Test]
        public static void LeftShiftTest()
        {
            var bits = new BitList(true);
            bits.LeftShift(1);
            Assert.IsTrue(bits == new BitList(new[] { true, false }));
        }

        [Test]
        public static void RightShiftTest()
        {
            var bits = new BitList(new[] { true, true });
            bits.RightShift(1);
            Assert.IsTrue(bits == new BitList(true));
        }

        [Test]
        public static void NotTest()
        {
            var bits = new BitList(false);
            bits.Not();
            Assert.IsTrue(bits == new BitList(true));
        }

        [Test]
        public static void AndTest() //bool And()
        {
            Assert.IsTrue(new BitList(new[] { true, true }).And());
        }

        [Test]
        public static void AndBitListTest() //void And(BitList bits)
        {
            var bits = new BitList(true);
            bits.And(new BitList(true));
            Assert.IsTrue(bits[0]);
        }

        [Test]
        public static void OrTest() //bool Or()
        {
            Assert.IsTrue(new BitList(new[] { true, false }).Or());
        }

        [Test]
        public static void OrBitListTest() //void Or(BitList bits)
        {
            var bits = new BitList(true);
            bits.Or(new BitList(false));
            Assert.IsTrue(bits[0]);
        }

        [Test]
        public static void XorTest() //bool Xor()
        {
            Assert.IsFalse(new BitList(new[] { true, true }).Xor());
        }

        [Test]
        public static void XorBitListTest() //void Xor(BitList bits)
        {
            var bits = new BitList(true);
            bits.Xor(new BitList(true));
            Assert.IsFalse(bits[0]);
        }

        [Test]
        public static void AddBitListTest() //void Add(BitList items)
        {
            var bits = new BitList() { new BitList(true) };
            Assert.IsTrue(bits.Count == 1 && bits[0]);
        }

        [Test]
        public static void AddBooleanTest() //void Add(bool item)
        {
            var bits = new BitList() { true };
            Assert.IsTrue(bits.Count == 1 && bits[0]);
        }

        [Test]
        public static void AddFirstBitListTest() //void AddFirst(BitList items)
        {
            var bits = new BitList(false);
            bits.AddFirst(new BitList(true));
            Assert.IsTrue(bits.Count == 2 && bits[0] && !bits[1]);
        }

        [Test]
        public static void AddFirstBoolean() //void AddFirst(bool item)
        {
            var bits = new BitList(false);
            bits.AddFirst(true);
            Assert.IsTrue(bits.Count == 2 && bits[0] && !bits[1]);
        }

        [Test]
        public static void RemoveAtTest()
        {
            var bits = new BitList(new[] { true, true, true });
            bits.RemoveAt(0);
            Assert.IsTrue(bits.Count == 2 && bits.And());
        }

        [Test]
        public static void RemoveRangeTest()
        {
            var bits = new BitList(new[] { true, true, true });
            bits.RemoveRange(0, 2);
            Assert.IsTrue(bits.Count == 1 && bits[0]);
        }

        [Test]
        public static void RemoveBooleanTest() //bool Remove(bool item)
        {
            var bits = new BitList(new[] { true, false, true });
            bits.Remove(false);
            Assert.IsTrue(bits.Count == 2 && bits.And());
        }

        [Test]
        public static void RemoveBitListTest() //bool Remove(BitList items)
        {
            var bits = new BitList(new[] { true, false, false, true, true });
            bits.Remove(new BitList(new[] { false, false, true }));
            Assert.IsTrue(bits.Count == 2 && bits.And());
        }

        [Test]
        public static void InsertBitListTest() //void Insert(int index, BitList items)
        {
            var bits = new BitList(new[] { true, true });
            bits.Insert(1, new BitList(new[] { false, false }));
            Assert.IsTrue(bits == new BitList(new[] { true, false, false, true }));
        }

        [Test]
        public static void InsertBooleanTest() //void Insert(int index, bool item)
        {
            var bits = new BitList(new[] { true, true });
            bits.Insert(1, false);
            Assert.IsTrue(bits == new BitList(new[] { true, false, true }));
        }

        [Test]
        public static void ReplaceTest()
        {
            var bits = new BitList(new[] { true, false, false, true, true });
            bits.Replace(new BitList(new[] { false, false, true }), new BitList(new[] { false, true, false }));
            Assert.IsTrue(bits == new BitList(new[] { true, false, true, false, true }));
        }

        [Test]
        public static void SetBooleanTest() //void Set(int index, bool value)
        {
            var bits = new BitList(new[] { false, true });
            bits.Set(0, true);
            Assert.IsTrue(bits == new BitList(new[] { true, true }));
        }

        [Test]
        public static void SetBitListTest() //void Set(int index, BitList value)
        {
            var bits = new BitList(new[] { false, false, true });
            bits.Set(0, new BitList(new[] { true, true }));
            Assert.IsTrue(bits == new BitList(new[] { true, true, true }));
        }

        [Test]
        public static void GetTest() //bool Get(int index)
        {
            Assert.IsFalse(new BitList(new[] { true, false, true }).Get(1));
        }

        [Test]
        public static void GetInt32Test() //BitList Get(int index, int count)
        {
            Assert.IsTrue(new BitList(new[] { false, true, true, false }).Get(1, 2) == new BitList(new[] { true, true }));
        }

        [Test]
        public static void ClearTest() //void Clear()
        {
            var bits = new BitList(true);
            bits.Clear();
            Assert.IsTrue(bits.IsEmpty);
        }

        [Test]
        public static void ToTextTest()
        {
            Assert.IsTrue(new BitList(new[] { true, false, true, false, true }).ToText() == "10101");
        }

        [Test]
        public static void ParseTest()
        {
            Assert.IsTrue(BitList.Parse("10101") == new BitList(new[] { true, false, true, false, true }));
        }

        [Test]
        public static void CopyToUnmanagedNonPrimitiveValueArrayTest() //void CopyTo<T>(T[] unmanagedNonPrimitiveValueArray, int arrayIndex)
        {
            var bits = new BitList(new Size(100, 50));
            var array = new Size[1];
            bits.CopyTo(array, 0);
            Assert.IsTrue(array.Length == 1 && array[0] == new Size(100, 50));
        }

        [Test]
        public static void CopyToBooleanArrayTest() //void CopyTo(bool[] array, int arrayIndex)
        {
            var bits = new BitList(true);
            var array = new bool[1];
            bits.CopyTo(array, 0);
            Assert.IsTrue(array.Length == 1 && array[0]);
        }

        [Test]
        public static void CopyToByteArrayTest() //void CopyTo(byte[] array, int arrayIndex)
        {
            var bits = new BitList((byte)1);
            var array = new byte[1];
            bits.CopyTo(array, 0);
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void CopyToInt16ArrayTest() //void CopyTo(byte[] array, int arrayIndex)
        {
            var bits = new BitList((short)1);
            var array = new short[1];
            bits.CopyTo(array, 0);
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void CopyToInt32ArrayTest() //void CopyTo(int[] array, int arrayIndex)
        {
            var bits = new BitList(1);
            var array = new int[1];
            bits.CopyTo(array, 0);
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void CopyToInt64ArrayTest() //void CopyTo(long[] array, int arrayIndex)
        {
            var bits = new BitList(1L);
            var array = new long[1];
            bits.CopyTo(array, 0);
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void CopySByteArrayTest() //void CopyTo(sbyte[] array, int arrayIndex)
        {
            var bits = new BitList((sbyte)1);
            var array = new sbyte[1];
            bits.CopyTo(array, 0);
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void CopyToUInt16ArrayTest() //void CopyTo(ushort[] array, int arrayIndex)
        {
            var bits = new BitList((ushort)1);
            var array = new ushort[1];
            bits.CopyTo(array, 0);
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void CopyToUInt32ArrayTest() //void CopyTo(uint[] array, int arrayIndex)
        {
            var bits = new BitList(1U);
            var array = new uint[1];
            bits.CopyTo(array, 0);
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void CopyUInt64ArrayTest() //void CopyTo(ulong[] array, int arrayIndex)
        {
            var bits = new BitList(1UL);
            var array = new ulong[1];
            bits.CopyTo(array, 0);
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void CopyToSingleArrayTest() //void CopyTo(float[] array, int arrayIndex)
        {
            var bits = new BitList(1F);
            var array = new float[1];
            bits.CopyTo(array, 0);
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void CopyToDoubleArrayTest() //void CopyTo(double[] array, int arrayIndex)
        {
            var bits = new BitList(1D);
            var array = new double[1];
            bits.CopyTo(array, 0);
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void CopyToDecimalArrayTest() //void CopyTo(decimal[] array, int arrayIndex)
        {
            var bits = new BitList(1M);
            var array = new decimal[1];
            bits.CopyTo(array, 0);
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void CopyToCharArrayTest() //void CopyTo(decimal[] array, int arrayIndex)
        {
            var bits = new BitList('a');
            var array = new char[1];
            bits.CopyTo(array, 0);
            Assert.IsTrue(array.Length == 1 && array[0] == 'a');
        }

        [Test]
        public static void ToUnmanagedNonPrimitiveValueArrayTest()
        {
            var array = new BitList(new Size(100, 50)).ToUnmanagedNonPrimitiveValueArray<Size>();
            Assert.IsTrue(array.Length == 1 && array[0] == new Size(100, 50));
        }

        [Test]
        public static void ToBooleanArrayTest()
        {
            var array = new BitList(true).ToBooleanArray();
            Assert.IsTrue(array.Length == 1 && array[0]);
        }

        [Test]
        public static void ToByteArrayTest()
        {
            var array = new BitList((byte)1).ToByteArray();
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void ToInt16ArrayTest()
        {
            var array = new BitList((short)1).ToInt16Array();
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void ToInt32ArrayTest()
        {
            var array = new BitList(1).ToInt32Array();
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void ToInt64ArrayTest()
        {
            var array = new BitList(1L).ToInt64Array();
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void ToSByteArrayTest()
        {
            var array = new BitList((sbyte)1).ToSByteArray();
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void ToUInt16ArrayTest()
        {
            var array = new BitList((ushort)1).ToUInt16Array();
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void ToUInt32ArrayTest()
        {
            var array = new BitList(1U).ToUInt32Array();
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void ToUInt64ArrayTest()
        {
            var array = new BitList(1UL).ToUInt64Array();
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void ToSingleArrayTest()
        {
            var array = new BitList(1F).ToSingleArray();
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void ToDoubleArrayTest()
        {
            var array = new BitList(1D).ToDoubleArray();
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void ToDecimalArrayTest()
        {
            var array = new BitList(1M).ToDecimalArray();
            Assert.IsTrue(array.Length == 1 && array[0] == 1);
        }

        [Test]
        public static void ToCharArrayTest()
        {
            var array = new BitList('a').ToCharArray();
            Assert.IsTrue(array.Length == 1 && array[0] == 'a');
        }

        [Test]
        public static void ToStringTest()
        {
            var array = new BitList("a").ToString();
            Assert.IsTrue(array.Length == 1 && array == "a");
        }

        [Test]
        public static void NotOperatorTest()
        {
            Assert.IsTrue(!new BitList(false) == new BitList(true));
        }

        [Test]
        public static void BitwiseComplementOperatorTest()
        {
            Assert.IsTrue(~new BitList(false) == new BitList(true));
        }

        [Test]
        public static void AndOperatorTest()
        {
            Assert.IsTrue((new BitList(true) & new BitList(true))[0]);
        }

        [Test]
        public static void OrOperatorTest()
        {
            Assert.IsTrue((new BitList(true) | new BitList(false))[0]);
        }

        [Test]
        public static void XorOperatorTest()
        {
            Assert.IsFalse((new BitList(true) ^ new BitList(true))[0]);
        }

        [Test]
        public static void BitListCombinationOperatorTest()
        {
            Assert.IsTrue((new BitList(true) + new BitList(true)) == new BitList(new[] { true, true }));
        }

        [Test]
        public static void EqualityOperatorTest()
        {
            Assert.IsTrue(new BitList(true) == new BitList(true));
        }

        [Test]
        public static void InequalityOperatorTest()
        {
            Assert.IsTrue(new BitList(true) != new BitList(false));
        }

        [Test]
        public static void LeftShiftOperatorTest()
        {
            Assert.IsTrue(new BitList(new[] { true, true }) << 1 == new BitList(new[] { true, true, false }));
        }

        [Test]
        public static void RightShiftOperatorTest()
        {
            Assert.IsTrue(new BitList(new[] { true, true }) >> 1 == new BitList(new[] { true }));
        }

        [Test]
        public static void CastOperatorTest_BooleanArray()
        {
            Assert.IsTrue(((bool[])new BitList(true))[0]);
        }

        [Test]
        public static void CastOperatorTest_ByteArray()
        {
            Assert.IsTrue(((byte[])new BitList((byte)1))[0] == 1);
        }

        [Test]
        public static void CastOperatorTest_Int16Array()
        {
            Assert.IsTrue(((short[])new BitList((short)1))[0] == 1);
        }

        [Test]
        public static void CastOperatorTest_Int32Array()
        {
            Assert.IsTrue(((int[])new BitList(1))[0] == 1);
        }

        [Test]
        public static void CastOperatorTest_Int64Array()
        {
            Assert.IsTrue(((long[])new BitList(1L))[0] == 1);
        }

        [Test]
        public static void CastOperatorTest_SByteArray()
        {
            Assert.IsTrue(((sbyte[])new BitList((sbyte)1))[0] == 1);
        }

        [Test]
        public static void CastOperatorTest_UInt16Array()
        {
            Assert.IsTrue(((ushort[])new BitList((ushort)1))[0] == 1);
        }

        [Test]
        public static void CastOperatorTest_UInt32Array()
        {
            Assert.IsTrue(((uint[])new BitList(1U))[0] == 1);
        }

        [Test]
        public static void CastOperatorTest_UInt64Array()
        {
            Assert.IsTrue(((ulong[])new BitList(1UL))[0] == 1);
        }

        [Test]
        public static void CastOperatorTest_SingleArray()
        {
            Assert.IsTrue(((float[])new BitList(1F))[0] == 1);
        }

        [Test]
        public static void CastOperatorTest_DoubleArray()
        {
            Assert.IsTrue(((double[])new BitList(1D))[0] == 1);
        }

        [Test]
        public static void CastOperatorTest_DecimalArray()
        {
            Assert.IsTrue(((decimal[])new BitList(1M))[0] == 1);
        }

        [Test]
        public static void CastOperatorTest_CharArray()
        {
            Assert.IsTrue(((char[])new BitList('a'))[0] == 'a');
        }

        [Test]
        public static void CastOperatorTest_String()
        {
            Assert.IsTrue((string)new BitList("a") == "a");
        }
    }
}
