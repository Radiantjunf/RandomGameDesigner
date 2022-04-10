using RandomGameDesigner;
using RandomGameDesigner.Models;
using RandomGameDesigner.Controllers;
using Xunit;

namespace RandomGameDesignerTests
{
    public class UnitTest1
    {
        [Fact]
        public void GetGameTest()
        {
            DataAccess g = new DataAccess();
            Game game = g.GetGameInfo("220");
            Assert.Equal("Half-Life 2", game.Title);
            Assert.Equal("https://cdn.akamai.steamstatic.com/steam/apps/220/header.jpg?t=1591063154", game.ImageLink);
            // Assert.Equal("Half-Life 2", game.Tags);
            Assert.Equal("Action", game.Genres);
        }

        //https://steamcommunity.com/id/alexjunf/


        [Fact]
        public void IsUrlTest()
        {
            IdeaGenerator gen = new IdeaGenerator();
            string url = "https://steamcommunity.com/id/alexjunf/";
            Assert.True(gen.IsUrl(url));
        }

        [Theory]
        [InlineData("https://steamcommunity.com/id/alexjunf/", "alexjunf")]
        [InlineData("https://steamcommunity.com/profiles/76561198024878463/", "76561198024878463")]
        [InlineData("https://steamcommunity.com/id/alexjunf", "alexjunf")]
        [InlineData("https://steamcommunity.com/profiles/76561198024878463", "76561198024878463")]
        public void ClearUrlTest(string url,string result)
        {
            IdeaGenerator gen = new IdeaGenerator();
            Assert.Equal(gen.ClearUrl(url), result);
        }

        [Theory]
        [InlineData("https://steamcommunity.com/id/alexjunf/", true)]
        [InlineData("https://steamcommunity.com/profiles/76561198024878463/", true)]
        [InlineData("https://steamcommunity.com/id/", false)]
        [InlineData("https://steamcommunity.com/profiles/", false)]
        public void IsValidUrlTest(string url, bool result)
        {
            IdeaGenerator gen = new IdeaGenerator();
            Assert.Equal(gen.IsValidUrl(url), result);
        }

        [Theory]
        [InlineData("alexjunf", true)]
        [InlineData("76561198024878463", false)]
        public void IsVanityTest(string name, bool result)
        {
            IdeaGenerator gen = new IdeaGenerator();
            Assert.Equal(gen.IsVanity(name), result);
        }

        [Theory]
        [InlineData("alexjunf", "76561198024878463")]
        [InlineData("alexjunf33333333333", null)]
        public void GetUserIdTest(string name, string result)
        {
            DataAccess dataAccess = new DataAccess();
            Assert.Equal(dataAccess.GetUserId(name), result);
            dataAccess.Close();
        }




    }
}