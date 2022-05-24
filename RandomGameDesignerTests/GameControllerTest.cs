using RandomGameDesigner;
using RandomGameDesigner.Models;
using RandomGameDesigner.Controllers;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace RandomGameDesignerTests
{
    public class GameControllerTest
    {
        [Fact]
        public void UserIsPrivateTest()
        {
            GameController gameController = new GameController();
            Request request = new Request() { Name = "76561197960287930" };
            List<Game> t = gameController.RequestGames(request).ToList();
            Assert.NotNull(t);
            Assert.Equal("Error user is private",t[0].Title);

        }
    }
}