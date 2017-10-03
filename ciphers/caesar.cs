using System;

namespace caesar
{
    class Program
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Encrypt/Decrypt? [e/d]: ");
            char option = Console.ReadKey().KeyChar;

            if (option != 'e' && option != 'd')
            {
                Console.WriteLine("\nInvalid choice. Use 'e' or 'd'");
                return;
            }

            Console.WriteLine("\n\nEnter MESSAGE: ");
            string text = Console.ReadLine();

            Console.WriteLine("\nEnter KEY: ");
            string key = Console.ReadLine();

            int iKey;
            if (!Int32.TryParse(key, out iKey) | iKey < 0 | iKey > 25)
            {
                Console.WriteLine("\nInvalid key! Please enter a nummber in the range of 0-25");
                return;
            }

            Console.WriteLine("-=-= RESULT =-=-");

            if (option == 'e')
                Console.WriteLine(Encrypt(text, iKey));
            else
                Console.WriteLine(Decrypt(text, iKey));
        }

        private static String Encrypt(String text, int key)
        {
            return CipherMain(text, key, true);
        }

        private static String Decrypt(String text, int key)
        {
            return CipherMain(text, key, false);
        }

        private static String CipherMain(String text, int key, bool bEncrypt)
        {
            String newText = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsLetter(text[i]))
                {
                    newText += text[i];
                    continue;
                }

                char letterA = char.IsUpper(text[i]) ? 'A' : 'a';
                char letterZ = char.IsUpper(text[i]) ? 'Z' : 'z';

                int c;
                if (bEncrypt)
                {
                    c = text[i] + key;
                    if (c > letterZ)
                        c -= 26;
                }
                else
                {
                    c = text[i] - key;
                    if (c < letterA)
                        c += 26;
                }

                newText += (char) c;
            }

            return newText;
        }
    }
}