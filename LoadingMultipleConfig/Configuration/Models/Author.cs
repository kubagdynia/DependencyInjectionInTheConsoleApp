using System;
using System.Text.Json.Serialization;

namespace LoadingMultipleConfig.Configuration.Models
{
    [Serializable]
    public class Author
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}