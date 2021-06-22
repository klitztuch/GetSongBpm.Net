using System.Text.Json.Serialization;

namespace GetSongBpm.Net.Models
{
    public class MusicKey
    {
        [JsonPropertyName("raw")]
        public string Raw { get; set; }

        [JsonPropertyName("key_of")]
        public string KeyOf { get; set; }

        [JsonPropertyName("mode")]
        public string Mode { get; set; }
    }
}