using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Numeric
{
    public static class Converter
    {
        static void Main(string[] args)
        {

            double C;
            double K;
            double F;

            Console.WriteLine("If you want to convert from Celsius to Fahrenheit ... type 1 and press enter: ");
            Console.WriteLine("If you want to convert from Fahrenheit to Celsius ... type 2 and hit enter:");
            Console.WriteLine("If you want to convert from Fahrenheit to Kelvin ... type 3 and hit enter:");
            Console.WriteLine("If you want to convert from Kelvin to Fahrenheit ... type 4 and hit enter:");
            Console.WriteLine("If you want to convert from Kelvin to Celsius ... type 5 and hit enter:");
            Console.WriteLine("If you want to convert from Celsius to Kelvin ... type 6 and hit enter:");
            int num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        Console.Write("Enter a number in Celsius, which will be converted to Fahrenheit: ");
                        C = Convert.ToDouble(Console.ReadLine());
                        F = C * 9 / 5 + 32;
                        Console.Write(C + "°C is equal to ");
                        Console.WriteLine(F + " Fahrenheit \n \n");
                        Console.WriteLine("Formula used: F = C * 9 / 5 + 32");
                    break;

                    case 2:
                        Console.Write("Enter a number in Fahrenheit, which will be converted to  Celsius: ");
                        F = Convert.ToDouble(Console.ReadLine());
                        C = ((F - 32) * 5 / 9);
                        Console.Write(F + "°F is equal to ");
                        Console.WriteLine(C + " Celsius \n \n");
                        Console.WriteLine("Formula used: C = (F-32) * 5/9");
                    break;

                    case 3:
                        Console.Write("Enter a number in Fahrenheit, which will be converted to  Kelvin: ");
                        F = Convert.ToDouble(Console.ReadLine());
                        K = ((F - 32) * 5 / 9 + 273.15);
                        Console.Write(F + "°F is equal to ");
                        Console.WriteLine(+K + " Kelvin\n");
                        Console.WriteLine("Formula used: K = (F-32) *5/9 + 273,15");
                    break;

                    case 4:
                        Console.Write("Enter a number in  Kelvin, which will be converted to Fahrenheit: ");
                        K = Convert.ToDouble(Console.ReadLine());
                        F = ((K - 273.15) * 9 / 5 + 32);
                        Console.Write(K + " Kelvin is equal to ");
                        Console.WriteLine(F + " Fahrenheit\n");
                        Console.WriteLine("Formula used: F = (K-273,15) * 9/5 + 32");
                    break;

                    case 5:                        
                        Console.Write("Enter a number in  Kelvin, which will be converted to Celsius:");
                        K = Convert.ToDouble(Console.ReadLine());
                        C = (K - 273.15);
                        Console.Write(K + " Kelvin is equal to ");
                        Console.WriteLine(C + " Graus Celsius\n");
                        Console.WriteLine("\nFormula used: C = (K - 273.15)");
                    break;

                    case 6:
                        Console.WriteLine("Enter a number in  Celsius, which will be converted to Kelvin: ");
                        C = Convert.ToDouble(Console.ReadLine());
                        K = C + 273.15;
                        Console.Write(C + "°C is equal to ");
                        Console.WriteLine(K + " Kelvin\n");
                        Console.WriteLine("Formula used: K = C + 273,15");
                    break;

                    default:
                        Console.WriteLine("Invalid selected option, Please select an option from 1 to 6.");
                    break;
                
               }


        }        
    }
}