using System;

namespace DataStructures.UnrolledList
{
    public class UnrolledLinkedListNode
    {
        private readonly int[] array;

        public UnrolledLinkedListNode(int nodeSize)
        {
            Next = null!;

            Count = 0;
            array = new int[nodeSize];
        }

        public UnrolledLinkedListNode Next { get; set; }

        public int Count { get; set; }

        public void Set(int pos, int val)
        {
            if (pos < 0 || pos > array.Length - 1)
            {
                throw new ArgumentException("Position is out of size", nameof(pos));
            }

            array[pos] = val;
            Count++;
        }

        public int Get(int pos)
        {
            if (pos < 0 || pos > array.Length - 1)
            {
                throw new ArgumentException("Position is out of size", nameof(pos));
            }

            return array[pos];
        }
    }
}
