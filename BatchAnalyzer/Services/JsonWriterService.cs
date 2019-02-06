using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using BatchAnalyzer.Models;

namespace BatchAnalyzer.Services
{
    public class JsonWriterService
    {
        public static void WriteStatisticsFile(IEnumerable<SlaughterStatistic> slaughterStatistics, string filePath, string statisticsFileName)
        {
            var fileName = Path.Combine(filePath, statisticsFileName);
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(fileName))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, slaughterStatistics);
            }
        }
    }
}
