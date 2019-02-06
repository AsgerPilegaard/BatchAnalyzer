using Newtonsoft.Json;

namespace BatchAnalyzer.Models
{
    public class SlaughterStatistic
    {
        [JsonProperty("batchId")]
        public string BatchId { get; }

        [JsonProperty("averageDifference")]
        public int AverageDifference { get; }

        [JsonProperty("bestPrediction")]
        public PredictionWithDeviation BestPrediction { get; }

        public SlaughterStatistic(string batchId, int averageDeviation, PredictionWithDeviation bestPrediction)
        {
            BatchId = batchId;
            AverageDifference = averageDeviation;
            BestPrediction = bestPrediction;
        }
    }
}
