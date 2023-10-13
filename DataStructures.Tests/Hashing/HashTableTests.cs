using System;
using System.Collections.Generic;
using DataStructures.Hashing;
using NUnit.Framework;

namespace DataStructures.Tests.Hashing
{
    [TestFixture]
    public class HashTableTests
    {
        [Test]
        public void Add_ThrowsException_WhenKeyIsNull()
        {
            var hashTable = new HashTable<string, int>();

            Assert.Throws<ArgumentNullException>(() => hashTable.Add(null, 1));
        }

        [Test]
        public void Add_ThrowsException_WhenKeyAlreadyExists()
        {
            var hashTable = new HashTable<string, int>();

            hashTable.Add("a", 1);

            Assert.Throws<ArgumentException>(() => hashTable.Add("a", 2));
        }

        [Test]
        public void Add_IncreasesCount_WhenKeyDoesNotExist()
        {
            var hashTable = new HashTable<string, int>();

            hashTable.Add("a", 1);

            Assert.AreEqual(1, hashTable.Count);
        }

        [Test]
        public void Add_DoesNotIncreaseCount_WhenKeyAlreadyExists()
        {
            var hashTable = new HashTable<string, int>();

            hashTable.Add("a", 1);
            try
            {
                hashTable.Add("a", 2);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException");
            }
            Assert.AreEqual(1, hashTable.Count);
        }

        [Test]
        public void Add_ThrowsException_WhenValueIsNull()
        {
            var hashTable = new HashTable<string, string>();

            Assert.Throws<ArgumentNullException>(() => hashTable.Add("a", null));
        }

        [Test]
        public void Add_IncreasesCount_WhenValueDoesNotExist()
        {
            var hashTable = new HashTable<string, int>();

            hashTable.Add("a", 1);

            Assert.AreEqual(1, hashTable.Count);
        }

        [Test]
        public void Add_DoesNotIncreaseCount_WhenValueAlreadyExists()
        {
            var hashTable = new HashTable<string, int>();

            hashTable.Add("a", 1);

            try
            {
                hashTable.Add("b", 1);
            }
            catch (ArgumentException)
            {
            }

            Assert.AreEqual(2, hashTable.Count);
        }

        [Test]
        public void Add_IncreasesCount_WhenValueIsNull()
        {
            var hashTable = new HashTable<string, string>();

            try
            {
                hashTable.Add("a", null);
            }
            catch (ArgumentNullException)
            {
            }
            Assert.AreEqual(0, hashTable.Count);
        }

        [Test]
        public void Add_IncreasesCount_WhenValueAlreadyExists()
        {
            var hashTable = new HashTable<string, int>();

            hashTable.Add("a", 1);
            hashTable.Add("b", 1);
            Assert.AreEqual(2, hashTable.Count);
        }

        [Test]
        public void Remove_ThrowsException_WhenKeyIsNull()
        {
            var hashTable = new HashTable<string, int>();

            Assert.Throws<ArgumentNullException>(() => hashTable.Remove(null));
        }

        [Test]
        public void Remove_ReturnsFalse_WhenKeyDoesNotExist()
        {
            var hashTable = new HashTable<string, int>();

            Assert.IsFalse(hashTable.Remove("a"));
        }

        [Test]
        public void Remove_ReturnsTrue_WhenKeyExists()
        {
            var hashTable = new HashTable<string, int>();

            hashTable.Add("a", 1);

            Assert.IsTrue(hashTable.Remove("a"));
        }

        [Test]
        public void Remove_DecreasesCount_WhenKeyExists()
        {
            var hashTable = new HashTable<string, int>();

            hashTable.Add("a", 1);
            hashTable.Remove("a");

            Assert.AreEqual(0, hashTable.Count);
        }

        [Test]
        public void Remove_DoesNotDecreaseCount_WhenKeyDoesNotExist()
        {
            var hashTable = new HashTable<string, int>();

            hashTable.Remove("a");

            Assert.AreEqual(0, hashTable.Count);
        }

        [Test]
        public void ContainsValue_ReturnsFalse_WhenValueDoesNotExist()
        {
            var hashTable = new HashTable<string, int>();

            Assert.IsFalse(hashTable.ContainsValue(1));
        }

        [Test]
        public void ContainsValue_ReturnsTrue_WhenValueExists()
        {
            var hashTable = new HashTable<string, int>();

            hashTable.Add("a", 1);

            Assert.IsTrue(hashTable.ContainsValue(1));
        }

        [Test]
        public void ContainsValue_ReturnsFalse_WhenValueIsNull()
        {
            var hashTable = new HashTable<string, string>();

            Assert.Throws<ArgumentNullException>(() => hashTable.ContainsValue(null));
        }

        [Test]
        public void ContainsKey_ReturnsFalse_WhenKeyDoesNotExist()
        {
            var hashTable = new HashTable<string, int>();

            Assert.IsFalse(hashTable.ContainsKey("a"));
        }

        [Test]
        public void ContainsKey_ReturnsTrue_WhenKeyExists()
        {
            var hashTable = new HashTable<string, int>();

            hashTable.Add("a", 1);

            Assert.IsTrue(hashTable.ContainsKey("a"));
        }

        [Test]
        public void ContainsKey_ReturnsFalse_WhenKeyIsNull()
        {
            var hashTable = new HashTable<string, int>();

            Assert.Throws<ArgumentNullException>(() => hashTable.ContainsKey(null));
        }

        [Test]
        public void Clear_SetsCountToZero()
        {
            var hashTable = new HashTable<string, int>();

            hashTable.Add("a", 1);
            hashTable.Clear();

            Assert.AreEqual(0, hashTable.Count);
        }

        [Test]
        public void Clear_RemovesAllElements()
        {
            var hashTable = new HashTable<string, int>();

            hashTable.Add("a", 1);
            hashTable.Clear();

            Assert.IsFalse(hashTable.ContainsKey("a"));
        }

        [Test]
        public void Resize_IncreasesCapacity()
        {
            var hashTable = new HashTable<string, int>(4);

            hashTable.Add("one", 1);
            hashTable.Add("two", 2);
            hashTable.Add("three", 3);
            hashTable.Add("four", 4);
            hashTable.Add("humour", 5);

            /// Next Prime number after 4 is 5
            /// Capacity should be 5
            /// After resizing, the capacity should be 10
            Assert.AreEqual(10, hashTable.Capacity);
        }
    }
}
