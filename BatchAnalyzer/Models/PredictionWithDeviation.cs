using System;
using Newtonsoft.Json;

namespace BatchAnalyzer.Models
{
    public class PredictionWithDeviation
    {
        [JsonIgnore]
        public int WeightDeviation { get; set; }

        [JsonIgnore]
        public string BatchId { get; set; }

        [JsonProperty("predictedWeight")]
        public int PredictedWeight { get; set; }

        [JsonProperty("birdAge")]
        public int BirdAge { get; set; }

        public PredictionWithDeviation(Prediction prediction, int slaughterWeight)
        {
            BatchId = prediction.BatchId;
            PredictedWeight = prediction.PredictedWeight;
            BirdAge = prediction.BirdAge;
            WeightDeviation = Math.Abs(slaughterWeight - prediction.PredictedWeight);
        }
    }
}
