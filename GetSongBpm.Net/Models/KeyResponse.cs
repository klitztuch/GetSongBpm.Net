using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GetSongBpm.Net.Models
{
    public class KeyResponse
    {
        [JsonPropertyName("key_of")]
        public List<KeyOf> KeyOf { get; set; }
    }
}