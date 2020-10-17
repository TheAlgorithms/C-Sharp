using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Searches
{
    public class JumpSearcher
    {
        public int FindIndex(List<int> sortedList, int toBeFound, int jumpStep = 0)
        {
            try
            {
                var prevInd = 0;
                var currInd = 0;
                var response = 0;

                if (jumpStep < 0)
                {
                    throw new ArgumentException("01 Jump step is less than 0");
                }
                else if (jumpStep > sortedList.Count())
                {
                    throw new IndexOutOfRangeException("02 Jump step is greater than the length of the list");
                }

                jumpStep = (jumpStep == 0) ? (int)Math.Sqrt(sortedList.Count()) : jumpStep;

                currInd += jumpStep;

                while (sortedList[Math.Min(currInd, sortedList.Count()) - 1] < toBeFound)
                {
                    prevInd = currInd;
                    currInd += jumpStep;

                    if (prevInd > sortedList.Count())
                    {
                        throw new IndexOutOfRangeException("03 Did not found given number");
                    }
                }

                for (var i = prevInd; i < currInd; i++)
                {
                    if (sortedList[i] == toBeFound)
                    {
                        response = i;
                    }
                }

                return response;
            }
            catch (ArgumentException arExc)
            {
                Console.WriteLine(arExc.Message);
                return -int.Parse(arExc.Message.Substring(0, 2));
            }
            catch (IndexOutOfRangeException outOfRangeExc)
            {
                Console.WriteLine(outOfRangeExc.Message);
                return -int.Parse(outOfRangeExc.Message.Substring(0, 2));
            }
        }
    }
}
