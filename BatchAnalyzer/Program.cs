using System.Linq;
using System.IO;
using BatchAnalyzer.Services;

namespace BatchAnalyzer
{
    class Program
    {
        public const string _dataFolderDirectory = @"C:\Users\asger\OneDrive\Desktop\Scio+\BatchAnalyzer\data";
        public const string _statisticsFileName = "PostSlaughterStatistics.json";

        public static void Main(string[] args)
        {
            var jsonFilesInDirectory = Directory.EnumerateFiles(_dataFolderDirectory).Where(file => file.Remove(0, file.Length - 5) == ".json");
            var predictions = FileReaderService.LoadPredictions(jsonFilesInDirectory);
            var slaughterWeights = FileReaderService.LoadSlaughterWeights(jsonFilesInDirectory);
            var slaughterStatistics = StatisticsService.CreateSlaughterStatistics(predictions, slaughterWeights);
            JsonWriterService.WriteStatisticsFile(slaughterStatistics, _dataFolderDirectory, _statisticsFileName);
        }
    }
}
