using Newtonsoft.Json;

namespace BatchAnalyzer.Models
{
    public class SlaughterWeight
    {
        public string BatchId { get; set; }

        [JsonProperty("SlaughterWeight")]
        public int Weight { get; set; }

        public int SlaughterAge { get; set; }
    }
}
