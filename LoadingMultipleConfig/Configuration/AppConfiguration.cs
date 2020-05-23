using System.Text.Json;
using LoadingMultipleConfig.Configuration.Models;

namespace LoadingMultipleConfig.Configuration
{
    public class AppConfiguration
    {
        public Config Config { get; }

        public AppConfiguration(string filename, ILoadData loadData)
        {
            var jsonString = loadData.ReadData(filename);
            Config = JsonSerializer.Deserialize<Config>(jsonString, new JsonSerializerOptions
            {
                IgnoreNullValues = false
            });
        }
    }
}