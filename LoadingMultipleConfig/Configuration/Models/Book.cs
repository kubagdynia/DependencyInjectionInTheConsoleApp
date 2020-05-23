using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LoadingMultipleConfig.Configuration.Models
{
    [Serializable]
    public class Book
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }
        
        [JsonPropertyName("authors")]
        public List<Author> Authors { get; set; } = new List<Author>();
    }
}