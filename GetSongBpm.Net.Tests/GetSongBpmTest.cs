using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using GetSongBpm.Net.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace GetSongBpm.Net.Tests
{
    public class GetSongBpmTest
    {
        private string _apiKey;
        private string _url;
        private IConfiguration Configuration { get; set; }

        [SetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder().AddUserSecrets<GetSongBpmTest>()
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            _apiKey = Configuration["TESTAPIKEY"];
            _url = "https://api.getsongbpm.com";
        }

        [Test]
        public async Task GetArtistTest()
        {
            // arrange
            var expectedArtistResponse = new ArtistResponse
            {
                Artist = new Artist
                {
                    From = "GB",
                    Genres = null,
                    Id = "OMrJg",
                    Img = "https://i.scdn.co/image/ab67616d0000b2731ff9ac286a38d5d85ce11ac3",
                    Mbid = "f18ed420-f81a-43f6-83ff-9c021d5ef1c9",
                    Name = "Sweet Little Band",
                    Uri = "https://getsongbpm.com/artist/sweet-little-band/OMrJg"
                }
            };
            var expectedArtist = expectedArtistResponse.Artist;
            var mockHandler = CreateMockHandler(expectedArtistResponse, _url);
            var httpClient = new HttpClient(mockHandler.Object);


            var getSongBpm = new GetSongBpm(_apiKey, _url, httpClient);
            // act
            var actualArtist = await getSongBpm.GetArtist(expectedArtist.Id);
            // assert
            Assert.IsNotNull(actualArtist);
            actualArtist.Should().BeEquivalentTo(expectedArtist);
        }

        [Test]
        public async Task GetSongTest()
        {
            // arrange
            var expectedSongResponse = new SongResponse
            {
                Song = new Song
                {
                    Artist = new Artist
                    {
                        From = "GB",
                        Genres = null,
                        Id = "OMrJg",
                        Img = "https://i.scdn.co/image/ab67616d0000b2731ff9ac286a38d5d85ce11ac3",
                        Mbid = "f18ed420-f81a-43f6-83ff-9c021d5ef1c9",
                        Name = "Sweet Little Band",
                        Uri = "https://getsongbpm.com/artist/sweet-little-band/OMrJg"
                    },
                    Id = "6RNxAQ",
                    Tempo = "43",
                    Title = "Blinding Lights",
                    Uri = "https://getsongbpm.com/song/blinding-lights/57633B",
                    KeyOf = "1",
                    OpenKey = "1",
                    TimeSig = "4/4"
                }
            };
            var expectedSong = expectedSongResponse.Song;
            var mockHandler = CreateMockHandler(expectedSongResponse, _url);
            var httpClient = new HttpClient(mockHandler.Object);

            var getSongBpm = new GetSongBpm(_apiKey, _url, httpClient);
            // act
            var actualSong = await getSongBpm.GetSong(expectedSong.Id);
            // assert
            Assert.IsNotNull(actualSong);
            actualSong.Should().BeEquivalentTo(expectedSong);
        }

        [Test]
        public async Task GetSongsByTempoTest()
        {
            // arrange
            const string bpm = "100";
            var expectedTempoResponse = new TempoResponse
            {
                Tempo = new List<Tempo>
                {
                    new()
                    {
                        Album = new Album(),
                        Artist = new Artist(),
                        SongId = "",
                        SongTitle = "",
                        SongUri = "",
                        TempoValue = bpm
                    },
                    new()
                    {
                        Album = new Album(),
                        Artist = new Artist(),
                        SongId = "",
                        SongTitle = "",
                        SongUri = "",
                        TempoValue = bpm
                    }
                }
            };
            var expectedTempos = expectedTempoResponse.Tempo;
            var mockHandler = CreateMockHandler(expectedTempoResponse, _url);
            var httpClient = new HttpClient(mockHandler.Object);

            var getSongBpm = new GetSongBpm(_apiKey, _url, httpClient);
            // act
            var actualTempos = await getSongBpm.GetSongsByTempo(bpm);
            // assert
            Assert.IsNotNull(actualTempos);
            actualTempos.Should().BeEquivalentTo(expectedTempos);
        }

        private static Mock<HttpMessageHandler> CreateMockHandler<T>(T responseObject,
            string url)
        {
            var json = JsonSerializer.Serialize(responseObject);


            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(message => message.Method == HttpMethod.Get
                                                             && message.RequestUri.ToString().StartsWith(url)),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);
            return mockHandler;
        }
    }
}