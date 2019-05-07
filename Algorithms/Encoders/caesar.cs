using System;

namespace Algorithms.Encoders
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Encrypt/Decrypt? [e/d]: ");
            var option = Console.ReadKey().KeyChar;

            if (option != 'e' && option != 'd')
            {
                Console.WriteLine("\nInvalid choice. Use 'e' or 'd'");
                return;
            }

            Console.WriteLine("\n\nEnter MESSAGE: ");
            var text = Console.ReadLine();

            Console.WriteLine("\nEnter KEY: ");
            var key = Console.ReadLine();

            if (!int.TryParse(key, out var iKey) | iKey < 0 | iKey > 25)
            {
                Console.WriteLine("\nInvalid key! Please enter a nummber in the range of 0-25");
                return;
            }

            Console.WriteLine("-=-= RESULT =-=-");

            if (option == 'e')
            {
                Console.WriteLine(Encrypt(text, iKey));
            }
            else
            {
                Console.WriteLine(Decrypt(text, iKey));
            }
        }

        private static string Encrypt(string text, int key) => CipherMain(text, key, true);

        private static string Decrypt(string text, int key) => CipherMain(text, key, false);

        private static string CipherMain(string text, int key, bool bEncrypt)
        {
            var newText = "";
            for (var i = 0; i < text.Length; i++)
            {
                if (!char.IsLetter(text[i]))
                {
                    newText += text[i];
                    continue;
                }

                var letterA = char.IsUpper(text[i]) ? 'A' : 'a';
                var letterZ = char.IsUpper(text[i]) ? 'Z' : 'z';

                int c;
                if (bEncrypt)
                {
                    c = text[i] + key;
                    if (c > letterZ)
                    {
                        c -= 26;
                    }
                }
                else
                {
                    c = text[i] - key;
                    if (c < letterA)
                    {
                        c += 26;
                    }
                }

                newText += (char)c;
            }

            return newText;
        }
    }
}