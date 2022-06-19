using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using RandomGameDesigner.Models;
using RandomGameDesigner.Models.RequestModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RandomGameDesigner.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class GameController : ControllerBase
    {
        [HttpGet] 
        public ActionResult login([FromQuery(Name = "openid.identity")] string identity)
        {
            if(identity == null)
            {
                return null;
            }
            //return identity.Split('/').Last();
            return Redirect("https://randomgamedesigner.azurewebsites.net/?id="+ identity.Split('/').Last());
        }



        // GET: api/<GameController>
        [HttpPost]
        public IEnumerable<Game> RequestGames([FromBody]Request request)
        {
            IdeaGenerator gen = new IdeaGenerator();
            DataAccess g = new DataAccess();
            List<Game> result = new List<Game>();
            if (gen.IsValidUrl(request.Name))
            {
                request.Name = gen.ClearUrl(request.Name);
            }

            if (gen.IsVanity(request.Name))
            {
                var name = g.GetUserId(request.Name);
                if(name == null)
                {
                    //Return a game with the error
                    result.Add(new Game(){Title = "Error user not valid"});
                    return result.ToArray();
                }
                request.Name = name;
            }




            
            var t = g.GetUserGamesIDs(request.Name);
            if(t == null)
            {
                //Return a game with the error
                result.Add(new Game() { Title = "Error can't find games" });
                return result.ToArray();
            }
            List<Game> te = new List<Game>();
            //foreach (var gameInfo in t)
            //{
            //    Game game = g.GetGameInfo(gameInfo.appid);
            //    if (game.IsValid())
            //    {
            //        te.Add(game);
            //    }
                
            //}



            var random = new Random();
            var intArray = Enumerable.Range(0, t.Count).OrderBy(t => random.Next()).Take(6).ToArray();
            foreach (var item in intArray)
            {
                Game game = g.GetGameInfo(t[item].appid);
                if (game.IsValid())
                {
                    te.Add(game);
                }
            }



            //var g1 = g.GetGameInfo(t[intArray[0]].appid);
            //var g2 = g.GetGameInfo(t[intArray[1]].appid);
            //var g3 = g.GetGameInfo(t[intArray[2]].appid);

            result.Add(AddText(te[0], "Art & Camera"));
            result.Add(AddText(te[1], "Mechanics"));
            result.Add(AddText(te[2], "Setting"));

            g.Close();

            return result.ToArray();
        }


        private Game AddText(Game game,string topText)
        {
            if(game == null) return null;
            game.TopText = topText;
            return game; 
        }



       




    }
}