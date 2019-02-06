using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using BatchAnalyzer.Models;
using System.Linq;

namespace BatchAnalyzer.Services
{
    public class FileReaderService
    {
        public static IReadOnlyCollection<Prediction> LoadPredictions(IEnumerable<string> filesInDirectory)
        {
            var predictionFilePaths = filesInDirectory.Where(file => file.Contains("prediction"));

            var predictions = new List<Prediction>();
            foreach (var filePath in predictionFilePaths)
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    var json = r.ReadToEnd();
                    var prediction = JsonConvert.DeserializeObject<Prediction>(json);
                    predictions.Add(prediction);
                }
            }
            return predictions;
        }

        public static IReadOnlyCollection<SlaughterWeight> LoadSlaughterWeights(IEnumerable<string> filesInDirectory)
        {
            var slaughterFilePaths = filesInDirectory.Where(file => file.Contains("slaughterweight"));

            var slaughterWeights = new List<SlaughterWeight>();
            foreach (var filePath in slaughterFilePaths)
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    var json = r.ReadToEnd();
                    var slaughterWeight = JsonConvert.DeserializeObject<SlaughterWeight>(json);
                    slaughterWeights.Add(slaughterWeight);
                }
            }

            return slaughterWeights;
        }
    }
}
