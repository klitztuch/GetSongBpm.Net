using System.Text.Json.Serialization;

namespace GetSongBpm.Net.Models
{
    public class SongResponse
    {
        [JsonPropertyName("song")]
        public Song Song { get; set; }
    }
}