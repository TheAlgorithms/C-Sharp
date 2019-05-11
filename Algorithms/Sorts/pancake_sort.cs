// C# program to sort array using 
// pancake sort 
using System;

class GFG
{

    // Reverses arr[0..i] 
    static void Flip(int[] arr, int i)
    {
        int temp, start = 0;
        while (start < i)
        {
            temp = arr[start];
            arr[start] = arr[i];
            arr[i] = temp;
            start++;
            i--;
        }
    }

    // Returns index of the  
    // maximum element in  
    // arr[0..n-1]  
    static int FindMax(int[] arr, int n)
    {
        int mi, i;
        for (mi = 0, i = 0; i < n; ++i)
        {
            if (arr[i] > arr[mi])
            {
                mi = i;
            }
        }

        return mi;
    }

    // The main function that 
    // sorts given array using  
    // flip operations 
    static int PancakeSort(int[] arr, int n)
    {

        // Start from the complete 
        // array and one by one 
        // reduce current size by one 
        for (var curr_size = n; curr_size > 1;
                                  --curr_size)
        {

            // Find index of the 
            // maximum element in 
            // arr[0..curr_size-1] 
            var mi = FindMax(arr, curr_size);

            // Move the maximum element 
            // to end of current array 
            // if it's not already at  
            // the end 
            if (mi != curr_size - 1)
            {
                // To move at the end, 
                // first move maximum 
                // number to beginning 
                Flip(arr, mi);

                // Now move the maximum  
                // number to end by 
                // reversing current array 
                Flip(arr, curr_size - 1);
            }
        }

        return 0;
    }

    // Utility function to print 
    // array arr[] 
    static void PrintArray(int[] arr,
                           int arr_size)
    {
        for (var i = 0; i < arr_size; i++)
        {
            Console.Write(arr[i] + " ");
        }

        Console.Write("");
    }

    // Driver function to check for  
    // above functions 
    public static void Main()
    {
        int[] arr = { 23, 10, 20, 11, 12, 6, 7 };
        var n = arr.Length;

        PancakeSort(arr, n);

        Console.Write("Sorted Array: ");
        PrintArray(arr, n);
    }
}
