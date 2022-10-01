using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Sorters.Comparison;
using Utilities.Extensions;

namespace Algorithms.DataCompression
{
    /// <summary>
    ///     Greedy lossless compression algorithm.
    /// </summary>
    public class HuffmanCompressor
    {
        // TODO: Use partial sorter
        private readonly IComparisonSorter<ListNode> sorter;
        private readonly Translator translator;

        public HuffmanCompressor(IComparisonSorter<ListNode> sorter, Translator translator)
        {
            this.sorter = sorter;
            this.translator = translator;
        }

        /// <summary>
        ///     Given an input string, returns a new compressed string
        ///     using huffman encoding.
        /// </summary>
        /// <param name="uncompressedText">Text message to compress.</param>
        /// <returns>Compressed string and keys to decompress it.</returns>
        public (string compressedText, Dictionary<string, string> decompressionKeys) Compress(string uncompressedText)
        {
            if (string.IsNullOrEmpty(uncompressedText))
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

        /// <summary>
        ///     Finds frequency for each character in the text.
        /// </summary>
        /// <returns>Symbol-frequency array.</returns>
        private static ListNode[] GetListNodesFromText(string text)
        {
            var occurenceCounts = new Dictionary<char, int>();

            foreach (var ch in text)
            {
                if (!occurenceCounts.ContainsKey(ch))
                {
                    occurenceCounts.Add(ch, 0);
                }

                occurenceCounts[ch]++;
            }

            return occurenceCounts.Select(kvp => new ListNode(kvp.Key, 1d * kvp.Value / text.Length)).ToArray();
        }

        private (Dictionary<string, string> compressionKeys, Dictionary<string, string> decompressionKeys) GetKeys(
            ListNode tree)
        {
            var compressionKeys = new Dictionary<string, string>();
            var decompressionKeys = new Dictionary<string, string>();

            if (tree.HasData)
            {
                compressionKeys.Add(tree.Data.ToString(), string.Empty);
                decompressionKeys.Add(string.Empty, tree.Data.ToString());
                return (compressionKeys, decompressionKeys);
            }

            if (tree.LeftChild is not null)
            {
                var (lsck, lsdk) = GetKeys(tree.LeftChild);
                compressionKeys.AddMany(lsck.Select(kvp => (kvp.Key, "0" + kvp.Value)));
                decompressionKeys.AddMany(lsdk.Select(kvp => ("0" + kvp.Key, kvp.Value)));
            }

            if (tree.RightChild is not null)
            {
                var (rsck, rsdk) = GetKeys(tree.RightChild);
                compressionKeys.AddMany(rsck.Select(kvp => (kvp.Key, "1" + kvp.Value)));
                decompressionKeys.AddMany(rsdk.Select(kvp => ("1" + kvp.Key, kvp.Value)));

                return (compressionKeys, decompressionKeys);
            }

            return (compressionKeys, decompressionKeys);
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

        /// <summary>
        ///     Represents tree structure for the algorithm.
        /// </summary>
        public class ListNode
        {
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

            public char Data { get; }

            public bool HasData { get; }

            public double Frequency { get; }

            public ListNode? RightChild { get; }

            public ListNode? LeftChild { get; }
        }

        public class ListNodeComparer : IComparer<ListNode>
        {
            public int Compare(ListNode? x, ListNode? y)
            {
                if (x is null || y is null)
                {
                    return 0;
                }

                return x.Frequency.CompareTo(y.Frequency);
            }
        }
    }
}
