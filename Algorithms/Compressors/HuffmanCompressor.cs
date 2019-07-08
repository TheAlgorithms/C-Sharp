using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Sorters;

namespace Algorithms.Compressors
{
    /// <summary>
    /// Greedy lossless compression algorithm.
    /// </summary>
    public class HuffmanCompressor
    {
        // TODO: Use partial sorter
        private readonly ISorter<ListNode> sorter;
        private readonly Translator translator;

        public HuffmanCompressor(ISorter<ListNode> sorter, Translator translator)
        {
            this.sorter = sorter;
            this.translator = translator;
        }

        /// <summary>
        /// Given an input string, returns a new compressed string
        /// using huffman enconding.
        /// </summary>
        /// <param name="inputText">Text message to compress.</param>
        /// <returns>Compressed string and keys to decompress it.</returns>
        public (string compressedText, Dictionary<string, string> decompressionKeys) Compress(string uncompressedText)
        {
            if (uncompressedText == string.Empty)
            {
                return (string.Empty, new Dictionary<string, string>());
            }

            if (uncompressedText.Distinct().Count() == 1)
            {
                var dict = new Dictionary<string, string>
                {
                    { "1", uncompressedText[0].ToString() },
                };
                return (new string('1', uncompressedText.Length), dict);
            }

            var nodes = GetListNodesFromText(uncompressedText);
            var tree = GenerateHuffmanTree(nodes);
            var (compressionKeys, decompressionKeys) = GetKeys(tree);
            return (translator.Translate(uncompressedText, compressionKeys), decompressionKeys);
        }

        private (Dictionary<string, string> compressionKeys, Dictionary<string, string> decompressionKeys) GetKeys(ListNode tree)
        {
            var compressionKeys = new Dictionary<string, string>();
            var decompressionKeys = new Dictionary<string, string>();

            if (tree.HasData)
            {
                compressionKeys.Add(tree.Data.ToString(), string.Empty);
                decompressionKeys.Add(string.Empty, tree.Data.ToString());
                return (compressionKeys, decompressionKeys);
            }

            if (tree.LeftChild != null)
            {
                var (lsck, lsdk) = GetKeys(tree.LeftChild);
                AddMany(compressionKeys, lsck.Select(kvp => (kvp.Key, "0" + kvp.Value)));
                AddMany(decompressionKeys, lsdk.Select(kvp => ("0" + kvp.Key, kvp.Value)));
            }

            if (tree.RightChild != null)
            {
                var (rsck, rsdk) = GetKeys(tree.RightChild);
                AddMany(compressionKeys, rsck.Select(kvp => (kvp.Key, "1" + kvp.Value)));
                AddMany(decompressionKeys, rsdk.Select(kvp => ("1" + kvp.Key, kvp.Value)));
            }

            return (compressionKeys, decompressionKeys);
        }

        private void AddMany(Dictionary<string, string> keys, IEnumerable<(string key, string value)> enumerable)
        {
            foreach (var (key, value) in enumerable)
            {
                keys.Add(key, value);
            }
        }

        private ListNode GenerateHuffmanTree(ListNode[] nodes)
        {
            var comparer = new ListNodeComparer();
            while (nodes.Length > 1)
            {
                sorter.Sort(nodes, comparer);

                var left = nodes[0];
                var right = nodes[1];

                var newNodes = new ListNode[nodes.Length - 1];
                Array.Copy(nodes, 2, newNodes, 1, nodes.Length - 2);
                newNodes[0] = new ListNode(left, right);
                nodes = newNodes;
            }

            return nodes[0];
        }

        private class ListNodeComparer : IComparer<ListNode>
        {
            public int Compare(ListNode x, ListNode y) => x.Frequency.CompareTo(y.Frequency);
        }

        /// <summary>
        /// Finds frequency for each character in the text.
        /// </summary>
        /// <returns>Symbol-frequency array.</returns>
        private ListNode[] GetListNodesFromText(string text)
        {
            var occurenceCounts = new Dictionary<char, double>();

            for (var i = 0; i < text.Length; i++)
            {
                var ch = text[i];
                if (!occurenceCounts.ContainsKey(ch))
                {
                    occurenceCounts.Add(ch, 0);
                }

                occurenceCounts[ch]++;
            }

            return occurenceCounts.Select(kvp => new ListNode(kvp.Key, 1d * kvp.Value / text.Length)).ToArray();
        }

        /// <summary>
        /// Represents tree structure for the algorithm.
        /// </summary>
        public class ListNode
        {
            public char Data { get; }

            public bool HasData { get; }

            public double Frequency { get; }

            public ListNode RightChild { get; }

            public ListNode LeftChild { get; }

            public ListNode(char data, double frequency)
            {
                HasData = true;
                Data = data;
                Frequency = frequency;
            }

            public ListNode(ListNode leftChild, ListNode rightChild)
            {
                LeftChild = leftChild;
                RightChild = rightChild;
                Frequency = leftChild.Frequency + rightChild.Frequency;
            }
        }
    }
}
