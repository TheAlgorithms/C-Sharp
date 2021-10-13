using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    /// <summary>
    /// Inverted index is the simplest form of document indexing,
    /// allowing performing boolean queries on text data.
    ///
    /// This realization is just simplified for better understanding the process of indexing
    /// and working on straightforward string inputs.
    /// </summary>
    public class InvertedIndex
    {
        private readonly Dictionary<string, List<string>> invertedIndex = new();

        /// <summary>
        /// Build inverted index with source name and source content.
        /// </summary>
        /// <param name="sourceName">Name of the source.</param>
        /// <param name="sourceContent">Content of the source.</param>
        public void AddToIndex(string sourceName, string sourceContent)
        {
            var context = sourceContent.Split(' ').Distinct();
            foreach (var word in context)
            {
                if (!invertedIndex.ContainsKey(word))
                {
                    invertedIndex.Add(word, new List<string> { sourceName });
                }
                else
                {
                    invertedIndex[word].Add(sourceName);
                }
            }
        }

        /// <summary>
        /// Returns the source names contains ALL terms inside at same time.
        /// </summary>
        /// <param name="terms">List of terms.</param>
        /// <returns>Source names.</returns>
        public IEnumerable<string> And(IEnumerable<string> terms)
        {
            List<IEnumerable<string>> surviers = terms
                .Select(term => invertedIndex.Where(x => x.Key.Equals(term))
                    .Select(x => x.Value)
                    .SelectMany(x => x))
                .ToList();

            var intersection = surviers
                .Skip(1)
                .Aggregate(new HashSet<string>(surviers.First()), (hashSet, enumerable) =>
                {
                    var values = hashSet.Intersect(enumerable);
                    return new HashSet<string>(values);
                });

            return intersection;
        }

        /// <summary>
        /// Returns the source names contains ALL terms (first and second) inside at same time.
        /// </summary>
        /// <param name="firstTerm">First term.</param>
        /// <param name="secondTerm">Second term.</param>
        /// <returns>Source names.</returns>
        public IEnumerable<string> And(string firstTerm, string secondTerm)
        {
            var source = invertedIndex.Where(x => x.Key.Equals(firstTerm))
                .Select(x => x.Value).SelectMany(x => x);

            var source2 = invertedIndex.Where(x => x.Key.Equals(secondTerm))
                .Select(x => x.Value).SelectMany(x => x);

            return source.Intersect(source2);
        }

        /// <summary>
        /// Returns the source names contains AT LEAST ONE from terms inside.
        /// </summary>
        /// <param name="terms">List of terms.</param>
        /// <returns>Source names.</returns>
        public IEnumerable<string> Or(IEnumerable<string> terms)
        {
            var sources = new List<string>();
            foreach (var term in terms)
            {
                var source = invertedIndex.Where(x => x.Key.Equals(term))
                    .Select(x => x.Value).SelectMany(x => x);

                sources.AddRange(source);
            }

            return sources.Distinct();
        }

        /// <summary>
        /// Returns the source names contains AT LEAST ONE (first or second or both) from terms inside.
        /// </summary>
        /// <param name="firstTerm">First term.</param>
        /// <param name="secondTerm">Second term.</param>
        /// <returns>Source names.</returns>
        public IEnumerable<string> Or(string firstTerm, string secondTerm)
        {
            var source = invertedIndex.Where(x => x.Key.Equals(firstTerm) || x.Key.Equals(secondTerm))
                .Select(x => x.Value).SelectMany(x => x);

            return source.Distinct();
        }
    }
}
