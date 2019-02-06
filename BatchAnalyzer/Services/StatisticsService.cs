using System;
using System.Collections.Generic;
using System.Linq;
using BatchAnalyzer.Models;

namespace BatchAnalyzer.Services
{
    public class StatisticsService
    {
        public static IReadOnlyCollection<SlaughterStatistic> CreateSlaughterStatistics(IReadOnlyCollection<Prediction> predictions, IReadOnlyCollection<SlaughterWeight> slaughterWeights)
        {
            var slaughteredBatchIds = slaughterWeights.Select(slaughterWeight => slaughterWeight.BatchId);
            var predictionsForSlaughteredBatches = predictions.Where(prediction => slaughteredBatchIds.Contains(prediction.BatchId));
            var predictionWithDeviations = AddWeightDeviationFromPredictionToSlaughter(predictionsForSlaughteredBatches, slaughterWeights);
            var slaughterStatistics = CreateSlaughterStatistics(slaughteredBatchIds, predictionWithDeviations); 
            return slaughterStatistics;
        }              

        public static IReadOnlyCollection<PredictionWithDeviation> AddWeightDeviationFromPredictionToSlaughter(IEnumerable<Prediction> predictions, IEnumerable<SlaughterWeight> slaughterWeights)
        {
            var predictionWithDeviations = new List<PredictionWithDeviation>();
            foreach (var prediction in predictions)
            {
                var predictionWithDeviation = new PredictionWithDeviation(prediction, GetSlaughterWeight(prediction.BatchId, slaughterWeights));
                predictionWithDeviations.Add(predictionWithDeviation);
            }
            return predictionWithDeviations;
        }

        public static int GetSlaughterWeight(string batchId, IEnumerable<SlaughterWeight> slaughterWeights)
        {
            var specificSlaughterWeights = slaughterWeights.Where(sW => sW.BatchId == batchId).Select(sW => sW.Weight);
            if(specificSlaughterWeights.Count() > 1) throw new ArgumentException($"For the batchId {0}, there are more than one slaughterWeight data set.", batchId);
            if(specificSlaughterWeights.Count() < 1) throw new ArgumentException("Implementation error for the matching between slaughtered BatchIds and predictions.");
            return specificSlaughterWeights.First();
        }

        private static IReadOnlyCollection<SlaughterStatistic> CreateSlaughterStatistics(IEnumerable<string> slaughteredBatchIds, IEnumerable<PredictionWithDeviation> predictionWithDeviations)
        {
            var slaughterStatistics = new List<SlaughterStatistic>();
            foreach (var batchId in slaughteredBatchIds)
            {
                var deviations = predictionWithDeviations.Where(pWD => pWD.BatchId == batchId).Select(pWD => pWD.WeightDeviation);
                var averageDeviation = (int) Math.Round(deviations.Average());
                //Consider changing the output format of the average deviation to a double. 

                var minDeviation = deviations.Min();
                var bestPredictions = predictionWithDeviations.Where(pWD => pWD.WeightDeviation == minDeviation);
                if (bestPredictions.Count() > 1)
                {
                    //TODO: Log this. 
                    //The json to be output only consists of one best prediction. If multiple exists consider extending the output to a list of best predictions.
                }

                var slaughterStatistic = new SlaughterStatistic(batchId, averageDeviation, bestPredictions.First());
                slaughterStatistics.Add(slaughterStatistic);
            }
            return slaughterStatistics;
        }
    }
}
