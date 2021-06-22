using System.Text.Json.Serialization;

namespace GetSongBpm.Net.Models
{
    public class Song
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        [JsonPropertyName("artist")]
        public Artist Artist { get; set; }

        [JsonPropertyName("tempo")]
        public string Tempo { get; set; }

        [JsonPropertyName("time_sig")]
        public string TimeSig { get; set; }

        [JsonPropertyName("key_of")]
        public string KeyOf { get; set; }

        [JsonPropertyName("open_key")]
        public string OpenKey { get; set; }
    }
}