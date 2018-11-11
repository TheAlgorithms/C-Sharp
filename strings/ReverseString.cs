namespace String
{

    public class ReverseString

    {

        /**
               *
               * Reverse String Prepending Chars
               * To The Begining Of Reversed Version
               *
               */
        public static string ReverseByPrependingChars(string input)
        {
            string reversed = String.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                reversed = input[i] + reversed;
            }

            return reversed;

        }


        /**
         *
         * Reverse String by convert the input to array
         * and using Reverse Method
         * to string
         *
         */
        public static string ReverseUsingReverseArray(string input)
        {
            return new String(input.ToCharArray().Reverse().ToArray());
        }




        public static void Main(string[] args)
        {
            string input = "Hello World!";

            Console.WriteLine("using Linq Extension Method: ");
            var reversedArray = input.Reverse().ToArray();
            String rev = new String(reversedArray);
            Console.WriteLine(rev);

            Console.WriteLine("using Reverse Method In The Array Class: ");
            Console.WriteLine(ReverseUsingReverseArray(input));


            string reversed = ReverseByPrependingChars(input);
            Console.WriteLine("Reverse String Prepending Chars To The Begining Of Reversed Version: ");
            System.Console.WriteLine(reversed);
            Console.ReadLine();
        }

    }


}
