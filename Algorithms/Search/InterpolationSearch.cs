using System.Collections.Generic;

namespace Lncodes.Algorithm.Search.Interpolation
{
    public sealed class InterpolationSearch
    {
        /// <summary>
        /// Method to interpolation search
        /// </summary>
        /// <param name="data"></param>
        /// <param name="element"></param>
        /// <returns cref="int"></returns>
        public int Search(IReadOnlyList<decimal> data, decimal element)
        {
            var minRangeIndex = 0;
            var maxRangeIndex = data.Count - 1;

            while (minRangeIndex <= maxRangeIndex)
            {
                var midIndex = InterpolationCalculator.GetInterpolationValue(element, data[minRangeIndex], data[maxRangeIndex], minRangeIndex, maxRangeIndex);

                if (data[midIndex].Equals(element))
                    return midIndex;

                if (data[midIndex] < element)
                    minRangeIndex = midIndex + 1;
                else
                    maxRangeIndex = midIndex - 1;
            }
            return -1;
        }
    }
}
