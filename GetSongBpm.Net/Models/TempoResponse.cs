using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GetSongBpm.Net.Models
{
    public class TempoResponse
    {
        [JsonPropertyName("tempo")]
        public List<Tempo> Tempo { get; set; }
    }
}