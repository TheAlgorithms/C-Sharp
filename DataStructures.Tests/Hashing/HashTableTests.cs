using DataStructures.Hashing;

namespace DataStructures.Tests.Hashing;

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

        Assert.That(hashTable.Count, Is.EqualTo(1));
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
        Assert.That(hashTable.Count, Is.EqualTo(1));
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

        hashTable.Add("b", 1);

        Assert.That(hashTable.Count, Is.EqualTo(1));
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
            Console.WriteLine("ArgumentException");
        }

        Assert.That(hashTable.Count, Is.EqualTo(2));
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
            Console.WriteLine("ArgumentNullException");
        }
        Assert.That(hashTable.Count, Is.EqualTo(0));
    }

    [Test]
    public void Add_IncreasesCount_WhenValueAlreadyExists()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);
        hashTable.Add("b", 1);
        Assert.That(hashTable.Count, Is.EqualTo(2));
    }

    [Test]
    public void Add_ThrowsException_OnCollision()
    {
        // Arrange
        var hashTable = new HashTable<Collider, int>();
        hashTable.Add(new Collider(1), 1);


        // Act & Assert
        Assert.Throws<ArgumentException>(() => hashTable.Add(new Collider(1), 2));
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

        Assert.That(hashTable.Remove("a"), Is.False);
    }

    [Test]
    public void Remove_ReturnsTrue_WhenKeyExists()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);

        Assert.That(hashTable.Remove("a"), Is.True);
    }

    [Test]
    public void Remove_DecreasesCount_WhenKeyExists()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);
        hashTable.Remove("a");

        Assert.That(hashTable.Count, Is.EqualTo(0));
    }

    [Test]
    public void Remove_DoesNotDecreaseCount_WhenKeyDoesNotExist()
    {
        var hashTable = new HashTable<string, int>();
        hashTable.Remove("a");

        Assert.That(hashTable.Count, Is.EqualTo(0));
    }

    [Test]
    public void Remove_TriggersResizeDown()
    {
        var hashTable = new HashTable<int, string>(4);
        for (var i = 1; i <= 50; i++)
        {
            hashTable.Add(i, $"Value{i}");
        }

        for (var i = 1; i <= 40; i++)
        {
            hashTable.Remove(i);
        }

        Assert.That(hashTable.Capacity, Is.EqualTo(40));
    }

    [Test]
    public void Remove_TriggersResizeDown_MinimumOfDefaultCapacity()
    {
        var hashTable = new HashTable<int, string>(4);
        for (var i = 1; i <= 50; i++)
        {
            hashTable.Add(i, $"Value{i}");
        }

        for (var i = 1; i <= 48; i++)
        {
            hashTable.Remove(i);
        }

        Assert.That(hashTable.Capacity, Is.EqualTo(16));
    }

    [Test]
    public void ContainsValue_ReturnsFalse_WhenValueDoesNotExist()
    {
        var hashTable = new HashTable<string, int>();

        Assert.That(hashTable.ContainsValue(1), Is.False);
    }

    [Test]
    public void ContainsValue_ReturnsTrue_WhenValueExists()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);

        Assert.That(hashTable.ContainsValue(1), Is.True);
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

        Assert.That(hashTable.ContainsKey("a"), Is.False);
    }

    [Test]
    public void ContainsKey_ReturnsTrue_WhenKeyExists()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);

        Assert.That(hashTable.ContainsKey("a"), Is.True);
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

        Assert.That(hashTable.Count, Is.EqualTo(0));
    }

    [Test]
    public void Clear_RemovesAllElements()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Add("a", 1);
        hashTable.Clear();

        Assert.That(hashTable.ContainsKey("a"), Is.False);
    }

    [Test]
    public void Clear_ResetsTable()
    {
        var hashTable = new HashTable<int, string>();
        hashTable.Add(1, "A");
        hashTable.Clear();
        hashTable.Add(2, "B");
        Assert.That(hashTable.Count, Is.EqualTo(1));
        Assert.That(hashTable[2], Is.EqualTo("B"));
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
        Assert.That(hashTable.Capacity, Is.EqualTo(10));
    }
    [Test]
    public void LoadFactor_ReturnsCorrectValue()
    {
        var hashTable = new HashTable<string, int>(4);

        hashTable.Add("one", 1);
        hashTable.Add("two", 2);
        hashTable.Add("three", 3);
        hashTable.Add("four", 4);
        hashTable.Add("humour", 5);
        Assert.That(hashTable.LoadFactor, Is.EqualTo(0.75f));
    }

    [Test]
    public void Keys_ReturnsCorrectKeys()
    {
        var hashTable = new HashTable<int, string>();
        hashTable.Add(1, "one");
        hashTable.Add(2, "two");
        hashTable.Add(3, "three");

        var keys = new List<int> { 1, 2, 3 };

        Assert.That(keys, Is.EquivalentTo(hashTable.Keys));
    }

    [Test]
    public void Values_ReturnsCorrectValues()
    {
        var hashTable = new HashTable<int, string>();
        hashTable.Add(1, "one");
        hashTable.Add(2, "two");
        hashTable.Add(3, "three");

        var values = new List<string> { "one", "two", "three" };

        Assert.That(values, Is.EquivalentTo(hashTable.Values));
    }

    [Test]
    public void Constructor_ThrowsException_WhenCapacityIsZero()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(0));
    }

    [Test]
    public void Constructor_ThrowsException_WhenLoadFactorIsZero()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(4, 0));
    }

    [Test]
    public void Constructor_ThrowsException_WhenLoadFactorIsLessThanZero()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(4, -1));
    }

    [Test]
    public void Constructor_ThrowsException_WhenLoadFactorIsGreaterThanOne()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(4, 2));
    }

    [Test]
    public void Constructor_RoundsCapacityToPrime()
    {
        var hashTable = new HashTable<int, string>(17);
        Assert.That(hashTable.Capacity, Is.EqualTo(19));
    }

    [Test]
    public void GetIndex_ThrowsException_WhenKeyIsNull()
    {
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<ArgumentNullException>(() => hashTable.GetIndex(null));
    }

    [Test]
    public void FindEntry_ThrowsException_WhenKeyIsNull()
    {
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<ArgumentNullException>(() => hashTable.FindEntry(null));
    }

    [Test]
    public void This_Get_ThrowsException_WhenKeyIsNull()
    {
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<ArgumentNullException>(() =>
        {
            var value = hashTable[null];
            Console.WriteLine(value);
        });
    }

    [Test]
    public void This_Set_ThrowsException_WhenKeyIsNull()
    {
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<ArgumentNullException>(() => hashTable[null] = 1);
    }

    [Test]
    public void This_Get_ReturnsCorrectValue()
    {
        var hashTable = new HashTable<string, int>(4);
        hashTable.Add("one", 1);
        Assert.That(hashTable["one"], Is.EqualTo(1));
    }

    [Test]
    public void This_Set_UpdatesValue()
    {
        var hashTable = new HashTable<string, int>(4);
        hashTable.Add("one", 1);
        hashTable["one"] = 2;
        Assert.That(hashTable["one"], Is.EqualTo(2));
    }

    [Test]
    public void This_Set_KeyNotFoundException_WhenKeyDoesNotExist()
    {
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<KeyNotFoundException>(() => hashTable["one"] = 2);
    }

    [Test]
    public void This_Get_KeyNotFoundException_WhenKeyDoesNotExist()
    {
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<KeyNotFoundException>(() =>
        {
            var value = hashTable["one"];
            Console.WriteLine(value);
        });
    }

    [Test]
    public void Test_NegativeHashKey_ReturnsCorrectValue()
    {
        var hashTable = new HashTable<NegativeHashKey, int>(4);
        hashTable.Add(new NegativeHashKey(1), 1);
        Assert.That(hashTable[new NegativeHashKey(1)], Is.EqualTo(1));
    }

    [Test]
    public void Resize_HandlesNegativeHashCodeCorrectly()
    {
        // Arrange
        var hashTable = new HashTable<NegativeHashKey, string>(2);

        // Act
        hashTable.Add(new NegativeHashKey(1), "A");
        hashTable.Add(new NegativeHashKey(2), "B");
        hashTable.Add(new NegativeHashKey(3), "C");

        // Assert
        Assert.That(hashTable[new NegativeHashKey(1)], Is.EqualTo("A"));
        Assert.That(hashTable[new NegativeHashKey(2)], Is.EqualTo("B"));
        Assert.That(hashTable[new NegativeHashKey(3)], Is.EqualTo("C"));
    }

    [Test]
    public void Resize_HandlesNegativeIndexCorrectly()
    {
        // Arrange
        var hashTable = new HashTable<NegativeHashKey, string>(2);
        var key = new NegativeHashKey(111);
        hashTable.Add(key, "Value");

        // Act
        hashTable.Resize();

        // Assert
        Assert.That(hashTable[key], Is.EqualTo("Value"));
    }

    [Test]
    public void Add_ShouldTriggerResize_WhenThresholdExceeded()
    {
        // Arrange
        var initialCapacity = 4;
        var hashTable = new HashTable<int, string>(initialCapacity);

        // Act
        for (var i = 1; i <= 32; i++)
        {
            hashTable.Add(i, $"Value{i}");
        }

        // Assert
        hashTable.Capacity.Should().BeGreaterThan(initialCapacity);
        hashTable.Count.Should().Be(32);
    }


    [Test]
    public void Add_ThrowsException_WhenKeyIsDefault()
    {
        // Arrange
        var hashTable = new HashTable<int, string>();

        // Act & Assert
        Action act = () => hashTable.Add(default, "Value");
        act.Should().Throw<ArgumentNullException>().WithMessage("*key*");
    }

    [Test]
    public void Add_ThrowsException_WhenValueIsDefault()
    {
        // Arrange
        var hashTable = new HashTable<int, string>();

        // Act & Assert
        Action act = () => hashTable.Add(1, default);
        act.Should().Throw<ArgumentNullException>().WithMessage("*value*");
    }

    [Test]
    public void Add_StoresValueCorrectly()
    {
        // Arrange
        var hashTable = new HashTable<int, string>();

        // Act
        hashTable.Add(1, "Value1");

        // Assert
        hashTable[1].Should().Be("Value1");
    }

    [Test]
    public void Get_ReturnsCorrectValue_ForExistingKey()
    {
        // Arrange
        var hashTable = new HashTable<string, int>();
        hashTable.Add("key", 42);

        // Act
        var value = hashTable["key"];

        // Assert
        value.Should().Be(42);
    }

    [Test]
    public void Get_ThrowsException_WhenKeyDoesNotExist()
    {
        // Arrange
        var hashTable = new HashTable<string, int>();

        // Act & Assert
        Action act = () => _ = hashTable["nonexistent"];
        act.Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void Capacity_Increases_WhenResizeOccurs()
    {
        var initialCapacity = 4;
        var hashTable = new HashTable<int, string>(initialCapacity);

        for (var i = 1; i <= 5; i++)
        {
            hashTable.Add(i, $"Value{i}");
        }

        hashTable.Capacity.Should().BeGreaterThan(initialCapacity);
    }

    [Test]
    public void IndexerSet_Throws_KeyNotFound()
    {
        // Arrange
        var hashTable = new HashTable<int, string>();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => hashTable[1] = "A");
    }
}

public class NegativeHashKey(int id)
{
    private readonly int id = id;

    public override int GetHashCode()
    {
        // Return a negative hash code
        return -id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is NegativeHashKey other)
        {
            return id == other.id;
        }
        return false;
    }
}

/// <summary>
/// Class to simulate hash collisions
/// </summary>
/// <param name="id">Id of this object</param>
public class Collider(int id)
{
    private readonly int id = id;
    public override int GetHashCode() => 42; // Force all instances to collide
    public override bool Equals(object? obj) => obj is Collider other && other.id == id;
}
