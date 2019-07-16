namespace cocktail_sort
{
    internal class Program
    {
        public static void CocktailSort(int[] intArray)
        {
            var swapped = true;

            var startIndex = 0;
            var endIndex = intArray.Length - 1;

            while (swapped)
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

                if (!swapped)
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
