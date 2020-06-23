using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DataStructures
{
    /// <summary>
    /// Exception generated calling a method when the BitList instance is empty.
    /// </summary>
    public class EmptyInstanceException: Exception { }

    /// <summary>
    /// Exception generated when a boolean operation fails.
    /// </summary>
    public class BooleanAlgebraException: Exception { }

    /// <summary>
    /// Exception generated when a BitList instance fails to write or read a file.
    /// </summary>
    public class FileException: Exception { }

    /// <summary>
    /// Exception generated when a BitList instance fails to read a file.
    /// </summary>
    public class FileReadingException: FileException { }

    /// <summary>
    /// Exception generated when a BitList instance fails to write a file.
    /// </summary>
    public class FileWritingException: FileException { }

    /// <summary>
    /// Exception generated when a BitList instance fails the conversion to another type.
    /// </summary>
    public class ConversionErrorException: Exception { }

    /// <summary>
    /// A list of bits.
    /// </summary>
    /// <exception cref="EmptyInstanceException" />
    /// <exception cref="BooleanAlgebraException" />
    /// <exception cref="FileReadingException" />
    /// <exception cref="FileWritingException" />
    /// <exception cref="ConversionErrorException" />
    public sealed class BitList:
        IList<bool>,
        IEquatable<BitList>
    {
        private BitArray bits;

        /// <summary>
        /// No params ctor.
        /// </summary>
        public BitList()
        {
            bits = null;
        }

        /// <summary>
        /// Ctor (Copy the <paramref name="bits"/> <see cref="BitList"/> object).
        /// </summary>
        public BitList(BitList bits)
        {
            this.bits = new BitArray(bits.bits);
        }

        /// <summary>
        /// Ctor (<paramref name="unmanagedNonPrimitiveBits"/> is an unmanaged value type object).
        /// </summary>
        public BitList(ValueType unmanagedNonPrimitiveBits)
        {
            try
            {
                if (unmanagedNonPrimitiveBits.GetType().IsPrimitive)
                {
                    throw new ConversionErrorException();
                }

                //Throw an exception if the type of unmanagedBits isn't unmanaged; otherwise, continue.
                typeof(Unmanaged<>).MakeGenericType(unmanagedNonPrimitiveBits.GetType());

                //Unit of measure: byte (8 bit).
                var size = Marshal.SizeOf(
                    unmanagedNonPrimitiveBits.GetType().IsEnum ?
                        Enum.GetUnderlyingType(unmanagedNonPrimitiveBits.GetType()) : unmanagedNonPrimitiveBits.GetType()
                );

                if (unmanagedNonPrimitiveBits.GetType().IsEnum)
                {
                    switch (Enum.GetUnderlyingType(unmanagedNonPrimitiveBits.GetType()).Name)
                    {
                        case "Byte":
                            bits = new BitArray(new[] { (byte)unmanagedNonPrimitiveBits });
                            break;

                        case "SByte":
                            bits = new BitArray(new[] { (byte)(sbyte)unmanagedNonPrimitiveBits });
                            break;

                        case "Int16":
                            bits = new BitArray(BitConverter.GetBytes((short)unmanagedNonPrimitiveBits));
                            break;

                        case "UInt16":
                            bits = new BitArray(BitConverter.GetBytes((ushort)unmanagedNonPrimitiveBits));
                            break;

                        case "Int32":
                            bits = new BitArray(new[] { (int)unmanagedNonPrimitiveBits });
                            break;

                        default:
                        case "UInt32":
                            bits = new BitArray(new[] { (int)(uint)unmanagedNonPrimitiveBits });
                            break;

                        case "Int64":
                            bits = new BitArray(BitConverter.GetBytes((long)unmanagedNonPrimitiveBits));
                            break;

                        case "UInt64":
                            bits = new BitArray(BitConverter.GetBytes((ulong)unmanagedNonPrimitiveBits));
                            break;
                    }
                }
                else
                {
                    var destination = new byte[size]; //A buffer that can contains unmanagedBits.
                    var ptr = Marshal.AllocHGlobal(size); //Will be a pointer to unmanagedNonPrimitiveBits
                    Marshal.StructureToPtr(unmanagedNonPrimitiveBits, ptr, false);
                    Marshal.Copy(ptr, destination, 0, size); //Copy unmanagedBits into destination buffer.
                    bits = new BitArray(destination); //Create a new BitArray object using destination buffer.
                }
            }
            catch
            {
                throw new ConversionErrorException();
            }
        }

        /// <summary>
        /// Ctor (<paramref name="unmanagedNonPrimitiveBits"/> is an array of unmanaged value type object).
        /// </summary>
        public BitList(ValueType[] unmanagedNonPrimitiveBits)
        {
            var result = new BitList();
            foreach (var value in unmanagedNonPrimitiveBits)
            {
                result += new BitList(value);
            }

            bits = result.bits;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bit">A bit.</param>
        public BitList(bool bit)
        {
            bits = new BitArray(new[] { bit });
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A bit array.</param>
        public BitList(bool[] bits)
        {
            this.bits = new BitArray(bits);
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A byte.</param>
        public BitList(byte bits)
        {
            this.bits = new BitArray(new[] { bits });
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A byte array.</param>
        public BitList(byte[] bits)
        {
            this.bits = new BitArray(bits);
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A 16 bit integer.</param>
        public BitList(short bits)
        {
            this.bits = new BitArray(BitConverter.GetBytes(bits));
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A 16 bit integer array.</param>
        public BitList(short[] bits)
        {
            var result = new BitList();
            foreach (var value in bits)
            {
                result += new BitList(value);
            }

            this.bits = result.bits;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A 32 bit integer.</param>
        public BitList(int bits)
        {
            this.bits = new BitArray(new[] { bits });
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A 32 bit integer array.</param>
        public BitList(int[] bits)
        {
            this.bits = new BitArray(bits);
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A 64 bit integer.</param>
        public BitList(long bits)
        {
            this.bits = new BitArray(BitConverter.GetBytes(bits));
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A 64 bit integer array.</param>
        public BitList(long[] bits)
        {
            var result = new BitList();
            foreach (var value in bits)
            {
                result += new BitList(value);
            }

            this.bits = result.bits;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A signed byte.</param>
        public BitList(sbyte bits)
        {
            this.bits = new BitArray(new[] { (byte)bits });
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A signed byte array.</param>
        public BitList(sbyte[] bits)
        {
            this.bits = new BitArray((byte[])(Array)bits);
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">An unsigned 16 bit integer.</param>
        public BitList(ushort bits)
        {
            this.bits = new BitArray(BitConverter.GetBytes(bits));
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">An unsigned 16 bit integer array.</param>
        public BitList(ushort[] bits)
        {
            this.bits = new BitList((short[])(Array)bits).bits;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">An unsigned 32 bit integer.</param>
        public BitList(uint bits)
        {
            this.bits = new BitArray(BitConverter.GetBytes(bits));
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">An unsigned 32 bit integer array.</param>
        public BitList(uint[] bits)
        {
            this.bits = new BitArray((int[])(Array)bits);
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">An unsigned 64 bit integer.</param>
        public BitList(ulong bits)
        {
            this.bits = new BitArray(BitConverter.GetBytes(bits));
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">An unsigned 64 bit integer array.</param>
        public BitList(ulong[] bits)
        {
            this.bits = new BitList((long[])(Array)bits).bits;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A 32 bit real.</param>
        public BitList(float bits)
        {
            this.bits = new BitArray(BitConverter.GetBytes(bits));
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A 32 bit real array.</param>
        public BitList(float[] bits)
        {
            var result = new BitList();
            foreach (var value in bits)
            {
                result += new BitList(value);
            }

            this.bits = result.bits;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A 64 bit real.</param>
        public BitList(double bits)
        {
            this.bits = new BitArray(BitConverter.GetBytes(bits));
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A 64 bit real array.</param>
        public BitList(double[] bits)
        {
            var result = new BitList();
            foreach (var value in bits)
            {
                result += new BitList(value);
            }

            this.bits = result.bits;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A <see cref="decimal"/> value.</param>
        public BitList(decimal bits)
        {
            this.bits = new BitArray(decimal.GetBits(bits));
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A <see cref="decimal"/> array.</param>
        public BitList(decimal[] bits)
        {
            var result = new BitList();
            foreach (var value in bits)
            {
                result += new BitList(value);
            }

            this.bits = result.bits;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A 16 bit character.</param>
        public BitList(char bits)
        {
            this.bits = new BitArray(BitConverter.GetBytes(bits));
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A 16 bit character array (<see cref="char"/> array).</param>
        public BitList(char[] bits)
        {
            var result = new BitList();
            foreach (var value in bits)
            {
                result += new BitList(value);
            }

            this.bits = result.bits;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A 16 bit character array (<see cref="string"/>).</param>
        public BitList(string bits)
        {
            this.bits = new BitList(bits.ToCharArray()).bits;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="bits">A 16 bit character array of array.</param>
        public BitList(string[] bits)
        {
            var result = new BitList();
            foreach (var value in bits)
            {
                result += new BitList(value);
            }

            this.bits = result.bits;
        }

        bool ICollection<bool>.IsReadOnly => false;

        /// <summary>
        /// This property return true if the instance has no values.
        /// </summary>
        public bool IsEmpty => bits == null || bits.Length == 0;

        /// <summary>
        /// The amount of bits of the instance.
        /// </summary>
        public int Count => IsEmpty ? 0 : bits.Length;

        /// <summary>
        /// Indexer.
        /// </summary>
        public bool this[int index]
        {
            get => Get(index);
            set => Set(index, value);
        }

        /// <summary>
        /// Indexer.
        /// </summary>
        /// <param name="index">Start index.</param>
        /// <param name="count">Count of bits.</param>
        public BitList this[int index, int count]
        {
            get => Get(index, count);
            set
            {
                if (count >= value.Count)
                {
                    Set(index, value);
                }
                else
                {
                    Set(index, value[0, count]);
                }
            }
        }

        public static bool operator ==(BitList left, BitList right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BitList left, BitList right)
        {
            return !left.Equals(right);
        }

        public static BitList operator !(BitList value)
        {
            value.Not();
            return value;
        }

        public static BitList operator ~(BitList value)
        {
            return !value;
        }

        public static BitList operator &(BitList left, BitList right)
        {
            left.And(right);
            return left;
        }

        public static BitList operator |(BitList left, BitList right)
        {
            left.Or(right);
            return left;
        }

        public static BitList operator ^(BitList left, BitList right)
        {
            left.Xor(right);
            return left;
        }

        public static BitList operator +(BitList left, BitList right)
        {
            var leftBitList = left.ToBooleanArray().ToList();
            var rightBitList = right.ToBooleanArray();
            foreach (var bit in rightBitList)
            {
                leftBitList.Add(bit);
            }

            return new BitList(leftBitList.ToArray());
        }

        public static BitList operator +(BitList left, bool right)
        {
            return left + new BitList(right);
        }

        public static BitList operator +(bool left, BitList right)
        {
            return right + left;
        }

        public static BitList operator <<(BitList left, int right)
        {
            left.LeftShift(right);
            return left;
        }

        public static BitList operator >>(BitList left, int right)
        {
            left.RightShift(right);
            return left;
        }

        public static explicit operator bool[](BitList value)
        {
            return value.ToBooleanArray();
        }

        public static explicit operator byte[](BitList value)
        {
            return value.ToByteArray();
        }

        public static explicit operator short[](BitList value)
        {
            return value.ToInt16Array();
        }

        public static explicit operator int[](BitList value)
        {
            return value.ToInt32Array();
        }

        public static explicit operator long[](BitList value)
        {
            return value.ToInt64Array();
        }

        public static explicit operator sbyte[](BitList value)
        {
            return value.ToSByteArray();
        }

        public static explicit operator ushort[](BitList value)
        {
            return value.ToUInt16Array();
        }

        public static explicit operator uint[](BitList value)
        {
            return value.ToUInt32Array();
        }

        public static explicit operator ulong[](BitList value)
        {
            return value.ToUInt64Array();
        }

        public static explicit operator float[](BitList value)
        {
            return value.ToSingleArray();
        }

        public static explicit operator double[](BitList value)
        {
            return value.ToDoubleArray();
        }

        public static explicit operator decimal[](BitList value)
        {
            return value.ToDecimalArray();
        }

        public static explicit operator char[](BitList value)
        {
            return value.ToCharArray();
        }

        public static explicit operator string(BitList value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Create a list of bit from a file.
        /// </summary>
        public static BitList Read(string filePath)
        {
            byte[] bytes;
            try
            {
                bytes = File.ReadAllBytes(filePath);
            }
            catch
            {
                throw new FileReadingException();
            }

            return new BitList(bytes);
        }

        /// <summary>
        /// Create an instance from a text rapresentation.
        /// </summary>
        public static BitList Parse(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new BitList();
            }

            var bools = new List<bool>();
            foreach (var stringBit in text)
            {
                bools.Add(stringBit == '1');
            }

            return new BitList(bools.ToArray());
        }

        /// <summary>
        /// Return true if the specified BitList is equal to this istance.
        /// </summary>
        /// <param name="other">A list of bit.</param>
        public bool Equals(BitList other)
        {
            if (Count == other.Count)
            {
                var result = true;
                for (var i = 0; i < Count; i++)
                {
                    result &= this[i] == other[i];
                }

                return result;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Clear the instance.
        /// </summary>
        public void Clear()
        {
            bits = null;
        }

        /// <summary>
        /// Copy this instance to the specified array.
        /// </summary>
        /// <param name="arrayIndex">Start position.</param>
        public void CopyTo<T>(T[] unmanagedNonPrimitiveValueArray, int arrayIndex) where T : unmanaged
        {
            ToUnmanagedNonPrimitiveValueArray<T>().CopyTo(unmanagedNonPrimitiveValueArray, arrayIndex);
        }

        /// <summary>
        /// Copy this instance to the specified array.
        /// </summary>
        /// <param name="arrayIndex">Start position.</param>
        public void CopyTo(bool[] array, int arrayIndex)
        {
            ToBooleanArray().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copy this instance to the specified array.
        /// </summary>
        /// <param name="arrayIndex">Start position.</param>
        public void CopyTo(byte[] array, int arrayIndex)
        {
            ToByteArray().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copy this instance to the specified array.
        /// </summary>
        /// <param name="arrayIndex">Start position.</param>
        public void CopyTo(short[] array, int arrayIndex)
        {
            ToInt16Array().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copy this instance to the specified array.
        /// </summary>
        /// <param name="arrayIndex">Start position.</param>
        public void CopyTo(int[] array, int arrayIndex)
        {
            ToInt32Array().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copy this instance to the specified array.
        /// </summary>
        /// <param name="arrayIndex">Start position.</param>
        public void CopyTo(long[] array, int arrayIndex)
        {
            ToInt64Array().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copy this instance to the specified array.
        /// </summary>
        /// <param name="arrayIndex">Start position.</param>
        public void CopyTo(sbyte[] array, int arrayIndex)
        {
            ToSByteArray().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copy this instance to the specified array.
        /// </summary>
        /// <param name="arrayIndex">Start position.</param>
        public void CopyTo(ushort[] array, int arrayIndex)
        {
            ToUInt16Array().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copy this instance to the specified array.
        /// </summary>
        /// <param name="arrayIndex">Start position.</param>
        public void CopyTo(uint[] array, int arrayIndex)
        {
            ToUInt32Array().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copy this instance to the specified array.
        /// </summary>
        /// <param name="arrayIndex">Start position.</param>
        public void CopyTo(ulong[] array, int arrayIndex)
        {
            ToUInt64Array().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copy this instance to the specified array.
        /// </summary>
        /// <param name="arrayIndex">Start position.</param>
        public void CopyTo(float[] array, int arrayIndex)
        {
            ToSingleArray().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copy this instance to the specified array.
        /// </summary>
        /// <param name="arrayIndex">Start position.</param>
        public void CopyTo(double[] array, int arrayIndex)
        {
            ToDoubleArray().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copy this instance to the specified array.
        /// </summary>
        /// <param name="arrayIndex">Start position.</param>
        public void CopyTo(decimal[] array, int arrayIndex)
        {
            ToDecimalArray().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copy this instance to the specified array.
        /// </summary>
        /// <param name="arrayIndex">Start position.</param>
        public void CopyTo(char[] array, int arrayIndex)
        {
            ToCharArray().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Get a the value in the specified index.
        /// </summary>
        public bool Get(int index)
        {
            if (!IsEmpty && index < bits.Length && index >= 0)
            {
                return bits[index];
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Get a range of value in the specified index.
        /// </summary>
        public BitList Get(int index, int count)
        {
            if (!IsEmpty && index < bits.Length && index >= 0 && index + count <= bits.Length - index && count > 0)
            {
                var bools = new List<bool>();
                for (var i = 0; i < count; i++)
                {
                    bools.Add(bits[index + i]);
                }

                return new BitList(bools.ToArray());
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Set the value in the specified index with a new value.
        /// </summary>
        public void Set(int index, bool value)
        {
            if (!IsEmpty && index < bits.Length && index >= 0)
            {
                bits[index] = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Set the value in the specified index with a new collection of values.
        /// </summary>
        public void Set(int index, BitList value)
        {
            if (!IsEmpty && index < bits.Length && index >= 0 && index + value.Count <= bits.Length - index && value.Count > 0)
            {
                var bools = ToBooleanArray().ToList();
                bools.RemoveRange(index, value.Count);
                bools.InsertRange(index, value.ToBooleanArray());
                bits = new BitArray(bools.ToArray());
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Add a list of bit to the last position of this instance.
        /// </summary>
        /// <param name="items">A list of bit.</param>
        public void Add(BitList items)
        {
            var newBitList = IsEmpty ? items : this + items;
            bits = newBitList.bits;
        }

        /// <summary>
        /// Add a bit to the last position of this instance.
        /// </summary>
        /// <param name="item">A bit.</param>
        public void Add(bool item)
        {
            var newBitList = IsEmpty ? new BitList(item) : this + item;
            bits = newBitList.bits;
        }

        /// <summary>
        /// Add a list of bit to the first position of this instance.
        /// </summary>
        /// <param name="bits">A list of bit.</param>
        public void AddFirst(BitList items)
        {
            Insert(0, items);
        }

        /// <summary>
        /// Add a bit to the first position of this instance.
        /// </summary>
        /// <param name="bit">A bit.</param>
        public void AddFirst(bool item)
        {
            Insert(0, item);
        }

        /// <summary>
        /// Return true if this instance contains the specified bit.
        /// </summary>
        /// <param name="item">A bit.</param>
        public bool Contains(bool item)
        {
            return IndexOf(item) != -1;
        }

        /// <summary>
        /// Return true if this instance contains the specified list of bit.
        /// </summary>
        /// <param name="items">A list of bit.</param>
        public bool Contains(BitList items)
        {
            return IndexOf(items) != -1;
        }

        /// <summary>
        /// Remove a bit in the specified position.
        /// </summary>
        public void RemoveAt(int index)
        {
            if (!IsEmpty && index < Count && index >= 0)
            {
                var bools = ToBooleanArray().ToList();
                bools.RemoveAt(index);
                bits = new BitArray(bools.ToArray());
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Remove a range of bits from this instance.
        /// </summary>
        /// <param name="index">Start index.</param>
        /// <param name="count">Count of bits.</param>
        public void RemoveRange(int index, int count)
        {
            if (!IsEmpty && index < bits.Length && index >= 0 && index + count <= bits.Length - index && count > 0)
            {
                var bools = ToBooleanArray().ToList();
                bools.RemoveRange(index, count);
                bits = new BitArray(bools.ToArray());
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Remove the specified bit from this instance.
        /// </summary>
        public bool Remove(bool item)
        {
            try
            {
                RemoveAt(IndexOf(item));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Remove the specified list of bit from this instance.
        /// </summary>
        public bool Remove(BitList items)
        {
            if (IsEmpty)
            {
                throw new EmptyInstanceException();
            }
            else
            {
                var thisBitList = ToText();
                var thoseBitList = items.ToText();
                if (!thisBitList.Contains(thoseBitList))
                {
                    return false;
                }

                var newBitList = thisBitList.Replace(thoseBitList, string.Empty);
                bits = Parse(newBitList).bits;
                return true;
            }
        }

        /// <summary>
        /// Get the index of a bit in this instance.
        /// </summary>
        /// <param name="bit">A bit.</param>
        /// <returns>The <paramref name="item"/> index.</returns>
        public int IndexOf(bool item)
        {
            for (var i = 0; i < Count; i++)
            {
                if (item == this[i])
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Get the index of a list of bit in this instance.
        /// </summary>
        /// <param name="items">A list of bit.</param>
        public int IndexOf(BitList items)
        {
            for (var i = 0; i < Count; i++)
            {
                if (items == this[i, items.Count])
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Shift bits to left.
        /// </summary>
        public void LeftShift(int count)
        {
            if (count != 0)
            {
                for (var i = 1; i == Math.Abs(count); i++)
                {
                    if (count > 0)
                    {
                        Add(false);
                    }
                    else
                    {
                        try
                        {
                            RemoveAt(Count - 1);
                        }
                        catch
                        {
                            break;
                        }
                    }
                }

                if (Count == 0 || IsEmpty)
                {
                    Add(false);
                }
            }
        }

        /// <summary>
        /// Shift bits to right.
        /// </summary>
        public void RightShift(int count)
        {
            if (count != 0)
            {
                for (var i = 1; i == Math.Abs(count); i++)
                {
                    if (count < 0)
                    {
                        Add(false);
                    }
                    else
                    {
                        try
                        {
                            RemoveAt(Count - 1);
                        }
                        catch
                        {
                            break;
                        }
                    }
                }

                if (Count == 0 || IsEmpty)
                {
                    Add(false);
                }
            }
        }

        /// <summary>
        /// NOT boolean operation.
        /// </summary>
        public void Not()
        {
            if (IsEmpty)
            {
                throw new BooleanAlgebraException();
            }
            else
            {
                bits.Not();
            }
        }

        /// <summary>
        /// AND boolean operation of this instance.
        /// </summary>
        public bool And()
        {
            if (IsEmpty)
            {
                throw new BooleanAlgebraException();
            }
            else
            {
                bool result = bits[0];
                for (var i = 1; i < bits.Length; i++)
                {
                    result &= bits[i];
                }

                return result;
            }
        }

        /// <summary>
        /// AND boolean operation with another list of bit.
        /// </summary>
        public void And(BitList bits)
        {
            if (IsEmpty)
            {
                throw new BooleanAlgebraException();
            }
            else
            {
                this.bits.And(bits.bits);
            }
        }

        /// <summary>
        /// OR boolean operation in this instance.
        /// </summary>
        public bool Or()
        {
            if (IsEmpty)
            {
                throw new BooleanAlgebraException();
            }
            else
            {
                bool result = bits[0];
                for (var i = 1; i < bits.Length; i++)
                {
                    result |= bits[i];
                }

                return result;
            }
        }

        /// <summary>
        /// OR boolean operation with another list of bit.
        /// </summary>
        public void Or(BitList bits)
        {
            if (IsEmpty)
            {
                throw new BooleanAlgebraException();
            }
            else
            {
                this.bits.Or(bits.bits);
            }
        }

        /// <summary>
        /// XOR boolean operation in this instance.
        /// </summary>
        public bool Xor()
        {
            if (IsEmpty)
            {
                throw new BooleanAlgebraException();
            }
            else
            {
                bool result = bits[0];
                for (var i = 1; i < bits.Length; i++)
                {
                    result ^= bits[i];
                }

                return result;
            }
        }

        /// <summary>
        /// XOR boolean operation with another list of bit.
        /// </summary>
        public void Xor(BitList bits)
        {
            if (IsEmpty)
            {
                throw new BooleanAlgebraException();
            }
            else
            {
                this.bits.Xor(bits.bits);
            }
        }

        /// <summary>
        /// Insert the specified list of bit in the specified index.
        /// </summary>
        public void Insert(int index, BitList items)
        {
            if (IsEmpty)
            {
                bits = new BitArray(items.bits);
            }
            else if (index < bits.Count && index >= 0)
            {
                var bools = ToBooleanArray().ToList();
                bools.InsertRange(index, items.ToBooleanArray());
                bits = new BitArray(bools.ToArray());
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Insert the specified bit in the specified index.
        /// </summary>
        public void Insert(int index, bool item)
        {
            if (IsEmpty)
            {
                bits = new BitArray(new[] { item });
            }
            else if (index < bits.Length && index >= 0)
            {
                var bools = ToBooleanArray().ToList();
                bools.Insert(index, item);
                bits = new BitArray(bools.ToArray());
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Replace a list of bit of the current instance with another list of bit.
        /// </summary>
        public void Replace(BitList oldBitList, BitList newBitList)
        {
            if (IsEmpty)
            {
                throw new EmptyInstanceException();
            }
            else
            {
                var thisBitListText = ToText();
                var oldBitListText = oldBitList.ToText();
                var newBitListText = newBitList.ToText();
                var bitsText = thisBitListText.Replace(oldBitListText, newBitListText);
                bits = Parse(bitsText).bits;
            }
        }

        /// <summary>
        /// Write the current instance into a file.
        /// </summary>
        public void Write(string filePath)
        {
            try
            {
                File.WriteAllBytes(filePath, ToByteArray());
            }
            catch
            {
                throw new FileWritingException();
            }
        }

        /// <summary>
        /// Convert this instance to a text rapresentation.
        /// </summary>
        public string ToText()
        {
            if (IsEmpty)
            {
                return string.Empty;
            }

            var text = string.Empty;
            foreach (var bit in ToBooleanArray())
            {
                text += bit ? "1" : "0";
            }

            return text;
        }

        /// <summary>
        /// Convert this instance to a specified unmanaged non primitive value array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] ToUnmanagedNonPrimitiveValueArray<T>() where T : unmanaged
        {
            try
            {
                if (typeof(T).IsPrimitive || typeof(T).IsPointer)
                {
                    throw new Exception();
                }
                else if (IsEmpty)
                {
                    return new T[0];
                }
                else
                {
                    var bytes = ToByteArray().ToList();

                    //Unit of measure: byte (8 bit).
                    var size = Marshal.SizeOf(typeof(T).IsEnum ? Enum.GetUnderlyingType(typeof(T)) : typeof(T));

                    while (bytes.Count % size != 0)
                    {
                        //Add a missing byte.
                        bytes.Add(0);
                    }

                    var result = new List<T>(); //A list of unmanaged non primitive value type objects.
                    for (var i = 0; i < bytes.Count; i += size)
                    {
                        if (typeof(T).IsEnum)
                        {
                            T obj;
                            switch (Enum.GetUnderlyingType(typeof(T)).Name)
                            {
                                case "Byte":
                                    obj = (T)Enum.ToObject(typeof(T), bytes[i]);
                                    break;

                                case "SByte":
                                    obj = (T)Enum.ToObject(typeof(T), (sbyte)bytes[i]);
                                    break;

                                case "Int16":
                                    obj = (T)Enum.ToObject(typeof(T), BitConverter.ToInt16(bytes.GetRange(i, 2).ToArray()));
                                    break;

                                case "UInt16":
                                    obj = (T)Enum.ToObject(typeof(T), BitConverter.ToUInt16(bytes.GetRange(i, 2).ToArray()));
                                    break;

                                default:
                                case "Int32":
                                    obj = (T)Enum.ToObject(typeof(T), BitConverter.ToInt32(bytes.GetRange(i, 4).ToArray()));
                                    break;

                                case "UInt32":
                                    obj = (T)Enum.ToObject(typeof(T), BitConverter.ToUInt32(bytes.GetRange(i, 4).ToArray()));
                                    break;

                                case "Int64":
                                    obj = (T)Enum.ToObject(typeof(T), BitConverter.ToInt64(bytes.GetRange(i, 8).ToArray()));
                                    break;

                                case "UInt64":
                                    obj = (T)Enum.ToObject(typeof(T), BitConverter.ToUInt64(bytes.GetRange(i, 8).ToArray()));
                                    break;
                            }
                            result.Add(obj);
                        }
                        else
                        {
                            var ptr = Marshal.AllocHGlobal(size); //An unmanaged type object pointer.
                            Marshal.Copy(bytes.GetRange(i, size).ToArray(), 0, ptr, size); //Copy the current buffer to ptr.
                            result.Add(Marshal.PtrToStructure<T>(ptr)); //Get the value pointed by ptr and add it to List<T> result.
                        }
                    }

                    return result.ToArray();
                }
            }
            catch
            {
                throw new ConversionErrorException();
            }
        }

        /// <summary>
        /// Convert this instance to a <see cref="bool"/> array.
        /// </summary>
        /// <returns>A <see cref="bool"/> array.</returns>
        public bool[] ToBooleanArray()
        {
            if (IsEmpty)
            {
                return new bool[0];
            }
            else
            {
                var bools = new bool[bits.Length];
                bits.CopyTo(bools, 0);
                return bools;
            }
        }

        /// <summary>
        /// Convert this instance to a <see cref="byte"/> array.
        /// </summary>
        /// <returns>A <see cref="byte"/> array.</returns>
        public byte[] ToByteArray()
        {
            //Convert the bits field (BitArray) to a Byte array dividing the bits field in 8 parts.
            if (IsEmpty)
            {
                return new byte[0];
            }
            else
            {
                int num = bits.Length / 8;
                if (bits.Length % 8 != 0)
                {
                    num++;
                }

                var bytes = new byte[num];
                bits.CopyTo(bytes, 0);
                return bytes;
            }
        }

        /// <summary>
        /// Convert this instance to a <see cref="short"/> array.
        /// </summary>
        /// <returns>A <see cref="short"/> array.</returns>
        public short[] ToInt16Array()
        {
            //Convert the bits field (BitArray) to a Byte array and then to a Int16 array.
            if (IsEmpty)
            {
                return new short[0];
            }
            else
            {
                var bytes = ToByteArray().ToList();
                if (bytes.Count % 2 != 0)
                {
                    bytes.Add(0);
                }

                var shorts = new List<short>();
                for (var i = 0; i < bytes.Count; i+= 2)
                {
                    shorts.Add(BitConverter.ToInt16(bytes.ToArray(), i));
                }

                return shorts.ToArray();
            }
        }

        /// <summary>
        /// Convert this instance to a <see cref="int"/> array.
        /// </summary>
        /// <returns>A <see cref="int"/> array.</returns>
        public int[] ToInt32Array()
        {
            //Convert the bits field (BitArray) to a Byte array dividing the bits field in 32 parts.
            if (IsEmpty)
            {
                return new int[0];
            }
            else
            {
                int num = bits.Length / 32;
                if (bits.Length % 32 != 0)
                {
                    num++;
                }

                var ints = new int[num];
                bits.CopyTo(ints, 0);
                return ints;
            }
        }

        /// <summary>
        /// Convert this instance to a <see cref="long"/> array.
        /// </summary>
        /// <returns>A <see cref="long"/> array.</returns>
        public long[] ToInt64Array()
        {
            //Convert the bits field (BitArray) to a Int32 array and then to a Int64 array.
            if (IsEmpty)
            {
                return new long[0];
            }
            else
            {
                var ints = ToInt32Array().ToList();
                if (ints.Count % 2 != 0)
                {
                    ints.Add(0);
                }

                var longs = new List<long>();
                for (var i = 0; i < ints.Count; i += 2)
                {
                    longs.Add(BitConverter.ToInt64(new BitList(new[] { ints[i], ints[i + 1] }).ToByteArray(), 0));
                }

                return longs.ToArray();
            }
        }

        /// <summary>
        /// Convert this instance to a <see cref="sbyte"/> array.
        /// </summary>
        /// <returns>A <see cref="sbyte"/> array.</returns>
        public sbyte[] ToSByteArray()
        {
            return (sbyte[])(Array)ToByteArray();
        }

        /// <summary>
        /// Convert this instance to a <see cref="ushort"/> array.
        /// </summary>
        /// <returns>A <see cref="ushort"/> array.</returns>
        public ushort[] ToUInt16Array()
        {
            return (ushort[])(Array)ToInt16Array();
        }

        /// <summary>
        /// Convert this instance to a <see cref="uint"/> array.
        /// </summary>
        /// <returns>A <see cref="uint"/> array.</returns>
        public uint[] ToUInt32Array()
        {
            return (uint[])(Array)ToInt32Array();
        }

        /// <summary>
        /// Convert this instance to a <see cref="ulong"/> array.
        /// </summary>
        /// <returns>A <see cref="ulong"/> array.</returns>
        public ulong[] ToUInt64Array()
        {
            return (ulong[])(Array)ToInt64Array();
        }

        /// <summary>
        /// Convert this instance to a <see cref="float"/> array.
        /// </summary>
        /// <returns>A <see cref="float"/> array.</returns>
        public float[] ToSingleArray()
        {
            //Convert the bits field (BitArray) to a Int32 array and then to a Single array.
            if (IsEmpty)
            {
                return new float[0];
            }
            else
            {
                var ints = ToInt32Array();
                var floats = new List<float>();
                foreach (var bits in ints)
                {
                    floats.Add(BitConverter.Int32BitsToSingle(bits));
                }

                return floats.ToArray();
            }
        }

        /// <summary>
        /// Convert this instance to a <see cref="double"/> array.
        /// </summary>
        /// <returns>A <see cref="double"/> array.</returns>
        public double[] ToDoubleArray()
        {
            //Convert the bits field (BitArray) to a Int64 array and then to a Double array.
            if (IsEmpty)
            {
                return new double[0];
            }
            else
            {
                var longs = ToInt64Array();
                var doubles = new List<double>();
                foreach (var bits in longs)
                {
                    doubles.Add(BitConverter.Int64BitsToDouble(bits));
                }

                return doubles.ToArray();
            }
        }

        /// <summary>
        /// Convert this instance to a <see cref="decimal"/> array.
        /// </summary>
        /// <returns>A <see cref="decimal"/> array.</returns>
        public decimal[] ToDecimalArray()
        {
            //Convert the bits field (BitArray) to a Int64 array and then to a Int32 array and then to a Decimal array.
            if (IsEmpty)
            {
                return new decimal[0];
            }
            else
            {
                try
                {
                    var longs = ToInt64Array().ToList();
                    if (longs.Count % 2 != 0)
                    {
                        longs.Add(0);
                    }

                    var decimals = new List<decimal>();
                    for (var i = 0; i < longs.Count; i += 2)
                    {
                        decimals.Add(new decimal(new BitList(new[] { longs[i], longs[i + 1] }).ToInt32Array()));
                    }

                    return decimals.ToArray();
                }
                catch
                {
                    throw new ConversionErrorException();
                }
            }
        }

        /// <summary>
        /// Convert this instance to a <see cref="char"/> array.
        /// </summary>
        /// <returns>A <see cref="char"/> array.</returns>
        public char[] ToCharArray()
        {
            //Convert the bits field (BitArray) to Int16 array and then to a Char array.
            if (IsEmpty)
            {
                return new char[0];
            }
            else
            {
                var shorts = ToInt16Array();
                var chars = new List<char>();
                foreach (var bits in shorts)
                {
                    chars.Add((char)bits);
                }

                return chars.ToArray();
            }
        }

        /// <summary>
        /// Convert this instance to a <see cref="string"/> array.
        /// </summary>
        /// <returns>A <see cref="string"/> array.</returns>
        public override string ToString()
        {
            //Convert the bits field (BitArray) to a Char array and then to a String.
            return IsEmpty ? string.Empty : new string(ToCharArray());
        }

        /// <summary>
        /// Return true if the specified BitList is equal to this istance.
        /// </summary>
        /// <param name="obj">A list of bit.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as BitList);
        }

        /// <summary>
        /// Gets hash code.
        /// </summary>
        /// <returns>Hash code.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        IEnumerator<bool> IEnumerable<bool>.GetEnumerator()
        {
            return ToBooleanArray().ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ToBooleanArray().ToList().GetEnumerator();
        }

        private class Unmanaged<T> where T : unmanaged { }
    }
}
