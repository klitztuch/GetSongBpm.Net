using System.Text.Json.Serialization;

namespace GetSongBpm.Net.Models
{
    public class KeyOf
    {
        [JsonPropertyName("song_id")]
        public string SongId { get; set; }

        [JsonPropertyName("song_title")]
        public string SongTitle { get; set; }

        [JsonPropertyName("song_uri")]
        public string SongUri { get; set; }

        [JsonPropertyName("music_key")]
        public MusicKey MusicKey { get; set; }

        [JsonPropertyName("artist")]
        public Artist Artist { get; set; }

        [JsonPropertyName("album")]
        public Album Album { get; set; }
    }
}