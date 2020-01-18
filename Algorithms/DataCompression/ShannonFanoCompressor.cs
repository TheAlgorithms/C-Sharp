using System.Collections.Generic;
using System.Linq;
using Algorithms.Knapsack;
using Utilities.Extensions;

namespace Algorithms.DataCompression
{
    /// <summary>
    /// Greedy lossless compression algorithm.
    /// </summary>
    public class ShannonFanoCompressor
    {
        private readonly IHeuristicKnapsackSolver<(char symbol, double frequency)> splitter;
        private readonly Translator translator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShannonFanoCompressor"/> class.
        /// TODO.
        /// </summary>
        /// <param name="splitter">TODO. 2.</param>
        /// <param name="translator">TODO. 3.</param>
        public ShannonFanoCompressor(IHeuristicKnapsackSolver<(char symbol, double frequency)> splitter, Translator translator)
        {
            this.splitter = splitter;
            this.translator = translator;
        }

        /// <summary>
        /// Given an input string, returns a new compressed string
        /// using Shannon-Fano enconding.
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

            var node = GetListNodeFromText(uncompressedText);
            var tree = GenerateShannonFanoTree(node);
            var (compressionKeys, decompressionKeys) = GetKeys(tree);
            return (translator.Translate(uncompressedText, compressionKeys), decompressionKeys);
        }

        private (Dictionary<string, string> compressionKeys, Dictionary<string, string> decompressionKeys) GetKeys(ListNode tree)
        {
            var compressionKeys = new Dictionary<string, string>();
            var decompressionKeys = new Dictionary<string, string>();

            if (tree.Data.Length == 1)
            {
                compressionKeys.Add(tree.Data[0].symbol.ToString(), string.Empty);
                decompressionKeys.Add(string.Empty, tree.Data[0].symbol.ToString());
                return (compressionKeys, decompressionKeys);
            }

            if (tree.LeftChild != null)
            {
                var (lsck, lsdk) = GetKeys(tree.LeftChild);
                compressionKeys.AddMany(lsck.Select(kvp => (kvp.Key, "0" + kvp.Value)));
                decompressionKeys.AddMany(lsdk.Select(kvp => ("0" + kvp.Key, kvp.Value)));
            }

            if (tree.RightChild != null)
            {
                var (rsck, rsdk) = GetKeys(tree.RightChild);
                compressionKeys.AddMany(rsck.Select(kvp => (kvp.Key, "1" + kvp.Value)));
                decompressionKeys.AddMany(rsdk.Select(kvp => ("1" + kvp.Key, kvp.Value)));
            }

            return (compressionKeys, decompressionKeys);
        }

        private ListNode GenerateShannonFanoTree(ListNode node)
        {
            if (node.Data.Length == 1)
            {
                return node;
            }

            var left = splitter.Solve(node.Data, 0.5 * node.Data.Sum(x => x.frequency), x => x.frequency, x => 1);
            var right = node.Data.Except(left).ToArray();

            node.LeftChild = GenerateShannonFanoTree(new ListNode(left));
            node.RightChild = GenerateShannonFanoTree(new ListNode(right));

            return node;
        }

        /// <summary>
        /// Finds frequency for each character in the text.
        /// </summary>
        /// <returns>Symbol-frequency array.</returns>
        private ListNode GetListNodeFromText(string text)
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

            return new ListNode(occurenceCounts.Select(kvp => (kvp.Key, 1d * kvp.Value / text.Length)).ToArray());
        }

        /// <summary>
        /// Represents tree structure for the algorithm.
        /// </summary>
        public class ListNode
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ListNode"/> class. TODO.
            /// </summary>
            /// <param name="data">TODO.</param>
            public ListNode((char symbol, double frequency)[] data) => Data = data;

            /// <summary>
            /// Gets tODO. TODO.
            /// </summary>
            public (char symbol, double frequency)[] Data { get; }

            /// <summary>
            /// Gets or sets tODO. TODO.
            /// </summary>
            public ListNode? RightChild { get; set; }

            /// <summary>
            /// Gets or sets tODO. TODO.
            /// </summary>
            public ListNode? LeftChild { get; set; }
        }
    }
}
