using System.Diagnostics;

namespace DataStructures.LinkedList.SkipList;

[DebuggerDisplay("Key = {Key}, Height = {Height}, Value = {Value}")]
internal class SkipListNode<TValue>
{
    public SkipListNode(int key, TValue? value, int height)
    {
        Key = key;
        Value = value;
        Height = height;
        Next = new SkipListNode<TValue>[height];
    }

    public int Key { get; }

    public TValue? Value { get; set; }

    public SkipListNode<TValue>[] Next { get; }

    public int Height { get; }
}
