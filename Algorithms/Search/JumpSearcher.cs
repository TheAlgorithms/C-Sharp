using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Searches
{
    public class JumpSearcher
    {
        /// <summary>
        /// Jump Search is a modification of Linear Search.
        /// We skip some elements by a fixed step and each time we check if the wanted number is lesser or greater than the number we landed on.
        /// For example, suppose we have an array arr[] of size n and block (to be jumped) size m.
        /// Then we search at the indexes arr[0], arr[m], arr[2m]…..arr[km] and so on.
        /// Once we find the interval (arr[km] &lt; x &lt; arr[(k+1)m]),
        /// we perform a linear search operation from the index km to find the element x.
        /// </summary>
        /// <param name="sortedList">Given list in which we will search for the wanted number.</param>
        /// <param name="wantedNumber">The number we are searching for.</param>
        /// <returns>Index of list that the wanted number is located.</returns>
        public int FindIndex(List<int> sortedList, int wantedNumber)
        {
            try
            {
                var prevInd = 0;
                var currInd = 0;
                var response = 0;
                var jumpStep = (int)Math.Sqrt(sortedList.Count()); // Number of steps we skip in each iteration.

                currInd += jumpStep;

                // We loop the list until the current position, in the list, is greater than or equal to the wanted number.
                while (sortedList[Math.Min(currInd, sortedList.Count()) - 1] < wantedNumber)
                {
                    prevInd = currInd;
                    currInd += jumpStep;
                }

                // We do a linear search to the sortedList from prevInd to currInd.
                for (var i = prevInd; i < currInd; i++)
                {
                    if (sortedList[i] == wantedNumber)
                    {
                        response = i;
                    }
                }

                return response;
            }
            catch (ArgumentException)
            {
                return -1;
            }
            catch (Exception)
            {
                return -2;
            }
        }

        /// <summary>
        /// Jump Search is a modification of Linear Search.
        /// We skip some elements by a fixed step and each time we check if the wanted number is lesser or greater than the number we landed on.
        /// For example, suppose we have an array arr[] of size n and block (to be jumped) size m.
        /// Then we search at the indexes arr[0], arr[m], arr[2m]…..arr[km] and so on.
        /// Once we find the interval (arr[km] &lt; x &lt; arr[(k+1)m]),
        /// we perform a linear search operation from the index km to find the element x.
        /// </summary>
        /// <param name="sortedList">Given list in which we will search for the wanted number.</param>
        /// <param name="wantedNumber">The number we are searching for.</param>
        /// <param name="jumpStep">Number of steps we skip in each iteration.</param>
        /// <returns>Index of list that the wanted number is located.</returns>
        public int FindIndex(List<int> sortedList, int wantedNumber, int jumpStep)
        {
            try
            {
                var prevInd = 0;
                var currInd = 0;
                var response = 0;

                currInd += jumpStep;

                // We loop the list until the current position, in the list, is greater than or equal to the wanted number.
                while (sortedList[Math.Min(currInd, sortedList.Count()) - 1] < wantedNumber)
                {
                    prevInd = currInd;
                    currInd += jumpStep;
                }

                // We do a linear search to the sortedList from prevInd to currInd.
                for (var i = prevInd; i < currInd; i++)
                {
                    if (sortedList[i] == wantedNumber)
                    {
                        response = i;
                    }
                }

                return response;
            }
            catch (ArgumentException)
            {
                return -1;
            }
            catch (Exception)
            {
                return -2;
            }
        }
    }
}
