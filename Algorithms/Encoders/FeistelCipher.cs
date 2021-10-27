using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Encoders
{
    /// <summary>
    ///     Encodes using Feistel cipher.
    ///     https://en.wikipedia.org/wiki/Feistel_cipher
    ///     In cryptography, a Feistel cipher (also known as Luby–Rackoff block cipher)
    ///     is a symmetric structure used in the construction of block ciphers,
    ///     named after the German-born physicist and cryptographer Horst Feistel
    ///     who did pioneering research while working for IBM (USA)
    ///     A large proportion of block ciphers use the scheme, including the US DES,
    ///     the Soviet/Russian GOST and the more recent Blowfish and Twofish ciphers.
    /// </summary>
    public class FeistelCipher : IEncoder<uint>
    {
        // number of rounds to transform data block, each round a new "round" key is generated.
        private const int Rounds = 32;

        /// <summary>
        ///     Encodes text using specified key,
        ///     where n - text length.
        /// </summary>
        /// <param name="text">Text to be encoded.</param>
        /// <param name="key">Key that will be used to encode the text.</param>
        /// <exception cref="ArgumentException">Error: key should be more than 0x00001111 for better encoding, key=0 will throw DivideByZero exception.</exception>
        /// <returns>Encoded text.</returns>
        public string Encode(string text, uint key)
        {
            List<ulong> blocks_list_plain = SplitTextToBlocks(text);
            StringBuilder encoded_text = new StringBuilder();

            foreach (ulong block in blocks_list_plain)
            {
                uint temp = 0;

                // decompose a block to two subblocks 0x0123456789ABCDEF => 0x01234567 & 0x89ABCDEF
                uint right_subblock = (uint)(block & 0x00000000FFFFFFFF);
                uint left_subblock = (uint)(block >> 32);

                uint round_key;

                // Feistel "network" itself
                for (int round = 0; round < Rounds; round++)
                {
                    round_key = GetRoundKey(key, round);
                    temp = right_subblock ^ BlockModification(left_subblock, round_key);
                    right_subblock = left_subblock;
                    left_subblock = temp;
                }

                // compile text string formating the block value to text (hex based), length of the output = 16 byte always
                ulong encoded_block = left_subblock;
                encoded_block = (encoded_block << 32) | right_subblock;
                encoded_text.Append(string.Format("{0:X16}", encoded_block));
            }

            return encoded_text.ToString();
        }

        /// <summary>
        ///     Decodes text that was encoded using specified key.
        /// </summary>
        /// <param name="text">Text to be decoded.</param>
        /// <param name="key">Key that was used to encode the text.</param>
        /// <exception cref="ArgumentException">Error: key should be more than 0x00001111 for better encoding, key=0 will throw DivideByZero exception.</exception>
        /// <exception cref="ArgumentException">Error: The length of text should be divisible by 16 as it the block lenght is 16 bytes.</exception>
        /// <returns>Decoded text.</returns>
        public string Decode(string text, uint key)
        {
            // The plain text will be padded to fill the size of block (16 bytes)
            if (text.Length % 16 != 0)
            {
                throw new ArgumentException($"The length of {nameof(key)} should be divisible by 16");
            }

            List<ulong> blocks_list_encoded = GetBlocksFromEncodedText(text);
            StringBuilder decoded_text_hex = new StringBuilder();

            foreach (ulong block in blocks_list_encoded)
            {
                uint temp = 0;

                // decompose a block to two subblocks 0x0123456789ABCDEF => 0x01234567 & 0x89ABCDEF
                uint right_subblock = (uint)(block & 0x00000000FFFFFFFF);
                uint left_subblock = (uint)(block >> 32);

                // Feistel "network" - decoding, the order of rounds and operations on the blocks is reverted
                uint round_key;
                for (int round = Rounds - 1; round >= 0; round--)
                {
                    round_key = GetRoundKey(key, round);
                    temp = left_subblock ^ BlockModification(right_subblock, round_key);
                    left_subblock = right_subblock;
                    right_subblock = temp;
                }

                // compose decoded block
                ulong decoded_block = left_subblock;
                decoded_block = (decoded_block << 32) | right_subblock;

                for(int i = 0; i < 8; i++)
                {
                    ulong a = (decoded_block & 0xFF00000000000000) >> 56;

                    // it's a trick, the code works with non zero characters, if your text has ASCII code 0x00 it will be skipped.
                    if (a != 0)
                    {
                        decoded_text_hex.Append((char)a);
                    }

                    decoded_block = decoded_block << 8;
                }
            }

            return decoded_text_hex.ToString();
        }

        // Using the size of block = 8 bytes this function splts the text and returns set of 8 bytes (ulong) blocks
        // the last block is extended up to 8 bytes if the tail of the text is smaller than 8 bytes
        private static List<ulong> SplitTextToBlocks(string text)
        {
            List<ulong> blocks_list_plain = new List<ulong>();
            byte[] text_array = Encoding.ASCII.GetBytes(text);
            int offset = 8;
            for(int i = 0; i < text.Length; i += 8)
            {
                // text not always has len%16 == 0, that's why the offset should be adjusted for the last part of the text
                if (i > text.Length - 8)
                {
                    offset = text.Length - i;
                }

                string block = Convert.ToHexString(text_array, i, offset);
                blocks_list_plain.Add(Convert.ToUInt64(block, 16));
            }

            return blocks_list_plain;
        }

        // convert the encoded text to the set of ulong values (blocks for decoding)
        private static List<ulong> GetBlocksFromEncodedText(string text)
        {
            List<ulong> blocks_list_plain = new List<ulong>();
            for(int i = 0; i < text.Length; i += 16)
            {
                ulong block = Convert.ToUInt64(text.Substring(i, 16), 16);
                blocks_list_plain.Add(block);
            }

            return blocks_list_plain;
        }

        // here might be any deterministic math formula
        private static uint BlockModification(uint block, uint key)
        {
            for (int i = 0; i < 32; i++)
            {
                // 0x55555555 for the better distribution 0 an 1 in the block
                block = ((block ^ 0x55555555) * block) % key;
                block = block ^ key;
            }

            return block;
        }

        // There are many ways to generate a round key, any deterministic math formula does work
        private static uint GetRoundKey(uint key, int round)
        {
            // "round + 2" - to avoid a situation when pow(key,1) ^ key  = key ^ key = 0
            uint a = (uint)Math.Pow((double)key, round + 2);
            return a ^ key;
        }
    }
}
