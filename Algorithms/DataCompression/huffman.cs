using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.DataCompression
{
    /// <summary>
    /// Represents Tree structure for the algorithm.
    /// </summary>
    internal class ListNode
    {
        public string Data { get; }

        public int Frequency { get; }

        public ListNode RightChild { get; }

        public ListNode LeftChild { get; }

        public ListNode(string data, int frequency)
        {
            Data = data;
            Frequency = frequency;
        }

        public ListNode(ListNode leftChild, ListNode rightChild)
        {
            LeftChild = leftChild;
            RightChild = rightChild;
            Data = leftChild.Data + ":" + rightChild.Data;
            Frequency = leftChild.Frequency + rightChild.Frequency;
        }
    }

    /// <summary>
    /// Stores encoded text message.
    /// </summary>
    internal class Man
    {
        public List<string> Codec { get; } = new List<string>();
        public List<string> Data { get; } = new List<string>();
    }

    /// <summary>
    /// Greedy lossless compression algorithm.
    /// </summary>
    public class HuffmanAlgorithm
    {
        private int Len { get; set; }

        private int Pos { get; set; }

        /// <summary>
        /// Given an input string, returns a new compressed string
        /// using huffman enconding.
        /// </summary>
        /// <param name="inputText">Text message to compress.</param>
        /// <returns>Coded string</returns>
        public string Compress(string inputText)
        {
            var text = inputText.ToLowerInvariant().Replace(" ", "#");
            Len = text.Length;

            var textAsCharArray = text.ToCharArray();
            var initCharCountArray = GetCharFrequencies(textAsCharArray);

            var (fFreqArray, fCharArray) =
                FlagCharsByFrequency(initCharCountArray, textAsCharArray);

            SortArrays(fFreqArray, fCharArray);

            var objects = FillHuffmanList(fFreqArray, fCharArray).ToList();

            var stack = GetSortedStack(objects);
            while (stack.Count > 1)
            {
                var leftChild = stack.Pop();
                var rightChild = stack.Pop();
                var parentNode = new ListNode(leftChild, rightChild);
                stack.Push(parentNode);
                stack = GetSortedStack(stack.ToList());
            }

            var genMan = GenerateHuffmanTree(stack);
            var hmanStr = GetHuffmanString(text, genMan);

            var resultStr = hmanStr.Replace(" ", "");
            return resultStr;
        }

        /// <summary>
        /// Joins the encoded string
        /// </summary>
        /// <param name="text">Text message</param>
        /// <param name="man">Encoded message</param>
        /// <returns>Huffman String</returns>
        private static string GetHuffmanString(string text, Man man)
        {
            var cStr = " ";
            foreach (var item in text)
            {
                var index = man.Data.IndexOf(item.ToString());
                cStr += man.Codec.ElementAt(index);
            }

            return cStr;
        }

        private static Man GenerateHuffmanTree(Stack<ListNode> stack)
        {
            // generated huffman tree
            var parentNode1 = stack.Pop();
            var man = new Man();

            // generates and displays the huffman code
            GenerateCode(parentNode1, string.Empty, man);

            return man;
        }

        /// <summary>
        /// Find the frequency(# of incidences) for each caracter on the text.
        /// </summary>
        /// <returns>Updateds frequency Array</returns>
        private int[] GetCharFrequencies(IReadOnlyList<char> text)
        {
            var temp = new int[Len];
            var count = 1;

            for (var i = 0; i < Len; i++)
            {
                for (var j = i + 1; j < Len; j++)
                {
                    if (text[i] == text[j])
                    {
                        count++;
                    }
                }

                temp[i] = count;
                count = 1;
            }

            return temp;
        }

        /// <summary>
        /// Segregated chars by frequency.
        /// </summary>
        /// <param name="countArray">initial count per char</param>
        /// <param name="textAsCharArray">text message as array of chars.</param>
        /// <returns>Filtered arrays</returns>
        private Tuple<int[], char[]> FlagCharsByFrequency(
            IReadOnlyList<int> countArray, IReadOnlyList<char> textAsCharArray)
        {
            var filteredFreqArray = new int[Len];
            var filteredCharArray = new char[Len];

            var flag = false;

            for (var i = 0; i < Len; i++)
            {
                for (var j = 0; j < Len; j++)
                {
                    if (textAsCharArray[i] == filteredCharArray[j])
                    {
                        flag = true;
                    }
                }

                if (!flag)
                {
                    filteredCharArray[Pos] = textAsCharArray[i];
                    filteredFreqArray[Pos] = countArray[i];
                    Pos++;
                }

                flag = false;
            }

            return new Tuple<int[], char[]>(filteredFreqArray, filteredCharArray);
        }

        /// <summary>
        /// Sorts array frequencies(d1) array and char array (c1).
        /// </summary>
        private void SortArrays(IList<int> fFreqArray, IList<char> fCharArray)
        {
            for (var i = 0; i < Pos; i++)
            {
                for (var j = i + 1; j < Pos; j++)
                {
                    if (fFreqArray[i] <= fFreqArray[j])
                    {
                        continue;
                    }

                    var temp = fFreqArray[i];
                    var ch = fCharArray[i];
                    fFreqArray[i] = fFreqArray[j];
                    fCharArray[i] = fCharArray[j];
                    fFreqArray[j] = temp;
                    fCharArray[j] = ch;
                }
            }
        }

        /// <summary>
        /// Fills the list  with ListNode-objects
        /// </summary>
        private IEnumerable<ListNode> FillHuffmanList(IReadOnlyList<int> fFreqArray, char[] fCharArray)
        {
            var huffmanObjects = new List<ListNode>();

            // fills the list  with ListNode-objects
            for (var i = 0; i < Pos; i++)
            {
                huffmanObjects.Add(new ListNode(fCharArray[i].ToString(), fFreqArray[i]));
            }

            return huffmanObjects;
        }

        private static Stack<ListNode> GetSortedStack(IList<ListNode> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                for (var j = i + 1; j < list.Count; j++)
                {
                    if (list[i].Frequency <= list[j].Frequency)
                    {
                        continue;
                    }

                    var tempNode = list[j];
                    list[j] = list[i];
                    list[i] = tempNode;
                }
            }

            var stack = new Stack<ListNode>();
            foreach (var t in list)
            {
                stack.Push(t);
            }

            return stack;
        }

        private static void GenerateCode(ListNode parentNode, string code, Man man)
        {
            var sbl = new StringBuilder(code);

            while (parentNode != null)
            {
                GenerateCode(parentNode.LeftChild, code + "0", man);
                if (parentNode.LeftChild == null && parentNode.RightChild == null)
                {
                    man.Codec.Add(sbl.ToString());
                    man.Data.Add(parentNode.Data);
                }

                parentNode = parentNode.RightChild;
                sbl.Append("1");
            }
        }
    }
}
