using System;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

public class GreedyAlgorithm

    /*
    // Info
    //
A greedy algorithm is the one that always chooses the best solution at the time, with no regard for how that choice will affect future choices. Here, we will discuss how to use Greedy algorithm to making coin changes.
It has been proven that an optimal solution for coin changing can always be found using the current American denominations of coins
For an example, Let’s say you buy some items at the store and the change from your purchase is 63 cents. How does the clerk determine the change to give you? If the clerk follows a greedy algorithm, he or she gives you two quarters, a dime, and three pennies. That is the smallest number of coins that will equal 63 cents.
*/
{
    public static void MakeChange(double origAmount, double remainAmount, int[] coins)
    {
        if ((origAmount % 0.25) < origAmount)
        {
            coins[3] = (int)(origAmount / 0.25);
            remainAmount = origAmount % 0.25;
            origAmount = remainAmount;
        }

        if ((origAmount % 0.1) < origAmount)
        {
            coins[2] = (int)(origAmount / 0.1);
            remainAmount = origAmount % 0.1;
            origAmount = remainAmount;
        }

        if ((origAmount % 0.05) < origAmount)
        {
            coins[1] = (int)(origAmount / 0.05);
            remainAmount = origAmount % 0.05;
            origAmount = remainAmount;
        }

        if ((origAmount % 0.01) < origAmount)
        {
            coins[0] = (int)(origAmount / 0.01);
            remainAmount = origAmount % 0.01;
        }
    }

    public static void ShowChange(int[] arr)
    {
        if (arr[3] > 0)
        {
            Console.WriteLine("Number of quarters: " + arr[3]);
        }

        if (arr[2] > 0)
        {
            Console.WriteLine("Number of dimes: " + arr[2]);
        }

        if (arr[1] > 0)
        {
            Console.WriteLine("Number of nickels: " + arr[1]);
        }

        if (arr[0] > 0)
        {
            Console.WriteLine("Number of pennies: " + arr[0]);
        }
    }

    public static void Main()
    {
            Console.WriteLine("Enter the amount of cents you want to change (e.g. 0.64):");
            double origAmount = Convert.ToDouble(Console.ReadLine());
            double toChange = origAmount;
            double remainAmount = 0.0;
            int[] coins = new int[4];
            MakeChange(origAmount, remainAmount, coins);
            Console.WriteLine("The best way to change " +
            toChange + " cents is: ");
            ShowChange(coins);
    }
}
