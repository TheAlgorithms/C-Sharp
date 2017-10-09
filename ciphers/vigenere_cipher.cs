using System;


namespace vigenere_cipher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Encrypt/Decrypt? [e/d]: ");
            char option = Console.ReadKey().KeyChar;

            if (option != 'e' && option != 'd')
            {
                Console.WriteLine("\nInvalid choice. Use 'e' or 'd'");
                return;
            }

            Console.WriteLine("\n\nEnter MESSAGE: ");
            string message = Console.ReadLine();

            Console.WriteLine("\nEnter KEY: ");
            string key = Console.ReadLine().ToUpper();
            for (int i = 0; i < key.Length; i++)
            {
                if (!char.IsLetter(key[i]))
                {
                    Console.WriteLine("Illegal Character in KEY, use only alphabetic characters");
                    return;
                }
            }

            Console.WriteLine("\n******************\n");

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
                if (!char.IsLetter(msg[i]))
                {
                    skip++;
                    crypted += msg[i];
                }
                else
                {
                    int shift = key[(i - skip) % key.Length] - 'A';
                    shift = reverse ? -shift : shift;

                    int c = msg[i] + shift;

                    char letterA = char.IsUpper(msg[i]) ? 'A' : 'a';
                    char letterZ = char.IsUpper(msg[i]) ? 'Z' : 'z';

                    if (c < letterA) c += 26;
                    if (c > letterZ) c -= 26;

                    crypted += (char)c;
                }
            }
            Console.WriteLine(crypted);
        }


    }
}
