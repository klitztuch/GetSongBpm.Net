using System.Text.Json.Serialization;

namespace GetSongBpm.Net.Models
{
    public class Album
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        [JsonPropertyName("img")]
        public string Img { get; set; }

        [JsonPropertyName("year")]
        public string Year { get; set; }
    }
}