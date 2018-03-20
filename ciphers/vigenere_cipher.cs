using System;
using System.Text.RegularExpressions;

namespace vigenere_cipher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Encrypt/Decrypt? [e/d]: ");
            char option = Console.ReadKey().KeyChar;

            // processes the user choice
            if (option != 'e' && option != 'd')
            {
                Console.WriteLine("\nInvalid choice. Use 'e' or 'd'");
                return;
            }

            Console.WriteLine("\n\nEnter MESSAGE: ");
            string message = Console.ReadLine();

            Console.WriteLine("\nEnter KEY: ");
            string key = Console.ReadLine().ToUpper();

            // makes sure the key contains only letters
            if (!Regex.IsMatch(key, @"^[a-zA-Z]+$"))
            {
                Console.WriteLine("Illegal Character in KEY, use only alphabetic characters");
                return;
            }


            Console.WriteLine("\n******************\n");

            // calls the method Cipher depending on the user choice
            switch (option)
            {
                case 'e':
                    Cipher(message, key, false);
                    break;
                case 'd':
                    Cipher(message, key, true);
                    break;
            }
        }

        static void Cipher(string msg, string key, bool reverse)
        {
            int skip = 0;
            string crypted = "";
            for (int i = 0; i < msg.Length; i++)
            {
                // if the character not are letter then skips it and 
                // don't en/dec it.
                if (!char.IsLetter(msg[i]))
                {
                    skip++;
                    crypted += msg[i];
                }
                else
                {
                    // computes the shift
                    int shift = key[(i - skip) % key.Length] - 'A';

                    // modifying the shift depending on the option 'reverse'
                    shift = reverse ? -shift : shift;

                    // shifts the letter of the message
                    int c = msg[i] + shift;

                    char letterA = char.IsUpper(msg[i]) ? 'A' : 'a';
                    char letterZ = char.IsUpper(msg[i]) ? 'Z' : 'z';

                    // makes sure the en/dec character is a letter.
                    if (c < letterA) c += 26;
                    if (c > letterZ) c -= 26;

                    crypted += (char)c;
                }
            }
            Console.WriteLine(crypted);
        }


    }
}
