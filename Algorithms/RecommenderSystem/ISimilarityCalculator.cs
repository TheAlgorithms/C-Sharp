namespace Algorithms.RecommenderSystem
{
    public interface ISimilarityCalculator
    {
        double CalculateSimilarity(Dictionary<string, double> user1Ratings, Dictionary<string, double> user2Ratings);
    }
}
