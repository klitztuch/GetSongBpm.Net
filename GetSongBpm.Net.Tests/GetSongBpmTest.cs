using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace GetSongBpm.Net.Tests
{
    public class GetSongBpmTest
    {
        private GetSongBpm _getSongBpm;
        private IConfiguration Configuration { get; set; }

        [SetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder().AddUserSecrets<GetSongBpmTest>()
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            var apiKey = Configuration["TESTAPIKEY"];
            _getSongBpm = new GetSongBpm(apiKey);
        }

        [Test]
        public async Task GetArtistTest()
        {
            const string id = "OMrJg";
            var artist = await _getSongBpm.GetArtist(id);
            Assert.IsNotNull(artist);
            Assert.AreEqual(id, artist.Id);
        }

        [Test]
        public async Task GetSongTest()
        {
            const string id = "6RNxAQ";
            var song = await _getSongBpm.GetSong(id);
            Assert.IsNotNull(song);
            Assert.AreEqual(id, song.Id);
        }

        [Test]
        public async Task GetSongsByTempoTest()
        {
            const string bpm = "100";
            var songsByTempo = await _getSongBpm.GetSongsByTempo(bpm);
            Assert.IsNotNull(songsByTempo);
            Assert.IsTrue(songsByTempo.All(o => o.TempoValue == bpm));
        }
    }
}
