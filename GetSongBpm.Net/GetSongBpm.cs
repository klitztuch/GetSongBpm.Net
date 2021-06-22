using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using GetSongBpm.Net.Models;
using GetSongBpm.Net.Models.Enum;

namespace GetSongBpm.Net
{
    public class GetSongBpm
    {
        private const string BASE_URL = "https://api.getsongbpm.com";
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public GetSongBpm(string apiKey,
            string baseUrl = null)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _baseUrl = baseUrl ?? BASE_URL;
            _httpClient = new HttpClient();
        }

        /// <summary>
        ///     Gets details about an artist.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Artist> GetArtist(string id)
        {
            var builder = new UriBuilder(_baseUrl);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["api_key"] = _apiKey;
            query["id"] = id;
            builder.Query = query.ToString() ?? string.Empty;
            var response = await _httpClient.GetAsync(builder.ToString());
            var artistResponse = await response.Content.ReadFromJsonAsync<ArtistResponse>();
            return artistResponse?.Artist;
        }

        /// <summary>
        ///     Gets details about a song.
        /// </summary>
        /// <param name="id">song ID</param>
        /// <returns></returns>
        public async Task<Song> GetSong(string id)
        {
            var uriBuilder = new UriBuilder(_baseUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["api_key"] = _apiKey;
            query["id"] = id;
            uriBuilder.Query = query.ToString() ?? string.Empty;
            var response = await _httpClient.GetAsync(uriBuilder.ToString());
            var songResponse = await response.Content.ReadFromJsonAsync<SongResponse>();
            return songResponse?.Song;
        }

        /// <summary>
        ///     Gets songs with targeted BPM.
        ///     Results limited to the 250 most viewed songs in the last 30 days.
        /// </summary>
        /// <param name="bpm">target BPM range: 40-220 BPM</param>
        /// <returns></returns>
        public async Task<List<Tempo>> GetSongsByTempo(string bpm)
        {
            var uriBuilder = new UriBuilder(_baseUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["api_key"] = _apiKey;
            query["bpm"] = bpm;
            uriBuilder.Query = query.ToString() ?? string.Empty;
            var response = await _httpClient.GetAsync(uriBuilder.ToString());
            var tempoResponse = await response.Content.ReadFromJsonAsync<TempoResponse>();
            return tempoResponse?.Tempo;
        }

        /// <summary>
        ///     Gets songs in the specified key.
        ///     Results limited to the 250 most viewed songs in the last 30 days.
        /// </summary>
        /// <param name="key">Key to find</param>
        /// <param name="mode">Minor or Major</param>
        /// <param name="notation">Flat or Sharp</param>
        /// <returns></returns>
        public async Task<List<KeyOf>> GetSongsByKey(Key key,
            Mode mode,
            Notation? notation = null)
        {
            var uriBuilder = new UriBuilder(_baseUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["api_key"] = _apiKey;
            query["key_of"] = ((int) key).ToString();
            query["mode"] = ((int) mode).ToString();
            if (notation != null) query["type"] = notation.ToString()?.ToLower();
            uriBuilder.Query = query.ToString() ?? string.Empty;
            var response = await _httpClient.GetAsync(uriBuilder.ToString());
            var keyResponse = await response.Content.ReadFromJsonAsync<KeyResponse>();
            return keyResponse?.KeyOf;
        }

        /// <summary>
        ///     Gets songs matching the query
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="artist"></param>
        /// <param name="song"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<List<Search>> SearchSongs(SearchType searchType,
            string artist = null,
            string song = null)
        {
            if (song == null && searchType != SearchType.Artist) throw new ArgumentNullException(nameof(song));
            if (artist == null && searchType != SearchType.Song) throw new ArgumentNullException(nameof(artist));
            var uriBuilder = new UriBuilder(_baseUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["api_key"] = _apiKey;
            query["type"] = searchType.ToString().ToLower();
            query["lookup"] = searchType switch
            {
                SearchType.Both => $"song:{song} artist:{artist}",
                SearchType.Artist => artist,
                SearchType.Song => song,
                _ => query["lookup"]
            };
            uriBuilder.Query = query.ToString() ?? string.Empty;
            var response = await _httpClient.GetAsync(uriBuilder.ToString());
            var searchResponse = await response.Content.ReadFromJsonAsync<SearchResponse>();
            return searchResponse?.Search;
        }
    }
}