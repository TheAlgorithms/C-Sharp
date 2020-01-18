// Original Author: Christian Bender
// Class: BitArray
//
// implements IComparable, ICloneable, IEnumerator, IEnumerable
//
// This class implements a bit-array and provides some
// useful functions/operations to deal with this type of
// data structure. You see a overview about the functionality, below.
//
//
// Overview
//
// Constructor (N : int)
// The constructor receives a length (N) of the to create bit-field.
//
// Constructor (sequence : string)
// setups the array with the input sequence.
// assumes: the sequence may only be allowed contains onese or zeros.
//
// Constructor (bits : bool[] )
// setups the bit-field with the input array.
//
// Compile(sequence : string)
// compiles a string sequence of 0's and 1's in the inner structure.
// assumes: the sequence may only be allowed contains onese or zeros.
//
// Compile (number : int)
// compiles a positive integer number in the inner data structure.
//
// Compile (number : long)
// compiles a positive long integer number in the inner data structure.
//
// ToString ()
// returns a string representation of the inner structure.
// The returned string is a sequence of 0's and 1's.
//
// Length : int
// Is a property that returns the length of the bit-field.
//
// Indexer : bool
// indexer for selecting the individual bits of the bit array.
//
// NumberOfOneBits() : int
// returns the number of One-bits.
//
// NumberOfZeroBits() : int
// returns the number of Zero-Bits.
//
// EvenParity() : bool
// returns true if parity is even, otherwise false.
//
// OddParity() : bool
// returns true if parity is odd, otherwise false.
//
// ToInt64() : long
// returns a long integer representation of the bit-array.
// assumes: the bit-array length must been smaller or equal to 64 bit.
//
// ToInt32() : int
// returns a integer representation of the bit-array.
// assumes: the bit-array length must been smaller or equal to 32 bit.
//
// ResetField() : void
// sets all bits on false.
//
// SetAll(flag : bool) : void
// sets all bits on the value of the flag.
//
// GetHashCode() : int
// returns hash-code (ToInt32())
//
// Equals (other : Object) : bool
// returns true if there inputs are equal otherwise false.
// assumes: the input bit-arrays must have same length.
//
// CompareTo (other : Object) : int  (interface IComparable)
// output:  0 - if the bit-arrays a equal.
// -1 - if this bit-array is smaller.
// 1 - if this bit-array is greater.
// assumes: bit-array lentgh must been smaller or equal to 64 bit
//
// Clone () : object
// returns a copy of this bit-array
//
// Current : object
// returns the current selected bit.
//
// MoveNext() : bool
// purpose: increases the position of the enumerator
// returns true if 'position' successful increased otherwise false.
//
// Reset() : void
// resets the position of the enumerator.
//
// GetEnumerator() : IEnumerator
// returns a enumerator for this BitArray-object.
//
// Operations:
//
// &amp; bitwise AND
// | bitwise OR
// ~ bitwise NOT
// >> bitwise shift right
// >> bitwise shift left
// ^ bitwise XOR
//
// Each operation (above) returns a new BitArray-object.
//
// == equal operator. : bool
// returns true if there inputs are equal otherwise false.
// assumes: the input bit-arrays must have same length.
//
// != not-equal operator : bool
// returns true if there inputs aren't equal otherwise false.
// assumes: the input bit-arrays must have same length.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    /// <summary>
    /// This class implements a bit-array and provides some
    /// useful functions/operations to deal with this type of
    /// data structure.
    /// </summary>
    public sealed class BitArray : ICloneable, IEnumerator<bool>, IEnumerable<bool>
    {
        private readonly bool[] field; // the actual bit-field
        private int position = -1; // position for enumerator

        /// <summary>
        /// Initializes a new instance of the <see cref="BitArray"/> class.
        /// setups the array with false-values.
        /// </summary>
        /// <param name="n">length of the array.</param>
        public BitArray(int n)
        {
            if (n < 1)
            {
                field = new bool[0];
            }

            field = new bool[n];

            // fills up the field with zero-bits.
            for (var i = 0; i < n; i++)
            {
                field[i] = false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitArray"/> class.
        /// Setups the array with the input sequence.
        ///
        /// purpose: Setups the array with the input sequence.
        /// assumes: sequence must been greater or equal to 1.
        /// the sequence may only be allowed contains onese or zeros.
        /// </summary>
        /// <param name="sequence">A string sequence of 0's and 1's.</param>
        public BitArray(string sequence)
        {
            // precondition I
            if (sequence.Length > 0)
            {
                // precondition II
                if (Match(sequence))
                {
                    field = new bool[sequence.Length];
                    Compile(sequence);
                }
                else
                {
                    // error case II
                    throw new Exception("BitArray: the sequence may only " +
                                        "be allowed contains onese or zeros.");
                }
            }
            else
            {
                // error case I
                throw new Exception("BitArray: sequence must been greater or equal as 1");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitArray"/> class.
        /// Setups the bit-array with the input array.
        /// </summary>
        /// <param name="bits">A boolean array of bits.</param>
        public BitArray(bool[] bits) => field = bits;

        /// <summary>
        /// Gets a value indicating whether the current bit of the array is set.
        /// </summary>
        public bool Current
        {
            get
            {
                try
                {
                    return field[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current bit of the array is set.
        /// </summary>
        object IEnumerator.Current
        {
            get
            {
                try
                {
                    return field[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Gets the length of the current bit array.
        /// </summary>
        private int Length => field.Length;

        /// <summary>
        /// Gets element given an offset.
        /// </summary>
        /// <param name="offset">Position.</param>
        /// <returns>Element on array.</returns>
        public bool this[int offset]
        {
            get => field[offset];
            private set => field[offset] = value;
        }

        /// <summary>
        /// Returns a bit-array that represents the bit by bit AND (&amp;).
        /// Assumes arrays have the same length.
        /// </summary>
        /// <param name="one">First bit-array.</param>
        /// <param name="two">Second bit-array.</param>
        /// <returns>bit-array.</returns>
        public static BitArray operator &(BitArray one, BitArray two)
        {
            var sequence1 = one.ToString();
            var sequence2 = two.ToString();
            var result = string.Empty;
            var tmp = string.Empty;

            // for scaling of same length.
            if (one.Length != two.Length)
            {
                int difference;
                if (one.Length > two.Length)
                {
                    // one is greater
                    difference = one.Length - two.Length;

                    // fills up with 0's
                    for (var i = 0; i < difference; i++)
                    {
                        tmp += '0';
                    }

                    tmp += two.ToString();
                    sequence2 = tmp;
                }
                else
                {
                    // two is greater
                    difference = two.Length - one.Length;

                    // fills up with 0's
                    for (var i = 0; i < difference; i++)
                    {
                        tmp += '0';
                    }

                    tmp += one.ToString();
                    sequence1 = tmp;
                }
            } // end scaling

            var len = one.Length > two.Length ? one.Length : two.Length;
            var ans = new BitArray(len);

            for (var i = 0; i < one.Length; i++)
            {
                result += sequence1[i].Equals('1') && sequence2[i].Equals('1') ? '1' : '0';
            }

            result = result.Trim();
            ans.Compile(result);

            return ans;
        }

        /// <summary>
        /// Returns a bit-array that represents the bit by bit OR.
        /// Assumes arrays have the same length.
        /// </summary>
        /// <param name="one">First bit-array.</param>
        /// <param name="two">Second bit-array.</param>
        /// <returns>bit-array that represents the bit by bit OR.</returns>
        public static BitArray operator |(BitArray one, BitArray two)
        {
            var sequence1 = one.ToString();
            var sequence2 = two.ToString();
            var result = string.Empty;
            var tmp = string.Empty;

            // for scaling of same length.
            if (one.Length != two.Length)
            {
                int difference;
                if (one.Length > two.Length)
                {
                    // one is greater
                    difference = one.Length - two.Length;

                    // fills up with 0's
                    for (var i = 0; i < difference; i++)
                    {
                        tmp += '0';
                    }

                    tmp += two.ToString();
                    sequence2 = tmp;
                }
                else
                {
                    // two is greater
                    difference = two.Length - one.Length;

                    // fills up with 0's
                    for (var i = 0; i < difference; i++)
                    {
                        tmp += '0';
                    }

                    tmp += one.ToString();
                    sequence1 = tmp;
                }
            } // end scaling

            var len = one.Length > two.Length ? one.Length : two.Length;
            var ans = new BitArray(len);

            for (var i = 0; i < len; i++)
            {
                result += sequence1[i].Equals('0') && sequence2[i].Equals('0') ? '0' : '1';
            }

            result = result.Trim();
            ans.Compile(result);

            return ans;
        }

        /// <summary>
        /// Returns a bit-array that represents the operator ~ (NOT).
        /// Assumes arrays have the same length.
        /// </summary>
        /// <param name="one">Bit-array.</param>
        /// <returns>bitwise not.</returns>
        public static BitArray operator ~(BitArray one)
        {
            var ans = new BitArray(one.Length);
            var sequence = one.ToString();
            var result = string.Empty;

            foreach (var ch in sequence)
            {
                if (ch == '1')
                {
                    result += '0';
                }
                else
                {
                    result += '1';
                }
            }

            result = result.Trim();
            ans.Compile(result);

            return ans;
        }

        /// <summary>
        /// Returns a bit-array that represents bitwise shift left (&gt;&gt;).
        /// Assumes arrays have the same length.
        /// </summary>
        /// <param name="other">Bit-array.</param>
        /// <param name="n">Number of bits.</param>
        /// <returns>Bitwise shifted BitArray.</returns>
        public static BitArray operator <<(BitArray other, int n)
        {
            var ans = new BitArray(other.Length + n);

            // actual shifting process
            for (var i = 0; i < other.Length; i++)
            {
                ans[i] = other[i];
            }

            return ans;
        }

        /// <summary>
        /// Returns a bit-array that represents the bit by bit XOR.
        /// Assumes arrays have the same length.
        /// </summary>
        /// <param name="one">First bit-array.</param>
        /// <param name="two">Second bit-array.</param>
        /// <returns>bit-array.</returns>
        public static BitArray operator ^(BitArray one, BitArray two)
        {
            var sequence1 = one.ToString();
            var sequence2 = two.ToString();
            var tmp = string.Empty;

            // for scaling of same length.
            if (one.Length != two.Length)
            {
                int difference;
                if (one.Length > two.Length)
                {
                    // one is greater
                    difference = one.Length - two.Length;

                    // fills up with 0's
                    for (var i = 0; i < difference; i++)
                    {
                        tmp += '0';
                    }

                    tmp += two.ToString();
                    sequence2 = tmp;
                }
                else
                {
                    // two is greater
                    difference = two.Length - one.Length;

                    // fills up with 0's
                    for (var i = 0; i < difference; i++)
                    {
                        tmp += '0';
                    }

                    tmp += one.ToString();
                    sequence1 = tmp;
                }
            } // end scaling

            var len = one.Length > two.Length ? one.Length : two.Length;
            var ans = new BitArray(len);

            var sb = new StringBuilder();

            for (var i = 0; i < len; i++)
            {
                _ = sb.Append(sequence1[i] == sequence2[i] ? '0' : '1');
            }

            var result = sb.ToString().Trim();
            ans.Compile(result);

            return ans;
        }

        /// <summary>
        /// Returns a bit-array that represents bitwise shift right (>>).
        /// Assumes arrays have the same length.
        /// </summary>
        /// <param name="other">Bit-array.</param>
        /// <param name="n">Number of bits.</param>
        /// <returns>Bitwise shifted BitArray.</returns>
        public static BitArray operator >>(BitArray other, int n)
        {
            var ans = new BitArray(other.Length - n);

            // actual shifting process.
            for (var i = 0; i < other.Length - n; i++)
            {
                ans[i] = other[i];
            }

            return ans;
        }

        /// <summary>
        /// Checks if both arrays are == (equal).
        /// The input assumes arrays have the same length.
        /// </summary>
        /// <param name="one">First bit-array.</param>
        /// <param name="two">Second bit-array.</param>
        /// <returns>Returns True if there inputs are equal; False otherwise.</returns>
        public static bool operator ==(BitArray one, BitArray two)
        {
            if (ReferenceEquals(one, two))
            {
                return true;
            }

            if (one is null || two is null)
            {
                return false;
            }

            if (one.Length != two.Length)
            {
                return false;
            }

            var status = true;
            for (var i = 0; i < one.Length; i++)
            {
                if (one[i] != two[i])
                {
                    status = false;
                    break;
                }
            }

            return status;
        }

        /// <summary>
        /// Checks if both arrays are != (not-equal).
        /// The input assumes arrays have the same length.
        /// </summary>
        /// <param name="one">First bit-array.</param>
        /// <param name="two">Second bit-array.</param>
        /// <returns>Returns True if there inputs aren't equal; False otherwise.</returns>
        public static bool operator !=(BitArray one, BitArray two) => !(one == two);

        /// <summary>
        /// Returns a copy of the current bit-array.
        /// </summary>
        /// <returns>Bit-array clone.</returns>
        public object Clone()
        {
            var theClone = new BitArray(Length);

            for (var i = 0; i < Length; i++)
            {
                theClone[i] = field[i];
            }

            return theClone;
        }

        /// <summary>
        /// Gets a enumerator for this BitArray-Object.
        /// </summary>
        /// <returns>Returns a enumerator for this BitArray-Object.</returns>
        public IEnumerator<bool> GetEnumerator() => this;

        /// <summary>
        /// Gets a enumerator for this BitArray-Object.
        /// </summary>
        /// <returns>Returns a enumerator for this BitArray-Object.</returns>
        IEnumerator IEnumerable.GetEnumerator() => this;

        /// <summary>
        /// MoveNext (for interface IEnumerator).
        /// </summary>
        /// <returns>Returns True if 'position' successful increased; False otherwise.</returns>
        public bool MoveNext()
        {
            if (position + 1 >= field.Length)
            {
                return false;
            }

            position++;
            return true;
        }

        /// <summary>
        /// Resets the position of the enumerator.
        /// Reset (for interface IEnumerator).
        /// </summary>
        public void Reset() => position = -1;

        /// <summary>
        /// Compiles the binary sequence into the inner data structure.
        /// The sequence must have the same length, as the bit-array.
        /// The sequence may only be allowed contains onese or zeros.
        /// </summary>
        /// <param name="sequence">A string sequence of 0's and 1's.</param>
        public void Compile(string sequence)
        {
            var tmp = string.Empty;

            sequence = sequence.Trim();

            // precondition I
            if (sequence.Length <= field.Length)
            {
                // precondition II
                if (Match(sequence))
                {
                    // for appropriate scaling
                    if (sequence.Length < field.Length)
                    {
                        var difference = field.Length - sequence.Length;

                        for (var i = 0; i < difference; i++)
                        {
                            tmp += '0';
                        }

                        tmp += sequence;
                        sequence = tmp;
                    }

                    // actual compile procedure.
                    for (var i = 0; i < sequence.Length; i++)
                    {
                        field[i] = sequence[i] == '1';
                    }
                }
                else
                {
                    // error case II
                    throw new Exception("Compile: the sequence may only " +
                                        "be allowed contains onese or zeros.");
                }
            }
            else
            {
                // error case I
                throw new Exception("Compile: not equal length!");
            }
        }

        /// <summary>
        /// Compiles integer number into the inner data structure.
        /// Assumes: the number must have the same bit length.
        /// </summary>
        /// <param name="number">A positive integer number.</param>
        public void Compile(int number)
        {
            var tmp = string.Empty;

            // precondition I
            if (number > 0)
            {
                // converts to binary representation
                var binaryNumber = Convert.ToString(number, 2);

                // precondition II
                if (binaryNumber.Length <= field.Length)
                {
                    // for appropriate scaling
                    if (binaryNumber.Length < field.Length)
                    {
                        var difference = field.Length - binaryNumber.Length;

                        for (var i = 0; i < difference; i++)
                        {
                            tmp += '0';
                        }

                        tmp += binaryNumber;
                        binaryNumber = tmp;
                    }

                    // actual compile procedure.
                    for (var i = 0; i < binaryNumber.Length; i++)
                    {
                        field[i] = binaryNumber[i] == '1';
                    }
                }
                else
                {
                    // error case II
                    throw new Exception("Compile: not apt length!");
                }
            }
            else
            {
                // error case I
                throw new Exception("Compile: only positive numbers > 0");
            }
        }

        /// <summary>
        /// Compiles integer number into the inner data structure.
        /// The number must have the same bit length.
        /// </summary>
        /// <param name="number">A positive long integer number.</param>
        public void Compile(long number)
        {
            var tmp = string.Empty;

            // precondition I
            if (number > 0)
            {
                // converts to binary representation
                var binaryNumber = Convert.ToString(number, 2);

                // precondition II
                if (binaryNumber.Length <= field.Length)
                {
                    // for appropriate scaling
                    if (binaryNumber.Length < field.Length)
                    {
                        var difference = field.Length - binaryNumber.Length;

                        for (var i = 0; i < difference; i++)
                        {
                            tmp += '0';
                        }

                        tmp += binaryNumber;
                        binaryNumber = tmp;
                    }

                    // actual compile procedure.
                    for (var i = 0; i < binaryNumber.Length; i++)
                    {
                        field[i] = binaryNumber[i] == '1';
                    }
                }
                else
                {
                    // error case II
                    throw new Exception("Compile: not apt length!");
                }
            }
            else
            {
                // error case I
                throw new Exception("Compile: only positive numbers > 0");
            }
        }

        /// <summary>
        /// Is the opposit of the Compile(...) method.
        /// </summary>
        /// <returns>Returns a string representation of the inner data structure.</returns>
        public override string ToString()
        {
            // creates return-string
            return field.Aggregate(string.Empty, (current, t) => current + (t ? "1" : "0"));
        }

        /// <summary>
        /// Gets the number of one-bits in the field.
        /// </summary>
        /// <returns>quantity of bits in current bit-array.</returns>
        public int NumberOfOneBits()
        {
            // counting one-bits.
            return field.Count(bit => bit);
        }

        /// <summary>
        /// Gets the number of zero-bits in the field.
        /// </summary>
        /// <returns>quantity of bits.</returns>
        public int NumberOfZeroBits()
        {
            // counting zero-bits
            return field.Count(bit => !bit);
        }

        /// <summary>
        /// To check for even parity.
        /// </summary>
        /// <returns>Returns True if parity is even; False otherwise.</returns>
        public bool EvenParity() => NumberOfOneBits() % 2 == 0;

        /// <summary>
        /// To check for odd parity.
        /// </summary>
        /// <returns>Returns True if parity is odd; False otherwise.</returns>
        public bool OddParity() => NumberOfOneBits() % 2 != 0;

        /// <summary>
        /// Returns a long integer representation of the bit-array.
        /// Assumes the bit-array length must been smaller or equal to 64 bit.
        /// </summary>
        /// <returns>Long integer array.</returns>
        public long ToInt64()
        {
            // Precondition
            if (field.Length > 64)
            {
                throw new Exception("ToInt: field is too long.");
            }

            var sequence = ToString();
            return Convert.ToInt64(sequence, 2);
        }

        /// <summary>
        /// Returns a long integer representation of the bit-array.
        /// Assumes the bit-array length must been smaller or equal to 32 bit.
        /// </summary>
        /// <returns>integer array.</returns>
        public int ToInt32()
        {
            // Precondition
            if (field.Length > 32)
            {
                throw new Exception("ToInt: field is too long.");
            }

            var sequence = ToString();
            return Convert.ToInt32(sequence, 2);
        }

        /// <summary>
        /// Sets all bits on false.
        /// </summary>
        public void ResetField()
        {
            for (var i = 0; i < field.Length; i++)
            {
                field[i] = false;
            }
        }

        /// <summary>
        /// Sets all bits on the value of the flag.
        /// </summary>
        /// <param name="flag">Bollean flag (false-true).</param>
        public void SetAll(bool flag)
        {
            for (var i = 0; i < field.Length; i++)
            {
                field[i] = flag;
            }
        }

        /// <summary>
        /// Checks if bit-array are equal.
        /// Assumes the input bit-arrays must have same length.
        /// </summary>
        /// <param name="other">Bit-array object.</param>
        /// <returns>Returns true if there inputs are equal otherwise false.</returns>
        public override bool Equals(object other)
        {
            var status = true;

            var otherBitArray = (BitArray)other;

            if (Length == otherBitArray?.Length)
            {
                for (var i = 0; i < Length; i++)
                {
                    if (field[i] != otherBitArray[i])
                    {
                        status = false;
                    }
                }
            }
            else
            {
                throw new Exception("== : inputs haven't same length!");
            }

            return status;
        }

        /// <summary>
        /// Gets has-code of bit-array.
        /// Assumes bit-array lentgh must been smaller or equal to 32.
        /// </summary>
        /// <returns>hash-code for this BitArray instance.</returns>
        public override int GetHashCode() => ToInt32();

        /// <summary>
        /// Disposes object, nothing to dispose here though.
        /// </summary>
        public void Dispose()
        {
            // Done
        }

        /// <summary>
        /// Utility method foir checking a given sequence contains only zeros and ones.
        /// This method will used in Constructor (sequence : string) and Compile(sequence : string).
        /// </summary>
        /// <param name="sequence">String sequence.</param>
        /// <returns>returns True if sequence contains only zeros and ones; False otherwise.</returns>
        private static bool Match(string sequence)
        {
            var status = true;

            foreach (var ch in sequence)
            {
                if (ch != '0' && ch != '1')
                {
                    status = false;
                }
            }

            return status;
        }
    }
}
