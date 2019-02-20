using System;

namespace cocktail_sort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter some integers, separated by spaces:");
            
            string input = Console.ReadLine();
            string[] integers = input.Split(' ');
            int[] data = new int[integers.Length];

            for (int i = 0; i < data.Length; i++)
                data[i] = int.Parse(integers[i]);
    
            Console.WriteLine("Unsorted: {0}", String.Join(" ", data));

            CocktailSort(data);

            Console.WriteLine("Sorted: {0}", String.Join(" ", data));

            Console.ReadKey();
        }

        public static void CocktailSort(int[] intArray)
        {
            bool swapped = true;

            int startIndex = 0;
            int endIndex = intArray.Length - 1;

            while(swapped)
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    if (intArray[i] > intArray[i + 1])
                    {
                        int highValue = intArray[i];

                        intArray[i] = intArray[i + 1];
                        intArray[i + 1] = highValue;

                        swapped = true;
                    }
                }
                endIndex--;
                
                if(!swapped)
                    break;
                    
                swapped = false;

                for (int i = endIndex; i > startIndex; i--)
                {
                    if (intArray[i] < intArray[i - 1])
                    {
                        int highValue = intArray[i];

                        intArray[i] = intArray[i - 1];
                        intArray[i - 1] = highValue;

                        swapped = true;
                    }
                }
                startIndex++;
            }
        }
    }
}