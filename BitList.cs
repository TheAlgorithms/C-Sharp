// By Lorenzo Lotti

using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Exception generated calling a method when the BitList instance is empty.
/// </summary>
public class EmptyInstanceException : Exception { }

/// <summary>
/// Exception generated when a boolean operation fails.
/// </summary>
public class BooleanAlgebraException : Exception { }

/// <summary>
/// Exception generated when a BitList instance fails to write or read a file.
/// </summary>
public class FileException : Exception { }

/// <summary>
/// Exception generated when a BitList instance fails to read a file.
/// </summary>
public class FileReadingException : FileException { }

/// <summary>
/// Exception generated when a BitList instance fails to write a file.
/// </summary>
public class FileWritingException : FileException { }

/// <summary>
/// Exception generated when a BitList instance fails the conversion to another type.
/// </summary>
public class ConversionErrorException : Exception { }

/// <summary>
/// A list of bits.
/// </summary>
/// <exception cref="EmptyInstanceException" />
/// <exception cref="BooleanAlgebraException" />
/// <exception cref="FileReadingException" />
/// <exception cref="FileWritingException" />
/// <exception cref="ConversionErrorException" />
public sealed class BitList :
    IEnumerable<bool>,
    ICollection<bool>,
    IList<bool>,
    ICloneable,
    IEquatable<BitList>
{
    private BitArray bits;
    bool ICollection<bool>.IsReadOnly { get => false; }

    /// <summary>
    /// This property return true if the instance has no values.
    /// </summary>
    public bool IsEmpty { get => bits.Equals(null) || bits.Equals(new BitArray(0)); }

    /// <summary>
    /// The amount of bits of the instance.
    /// </summary>
    public int Count { get => IsEmpty ? 0 : bits.Length; }

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
    /// <exception cref="ArgumentOutOfRangeException" />
    public BitList this[int index, int count]
    {
        get => Get(index, count);
        set
        {
            if (count == value.Count)
                Set(index, value);
            else
                throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary>
    /// No params ctor.
    /// </summary>
    public BitList()
    {
        bits = null;
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
        var bytesList = new List<byte[]>();
        foreach (var num in bits)
            bytesList.Add(BitConverter.GetBytes(num));
        var byteList = new List<byte>();
        foreach (var bytes in bytesList)
        {
            foreach (var num in bytes)
                byteList.Add(num);
        }
        this.bits = new BitArray(byteList.ToArray());
    }

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="bits">A 32 bit integer.</param>
    public BitList(int bits)
    {
        this.bits = new BitArray(new int[] { bits });
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
        var bytesList = new List<byte[]>();
        foreach (var num in bits)
            bytesList.Add(BitConverter.GetBytes(num));
        var byteList = new List<byte>();
        foreach (var bytes in bytesList)
        {
            foreach (var num in bytes)
                byteList.Add(num);
        }
        this.bits = new BitArray(byteList.ToArray());
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
        var bytes = new byte[bits.Length];
        for (int i = 0; i < bits.Length; i++)
            bytes[i] = (byte)bits[i];
        this.bits = new BitArray(bytes);
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
        var bytesList = new List<byte[]>();
        foreach (var num in bits)
            bytesList.Add(BitConverter.GetBytes(num));
        var byteList = new List<byte>();
        foreach (var bytes in bytesList)
        {
            foreach (var num in bytes)
                byteList.Add(num);
        }
        this.bits = new BitArray(byteList.ToArray());
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
        var ints = new int[bits.Length];
        for (int i = 0; i < bits.Length; i++)
            ints[i] = (int)bits[i];
        this.bits = new BitArray(ints);
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
        var bytesList = new List<byte[]>();
        foreach (var num in bits)
            bytesList.Add(BitConverter.GetBytes(num));
        var byteList = new List<byte>();
        foreach (var bytes in bytesList)
        {
            foreach (var num in bytes)
                byteList.Add(num);
        }
        this.bits = new BitArray(byteList.ToArray());
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
        var bytesList = new List<byte[]>();
        foreach (var num in bits)
            bytesList.Add(BitConverter.GetBytes(num));
        var byteList = new List<byte>();
        foreach (var bytes in bytesList)
        {
            foreach (var num in bytes)
                byteList.Add(num);
        }
        this.bits = new BitArray(byteList.ToArray());
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
        var bytesList = new List<byte[]>();
        foreach (var num in bits)
            bytesList.Add(BitConverter.GetBytes(num));
        var byteList = new List<byte>();
        foreach (var bytes in bytesList)
        {
            foreach (var num in bytes)
                byteList.Add(num);
        }
        this.bits = new BitArray(byteList.ToArray());
    }

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="bits">A <see cref="System.Decimal"/> value.</param>
    public BitList(decimal bits)
    {
        this.bits = new BitArray(decimal.GetBits(bits));
    }

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="bits">A <see cref="System.Decimal"/> array.</param>
    public BitList(decimal[] bits)
    {
        var intsList = new List<int[]>();
        foreach (var num in bits)
            intsList.Add(decimal.GetBits(num));
        var intList = new List<int>();
        foreach (var ints in intsList)
        {
            foreach (var num in ints)
                intList.Add(num);
        }
        this.bits = new BitArray(intList.ToArray());
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
    /// <param name="bits">A 16 bit character array (<see cref="System.Char"/> array).</param>
    public BitList(char[] bits)
    {
        var bytesList = new List<byte[]>();
        foreach (var num in bits)
            bytesList.Add(BitConverter.GetBytes(num));
        var byteList = new List<byte>();
        foreach (var bytes in bytesList)
        {
            foreach (var num in bytes)
                byteList.Add(num);
        }
        this.bits = new BitArray(byteList.ToArray());
    }

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="bits">A 16 bit character array (<see cref="System.String"/>).</param>
    public BitList(string bits)
    {
        var bytesList = new List<byte[]>();
        foreach (var num in bits)
            bytesList.Add(BitConverter.GetBytes(num));
        var byteList = new List<byte>();
        foreach (var bytes in bytesList)
        {
            foreach (var num in bytes)
                byteList.Add(num);
        }
        this.bits = new BitArray(byteList.ToArray());
    }

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="bits">A 16 bit character array of array.</param>
    public BitList(string[] bits)
    {
        var _bytesList = new List<byte[]>();
        var _byteList = new List<byte>();
        foreach (var str in bits)
        {
            var bytesList = new List<byte[]>();
            foreach (var chr in str)
                bytesList.Add(BitConverter.GetBytes(chr));
            var byteList = new List<byte>();
            foreach (var bytes in bytesList)
            {
                foreach (var num in bytes)
                    byteList.Add(num);
            }
            _bytesList.Add(byteList.ToArray());
        }
        foreach (var bytes in _bytesList)
        {
            foreach (var num in bytes)
                _byteList.Add(num);
        }
        this.bits = new BitArray(_byteList.ToArray());
    }

    IEnumerator<bool> IEnumerable<bool>.GetEnumerator()
    {
        return new BitListEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return new BitListEnumerator(this);
    }

    /// <summary>
    /// Get the index of a bit in this instance.
    /// </summary>
    /// <param name="bit">A bit.</param>
    /// <returns>The <paramref name="item"/> index.</returns>
    public int IndexOf(bool item)
    {
        for (int i = 0; i < Count; i++)
        {
            if (item == this[i])
                return i;
        }
        return -1;
    }

    /// <summary>
    /// Get the index of a list of bit in this instance.
    /// </summary>
    /// <param name="items">A list of bit.</param>
    public int IndexOf(BitList items)
    {
        for (int i = 0; i < Count; i++)
        {
            if (items == this[i, bits.Count])
                return i;
        }
        return -1;
    }

    /// <summary>
    /// Clone this instance in a new object.
    /// </summary>
    public object Clone()
    {
        return this;
    }

    /// <summary>
    /// Return true if the specified BitList is equal to this istance.
    /// </summary>
    /// <param name="other">A list of bit.</param>
    public bool Equals(BitList other)
    {
        return Equals(this, other);
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
    /// Shift bits to left.
    /// </summary>
    public void LeftShift(int count)
    {
        if (count != 0)
        {
            for (int i = 1; i == Math.Abs(count); i++)
            {
                if (count > 0)
                    Add(false);
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
                Add(false);
        }
    }

    /// <summary>
    /// Shift bits to right.
    /// </summary>
    public void RightShift(int count)
    {
        if (count != 0)
        {
            for (int i = 1; i == Math.Abs(count); i++)
            {
                if (count < 0)
                    Add(false);
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
                Add(false);
        }
    }

    /// <summary>
    /// NOT boolean operation.
    /// </summary>
    /// <exception cref="BooleanAlgebraException" />
    public void Not()
    {
        if (IsEmpty)
            throw new BooleanAlgebraException();
        else
            bits.Not();
    }

    /// <summary>
    /// AND boolean operation of this instance.
    /// </summary>
    /// <exception cref="BooleanAlgebraException" />
    public bool And()
    {
        if (IsEmpty)
            throw new BooleanAlgebraException();
        else
        {
            bool result = bits[0];
            for (int i = 1; i < bits.Length; i++)
                result &= bits[i];
            return result;
        }
    }

    /// <summary>
    /// AND boolean operation with another list of bit.
    /// </summary>
    /// <exception cref="BooleanAlgebraException" />
    public void And(BitList bits)
    {
        if (IsEmpty)
            throw new BooleanAlgebraException();
        else
            this.bits.And(bits.bits);
    }

    /// <summary>
    /// OR boolean operation in this instance.
    /// </summary>
    /// <exception cref="BooleanAlgebraException" />
    public bool Or()
    {
        if (IsEmpty)
            throw new BooleanAlgebraException();
        else
        {
            bool result = bits[0];
            for (int i = 1; i < bits.Length; i++)
                result |= bits[i];
            return result;
        }
    }

    /// <summary>
    /// OR boolean operation with another list of bit.
    /// </summary>
    /// <exception cref="BooleanAlgebraException" />
    public void Or(BitList bits)
    {
        if (IsEmpty)
            throw new BooleanAlgebraException();
        else
            this.bits.Or(bits.bits);
    }

    /// <summary>
    /// XOR boolean operation in this instance.
    /// </summary>
    /// <exception cref="BooleanAlgebraException" />
    public bool Xor()
    {
        if (IsEmpty)
            throw new BooleanAlgebraException();
        else
        {
            bool result = bits[0];
            for (int i = 1; i < bits.Length; i++)
                result ^= bits[i];
            return result;
        }
    }

    /// <summary>
    /// XOR boolean operation with another list of bit.
    /// </summary>
    /// <exception cref="BooleanAlgebraException" />
    public void Xor(BitList bits)
    {
        if (IsEmpty)
            throw new BooleanAlgebraException();
        else
            this.bits.Xor(bits.bits);
    }

    /// <summary>
    /// Add a list of bit to the last position of this instance.
    /// </summary>
    /// <param name="items">A list of bit.</param>
    public void Add(BitList items)
    {
        var newBitList = IsEmpty ? items : this + items;
        this.bits = newBitList.bits;
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
    /// Remove a bit in the specified position.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException" />
    public void RemoveAt(int index)
    {
        if (!IsEmpty && index < Count && index >= 0)
        {
            var bools = ToBooleanArray().ToList();
            bools.RemoveAt(index);
            bits = new BitArray(bools.ToArray());
        }
        else
            throw new ArgumentOutOfRangeException();
    }

    /// <summary>
    /// Remove a range of bits from this instance.
    /// </summary>
    /// <param name="index">Start index.</param>
    /// <param name="count">Count of bits.</param>
    /// <exception cref="ArgumentOutOfRangeException" />
    public void RemoveRange(int index, int count)
    {
        if (!IsEmpty && index < bits.Length && index >= 0 && count <= bits.Length - index && count > 0)
        {
            var bools = ToBooleanArray().ToList();
            bools.RemoveRange(index, count);
            bits = new BitArray(bools.ToArray());
        }
        else
            throw new ArgumentOutOfRangeException();
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
    /// <exception cref="EmptyInstanceException" />
    public bool Remove(BitList items)
    {
        if (IsEmpty)
            throw new EmptyInstanceException();
        else
        {
            var thisBitList = ToText();
            var thoseBitList = items.ToText();
            if (!thisBitList.Contains(thoseBitList))
                return false;
            var newBitList = thisBitList.Replace(thoseBitList, string.Empty);
            this.bits = Parse(newBitList).bits;
            return true;
        }
    }

    /// <summary>
    /// Insert the specified list of bit in the specified index.
    /// </summary>
    /// <exception cref="IndexOutOfRangeException" />
    public void Insert(int index, BitList items)
    {
        if (IsEmpty)
            this.bits = new BitArray(items.bits);
        else if (index < bits.Count && index >= 0)
        {
            var bools = ToBooleanArray().ToList();
            bools.InsertRange(index, items.ToBooleanArray());
            this.bits = new BitArray(bools.ToArray());
        }
        else
            throw new IndexOutOfRangeException();
    }

    /// <summary>
    /// Insert the specified bit in the specified index.
    /// </summary>
    /// <exception cref="IndexOutOfRangeException" />
    public void Insert(int index, bool item)
    {
        if (IsEmpty)
            bits = new BitArray(new[] { item });
        else if (index < bits.Length && index >= 0)
        {
            var bools = ToBooleanArray().ToList();
            bools.Insert(index, item);
            bits = new BitArray(bools.ToArray());
        }
        else
            throw new IndexOutOfRangeException();
    }

    /// <summary>
    /// Replace a list of bit of the current instance with another list of bit.
    /// </summary>
    /// <exception cref="EmptyInstanceException" />
    public void Replace(BitList oldBitList, BitList newBitList)
    {
        if (IsEmpty)
            throw new EmptyInstanceException();
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
    /// Set the value in the specified index with a new value.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException" />
    public void Set(int index, bool value)
    {
        if (!IsEmpty && (index < bits.Length || index >= 0))
            bits[index] = value;
        else
            throw new ArgumentOutOfRangeException();
    }

    /// <summary>
    /// Set the value in the specified index with a new collection of values.
    /// </summary>
    /// <exception cref="IndexOutOfRangeException" />
    public void Set(int index, BitList value)
    {
        if (!IsEmpty && index < bits.Length && index >= 0 && value.Count <= bits.Length - index && value.Count > 0)
        {
            var bools = ToBooleanArray().ToList();
            bools.RemoveRange(index, value.Count);
            bools.InsertRange(index, value.ToBooleanArray());
            bits = new BitArray(bools.ToArray());
        }
        else
            throw new IndexOutOfRangeException();
    }

    /// <summary>
    /// Get a the value in the specified index.
    /// </summary>
    /// <exception cref="IndexOutOfRangeException" />
    public bool Get(int index)
    {
        if (!IsEmpty && (index < bits.Length || index >= 0))
            return bits[index];
        else
            throw new IndexOutOfRangeException();
    }

    /// <summary>
    /// Get a range of value in the specified index.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException" />
    public BitList Get(int index, int count)
    {
        if (!IsEmpty && index < bits.Length && index >= 0 && count <= bits.Length - index && count > 0)
        {
            var bools = new List<bool>();
            for (int i = 0; i < count; i++)
                bools.Add(bits[index + i]);
            return new BitList(bools.ToArray());
        }
        else
            throw new ArgumentOutOfRangeException();
    }

    /// <summary>
    /// Clear the instance.
    /// </summary>
    public void Clear()
    {
        bits = null;
    }

    /// <summary>
    /// Create a list of bit from a file.
    /// </summary>
    /// <exception cref="FileReadingException" />
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
    /// Write the current instance into a file.
    /// </summary>
    /// <exception cref="FileWritingException" />
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
            return string.Empty;
        var text = string.Empty;
        foreach (var bit in ToBooleanArray())
            text += bit ? "1" : "0";
        return text;
    }

    /// <summary>
    /// Create an instance from a text rapresentation.
    /// </summary>
    public static BitList Parse(string text)
    {
        if (string.IsNullOrEmpty(text))
            return new BitList();
        var bools = new List<bool>();
        foreach (var stringBit in text)
            bools.Add(stringBit == '1');
        return new BitList(bools.ToArray());
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

    public bool[] ToBooleanArray()
    {
        if (IsEmpty)
            return new bool[0];
        else
        {
            var bools = new bool[bits.Length];
            for (int i = 0; i < bits.Length; i++)
                bools[i] = bits[i];
            return bools;
        }
    }

    public byte[] ToByteArray()
    {
        if (IsEmpty)
            return new byte[0];
        else
        {
            int inum = bits.Length / 8;
            double dnum = (double)bits.Length / 8;
            if (dnum.ToString().Contains(".") || dnum.ToString().Contains(","))
                inum++;
            var bytes = new byte[inum];
            bits.CopyTo(bytes, 0);
            return bytes;
        }
    }

    public short[] ToInt16Array()
    {
        if (IsEmpty)
            return new short[0];
        else
        {
            var bytes = ToByteArray();
            var bytesList = new List<byte[]>();
            var c = -1;
            for (int i = 0; i < bytes.Length; i++)
            {
                c++;
                if (i == 0 || c == 2)
                {
                    bytesList.Add(new byte[2]);
                    c = 0;
                }
                bytesList[bytesList.Count - 1][c] = bytes[i];
            }
            var shortList = new List<short>();
            foreach (var shbytes in bytesList)
                shortList.Add(BitConverter.ToInt16(shbytes, 0));
            return shortList.ToArray();
        }
    }

    public int[] ToInt32Array()
    {
        if (IsEmpty)
            return new int[0];
        else
        {
            int inum = bits.Length / 32;
            double dnum = (double)bits.Length / 32;
            if (dnum.ToString().Contains(".") || dnum.ToString().Contains(","))
                inum++;
            var ints = new int[inum];
            bits.CopyTo(ints, 0);
            return ints;
        }
    }

    public long[] ToInt64Array()
    {
        if (IsEmpty)
            return new long[0];
        else
        {
            var bytes = ToByteArray();
            var bytesList = new List<byte[]>();
            var c = -1;
            for (int i = 0; i < bytes.Length; i++)
            {
                c++;
                if (i == 0 || c == 8)
                {
                    bytesList.Add(new byte[8]);
                    c = 0;
                }
                bytesList[bytesList.Count - 1][c] = bytes[i];
            }
            var longList = new List<long>();
            foreach (var lbytes in bytesList)
                longList.Add(BitConverter.ToInt64(lbytes, 0));
            return longList.ToArray();
        }
    }

    public sbyte[] ToSByteArray()
    {
        if (IsEmpty)
            return new sbyte[0];
        else
        {
            var bytes = ToByteArray();
            var sbytes = new List<sbyte>();
            foreach (var bits in bytes)
                sbytes.Add((sbyte)bits);
            return sbytes.ToArray();
        }
    }

    public ushort[] ToUInt16Array()
    {
        if (IsEmpty)
            return new ushort[0];
        else
        {
            var shorts = ToInt16Array();
            var ushorts = new List<ushort>();
            foreach (var bits in shorts)
                ushorts.Add((ushort)bits);
            return ushorts.ToArray();
        }
    }

    public uint[] ToUInt32Array()
    {
        if (IsEmpty)
            return new uint[0];
        else
        {
            var ints = ToInt32Array();
            var uints = new List<uint>();
            foreach (var bits in ints)
                uints.Add((uint)bits);
            return uints.ToArray();
        }
    }

    public ulong[] ToUInt64Array()
    {
        if (IsEmpty)
            return new ulong[0];
        else
        {
            var longs = ToInt64Array();
            var ulongs = new List<ulong>();
            foreach (var bits in longs)
                ulongs.Add((ulong)bits);
            return ulongs.ToArray();
        }
    }

    public float[] ToSingleArray()
    {
        if (IsEmpty)
            return new float[0];
        else
        {
            var bytes = ToByteArray();
            var bytesList = new List<byte[]>();
            var c = -1;
            for (int i = 0; i < bytes.Length; i++)
            {
                c++;
                if (i == 0 || c == 4)
                {
                    bytesList.Add(new byte[4]);
                    c = 0;
                }
                bytesList[bytesList.Count - 1][c] = bytes[i];
            }
            var floatList = new List<float>();
            foreach (var fbytes in bytesList)
                floatList.Add(BitConverter.ToSingle(fbytes, 0));
            return floatList.ToArray();
        }
    }

    public double[] ToDoubleArray()
    {
        if (IsEmpty)
            return new double[0];
        else
        {
            var bytes = ToByteArray();
            var bytesList = new List<byte[]>();
            var c = -1;
            for (int i = 0; i < bytes.Length; i++)
            {
                c++;
                if (i == 0 || c == 8)
                {
                    bytesList.Add(new byte[8]);
                    c = 0;
                }
                bytesList[bytesList.Count - 1][c] = bytes[i];
            }
            var doubleList = new List<double>();
            foreach (var dbytes in bytesList)
                doubleList.Add(BitConverter.ToDouble(dbytes, 0));
            return doubleList.ToArray();
        }
    }

    public decimal[] ToDecimalArray()
    {
        if (IsEmpty)
            return new decimal[0];
        else
        {
            try
            {
                var ints = ToInt32Array();
                var intsList = new List<int[]>();
                var c = -1;
                for (int i = 0; i < ints.Length; i++)
                {
                    c++;
                    if (i == 0 || c == 4)
                    {
                        intsList.Add(new int[4]);
                        c = 0;
                    }
                    intsList[intsList.Count - 1][c] = ints[i];
                }
                var decimalList = new List<decimal>();
                foreach (var dints in intsList)
                    decimalList.Add(new decimal(dints));
                return decimalList.ToArray();
            }
            catch
            {
                throw new ConversionErrorException();
            }
        }
    }

    public char[] ToCharArray()
    {
        if (IsEmpty)
            return new char[0];
        else
        {
            var bytes = ToByteArray();
            var bytesList = new List<byte[]>();
            var c = -1;
            for (int i = 0; i < bytes.Length; i++)
            {
                c++;
                if (i == 0 || c == 4)
                {
                    bytesList.Add(new byte[4]);
                    c = 0;
                }
                bytesList[bytesList.Count - 1][c] = bytes[i];
            }
            var charList = new List<char>();
            foreach (var cbytes in bytesList)
                charList.Add(BitConverter.ToChar(cbytes, 0));
            return charList.ToArray();
        }
    }

    public override string ToString()
    {
        if (IsEmpty)
            return string.Empty;
        else
            return new string(ToCharArray());
    }

    /// <summary>
    /// Return true if the specified BitList is equal to this istance.
    /// </summary>
    /// <param name="obj">A list of bit.</param>
    public override bool Equals(object obj)
    {
        return Equals(this, obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
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
        var leftBitList = left.ToBooleanArray();
        var rightBitList = right.ToBooleanArray();
        var leftList = leftBitList.ToList();
        foreach (var bit in rightBitList)
            leftList.Add(bit);
        return new BitList(leftList.ToArray());
    }

    public static BitList operator +(BitList left, bool right)
    {
        return left + new BitList(right);
    }

    public static BitList operator +(bool left, BitList right)
    {
        return right + left;
    }

    public static bool operator ==(BitList left, BitList right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(BitList left, BitList right)
    {
        return !left.Equals(right);
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

    private class BitListEnumerator : IEnumerator<bool>
    {
        public int index;
        public BitList bits;
        object IEnumerator.Current { get => bits[index]; }
        bool IEnumerator<bool>.Current { get => bits[index]; }
        void IDisposable.Dispose() { /*Do nothing becouse is useless but necessary for the IEnumerator<bool> interface*/ }

        public BitListEnumerator(BitList bits)
        {
            index = -1;
            this.bits = bits;
        }

        bool IEnumerator.MoveNext()
        {
            if (bits.IsEmpty)
                return false;
            else
            {
                index++;
                return index < bits.Count;
            }
        }

        void IEnumerator.Reset()
        {
            index = -1;
        }
    }
}
