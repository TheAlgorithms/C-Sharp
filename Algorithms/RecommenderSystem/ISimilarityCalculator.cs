using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.RecommenderSystem
{
    public interface ISimilarityCalculator
    {
        double CalculateSimilarity(Dictionary<string, double> user1Ratings, Dictionary<string, double> user2Ratings);
    }
}
