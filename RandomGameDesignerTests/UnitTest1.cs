using RandomGameDesigner;
using RandomGameDesigner.Models;
using Xunit;

namespace RandomGameDesignerTests
{
    public class UnitTest1
    {
        [Fact]
        public void GetGame()
        {
            DataAccess g = new DataAccess();
            Game game = g.GetGameInfo("220");
            Assert.Equal("Half-Life 2", game.Title);
            Assert.Equal("https://cdn.akamai.steamstatic.com/steam/apps/220/header.jpg?t=1591063154", game.ImageLink);
            // Assert.Equal("Half-Life 2", game.Tags);
            Assert.Equal("Action", game.Genres);
        }
    }
}