using System;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace DataStructures.Probabilistic
{
    public class BloomFilter<T> where T : notnull
    {
        private const ulong FnvPrime = 1099511628211;
        private const ulong FnvOffsetBasis = 14695981039346656037;
        private readonly byte[] filter;
        private readonly byte[][] salts;
        private readonly uint sizeBits;

        /// <summary>
        /// Initializes a new instance of the <see cref="BloomFilter{T}"/> class. This constructor will create a Bloom Filter
        /// of an optimal size with the optimal number of hashes to minimize the error rate.
        /// </summary>
        /// <param name="expectedNumElements">Expected number of unique elements that could be added to the filter.</param>
        public BloomFilter(uint expectedNumElements)
        {
            var k = (int)Math.Ceiling(.693 * 8 * expectedNumElements / expectedNumElements); // compute optimal number of hashes
            filter = new byte[expectedNumElements]; // set up filter with 8 times as many bits as elements
            sizeBits = expectedNumElements * 8; // number of bit slots in the filter
            salts = new byte[k][]; // number of salts to use (corresponds to our k)
            var rand = new Random();
            for (var i = 0; i < k; i++)
            {
                salts[i] = BitConverter.GetBytes(rand.Next());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BloomFilter{T}"/> class.
        /// This constructor let's you decide how large you want the filter to be as well as allowing you to specify
        /// how many hashes it will use. Only use if you don't care to optimize false positivity.
        /// </summary>
        /// <param name="sizeBits">size in bits you want the filter to be.</param>
        /// <param name="numHashes">number of hash functions to be used.</param>
        public BloomFilter(uint sizeBits, uint numHashes)
        {
            filter = new byte[sizeBits / 8 + 1];
            salts = new byte[numHashes][];
            this.sizeBits = sizeBits;
            var rnd = new Random();
            for (var i = 0; i < numHashes; i++)
            {
                salts[i] = BitConverter.GetBytes(rnd.Next());
            }
        }

        /// <summary>
        /// Inserts an item into the bloom filter.
        /// </summary>
        /// <param name="item">The item being inserted into the Bloom Filter.</param>
        public void Insert(T item)
        {
            var hashBytes = Serialize(item); // serialize the item to an array of bytes
            foreach (var salt in salts)
            {
                var arr = salt.Concat(hashBytes); // prepend the salt to the array of bytes to make a more unique hash
                var slot = Fnv1(arr.ToArray()) % sizeBits; // hash the bytes to come up with a slot number.
                filter[slot / 8] |= (byte)(1 << ((int)(slot % 8))); // set the filter at the decided slot to 1.
            }
        }

        /// <summary>
        /// Searches the Bloom Filter to determine if the item exists in the Bloom Filter.
        /// </summary>
        /// <param name="item">The item being searched for in the Bloom Filter.</param>
        /// <returns>true if the item has been added to the Bloom Filter, false otherwise.</returns>
        public bool Search(T item)
        {
            var hashBytes = Serialize(item);
            foreach (var salt in salts)
            {
                var fnvBytes = salt.Concat(hashBytes); // prepend the salt bytes to the serialized object bytes to make a unique string for hashing.
                var slot = (int)(Fnv1(fnvBytes.ToArray()) % sizeBits); // calculate the slot in the filter.
                var @byte = filter[slot / 8]; // Extract the byte in the filter.
                var mask = 1 << (slot % 8); // Build the mask for the slot number.
                if ((@byte & mask) != mask)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Implementation of the FNV1 hashing function.
        /// </summary>
        /// <param name="data">data to be hashed.</param>
        /// <returns>the hashed value.</returns>
        private static ulong Fnv1(byte[] data)
        {
            var hash = FnvOffsetBasis;
            foreach (var @byte in data)
            {
                hash = hash * FnvPrime;
                hash ^= @byte;
            }

            return hash;
        }

        /// <summary>
        /// Serializes the item into a byte array. This uses the JSON Serializer as opposed to the BinarySerializer due to the
        /// noted security issues of the BinarySerializer. The purpose of reducing the object to JSON is to ensure that
        /// items with equal internals are considered the same object.
        /// </summary>
        /// <param name="item">the item to be serialized.</param>
        /// <returns>The item being serialized sent to an array of UTF8 bytes of it's JSON data.</returns>
        /// <exception cref="ArgumentNullException">Thrown if you pass in a null value.</exception>
        private byte[] Serialize(T item)
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(item));
        }
    }
}
