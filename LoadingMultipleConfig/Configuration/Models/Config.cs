using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LoadingMultipleConfig.Configuration.Models
{
    [Serializable]
    public class Config
    {
        [JsonPropertyName("importType")]
        public ImportType ImportType { get; set; }
        
        [JsonPropertyName("books")]
        public List<Book> Books { get; set; } = new List<Book>();
    }
}