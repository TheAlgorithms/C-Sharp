using System;

namespace cocktail_sort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter some integers, separated by spaces:");
            
            var input = Console.ReadLine();
            var integers = input.Split(' ');
            var data = new int[integers.Length];

            for (var i = 0; i < data.Length; i++)
            {
                data[i] = int.Parse(integers[i]);
            }

            Console.WriteLine("Unsorted: {0}", string.Join(" ", data));

            CocktailSort(data);

            Console.WriteLine("Sorted: {0}", string.Join(" ", data));

            Console.ReadKey();
        }

        public static void CocktailSort(int[] intArray)
        {
            var swapped = true;

            var startIndex = 0;
            var endIndex = intArray.Length - 1;

            while(swapped)
            {
                for (var i = startIndex; i < endIndex; i++)
                {
                    if (intArray[i] > intArray[i + 1])
                    {
                        var highValue = intArray[i];

                        intArray[i] = intArray[i + 1];
                        intArray[i + 1] = highValue;

                        swapped = true;
                    }
                }
                endIndex--;
                
                if(!swapped)
                {
                    break;
                }

                swapped = false;

                for (var i = endIndex; i > startIndex; i--)
                {
                    if (intArray[i] < intArray[i - 1])
                    {
                        var highValue = intArray[i];

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