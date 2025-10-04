namespace DataStructures.UnrolledList;

/// <summary>
/// Unrolled linked list is a linked list of small arrays,
/// all of the same size where each is so small that the insertion
/// or deletion is fast and quick, but large enough to fill the cache line.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UnrolledLinkedList"/> class.
/// Create a unrolled list with start chunk size.
/// </remarks>
/// <param name="chunkSize">The size of signe chunk.</param>
public class UnrolledLinkedList(int chunkSize)
{
    private readonly int sizeNode = chunkSize + 1;

    private UnrolledLinkedListNode start = null!;
    private UnrolledLinkedListNode end = null!;

    /// <summary>
    /// Add value to list [O(n)].
    /// </summary>
    /// <param name="value">The entered value.</param>
    public void Insert(int value)
    {
        if (start == null)
        {
            start = new UnrolledLinkedListNode(sizeNode);
            start.Set(0, value);

            end = start;
            return;
        }

        if (end.Count + 1 < sizeNode)
        {
            end.Set(end.Count, value);
        }
        else
        {
            var pointer = new UnrolledLinkedListNode(sizeNode);
            var j = 0;
            for (var pos = end.Count / 2 + 1; pos < end.Count; pos++)
            {
                pointer.Set(j++, end.Get(pos));
            }

            pointer.Set(j++, value);
            pointer.Count = j;

            end.Count = end.Count / 2 + 1;
            end.Next = pointer;
            end = pointer;
        }
    }

    /// <summary>
    /// Help method. Get all list inside to check the state.
    /// </summary>
    /// <returns>Items from all nodes.</returns>
    public IEnumerable<int> GetRolledItems()
    {
        UnrolledLinkedListNode pointer = start;
        List<int> result = [];

        while (pointer != null)
        {
            for (var i = 0; i < pointer.Count; i++)
            {
                result.Add(pointer.Get(i));
            }

            pointer = pointer.Next;
        }

        return result;
    }
}
