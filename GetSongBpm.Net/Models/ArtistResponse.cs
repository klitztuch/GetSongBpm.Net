using System.Text.Json.Serialization;

namespace GetSongBpm.Net.Models
{
    public class ArtistResponse
    {
        [JsonPropertyName("artist")]
        public Artist Artist { get; set; }
    }
}