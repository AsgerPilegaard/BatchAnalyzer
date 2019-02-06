using System.Linq;
using System.IO;
using System.Reflection;
using BatchAnalyzer.Services;

namespace BatchAnalyzer
{
    class Program
    {
        public const string _nameOfApplication = "BatchAnalyzer";
        public const string _dataFolderDirectory = @"\data";
        public const string _statisticsFileName = "PostSlaughterStatistics.json";

        public static void Main(string[] args)
        {
            var dataPath = GetCurrentDataFolderPath();
            var jsonFilesInDirectory = Directory.EnumerateFiles(dataPath).Where(file => file.Remove(0, file.Length - 5) == ".json");
            var predictions = FileReaderService.LoadPredictions(jsonFilesInDirectory);
            var slaughterWeights = FileReaderService.LoadSlaughterWeights(jsonFilesInDirectory);
            var slaughterStatistics = StatisticsService.CreateSlaughterStatistics(predictions, slaughterWeights);
            JsonWriterService.WriteStatisticsFile(slaughterStatistics, dataPath, _statisticsFileName);
        }

        public static string GetCurrentDataFolderPath()
        {
            var executingDirectory = Assembly.GetExecutingAssembly().Location;
            var baseDirectory = executingDirectory.Remove(executingDirectory.IndexOf(_nameOfApplication));
            var dataPath = baseDirectory + $@"\{_nameOfApplication}" + _dataFolderDirectory;
            return dataPath;
        }
    }
}
