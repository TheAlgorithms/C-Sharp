using System;

namespace DataStructures.Probabilistic;

public class CountMinSketch<T> where T : notnull
{
    private readonly int[][] sketch;
    private readonly int numHashes;

    /// <summary>
    /// Initializes a new instance of the <see cref="CountMinSketch{T}"/> class based off dimensions
    /// passed by the user.
    /// </summary>
    /// <param name="width">The width of the sketch.</param>
    /// <param name="numHashes">The number of hashes to use in the sketch.</param>
    public CountMinSketch(int width, int numHashes)
    {
        sketch = new int[numHashes][];
        for (var i = 0; i < numHashes; i++)
        {
            sketch[i] = new int[width];
        }

        this.numHashes = numHashes;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CountMinSketch{T}"/> class based off the optimizing error rate
    /// and error probability formula width = e/errorRate numHashes = ln(1.0/errorProp).
    /// </summary>
    /// <param name="errorRate">The amount of acceptable over counting for the sketch.</param>
    /// <param name="errorProb">The probability that an item will be over counted.</param>
    public CountMinSketch(double errorRate, double errorProb)
    {
        var width = (int)Math.Ceiling(Math.E / errorRate);
        numHashes = (int)Math.Ceiling(Math.Log(1.0 / errorProb));
        sketch = new int[numHashes][];
        for (var i = 0; i < numHashes; i++)
        {
            sketch[i] = new int[width];
        }
    }

    /// <summary>
    /// Inserts the provided item into the sketch.
    /// </summary>
    /// <param name="item">Item to insert.</param>
    public void Insert(T item)
    {
        var initialHash = item.GetHashCode();
        for (int i = 0; i < numHashes; i++)
        {
            var slot = GetSlot(i, initialHash);
            sketch[i][slot]++;
        }
    }

    /// <summary>
    /// Queries the count of the given item that have been inserted into the sketch.
    /// </summary>
    /// <param name="item">item to insert into the sketch.</param>
    /// <returns>the number of times the provided item has been inserted into the sketch.</returns>
    public int Query(T item)
    {
        var initialHash = item.GetHashCode();
        var min = int.MaxValue;
        for (int i = 0; i < numHashes; i++)
        {
            var slot = GetSlot(i, initialHash);
            min = Math.Min(sketch[i][slot], min);
        }

        return min;
    }

    private int GetSlot(int i, int initialHash) => Math.Abs((i + 1) * initialHash) % sketch[0].Length;
}
