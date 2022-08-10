using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPuzzles
{
    public static class FizzBuzz
    {
        public static string GetFizzBuzz()
        {
            string fbString = "";
            for (int i = 1; i &lt; 101; i++)
            {
                if ((i % 3 == 0) &amp;&amp; (i % 5 == 0))
                    fbString += "FizzBuzz" + Environment.NewLine;
                else if (i % 3 == 0)
                    fbString += "Fizz" + Environment.NewLine;
                else if (i % 5 == 0)
                    fbString += "Buzz" + Environment.NewLine;
                else
                    fbString += i.ToString() + Environment.NewLine;
            }
            return fbString;
        }
    }
}