using System.Text.Json.Serialization;

namespace GetSongBpm.Net.Models
{
    public class Search
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        [JsonPropertyName("artist")]
        public Artist Artist { get; set; }
    }
}