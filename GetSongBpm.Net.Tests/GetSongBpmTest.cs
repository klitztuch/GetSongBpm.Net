using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace GetSongBpm.Net.Tests
{
    public class GetSongBpmTest
    {
        private IConfiguration Configuration { get; set; }
        private GetSongBpm _getSongBpm;
        
        [SetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder().AddUserSecrets<GetSongBpmTest>();
            Configuration = builder.Build();
            var apiKey = Configuration["TestApiKey"];
            _getSongBpm = new GetSongBpm(apiKey);
        }

        [Test]
        public async Task GetArtistTest()
        {
            var artist = await _getSongBpm.GetArtist("1");
            Assert.IsNotNull(artist);
            Assert.Pass();
        }
    }
}