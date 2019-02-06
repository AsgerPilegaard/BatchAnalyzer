using Newtonsoft.Json;

namespace BatchAnalyzer.Models
{
    public class Prediction
    {
        public string BatchId { get; set; }
        public int PredictedWeight { get; set; }
        public int BirdAge { get; set; }
    }
}
