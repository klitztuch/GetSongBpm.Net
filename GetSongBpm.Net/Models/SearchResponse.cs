using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GetSongBpm.Net.Models
{
    public class SearchResponse
    {
        [JsonPropertyName("search")]
        public List<Search> Search { get; set; }
    }
}