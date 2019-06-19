using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.DataCompression
{
    internal class Huff
    {
        public string Data { get; }
        public int Frequency { get; }
        public Huff RightChild { get; }
        public Huff LeftChild { get; }

        public Huff(string data, int frequency)
        {
            Data = data;
            Frequency = frequency;
        }

        public Huff(Huff leftChild, Huff rightChild)
        {
            LeftChild = leftChild;
            RightChild = rightChild;
            Data = leftChild.Data + ":" + rightChild.Data;
            Frequency = leftChild.Frequency + rightChild.Frequency;
        }
    }

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

        private int[] _d;
        private int[] _d1;
        private char[] _c;
        private char[] _c1;

        /// <summary>
        /// Given an input string, returns a new compressed string
        /// using huffman enconding.
        /// </summary>
        /// <param name="inputText">Text message to compress.</param>
        /// <returns>Coded string</returns>
        public string Compress(string inputText)
        {
            // Replacing white spaces as #.
            var text = inputText.ToLowerInvariant().Replace(" ", "#");
            Len = text.Length;

            Console.WriteLine("Space will be represented by #");

            InitArrays(text);

            GetCharFrequencies();
            FlagCharsByFrequency();

            PrintArray(_c1);
            PrintArray(_d1);

            SortArrays();
            Console.WriteLine("\n\nAfter Sorting: ");
            PrintArray(_c1, true);
            PrintArray(_d1, true);

            ComputeInformationInBits();

            var objects = FillHuffmanList().ToList();
            var stack = GetSortedStack(objects);
            while (stack.Count > 1)
            {
                var leftChild = stack.Pop();
                var rightChild = stack.Pop();
                var parentNode = new Huff(leftChild, rightChild);
                stack.Push(parentNode);
                stack = GetSortedStack(stack.ToList());
            }

            var genMan = GenerateHuffmanTree(stack);
            var hmanStr = GetHuffmanString(text, genMan);

            PrintHuffManInfo(hmanStr);

            var resultStr = hmanStr.Replace(" ", "");
            return resultStr;
        }

        /// <summary>
        /// Initializes global arrays given input.
        /// </summary>
        /// <param name="text"></param>
        private void InitArrays(string text)
        {
            _d = new int[Len];
            _d1 = new int[Len];
            _c = text.ToCharArray();
            _c1 = new char[Len];
        }

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
        /// <summary>
        /// Prints information on estimates of compression.
        /// </summary>
        /// <param name="text">Coded string</param>
        private static void PrintHuffManInfo(string text)
        {
            var hfBits = 0;
            var s = text.Replace(" ", "");

            foreach (var unused in s)
            {
                hfBits++;
            }

            Console.WriteLine("Huffman Bits: " + hfBits);
            Console.WriteLine("\nCoded String: ");
            Console.WriteLine(s);
        }

        private static Man GenerateHuffmanTree(Stack<Huff> stack)
        {
            // generated huffman tree
            var parentNode1 = stack.Pop();
            var man = new Man();

            // generates and displays the huffman code
            Console.WriteLine("\nHuffman Code:");
            GenerateCode(parentNode1, "", man);

            return man;
        }

        /// <summary>
        /// Find the frequency for each caracter on the text.
        /// </summary>
        /// <returns>Updateds frequency Array</returns>
        private void GetCharFrequencies()
        {
            var count = 1;

            for (var i = 0; i < Len; i++)
            {
                for (var j = i + 1; j < Len; j++)
                {
                    if (_c[i] == _c[j])
                    {
                        count++;
                    }
                }

                _d[i] = count;
                count = 1;
            }
        }

        private void FlagCharsByFrequency()
        {
            var flag = false;

            for (var i = 0; i < Len; i++)
            {
                for (var j = 0; j < Len; j++)
                {
                    if (_c[i] == _c1[j])
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    _c1[Pos] = _c[i];
                    _d1[Pos] = _d[i];
                    Pos++;
                }
                flag = false;
            }
        }

        /// <summary>
        /// Prints to console the input array
        /// </summary>
        /// <param name="arr">Input array</param>
        /// <param name="inverse">To print starting at last index/</param>
        private void PrintArray(Array arr, bool inverse = false)
        {
            if (inverse)
            {
                for (var i = Pos - 1; i >= 0; i--)
                {
                    Console.Write("{0}\t", arr.GetValue(i));
                }
            }
            else
            {
                for (var i = 0; i < Pos; i++)
                {
                    Console.Write("{0}\t", arr.GetValue(i));
                }
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Sorts array frequencies(d1) array and char array (c1).
        /// </summary>
        private void SortArrays()
        {
            for (var i = 0; i < Pos; i++)
            {
                for (var j = i + 1; j < Pos; j++)
                {
                    if (_d1[i] <= _d1[j])
                    {
                        continue;
                    }

                    var temp = _d1[i];
                    var ch = _c1[i];
                    _d1[i] = _d1[j];
                    _c1[i] = _c1[j];
                    _d1[j] = temp;
                    _c1[j] = ch;
                }
            }
        }

        /// <summary>
        /// computes the information content in bits.
        /// </summary>
        private void ComputeInformationInBits()
        {
            double infoBit = 0;
            for (var i = 0; i < Pos; i++)
            {
                var prob = _d1[i] / (double)Len;
                var si = -(Math.Log(prob) / Math.Log(2));
                infoBit += si * _d1[i];
            }

            Console.WriteLine("\nTotal Information Count: {0}", infoBit + " Bits");
            Console.WriteLine("Number of Bits required before Compression: {0}", Len * 8);
        }

        private IEnumerable<Huff> FillHuffmanList()
        {
            var huffmanObjects = new List<Huff>();

            // fills the list  with Huff-objects
            for (var i = 0; i < Pos; i++)
            {
                huffmanObjects.Add(new Huff(_c1[i].ToString(), _d1[i]));
            }

            return huffmanObjects;
        }

        private static Stack<Huff> GetSortedStack(IList<Huff> list)
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

            var stack = new Stack<Huff>();
            foreach (var t in list)
            {
                stack.Push(t);
            }

            return stack;
        }

        private static void GenerateCode(Huff parentNode, string code, Man man)
        {
            while (true)
            {
                if (parentNode == null)
                {
                    return;
                }

                GenerateCode(parentNode.LeftChild, code + "0", man);
                if (parentNode.LeftChild == null && parentNode.RightChild == null)
                {
                    Console.WriteLine(parentNode.Data + "\t" + code);
                    man.Codec.Add(code);
                    man.Data.Add(parentNode.Data);
                }

                parentNode = parentNode.RightChild;
                code += "1";
            }
        }
    }
}
