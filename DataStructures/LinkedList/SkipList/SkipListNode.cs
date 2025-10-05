using System.Diagnostics;

namespace DataStructures.LinkedList.SkipList;

[DebuggerDisplay("Key = {Key}, Height = {Height}, Value = {Value}")]
internal class SkipListNode<TValue>(int key, TValue? value, int height)
{
    public int Key { get; } = key;

    public TValue? Value { get; set; } = value;

    public SkipListNode<TValue>[] Next { get; } = new SkipListNode<TValue>[height];

    public int Height { get; } = height;
}
